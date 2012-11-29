using System;
using System.Collections.Generic;

namespace Toleralize2011_0
{
    /* Структура для хранения результата расчета */
    class Result
    {
        //public string formule;
        //public string valueForPositive;
        //public string valueForNegative;
        public PassiveElement[] selectedElements;
        public Dictionary<string, DetansPair> elementNamesToDetansDictionary;
        public FormuleValue formuleValue;
    }

    /* Формула и значение */
    class FormuleValue
    {
        public string formule;
        public string value;
    }

    /* Формула с положительным и отрицательным значением */
    class FormuleValueWithNegative : FormuleValue
    {
        public string negativeValue;
    }

    /* Пара формул определителей N и D */
    public class DetansPair
    {
        public DetansPair(string numeratorDetan, string denominatorDetan)
        {
            itsNumeratorDetan = numeratorDetan;
            itsDenominatorDetan = denominatorDetan;
        }
        public string itsNumeratorDetan;
        public string itsDenominatorDetan;
    }

    /* Функции для формирования и расчета формул */
    static class Calculations
    {
        //static int detansCount = 0;


        /* Расчет допусков для каждого элемента из elements */
        /* Возвращает ассоциативный массив, где ключ - элемент, значение - Result */
        public static Dictionary<PassiveElement[], Result> calculateTolerance_Total(SchemeForTolerance scheme, ref int detansCount)/*, Dictionary<PassiveElement[], Result> elementToToleranceDictionary)/*,
            PassiveElement[] elements, bool forNegative, string error)*/
        {
            Dictionary<PassiveElement[], Result> elementToToleranceDictionary = new Dictionary<PassiveElement[], Result>();
            foreach (PassiveElement element in scheme.getSelectedElements())
            {
                List<PassiveElement> selectedElement = new List<PassiveElement>();
                selectedElement.Add(element);
                SchemeForTolerance tempScheme = new SchemeForTolerance(Scheme.currentScheme,
                    new PassiveElement[] { element }, scheme.error, scheme.useNegativeValues);
                //scheme.setSelectedElements(selectedElement);
                Result result = calculateTolerance(tempScheme);
                detansCount += 4;
                elementToToleranceDictionary.Add(new PassiveElement[] { element }, result);
            }
            return elementToToleranceDictionary;
        }

        /* Расчет допуска для одного элемента (element) из схемы scheme */
        static Result calculateTolerance(SchemeForTolerance scheme)/*PassiveElement element, SchemeForTolerance scheme, bool forNegative)*/
        {
            PassiveElement element = scheme.getSelectedElements()[0];
            Result result = new Result();
            string[] detans = getDetans(element, scheme);
            result.elementNamesToDetansDictionary = new Dictionary<string, DetansPair>();
            result.elementNamesToDetansDictionary.Add("", new DetansPair(detans[1], detans[3]));
            result.elementNamesToDetansDictionary.Add(element.getRightName(), new DetansPair(detans[0], detans[2]));
            string formule = makeToleranceFormule(detans, scheme.error, false);
            if (scheme.useNegativeValues)
                result.formuleValue = new FormuleValueWithNegative();
            else
                result.formuleValue = new FormuleValue();
            result.formuleValue.formule = formule;
            string value = calculateFormule(formule, scheme);
            if (!String.IsNullOrEmpty(value))
                //if (!result.valueForPositive.Contains("j"))
                value = (Convert.ToDouble(value) * 100
                    / Convert.ToDouble(element.getFormattedValue())).ToString();
            result.formuleValue.value = value;
            if (scheme.useNegativeValues)
            {
                string negativeValue = calculateFormule(makeToleranceFormule(detans, scheme.error, true), scheme);
                if (!String.IsNullOrEmpty(negativeValue))
                    //if (!result.valueForNegative.Contains("j"))
                    negativeValue = (Convert.ToDouble(negativeValue) * 100
                        / Convert.ToDouble(element.getFormattedValue())).ToString();
                ((FormuleValueWithNegative)result.formuleValue).negativeValue = negativeValue;
            }
            return result;
        }

