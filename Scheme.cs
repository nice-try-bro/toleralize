using System;
using System.Collections.Generic;
using System.Text;

namespace Toleralize2011_0
{
    /* Схема (только список элементов) */
    public class Scheme
    {
        public static Scheme currentScheme;
        protected List<Element> elementsList;
        //Dictionary<string,>

        protected Scheme() { }

        public Scheme(string fileNameString) { elementsList = FileOperations.getElementsListFromFile(fileNameString); }

        public List<Element> getElementsList() { return elementsList; }

        public int getElementsCount() { return elementsList.Count; }
    }

    /* Схема с разделением и модификацией элементов  */
    public abstract class AdvancedScheme : Scheme
    {
        protected struct modifiedElementListsPair
        {
            public List<Element> modifiedToInfinElements;
            public List<Element> modifiedToZeroElements;
        }

        //protected Element[] elements;
        protected List<PassiveElement> itsSelectedElements;
        public List<PassiveElement> passiveElementsList;
        public List<Element> activeElementsList;
        public List<Element> activeElementsListForZ;
        public List<Element> activeElementsListForQ;
        public List<Element> nullersList;
        protected Dictionary<Element, modifiedElementListsPair> elementToModifiedElementsDictionary;
        public Dictionary<Element, string> elementToValueDictionary;

        public AdvancedScheme(Scheme baseScheme, PassiveElement[] selectedElements)
        {
            elementsList = baseScheme.getElementsList();
            itsSelectedElements = new List<PassiveElement>(selectedElements);
            makePassiveList();
            makeActiveList();
            makeNullersList();
            makeActiveListForZ();
            makeActiveListForQ();
            makeElementToValueAssociations();
        }

        public List<PassiveElement> getSelectedElements()
        {
            return itsSelectedElements;
        }

        public void setSelectedElements(List<PassiveElement> selectedElements)
        {
            itsSelectedElements = selectedElements;
        }

        /* Форсирование списка пассивных элементов */
        void makePassiveList()
        {
            passiveElementsList = new List<PassiveElement>();
            foreach (Element element in elementsList)
            {
                if (element is PassiveElement)
                {
                    if (Array.IndexOf(itsSelectedElements.ToArray(), element) < 0)
                        passiveElementsList.Add((PassiveElement)element);
                }
            }
        }

        /* Форсирование списка нуллеров */
        void makeNullersList()
        {
            nullersList = new List<Element>();
            foreach (Element element in elementsList)
            {
                if (element is Nuller)
                    nullersList.Add(element);
            }
        }

        /* Форсирование списка активных элементов */
        void makeActiveList()
        {
            activeElementsList = new List<Element>();
            foreach (Element element in elementsList)
            {
                if (element is ActiveElement)
                {
                    activeElementsList.Add(element);
                }
            }
        }

        /* Форсирование списка для модифицированных источников */
        void makeActiveListForZ()
        {
            activeElementsListForZ = new List<Element>();
            foreach (Element element in activeElementsList)
                activeElementsListForZ.Add(((ActiveElement)element).neutralizeToZ());
        }

        /* Форсирование списка для модифицированных источников */
        void makeActiveListForQ()
        {
            activeElementsListForQ = new List<Element>();
            foreach (Element element in activeElementsList)
                activeElementsListForQ.AddRange(((ActiveElement)element).neutralizeToQ());
        }

        public List<PassiveElement> getPassiveElementsList() { return new List<PassiveElement>(passiveElementsList); }

        public List<Element> getActiveElementsListForZ() { return activeElementsListForZ; }

        public List<Element> getActiveElementsListForQ() { return activeElementsListForQ; }

        public Element[] getPassiveElementsByIndexes(int[] indexes)
        {
            int count = indexes.Length;
            Element[] elements = new Element[count];
            for (int i = 0; i < count; i++)
                elements[i] = passiveElementsList[indexes[i]];
            return elements;
        }

        /* Удаление элемента */
        public void transformElementToInfin(PassiveElement element, List<Element> elementsList)
        {
            List<Element> modifiedElements = elementToModifiedElementsDictionary[element].modifiedToInfinElements;
            if (modifiedElements != null)
                elementsList.AddRange(modifiedElements);
            //elementsList.Remove(element);
        }

