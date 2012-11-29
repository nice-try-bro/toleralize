using System;
using System.ComponentModel;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing;
using System.Threading;
using System.IO;
using System.Text;
using System.Diagnostics;

public enum TargetFormule { Error, Tolerance, SSF };
public enum Task { Tolerances, Error, SingleErrors, AllErrors, SSF };

namespace Toleralize2011_0
{
    public partial class mainForm : Form
    {
        ToolTip errorToolTip = new ToolTip();
        string fileName;
        Dictionary<DataGridViewRow, Element> rowToElementDictionary;
        Dictionary<PassiveElement[], Result> elementsToResultDictionary;
        public static Dictionary<string, DetansPair> elementsStringToDetansMap;
        DataGridViewCheckBoxHeaderCell checkBoxHeaderCell;
        AdvancedScheme schemeForCalculations;
        string formuleName;
        static int detansCount;
        static int currentPercentage = 0;
        int detansTargetAmount;
        System.Threading.Timer timer;

        public mainForm()
        {
            InitializeComponent();
        }

        public mainForm(string fileName)
        {
            InitializeComponent();
            this.fileName = fileName;
        }

        private void openCirFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            loadSchemeFromFile(openCirFileDialog.FileName);
        }

        public void loadSchemeFromFile(string fileName)
        {
            elementsStringToDetansMap = new Dictionary<string, DetansPair>();
            rowToElementDictionary = new Dictionary<DataGridViewRow, Element>();
            Scheme.currentScheme = new Scheme(fileName);
            List<Element> elementsList = Scheme.currentScheme.getElementsList();
            int rowCount = 0;
            if (elementsList.Count > 0)
            {
                schemeDataGrid.Rows.Clear();
            }
            for (int i = 0; i < elementsList.Count; i++, rowCount++)
            {
                Element element = elementsList[i];
                DataGridViewRow row = new DataGridViewRow();
                row = getElementRow(element, rowCount);
                rowToElementDictionary.Add(row, element);
                schemeDataGrid.Rows.Add(row);
                if (element is ActiveElement)
                {
                    AcceptingElement acceptor = ((ActiveElement)element).dependingElement;
                    if (acceptor != null)
                    {
                        row = getElementRow(acceptor, ++rowCount);
                        rowToElementDictionary.Add(row, acceptor);
                        schemeDataGrid.Rows.Add(row);
                    }
                }
            }
            targetFormuleComboBox.SelectedItem = targetFormuleComboBox.Items[0];
            targetFormuleComboBox.Enabled = true;
            schemeDataGrid.Enabled = true;
            calculateButton.Enabled = true;
        }

        DataGridViewRow getElementRow(Element element, int index)
        {
            string elementType = element.getTypeString();
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(schemeDataGrid);
            row.HeaderCell.Value = index;
            if (!(element is PassiveElement))
            {
                row.Cells[0].ReadOnly = true;
                row.Cells[0].Style.BackColor = Color.LightGray;
                row.Cells[0].Style.ForeColor = Color.DarkGray;
                row.Cells[8].ReadOnly = true;
                row.Cells[8].Style.BackColor = Color.LightGray;
                row.Cells[8].Style.ForeColor = Color.DarkGray;
            }
            row.Cells[0].Value = false;
            row.Cells[1].Value = element.getRightName();
            row.Cells[2].Value =
                Element.typeStringToElementTypeDictionary[elementType];
            for (int j = 0; j < element.knots.Length; j++)
            {
                row.Cells[j + 3].Value = element.knots[j];
            }
            if (element is ValueHavingElement)
                row.Cells[7].Value = ((ValueHavingElement)element).value;
            return row;
        }