        /* Расчет погрешностей для всех возможных вариантов выбора элементов */
        /* Принимает ассоциативный массив, где ключ - элемент, значение - допуск */
        /* Заполняет ассоциативный массив, где ключ - элементы, значение - Result */
        public static Dictionary<PassiveElement[], Result> calculateAllErrors(SchemeForError scheme, ref int detansCount)
        {
            Dictionary<PassiveElement[], Result> elementsToErrorDictionary = new Dictionary<PassiveElement[], Result>();
            List<PassiveElement> passiveElementsList = new List<PassiveElement>();
            foreach (Element element in Scheme.currentScheme.getElementsList())
            {
                if (element is PassiveElement)
                    passiveElementsList.Add((PassiveElement)element);
            }
            Dictionary<PassiveElement, string> elementToToleranceDictionary = scheme.elementToToleranceDictionary;
            int passiveElementsCount = passiveElementsList.Count;
            /* Перебор длин сочетаний элементов */
            for (int i = 1; i <= passiveElementsCount; i++)
            {
                /* Первое сочетание данной длины элементов */
                int[] indexes = makeCombination(i);
                /* Перебор остальных сочетаний элементов */
                while (true)
                {
                    List<PassiveElement> selectedElements = new List<PassiveElement>();
                    foreach (int index in indexes)
                        selectedElements.Add(passiveElementsList[index - 1]);
                    Result result = new Result();
                    PassiveElement[] selectedElementsArray = selectedElements.ToArray();
                    SchemeForError tempScheme = new SchemeForError(Scheme.currentScheme,
                        selectedElementsArray, scheme.elementToToleranceDictionary, scheme.useNegativeValues);
                    result = calculateError(tempScheme, ref detansCount);
                    elementsToErrorDictionary.Add(selectedElementsArray, result);
                    if (nextCombination(indexes, passiveElementsList.Count) != 0)
                        break;
                }
            }
            return elementsToErrorDictionary;
        }

        /* Расчет погрешности для выбранной комбинации элементов */
        public static Result calculateError(SchemeForError scheme, ref int detansCount)
        {
            //SchemeForError scheme = new SchemeForError(Scheme.currentScheme, elements, elementToToleranceDictionary);
            PassiveElement[] selectedElements = scheme.getSelectedElements().ToArray();
            Dictionary<string, DetansPair> elementsToDetanPairDictionary =
                getDetans(selectedElements, scheme, ref detansCount);
            string formule = makeErrorFormule(elementsToDetanPairDictionary, false);
            Result result = new Result();
            result.elementNamesToDetansDictionary = elementsToDetanPairDictionary;
            if (scheme.useNegativeValues)
                result.formuleValue = new FormuleValueWithNegative();
            else
                result.formuleValue = new FormuleValue();
            result.formuleValue.formule = formule;
            string value = calculateFormule(formule, scheme);
            if (!String.IsNullOrEmpty(value))
            {
                if (!value.Contains("j"))
                    //value = value.Replace('.', ',');
                    value = (Convert.ToDouble(value) * 100).ToString();
            }
            result.formuleValue.value = value;
            if (scheme.useNegativeValues)
            {
                formule = makeErrorFormule(elementsToDetanPairDictionary, true);
                string resultForNegative = calculateFormule(formule, scheme);
                if (!String.IsNullOrEmpty(resultForNegative))
                    if (!resultForNegative.Contains("j"))
                        //resultForNegative = resultForNegative.Replace('.', ',');
                        resultForNegative = (Convert.ToDouble(resultForNegative) * 100).ToString();
                ((FormuleValueWithNegative)result.formuleValue).negativeValue = resultForNegative;
            }
            detansCount += result.elementNamesToDetansDictionary.Count * 2;
            return result;
        }

        /* Расчет дробной символьной схемной функции */
        public static Result calculateSSF(SchemeForSSF scheme, ref int detansCount)
        {
            Result result = new Result();
            //SchemeForSSF schemeForSSF = new SchemeForSSF(Scheme.currentScheme, elements);
            return scheme.calculateSSF(ref detansCount);
        }