        /* Стягивание элемента */
        public void transformElementToZero(Element element,
            List<Element> elementsList)
        {
            List<Element> modifiedElements = elementToModifiedElementsDictionary[element].modifiedToZeroElements;
            if (modifiedElements != null)
                elementsList.AddRange(modifiedElements);
            //elementsList.Remove(element);
        }

        /* Формирование соответствий между элементами и их модификациями */
        protected virtual void makeModifiedElementsAssociation()
        {
            elementToModifiedElementsDictionary = new Dictionary<Element, modifiedElementListsPair>();
            foreach (PassiveElement element in itsSelectedElements)
            {
                modifiedElementListsPair associatedPair = new modifiedElementListsPair();
                associatedPair.modifiedToInfinElements = element.cloneElement.getModifiedToInfin();
                associatedPair.modifiedToZeroElements = element.cloneElement.getModifiedToZero();
                elementToModifiedElementsDictionary.Add(element.cloneElement, associatedPair);
            }
        }

        /* Формирование соответствий между элементами и их значениями */
        void makeElementToValueAssociations()
        {
            elementToValueDictionary = new Dictionary<Element, string>();
            foreach (Element element in elementsList)
            {
                if (!(element is Nuller))
                    elementToValueDictionary.Add(element, ((ValueHavingElement)element).getFormattedValue());
            }
            /*foreach (Element element in activeElementsList)
            {
                elementToValueDictionary.Add(element, ((ValueHavingElement)element).getFormattedValue());
            }*/
        }

        /* Получение максимального номера узла */
        protected int getMaxKnot()
        {
            int maxKnot = 0;
            foreach (Element element in elementsList)
            {
                int knot = element.getMaxKnot();
                if (knot > maxKnot)
                    maxKnot = knot;
            }
            return maxKnot;
        }

        protected abstract void addClonesToList();
    }

    /* Схема для рассчета допуска */
    class SchemeForTolerance : AdvancedScheme
    {
        public string error;
        public bool useNegativeValues;
        public SchemeForTolerance(Scheme scheme, PassiveElement[] elements, string receivedError, bool useNegativeValues)
            : base(scheme, elements)
        {
            error = receivedError;//(Convert.ToDouble(receivedError) * 0.01).ToString();
            this.useNegativeValues = useNegativeValues;
            addClonesToList();
            makeModifiedElementsAssociation();
        }

        /* Моделирование допусков элементов */
        /* эта операция не является необходимой для расчета допуска */
        protected override void addClonesToList()
        {
            int maxKnot = getMaxKnot();
            foreach (PassiveElement element in itsSelectedElements)
            {
                if (((PassiveElement)element).clonnedElements == null)
                {
                    if (!(element is GU)) { maxKnot++; }
                    element.makeClonned(maxKnot, element.value);
                    if (element is HI) { maxKnot++; }
                }
                foreach (PassiveElement clonnedElement in element.clonnedElements)
                {
                    if (clonnedElement != null)
                        elementToValueDictionary.Add(clonnedElement, clonnedElement.getFormattedValue());
                }
            }
        }

        /*protected override void makeModifiedElementsAssociation()
        {
            elementToModifiedElementsDictionary = new Dictionary<Element, modifiedElementListsPair>();
            foreach (PassiveElement element in elements)
            {
                modifiedElementListsPair associatedPair = new modifiedElementListsPair();
                associatedPair.modifiedToInfinElements = element.getModifiedToInfin();
                associatedPair.modifiedToZeroElements = element.getModifiedToZero();
                elementToModifiedElementsDictionary.Add(element, associatedPair);
            }
        }*/
    }

    /* Схема для расчета погрешности */
    class SchemeForError : AdvancedScheme
    {
        public Dictionary<PassiveElement, string> elementToToleranceDictionary;
        public bool useNegativeValues;

        public SchemeForError(Scheme baseScheme, PassiveElement[] elements,
            Dictionary<PassiveElement, string> elementToToleranceAssociations, bool useNegativeValues)
            : base(baseScheme, elements)
        {
            elementToToleranceDictionary = elementToToleranceAssociations;
            this.useNegativeValues = useNegativeValues;
            addClonesToList();
            makeModifiedElementsAssociation();
        }