        List<PassiveElement> getSelectedElements()
        {
            List<PassiveElement> selectedElements = new List<PassiveElement>();
            foreach (DataGridViewRow row in schemeDataGrid.Rows)
            {
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)row.Cells[0];
                if ((bool)cell.Value == true)
                {
                    selectedElements.Add((PassiveElement)rowToElementDictionary[row]);
                }
            }
            return selectedElements;
        }

        void working(object parameter)
        {
            object[] parameters = (object[])parameter;
            calculateFunction ff = (calculateFunction)parameters[0];
            ff((Dictionary<PassiveElement[], Result>)parameters[1], (List<PassiveElement>)parameters[2],
                (bool)parameters[3], (string)parameters[4]);
        }

        delegate void calculateFunction(Dictionary<PassiveElement[], Result> elementsToResultDictionary, List<PassiveElement> selectedElements, bool forNegative, string error);

        static Dictionary<PassiveElement[], Result> calculateTolerances(SchemeForTolerance scheme, ref int detansCount)
        {
            Dictionary<PassiveElement[], Result> elementToToleranceDictionary = new Dictionary<PassiveElement[], Result>();
            //schemeForCalculations = new SchemeForTolerance(Scheme.currentScheme, selectedElements.ToArray(), error);
            elementToToleranceDictionary = Calculations.calculateTolerance_Total(scheme, ref detansCount);
            return elementToToleranceDictionary;
        }

        static Dictionary<PassiveElement[], Result> calculateError(SchemeForError scheme, ref int detansCount)
        {
            Dictionary<PassiveElement[], Result> elementsToErrorDictionary = new Dictionary<PassiveElement[], Result>();
            //Dictionary<PassiveElement, string> elementToToleranceDictionary = getElementToToleranceAssociations();
            //schemeForCalculations = new SchemeForError(Scheme.currentScheme, selectedElements.ToArray(), elementToToleranceDictionary);
            elementsToErrorDictionary.Add(scheme.getSelectedElements().ToArray(), Calculations.calculateError(scheme, ref detansCount));
            // = Calculations.calculateError(selectedElements.ToArray());
            return elementsToErrorDictionary;
        }

        static Dictionary<PassiveElement[], Result> calculateSingleErrors(SchemeForError scheme, ref int detansCount)
        {
            Dictionary<PassiveElement[], Result> elementsToErrorDictionary = new Dictionary<PassiveElement[], Result>();
            //Dictionary<PassiveElement, string> elementToToleranceDictionary = getElementToToleranceAssociations();
            //schemeForCalculations = new SchemeForError(Scheme.currentScheme, selectedElements.ToArray(), elementToToleranceDictionary);
            List<PassiveElement> selectedElements = scheme.getSelectedElements();
            foreach (PassiveElement element in selectedElements)
            {
                List<PassiveElement> selectedElement = new List<PassiveElement>();
                selectedElement.Add(element);
                SchemeForError tempScheme = new SchemeForError(Scheme.currentScheme,
                    selectedElement.ToArray(), scheme.elementToToleranceDictionary, scheme.useNegativeValues);
                //scheme.setSelectedElements(selectedElement);
                elementsToErrorDictionary.Add(selectedElement.ToArray(),
                    Calculations.calculateError(tempScheme, ref detansCount));
            }
            return elementsToErrorDictionary;
        }

        static Dictionary<PassiveElement[], Result> calculateAllErrors(SchemeForError scheme, ref int detansCount)
        {
            Dictionary<PassiveElement[], Result> elementsToErrorDictionary = new Dictionary<PassiveElement[], Result>();
            List<PassiveElement> selectedElements = new List<PassiveElement>();
            //foreach (DataGridViewRow row in schemeDataGrid.Rows)
            //{
            //    if (!row.Cells[0].ReadOnly)
            //        selectedElements.Add((PassiveElement)rowToElementDictionary[row]);
            //}
            //Dictionary<PassiveElement, string> elementsToToleranceDictionary = getElementToToleranceAssociations();
            elementsToErrorDictionary = Calculations.calculateAllErrors(scheme, ref detansCount);
            //schemeForCalculations = new SchemeForError(Scheme.currentScheme, selectedElements.ToArray(), elementsToToleranceDictionary);
            return elementsToErrorDictionary;
        }

        static Dictionary<PassiveElement[], Result> calculateSSF(SchemeForSSF scheme, ref int detansCount)
        {
            Dictionary<PassiveElement[], Result> elementsToSSFDictionary = new Dictionary<PassiveElement[], Result>();
            elementsToSSFDictionary.Add(scheme.getSelectedElements().ToArray(), Calculations.calculateSSF(scheme, ref detansCount));
            //schemeForCalculations = new SchemeForSSF(Scheme.currentScheme, selectedElements.ToArray());
            return elementsToSSFDictionary;
        }

        void passResultsToTable(Dictionary<PassiveElement[], Result> elementsToResultDictionary,
            string formuleName)
        {
            formuleNameLabel.Text = formuleName;
            resultsDataGrid.Columns[0].Visible = true;
            resultsDataGrid.Columns[2].Visible = true;
            resultsDataGrid.Rows.Clear();
            foreach (KeyValuePair<PassiveElement[], Result> elementsToResultPair in elementsToResultDictionary)
            {
                bool needNegativeColumn = false;
                string elementsString = "";
                foreach (Element element in elementsToResultPair.Key)
                {
                    elementsString += element.getRightName() + ";";
                }
                elementsString = elementsString.Remove(elementsString.Length - 1);
                DataGridViewRow row = new DataGridViewRow();
                Result res = elementsToResultPair.Value;
                row.CreateCells(resultsDataGrid);
                row.Cells[0].Value = elementsString;
                row.Cells[1].Value = res.formuleValue.formule;
                row.Cells[2].Value = res.formuleValue.value;
                resultsDataGrid.Rows.Add(row);
                if (res.formuleValue is FormuleValueWithNegative)
                {
                    row.Cells[3].Value = ((FormuleValueWithNegative)elementsToResultPair.Value.formuleValue).negativeValue;
                    needNegativeColumn = true;
                }
                if (needNegativeColumn)
                {
                    resultsDataGrid.Columns[3].Visible = true;
                    valueForNegativeLabel.Visible = true;
                    valueForNegativeTextBox.Visible = true;
                }
                else
                {
                    resultsDataGrid.Columns[3].Visible = false;
                    valueForNegativeLabel.Visible = false;
                    valueForNegativeTextBox.Visible = false;
                }
            }
        }

        Dictionary<PassiveElement, string> getElementToToleranceAssociations()
        {
            Dictionary<PassiveElement, string> elementToToleranceDictionary = new Dictionary<PassiveElement, string>();
            foreach (DataGridViewRow row in schemeDataGrid.Rows)
            {
                {
                    Element element = rowToElementDictionary[row];
                    if (element is PassiveElement)
                    {
                        string tolerance = (string)row.Cells[8].Value;
                        elementToToleranceDictionary.Add((PassiveElement)element, tolerance);
                    }
                }
            }
            return elementToToleranceDictionary;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int selectedIndex = targetFormuleComboBox.SelectedIndex;

            if (selectedIndex < 2)
                if (String.IsNullOrEmpty(errorTextBox.Text))
                {
                    errorToolTip.Show("Сначала введите значение погрешности", calculateButton, 1000);
                    return;
                }
            if (selectedIndex >= 2 && selectedIndex < 8)
            {
                foreach (DataGridViewRow row in schemeDataGrid.Rows)
                {
                    if (rowToElementDictionary[row] is PassiveElement)
                        if ((bool)row.Cells[0].Value == true)
                            if (String.IsNullOrEmpty((string)row.Cells[8].Value))
                            {
                                errorToolTip.Show("Введите значение допуска для элемента " + (string)row.Cells[1].Value, calculateButton);
                                return;
                            }
                }
            }
            List<PassiveElement> selectedElements = getSelectedElements();
            if (selectedIndex < 6 || selectedIndex > 7)
                if (selectedElements.Count == 0)
                {
                    errorToolTip.Show("Не выбран ни один элемент", calculateButton, 1000);
                    return;
                }
            bool forNegative = (selectedIndex % 2 == 0) || selectedIndex == 8 ? false : true;
            formuleName = selectedIndex < 2 ? "Допуск" : (selectedIndex < 8 ? "Погрешность" : "ССФ");
            Task task;
            switch (selectedIndex)
            {
                case 0:
                    schemeForCalculations = new SchemeForTolerance(Scheme.currentScheme,
                        selectedElements.ToArray(), (Convert.ToDouble(errorTextBox.Text) * 0.01).ToString(), false);
                    task = Task.Tolerances;
                    break;
                case 1:
                    schemeForCalculations = new SchemeForTolerance(Scheme.currentScheme,
                        selectedElements.ToArray(), (Convert.ToDouble(errorTextBox.Text) * 0.01).ToString(), true);
                    task = Task.Tolerances;
                    break;
                case 2:
                    schemeForCalculations = new SchemeForError(Scheme.currentScheme, selectedElements.ToArray(), getElementToToleranceAssociations(), false);
                    task = Task.Error;
                    break;
                case 3:
                    schemeForCalculations = new SchemeForError(Scheme.currentScheme, selectedElements.ToArray(), getElementToToleranceAssociations(), true);
                    task = Task.Error;
                    break;
                case 4:
                    schemeForCalculations = new SchemeForError(Scheme.currentScheme, selectedElements.ToArray(), getElementToToleranceAssociations(), false);
                    task = Task.SingleErrors;
                    break;
                case 5:
                    schemeForCalculations = new SchemeForError(Scheme.currentScheme, selectedElements.ToArray(), getElementToToleranceAssociations(), true);
                    task = Task.SingleErrors;
                    break;
                case 6:
                    schemeForCalculations = new SchemeForError(Scheme.currentScheme, selectedElements.ToArray(), getElementToToleranceAssociations(), false);
                    task = Task.AllErrors;
                    break;
                case 7:
                    schemeForCalculations = new SchemeForError(Scheme.currentScheme, selectedElements.ToArray(), getElementToToleranceAssociations(), true);
                    task = Task.AllErrors;
                    break;
                case 8:
                    schemeForCalculations = new SchemeForSSF(Scheme.currentScheme, selectedElements.ToArray(), 1);
                    task = Task.SSF;
                    break;
                default:
                    schemeForCalculations = null;
                    task = Task.Tolerances;
                    break;
            }
            DisableControls();
            int selectedElementsCount = selectedElements.Count;
            switch (task)
            {
                case Task.Tolerances:
                case Task.SingleErrors:
                    detansTargetAmount = selectedElementsCount * 4;
                    break;
                case Task.Error:
                case Task.SSF:
                    detansTargetAmount = (int)Math.Pow(2, selectedElementsCount + 1);
                    break;
                case Task.AllErrors:
                    int maxfactor = factorial(selectedElementsCount);
                    detansTargetAmount = 0;
                    for (int i = 1; i < selectedElementsCount; i++)
                    {
                        detansTargetAmount += (int)(maxfactor / factorial(selectedElementsCount - i) * Math.Pow(2, i + 1));
                    }
                    break;

            }
            statusLabel.Text = "Вычисление";
            currentPercentage = 0;
            detansCount = 0;
            calculationsProgressBar.Visible = true;
            calculationsBackgroundWorker.RunWorkerAsync(new Object[] { schemeForCalculations, task, formuleName });
            TimerCallback callBack = this.checkDetansCount;
            timer = new System.Threading.Timer(callBack, null, 0, 10);
        }

        int factorial(int val)
        {
            if (val < 0) return 0;
            if (val < 2) return 1;
            for (int i = 1; i < val; i++)
            {
                val = val * (val - i);
            }
            return val;
        }

        void checkDetansCount(object stateInfo)
        {
            int currentRatio = (int)((float)detansCount / detansTargetAmount * 100);

            if (currentPercentage < 100 && currentRatio < 100)
                if (currentRatio > currentPercentage)
                {
                    currentPercentage = currentRatio;
                    //string infoString = "Получено " + detansCount.ToString() + " из " + detansTargetAmount.ToString() + " определителей";
                    if (calculationsBackgroundWorker.IsBusy)
                        calculationsBackgroundWorker.ReportProgress(currentPercentage);
                }
        }

        void DisableControls()
        {
            resultsDataGrid.Enabled = false;
            resultInfoGroupBox.Enabled = false;
            calculateButton.Enabled = false;
            schemeDataGrid.Enabled = false;
        }

        void EnableControls()
        {
            вывестиРезультатВФайлToolStripMenuItem.Visible = true;
            calculateButton.Enabled = true;
            посчитатьОпределителиToolStripMenuItem.Enabled = true;
            resultsDataGrid.Enabled = true;
            resultsDataGrid.Visible = true;
            if (resultsDataGrid.Rows.Count > 0)
                resultsDataGrid.Rows[0].Cells[0].Selected = true;
            resultInfoGroupBox.Enabled = true;
            resultInfoGroupBox.Visible = true;
            schemeDataGrid.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Application.CurrentCulture = CultureInfo.InvariantCulture;
            checkBoxHeaderCell = new DataGridViewCheckBoxHeaderCell();
            schemeDataGrid.Columns.Insert(0, new DataGridViewCheckBoxColumn());
            schemeDataGrid.Columns[0].HeaderText = "Выбрать";
            schemeDataGrid.Columns[0].HeaderCell = checkBoxHeaderCell;
            schemeDataGrid.Columns[0].Frozen = true;
            schemeDataGrid.Columns[0].Width = 40;
            schemeDataGrid.CellValueChanged += schemeDataGrid_CellValueChanged;
            schemeDataGrid.Columns["toleranceColumn"].HeaderCell.Style.BackColor = Color.White;
            loadSchemeFromFile(fileName);
            calculationsBackgroundWorker.WorkerReportsProgress = true;
        }

        private static Dictionary<PassiveElement[], Result> makeCalculations(
            AdvancedScheme scheme, Task task, ref int detansCount)
        {
            Dictionary<PassiveElement[], Result> elementsToResultMap = new Dictionary<PassiveElement[], Result>();
            switch (task)
            {
                case Task.Tolerances:
                    elementsToResultMap = calculateTolerances((SchemeForTolerance)scheme, ref detansCount);
                    break;
                case Task.Error:
                    elementsToResultMap = calculateError((SchemeForError)scheme, ref detansCount);
                    break;
                case Task.SingleErrors:
                    elementsToResultMap = calculateSingleErrors((SchemeForError)scheme, ref detansCount);
                    break;
                case Task.AllErrors:
                    elementsToResultMap = calculateAllErrors((SchemeForError)scheme, ref detansCount);
                    break;
                case Task.SSF:
                    elementsToResultMap = calculateSSF((SchemeForSSF)scheme, ref detansCount);
                    break;
                default:
                    return null;
            }
            return elementsToResultMap;
        }

        private void schemeDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (schemeDataGrid.CurrentCell.ColumnIndex != 8 || schemeDataGrid.CurrentCell.ReadOnly == true)
                schemeDataGrid.ClearSelection();
        }

        private void schemeDataGrid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                for (int i = 0; i < schemeDataGrid.Rows.Count; i++)
                {
                    schemeDataGrid.Rows[i].Cells[0].Value = checkBoxHeaderCell.CheckAll;
                }
            }
        }

        private void schemeDataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
                if (schemeDataGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
                {
                    if (e.RowIndex >= 0)
                        if (((DataGridView)sender)[e.ColumnIndex, e.RowIndex].ReadOnly)
                        {
                            (((DataGridView)sender)[e.ColumnIndex, e.RowIndex]).Value = false;
                        }
                }
            if (targetFormuleComboBox.Text == "Все погрешности" ||
                targetFormuleComboBox.Text == "Все погрешности (+для отрицательных допусков)")
            {
                calculateButton.Enabled = true;
                return;
            }
            if (e.ColumnIndex == 0)

                if (schemeDataGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
                {
                    if (e.RowIndex > 0 && e.ColumnIndex > 0)
                        if ((bool)schemeDataGrid[e.ColumnIndex, e.RowIndex].Value)
                        {
                            calculateButton.Enabled = true;
                            return;
                        }
                    bool selectionExist = false;
                    foreach (DataGridViewRow row in schemeDataGrid.Rows)
                    {
                        if (!row.Cells[0].ReadOnly)
                            if ((bool)((DataGridViewCheckBoxCell)row.Cells[0]).Value == true)
                            {
                                selectionExist = true;
                                break;
                            }
                    }
                    if (selectionExist)
                        calculateButton.Enabled = true;
                    else
                        calculateButton.Enabled = false;
                }
        }

        private void targetFormuleComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)0;
        }

        private void schemeDataGrid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            schemeDataGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void targetFormuleComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (targetFormuleComboBox.Text.Contains("Допуски для выбранных элементов"))
            {
                errorLabel.Visible = true;
                errorTextBox.Visible = true;
                schemeDataGrid.Columns["toleranceColumn"].Visible = false;
            }
            else
            {
                errorLabel.Visible = false;
                errorTextBox.Visible = false;
                if (!targetFormuleComboBox.Text.Contains("Дробная ССФ"))
                    schemeDataGrid.Columns["toleranceColumn"].Visible = true;
            }
            if (targetFormuleComboBox.Text.Contains("Все погрешности"))
                calculateButton.Enabled = true;
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openCirFileDialog.ShowDialog();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void resultsDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            resultInfoGroupBox.Text = (string)resultsDataGrid.CurrentRow.Cells[0].Value;
            formuleTextBox.Text = (string)resultsDataGrid.CurrentRow.Cells[1].Value;
            valueForPositiveTextBox.Text = (string)resultsDataGrid.CurrentRow.Cells[2].Value;
            valueForNegativeTextBox.Text = (string)resultsDataGrid.CurrentRow.Cells[3].Value;
        }

        private void errorTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 46 && e.KeyChar != 8)
                e.KeyChar = char.MinValue;
        }

        private void вывестиРезультатВФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveResultDialog = new SaveFileDialog();
            if (saveResultDialog.ShowDialog() == DialogResult.OK)
            {
                List<string> fileStrings = new List<string>();
                foreach (KeyValuePair<PassiveElement[], Result> elementsToResult in elementsToResultDictionary)
                {

                    string forElements = elementsToResult.Key.Length == 1 ? "элемента" : "элементов";
                    string titleString = formuleNameLabel.Text + " для " + forElements + " ";
                    foreach (Element element in elementsToResult.Key)
                    {
                        titleString += element.getRightName() + ", ";
                    }
                    titleString = titleString.Remove(titleString.Length - 2, 2) + ":";
                    fileStrings.Add(titleString);
                    fileStrings.Add("    Определители:");
                    foreach (KeyValuePair<string, DetansPair> elementToDetans in elementsToResult.Value.elementNamesToDetansDictionary)
                    {
                        if (elementToDetans.Key.Length > 0)
                        {
                            string elementsString = elementToDetans.Key.Length > 1 ? "        Для элементов, стремящиеся к бесконечности: " : "        Для элемента, стремящигося к бесконечности: ";
                            foreach (string element in elementToDetans.Key.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
                            {
                                elementsString += element + ", ";
                            }
                            elementsString = elementsString.Remove(elementsString.Length - 2, 2) + ":";
                            fileStrings.Add(elementsString);
                        }
                        else
                        {
                            string elementsString = "        Для всех элементов, стремящиеся к нулю: ";
                            fileStrings.Add(elementsString);
                        }
                        fileStrings.Add("            Определитель Z: " + elementToDetans.Value.itsNumeratorDetan);
                        fileStrings.Add("            Определитель Q: " + elementToDetans.Value.itsDenominatorDetan);
                        fileStrings.Add("");
                    }
                    fileStrings.Add("    Формула: " + elementsToResult.Value.formuleValue.formule);
                    fileStrings.Add("");
                    fileStrings.Add("    Значение: " + elementsToResult.Value.formuleValue.value);
                    if (elementsToResult.Value.formuleValue is FormuleValueWithNegative)
                        if (!String.IsNullOrEmpty(((FormuleValueWithNegative)elementsToResult.Value.formuleValue).negativeValue))
                        {
                            string parameterString = formuleNameLabel.Text == "Допуск" ?
                                "отрицательной погрешности" : "отрицательных допусков";
                            fileStrings.Add("    Значение (для " + parameterString + "): " +
                                ((FormuleValueWithNegative)elementsToResult.Value.formuleValue).negativeValue);
                        }
                    fileStrings.Add(""); fileStrings.Add("");
                }
                File.WriteAllLines(saveResultDialog.FileName, fileStrings.ToArray());
            }
        }

        private void schemeDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void посчитатьОпределителиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Dictionary<string, DetansPair> elementsStringToDetansPairMap = new Dictionary<string, DetansPair>();
            //foreach (Result res in elementsToResultDictionary.Values)
            //    foreach (KeyValuePair<string, DetansPair> keyValue in res.elementNamesToDetansDictionary)
            //        if (!elementsStringToDetansPairMap.ContainsKey(keyValue.Key))
            //            elementsStringToDetansPairMap.Add(keyValue.Key, keyValue.Value);
            DetansForm detansForm = new DetansForm(elementsStringToDetansMap, schemeForCalculations);
            detansForm.ShowDialog();
        }

        private void calculationsBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            detansCount = 0;
            object[] parameters = (object[])e.Argument;
            AdvancedScheme schemeForCalcuations = (AdvancedScheme)parameters[0];
            Task task = (Task)parameters[1];
            e.Result = makeCalculations(schemeForCalculations, task, ref detansCount);
        }

        private void calculationsBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            timer.Dispose();
            elementsToResultDictionary = (Dictionary<PassiveElement[], Result>)e.Result;
            passResultsToTable(elementsToResultDictionary, formuleName);
            EnableControls();
            calculationsProgressBar.Value = 0;
            calculationsProgressBar.Visible = false;
            statusLabel.Text = "Ожидание";
        }

        private void calculationsBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage <= 100)
            {
                calculationsProgressBar.Value = e.ProgressPercentage;
                statusLabel.Text = "Вычисление. Выполнено " + e.ProgressPercentage.ToString() + " %";
            }
        }
    }

    class DataGridViewCheckBoxHeaderCell : DataGridViewColumnHeaderCell
    {
        private Rectangle checkBoxRegion;
        private bool checkAll = false;
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates dataGridViewElementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, dataGridViewElementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
            graphics.FillRectangle(new SolidBrush(cellStyle.BackColor), cellBounds);

            checkBoxRegion = new Rectangle(
                cellBounds.Location.X + 1,
                cellBounds.Location.Y + 2,
                25, cellBounds.Size.Height - 4);


            if (this.checkAll)
                ControlPaint.DrawCheckBox(graphics, checkBoxRegion, ButtonState.Checked);
            else
                ControlPaint.DrawCheckBox(graphics, checkBoxRegion, ButtonState.Normal);

            Rectangle normalRegion =
                new Rectangle(
                cellBounds.Location.X + 1 + 25,
                cellBounds.Location.Y,
                cellBounds.Size.Width - 26,
                cellBounds.Size.Height);

            graphics.DrawString(value.ToString(), cellStyle.Font, new SolidBrush(cellStyle.ForeColor), normalRegion);
        }

        protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
        {
            //Convert the CheckBoxRegion 
            Rectangle rec = new Rectangle(new Point(0, 0), this.checkBoxRegion.Size);
            this.checkAll = !this.checkAll;
            if (rec.Contains(e.Location))
            {
                this.DataGridView.Invalidate();
            }
            base.OnMouseClick(e);
        }

        public bool CheckAll
        {
            get { return this.checkAll; }
            set { this.checkAll = value; }
        }
    }
}