        /* Нахождение пар определителей N и D для всех возможных сочетаний элементов */
        /* из заданного набора elements */
        /* Возвращает ассоциативный массив, где ключ - сочетание элементов, значение - пара определителей */
        //static Dictionary<string, detansPair> getDetans(
        static Dictionary<string, DetansPair> getDetans(
            PassiveElement[] elements, AdvancedScheme scheme, ref int detansCount)
        {
            Dictionary<string, DetansPair> elementsToDetanPairDictionary = new Dictionary<string, DetansPair>();
            int elementsCount = elements.Length;
            List<Element> elementsList = new List<Element>();
            foreach (PassiveElement element in scheme.passiveElementsList)
            {
                elementsList.Add(element);
            }
            List<Element> activeElementsForZ = scheme.activeElementsListForZ;
            List<Element> activeElementsForQ = scheme.activeElementsListForQ;
            foreach (PassiveElement element in elements)
            {
                elementsList.AddRange((element).clonnedElements);

                elementsList.Remove((element).cloneElement);
            }
            string selectedElementsString = elementsToNamesString(elements);
            //elementsList.AddRange(scheme.nullersList);
            /* Перебор длин сочетаний элементов */
            for (int i = 0; i <= elementsCount; i++)
            {
                /* Первое сочетание данной длины */
                int[] indexes = makeCombination(i);
                /* Перебор остальных сочетаний данной длины */
                while (true)
                {
                    List<Element> removedElements = new List<Element>();
                    List<Element> subtendedElements = new List<Element>();
                    List<Element> localElementsList = new List<Element>(elementsList);
                    List<Element> associatedElements = new List<Element>();
                    /* Перебор элементов данного сочетания */
                    for (int j = 0; j < elementsCount; j++)
                    {
                        /* Нейтрализация или стягивание элемента */
                        if (Array.IndexOf(indexes, j + 1) >= 0)
                        {
                            scheme.transformElementToInfin((elements[j]).cloneElement, localElementsList);
                            associatedElements.Add(elements[j]);
                            removedElements.Add(elements[j]);
                        }
                        else
                        {
                            scheme.transformElementToZero((elements[j]).cloneElement, localElementsList);
                            subtendedElements.Add(elements[j]);
                        }
                    }
                    /* Далее - формирование списка элементов и рассчет определителей */
                    List<Element> elementsListToZ = new List<Element>(localElementsList);
                    elementsListToZ.AddRange(activeElementsForZ);
                    List<Element> elementsListToQ = new List<Element>(localElementsList);
                    elementsListToQ.AddRange(activeElementsForQ);

                    string pattern = "δ(%element%)";
                    string delimiter = ";";
                    string removedElementsString = elementsToNamesString(removedElements, pattern, delimiter);
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
                        numeratorDetan = getDetan(elementsListToZ);
                        //mainForm.elementsStringToDetansMap[keyString].itsNumeratorDetan = numeratorDetan;
                        denominatorDetan = getDetan(elementsListToQ);
                        correctFormule(ref numeratorDetan);
                        correctFormule(ref denominatorDetan);
                        //mainForm.elementsStringToDetansMap[keyString].itsDenominatorDetan = denominatorDetan;
                        mainForm.elementsStringToDetansMap.Add(
                            keyString, new DetansPair(numeratorDetan,denominatorDetan));
                    }
                    string elementsString = elementsToNamesString(associatedElements);
                    elementsToDetanPairDictionary.Add(elementsString, new DetansPair(numeratorDetan, denominatorDetan));
                    detansCount += 2;

                    if (nextCombination(indexes, elements.Length) != 0)
                        break;
                }
            }
            return elementsToDetanPairDictionary;
        }

        /* Частный случай функции getDetans для одного элемента */
        static string[] getDetans(PassiveElement selectedElement, SchemeForTolerance scheme)
        {
            List<Element> elementsList = new List<Element>();//scheme.passiveElementsList;
            foreach (PassiveElement element in scheme.passiveElementsList)
            {
                elementsList.Add(element);
            }
            elementsList.AddRange((selectedElement).clonnedElements);
            elementsList.Remove((selectedElement).cloneElement);
            elementsList.AddRange(scheme.nullersList);
            List<Element> elementToInfinList = new List<Element>(elementsList);
            List<Element> elementToZeroList = new List<Element>(elementsList);
            scheme.transformElementToInfin(((PassiveElement)selectedElement).cloneElement, elementToInfinList);
            scheme.transformElementToZero(((PassiveElement)selectedElement).cloneElement, elementToZeroList);
            //scheme.transformElementToInfin(element, elementToInfinList);
            //scheme.transformElementToZero(element, elementToZeroList);
            List<Element> activeElementsForZ = scheme.getActiveElementsListForZ();
            List<Element> activeElementsForQ = scheme.getActiveElementsListForQ();
            List<Element> elementsToInfinToZList = new List<Element>(elementToInfinList);
            elementsToInfinToZList.AddRange(activeElementsForZ);
            List<Element> elementsToZeroToZList = new List<Element>(elementToZeroList);
            elementsToZeroToZList.AddRange(activeElementsForZ);
            List<Element> elementsToInfinToQList = new List<Element>(elementToInfinList);
            elementsToInfinToQList.AddRange(activeElementsForQ);
            List<Element> elementsToZeroToQList = new List<Element>(elementToZeroList);
            elementsToZeroToQList.AddRange(activeElementsForQ);
            string[] detans = new string[4];
            string elementName = selectedElement.getRightName();
            string keyForRemoved = elementName + "|" + "δ(" + elementName + ")";
            string keyForSubtended = elementName + "|";
            if (mainForm.elementsStringToDetansMap.ContainsKey(keyForRemoved))
            {
                detans[0] = mainForm.elementsStringToDetansMap[keyForRemoved].itsNumeratorDetan;
                detans[2] = mainForm.elementsStringToDetansMap[keyForRemoved].itsDenominatorDetan;
            }
            else
            {
                detans[0] = getDetan(elementsToInfinToZList);
                correctFormule(ref detans[0]);
                detans[2] = getDetan(elementsToInfinToQList);
                correctFormule(ref detans[2]);
                DetansPair detansPair = new DetansPair(detans[0], detans[2]);
                mainForm.elementsStringToDetansMap.Add(keyForRemoved, detansPair);
                //mainForm.elementsStringToDetansMap[keyForRemoved].itsDenominatorDetan = detans[2];
            }
            if (mainForm.elementsStringToDetansMap.ContainsKey(keyForSubtended))
            {
                detans[1] = mainForm.elementsStringToDetansMap[keyForSubtended].itsNumeratorDetan;
                detans[3] = mainForm.elementsStringToDetansMap[keyForSubtended].itsDenominatorDetan;
            }
            else
            {
                detans[1] = getDetan(elementsToZeroToZList);
                correctFormule(ref detans[1]);
                detans[3] = getDetan(elementsToZeroToQList);
                correctFormule(ref detans[3]);
                DetansPair detansPair = new DetansPair(detans[1], detans[3]);
                mainForm.elementsStringToDetansMap.Add(keyForSubtended, detansPair);
                //mainForm.elementsStringToDetansMap[keyForSubtended].itsNumeratorDetan = detans[1];
                //mainForm.elementsStringToDetansMap[keyForSubtended].itsDenominatorDetan = detans[3];

            }
            for (int i = 0; i < detans.Length; i++)
            {
                correctFormule(ref detans[i]);
            }
            return detans;
        }