        /* Моделирование допусков элементов */
        protected override void addClonesToList()
        {
            int maxKnot = getMaxKnot();
            foreach (PassiveElement element in itsSelectedElements)
            {
                if (!(element is GU)) { maxKnot++; }
                string cloneValue = (Convert.ToDouble(element.value) * 0.01 *
                    Convert.ToDouble(elementToToleranceDictionary[element])).ToString();
                element.makeClonned(++maxKnot, cloneValue);
                if (element is HI) { maxKnot++; }
                foreach (PassiveElement clonnedElement in element.clonnedElements)
                {
                    if (clonnedElement != null)
                        elementToValueDictionary.Add(clonnedElement, clonnedElement.getFormattedValue());
                }
            }
        }
    }

    /* Схема для расчета дробной символьной схемной функции */
    class SchemeForSSF : AdvancedScheme
    {
        int itsSSFtype;

        public SchemeForSSF(Scheme scheme, PassiveElement[] elements, int SSFtype)
            : base(scheme, elements)
        {
            //error = (Convert.ToDouble(receivedError)*0.01).ToString();
            //addClonesToList();
            itsSelectedElements = new List<PassiveElement>(elements);
            itsSSFtype = SSFtype;
            makeModifiedElementsAssociation();
        }

        protected override void addClonesToList() { }

        /* Расчет дробной ССФ */
        public Result calculateSSF(ref int detansCount)
        {
            Result result = new Result();
            Dictionary<string, DetansPair> elementsToDetainPairDictionary = getDetans(itsSelectedElements.ToArray(), this, ref detansCount);
            string formule = makeSSFFormule(elementsToDetainPairDictionary, itsSelectedElements.ToArray(), itsSSFtype);
            string resultString;
            resultString = Calculations.calculateFormule(formule, this);
            result.elementNamesToDetansDictionary = elementsToDetainPairDictionary;
            result.selectedElements = (PassiveElement[])(itsSelectedElements.ToArray());
            result.formuleValue = new FormuleValue();
            result.formuleValue.formule = formule;
            result.formuleValue.value = resultString;
            return result;
        }

        /* Формирование соответствий между элементами и их модификациями */
        protected override void makeModifiedElementsAssociation()
        {
            elementToModifiedElementsDictionary = new Dictionary<Element, modifiedElementListsPair>();
            foreach (PassiveElement element in itsSelectedElements)
            {
                modifiedElementListsPair associatedPair = new modifiedElementListsPair();
                associatedPair.modifiedToInfinElements = element.getModifiedToInfin();
                associatedPair.modifiedToZeroElements = element.getModifiedToZero();
                elementToModifiedElementsDictionary.Add(element, associatedPair);
            }
        }

        /* Формирование формулы символьной схемной функции из определителей */
        string makeSSFFormule(Dictionary<string, DetansPair> elementsToDetanPairDictionary,
            PassiveElement[] selectedElements, int SSFType)
        {
            string formule = "";
            string fractionNumerator = "";
            string fractionDenominator = "";
            string lastKey = Calculations.elementsToNamesString(selectedElements);
            fractionNumerator = makeSSFFractionPart(elementsToDetanPairDictionary, selectedElements, SSFType, 1);
            fractionDenominator = makeSSFFractionPart(elementsToDetanPairDictionary, selectedElements, SSFType, 2);
            formule =
                "(" +
                    fractionNumerator +
                ")" +
                "/" +
                "(" +
                    fractionDenominator +
                ")";
            return formule;
        }

        /* Формирование части формулы дробной ССФ */
        /* чилителя или знаменателя - определяется значением fractionPart */
        string makeSSFFractionPart(Dictionary<string, DetansPair> elementsToDetanPairDictionary,
            PassiveElement[] selectedElements, int SSFType, int fractionPartType)
        {
            string formule = "(";
            switch (SSFType)
            {
                case 1:
                    switch (fractionPartType)
                    {
                        case 1:
                            formule += elementsToDetanPairDictionary[""].itsNumeratorDetan;
                            break;
                        case 2:
                            formule += elementsToDetanPairDictionary[""].itsDenominatorDetan;
                            break;
                        default:
                            return null;
                    }
                    formule += "+";
                    break;
                default:
                    return null;
            }
            foreach (KeyValuePair<string, DetansPair> elementsToDetanPair in elementsToDetanPairDictionary)
            {
                if (elementsToDetanPair.Key.Length > 0)
                {
                    foreach (string element in elementsToDetanPair.Key.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        formule += element + "*";
                    }
                    formule +=
                            "(";
                    switch (SSFType)
                    {
                        case 1:
                            switch (fractionPartType)
                            {
                                case 1:
                                    formule += elementsToDetanPair.Value.itsNumeratorDetan;
                                    break;
                                case 2:
                                    formule += elementsToDetanPair.Value.itsDenominatorDetan;
                                    break;
                                default:
                                    return null;
                            }
                            break;
                        default:
                            return null;
                    }
                    formule +=
                            ")" +
                        "+";
                }
            }
            formule = formule.Remove(formule.Length - 1, 1) + ")";
            return formule;
        }

