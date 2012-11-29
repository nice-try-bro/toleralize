using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Toleralize2011_0
{
    public partial class DetansForm : Form
    {
        Dictionary<string, DetanValuesPair> itsElementsStringToDetanValuesPairMap = new Dictionary<string, DetanValuesPair>();
        Dictionary<string, DetansPair> itsElementsStringToDetanPairMap = new Dictionary<string, DetansPair>();
        AdvancedScheme itsSchemeForCalculaions;

        //public DetansForm()
        //{
        //    InitializeComponent();
        //}

        public DetansForm(Dictionary<string, DetansPair> elementsStringToDetanPairMap, AdvancedScheme scheme)
        {
            InitializeComponent();
            //string elementNamesPattern =
            //    "r|R|g|G|c|C|l|L|bi|Bi|bI|BI|hi|Hi|hI|HI|gu|Gu|gU|GU|ku|Ku|kU|KU";
            //Regex regex = new Regex("((\\δ\\((" + elementNamesPattern + ")\\d{1,}\\)" + "|" +
            //    "(" + elementNamesPattern + ")\\d{1,}))");
            int count = 0;
            itsElementsStringToDetanPairMap = elementsStringToDetanPairMap;
            itsSchemeForCalculaions = scheme;
            foreach (string elementsString in elementsStringToDetanPairMap.Keys)
            {
                string[] elementCombinationStrings = elementsString.Split(
                    new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (elementCombinationStrings.Length == 0)
                    return;
                string selectedElementsString = elementCombinationStrings[0];
                string removedElementsString = "";
                string subtendedElementsString = "";
                if (elementCombinationStrings.Length > 1)
                    removedElementsString = elementCombinationStrings[1];
                string[] selectedElementNameStrings = selectedElementsString.Split(
                    new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                string[] removedElementNameStrings = removedElementsString.Split(
                    new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (removedElementNameStrings.Length == selectedElementNameStrings.Length)
                    subtendedElementsString = "-";
                else
                {
                    List<string> subtendedElementNamesList = new List<string>();
                    for (int i = 0; i < selectedElementNameStrings.Length; i++)
                    {
                        string selectedElementName = selectedElementNameStrings[i];
                        bool isRemoved = false;
                        string subtendedElementName = "";
                        if (itsSchemeForCalculaions is SchemeForTolerance ||
                            itsSchemeForCalculaions is SchemeForError)
                            subtendedElementName = "δ(" + selectedElementName + ")";
                        else
                            subtendedElementName = selectedElementName;
                        foreach (string removedElementName in removedElementNameStrings)
                        {
                            if (itsSchemeForCalculaions is SchemeForTolerance
                                || itsSchemeForCalculaions is SchemeForError)
                            {
                                if (removedElementName.Contains("(" + selectedElementName + ")"))
                                {
                                    isRemoved = true;
                                    break;
                                }
                            }
                            else
                            {
                                if (removedElementName == selectedElementName)
                                {
                                    isRemoved = true;
                                    break;
                                }                                    
                            }

                        }
                        if (!isRemoved)
                            subtendedElementsString += subtendedElementName + ";";
                    }
                    if (subtendedElementsString.Length > 0)
                        if (subtendedElementsString[subtendedElementsString.Length - 1] == ';')
                            subtendedElementsString = subtendedElementsString.Remove(subtendedElementsString.Length - 1);
                }
                if (removedElementsString.Length == 0)
                    removedElementsString = "-";
                //string value = String.IsNullOrEmpty(elementsString) ? "-" : elementsString;
                elementsDataGridView.Rows.Add(new Object[] { 
                    ++count, selectedElementsString,removedElementsString,subtendedElementsString });
            }
            //if (itsSchemeForCalculaions is SchemeForTolerance)
            //    this.Text = "Определители (расчет допуска)";
            //if (itsSchemeForCalculaions is SchemeForError)
            //    this.Text = "Определители (расчет погрешности)";
            //if (itsSchemeForCalculaions is SchemeForSSF)
            //    this.Text = "Определители (расчет дробной ССФ)";
        }

        private void DetansForm_Load(object sender, EventArgs e)
        {

        }

        private void посчитатьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void elementsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            string selectedElements = elementsDataGridView.CurrentRow.Cells[1].Value.ToString();
            string removedElemenntsString = elementsDataGridView.CurrentRow.Cells[2].Value.ToString();
            string elementsString = selectedElements + "|" + (removedElemenntsString == "-" ? "" : removedElemenntsString);
            string numeratorDetanFormule = itsElementsStringToDetanPairMap[elementsString].itsNumeratorDetan;
            string denominatorDetanFormule = itsElementsStringToDetanPairMap[elementsString].itsDenominatorDetan;
            string numeratorDetanValue;
            string denominatorDetanValue;
            if (itsElementsStringToDetanValuesPairMap.ContainsKey(elementsString))
            {
                numeratorDetanValue = itsElementsStringToDetanValuesPairMap[elementsString].getNumeratorDetanValue();
                denominatorDetanValue = itsElementsStringToDetanValuesPairMap[elementsString].getDenominatorDetanValue();
            }
            else
            {
                numeratorDetanValue = Calculations.calculateFormule(numeratorDetanFormule, itsSchemeForCalculaions);
                denominatorDetanValue = Calculations.calculateFormule(denominatorDetanFormule, itsSchemeForCalculaions);
            }
            numeratorDetanFormuleTextBox.Text = numeratorDetanFormule;
            numeratorDetanValueTextBox.Text = numeratorDetanValue;
            denominatorDetanFormuleTextBox.Text = denominatorDetanFormule;
            denominatorDetanValueTextBox.Text = denominatorDetanValue;
        }
    }

    class DetanValuesPair
    {
        string itsNumeratorDetanValue;
        string itsDenominatorDetanValue;
        public DetanValuesPair(string numeratorDetanValue, string denominatorDetanValue)
        {
            itsNumeratorDetanValue = numeratorDetanValue;
            itsDenominatorDetanValue = denominatorDetanValue;
        }
        public string getNumeratorDetanValue() { return itsNumeratorDetanValue; }
        public string getDenominatorDetanValue() { return itsDenominatorDetanValue; }
    }
}