        /* Запись списка элементов в файл и рассчет определителя */
        public static string getDetan(List<Element> elementsList)
        {
            string[] elementStrings = elementsListToStrings(elementsList);
            FileOperations.writeFileCir(elementStrings);
            FileOperations.runCirmulw();
            string formule = FileOperations.getFormuleFromOutFile();
            return formule;
        }

        /* Конвертация списка элементов в массив строк для записи в файл */
        static string[] elementsListToStrings(List<Element> elementsList)
        {
            int elementsCount = elementsList.Count;
            List<string> elementStrings = new List<string>();
            foreach (Element element in elementsList)
            {
                elementStrings.Add(element.ToString());
            }
            return elementStrings.ToArray();
        }

        /* Конвертация массива элементов в строку вида Шаблон(<элемент1>)<разделитель>Шаблон(<элемент2>);... */
        public static string elementsToNamesString(Element[] elements, string pattern, string delimiter)
        {
            const string elementNameSymbol = "%element%";
            if (!pattern.Contains(elementNameSymbol))
                pattern = elementNameSymbol;
            string elementsString = "";
            string[] names = new string[elements.Length];
            for (int i = 0; i < elements.Length; i++)
            {
                names[i] = elements[i].getRightName();
            }
            Array.Sort(names);
            for (int i = 0; i < names.Length; i++)
            {
                elementsString += pattern.Replace(elementNameSymbol, names[i]);
                elementsString += delimiter;
            }
            if (elementsString.Length > 0)
                if (elementsString.Substring(elementsString.Length - delimiter.Length) == delimiter)
                    elementsString = elementsString.Remove(elementsString.Length - delimiter.Length);
            return elementsString;
        }

        public static string elementsToNamesString(Element[] elements)
        {
            const string defaultDelimiter = ";";
            const string defaultPattern = "";
            return elementsToNamesString(elements, defaultPattern, defaultDelimiter);
        }

        public static string elementsToNamesString(List<Element> elements, string pattern, string delimiter)
        {
            return elementsToNamesString(elements.ToArray(), pattern, delimiter);
        }

        public static string elementsToNamesString(List<Element> elements)
        {
            return elementsToNamesString(elements.ToArray());
        }

        //static Element[] findElementsByIndexes(Element[] elements, int[] indexes)
        //{
        //    int index = 0;
        //    int indexesCount = indexes.Length;
        //    Element[] returnStrings = new Element[indexesCount];
        //    Predicate<Element> indexElementPredicate = delegate(Element element)
        //    {
        //        return Array.IndexOf(elements, element) == index;
        //    };
        //    for (int i = 0; i < indexesCount; i++)
        //    {
        //        index = indexes[i];
        //        returnStrings[i] = Array.Find<Element>(elements, indexElementPredicate);
        //    }
        //    return returnStrings;
        //}