        static Dictionary<string, DetansPair> getDetans(PassiveElement[] elements, AdvancedScheme scheme, ref int detansCount)
        {
            Dictionary<string, DetansPair> elementsToDetanPairDictionary = new Dictionary<string, DetansPair>();
            int elementsCount = elements.Length;
            List<Element> elementsList = new List<Element>();
            foreach (PassiveElement element in scheme.passiveElementsList)
                elementsList.Add(element);
            List<Element> activeElementsForZ = scheme.activeElementsListForZ;
            List<Element> activeElementsForQ = scheme.activeElementsListForQ;
            //elementsList.AddRange(scheme.nullersList);
            string selectedElementsString = Calculations.elementsToNamesString(elements);
            for (int i = 0; i <= elementsCount; i++)
            {
                int[] indexes = Calculations.makeCombination(i);
                while (true)
                {
                    List<Element> removedElements = new List<Element>();
                    List<Element> subtendedElements = new List<Element>();
                    List<Element> localElementsList = new List<Element>(elementsList);
                    List<Element> associatedElements = new List<Element>();
                    for (int j = 0; j < elementsCount; j++)
                    {
                        if (Array.IndexOf(indexes, j + 1) >= 0)
                        {
                            scheme.transformElementToInfin(elements[j], localElementsList);
                            associatedElements.Add(elements[j]);
                            removedElements.Add(elements[j]);
                        }
                        else
                        {
                            scheme.transformElementToZero(elements[j], localElementsList);
                            subtendedElements.Add(elements[j]);
                        }
                    }
                    List<Element> elementsListToZ = new List<Element>(localElementsList);
                    elementsListToZ.AddRange(activeElementsForZ);
                    List<Element> elementsListToQ = new List<Element>(localElementsList);
                    elementsListToQ.AddRange(activeElementsForQ);

                    string pattern = "";
                    string delimiter = ";";
                    string removedElementsString = Calculations.elementsToNamesString(removedElements, pattern, delimiter);
                    string keyString = selectedElementsString + "|" + removedElementsString;

                    string numeratorDetan;
                    string denominatorDetan;
                    if (mainForm.elementsStringToDetansMap.ContainsKey(keyString))
                    {
                        numeratorDetan = mainForm.elementsStringToDetansMap[keyString].itsNumeratorDetan;
                        denominatorDetan = mainForm.elementsStringToDetansMap[keyString].itsDenominatorDetan;
                    }
                    else
                    {
                        numeratorDetan = Calculations.getDetan(elementsListToZ);
                        //mainForm.elementsStringToDetansMap[keyString].itsNumeratorDetan = numeratorDetan;
                        denominatorDetan = Calculations.getDetan(elementsListToQ);
                        Calculations.correctFormule(ref numeratorDetan);
                        Calculations.correctFormule(ref denominatorDetan);
                        //mainForm.elementsStringToDetansMap[keyString].itsDenominatorDetan = denominatorDetan;
                        mainForm.elementsStringToDetansMap.Add(
                            keyString, new DetansPair(numeratorDetan, denominatorDetan));
                    }

                    //string detanZ = Calculations.getDetan(elementsListToZ);
                    //Calculations.correctFormule(ref detanZ);
                    //string detanQ = Calculations.getDetan(elementsListToQ);
                    //Calculations.correctFormule(ref detanQ);
                    string elementNames = Calculations.elementsToNamesString(associatedElements);
                    elementsToDetanPairDictionary.Add(elementNames, new DetansPair(numeratorDetan, denominatorDetan));
                    detansCount += 2;
                    if (Calculations.nextCombination(indexes, elements.Length) != 0)
                        break;
                }
            }
            return elementsToDetanPairDictionary;
        }
    }
}