        /* Вычисление значения формулы для схемы */
        public static string calculateFormule(string formule, AdvancedScheme scheme)
        {
            FileOperations.writeFileOut(formule, scheme);
            FileOperations.runCalcsym();
            return FileOperations.getResultFromCLCFile();
        }

        /* Исправление имен элементов */
        public static void correctFormule(ref string formule)
        {
            for (int i = 0; i < formule.Length - 1; i++)
            {
                if (string.Compare(formule.Substring(i, 2), "FI", true) == 0)
                {
                    formule = formule.Remove(i, 2).Insert(i++, "BI");
                }
            }
        }

        /* Формирование формулы допуска из определителей и значения погрешности */
        static string makeToleranceFormule(string[] detans, string error, bool forNegative)
        {
            string minus = forNegative ? "-" : "";
            string formule =
                "(" +
                        minus +
                        error +
                        "*" +
                        "(" + detans[1] + ")" +
                        "*" +
                        "(" + detans[3] + ")" +
                ")" +
                "/" +
                "(" +
                        "(" +
                                "(" + detans[0] + ")" +
                                "*" +
                                "(" + detans[3] + ")" +
                        ")" +
                        "-" +
                        "(" +
                                "(" + detans[1] + ")" +
                                "*" +
                                "(" + detans[2] + ")" +
                                "*" +
                                "(" +
                                        "(" + minus + error + ")" + "+" + "1" +
                                ")" +
                        ")" +
                ")";
            return formule;
        }

        /* Формирование формулы допуска из определителей и значений допусков элементов */
        static string makeErrorFormule(Dictionary<string, DetansPair> elementsToDetanPairDictionary, bool forNegative)
        {
            string minus = forNegative ? "-" : "";
            string detanZZero = "";
            string detanQZero = "";
            foreach (KeyValuePair<string, DetansPair> elementDetansPair in elementsToDetanPairDictionary)
            {
                if (elementDetansPair.Key.Length == 0)
                {
                    detanZZero = elementDetansPair.Value.itsNumeratorDetan;
                    detanQZero = elementDetansPair.Value.itsDenominatorDetan;
                    break;
                }
            }
            string numerator = "";
            string denominator = "(" + "(" + detanZZero + ")" + "*" + "(";
            foreach (KeyValuePair<string, DetansPair> elementsToDetanPair in elementsToDetanPairDictionary)
            {
                DetansPair associatedDetanPair = elementsToDetanPair.Value;
                if (elementsToDetanPair.Key.Length > 0)
                {
                    string detanZ = associatedDetanPair.itsNumeratorDetan;
                    string detanQ = associatedDetanPair.itsDenominatorDetan;
                    string[] elements = elementsToDetanPair.Key.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string elementName in elements)
                    {
                        numerator += minus + elementName + "*";
                        denominator += minus + elementName + "*";
                    }
                    numerator += "(" + "(" + detanZ + ")" + "*" + "(" + detanQZero + ")" + "-" + "(" + detanQ + ")" + "*" + "(" + detanZZero + ")" + ")" + "+";
                    denominator += detanQ + "+";
                }
            }
            numerator = numerator.Remove(numerator.Length - 1, 1);
            denominator += "(" + detanQZero + ")" + ")" + ")";
            string formule = "(" + numerator + ")" + "/" + denominator;
            return formule;
        }

        /* Формирование первого варианта сочетания индексов для заданной длины сочетания */
        public static int[] makeCombination(int count)
        {
            int[] indexes = new int[count];
            for (int i = 0; i < count; i++)
                indexes[i] = i + 1;
            return indexes;
        }

        /* Формирование следующего варианта сочетания индексов для заданной длины сочетания */
        /* на основе  предыдущего (indexes) */
        /* заполняет indexes новыми индексами */
        /* если больше нет сочетаний, возвращает единицу */
        public static int nextCombination(int[] indexes, int elementsCount)
        {
            if (indexes.Length == 0)
                return 1;
            for (int k = indexes.Length - 1; k >= 0; k--)
            {
                if (k < indexes.Length - 1)
                {
                    if (indexes[k] < indexes[k + 1] - 1)
                    {
                        indexes[k]++;
                        for (int l = k + 1; l < indexes.Length; l++)
                        {
                            indexes[l] = indexes[l - 1] + 1;
                        }
                        break;
                    }
                }
                else if (indexes[k] < elementsCount)
                {
                    indexes[k]++;
                    break;
                }
                else if (indexes.Length == 1)
                    return 1;
            }
            if ((indexes[0] == elementsCount - indexes.Length + 1) && indexes.Length > 1)
                return 1;
            return 0;
        }
    }
}