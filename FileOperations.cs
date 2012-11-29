using System;
using System.Collections.Generic;
//using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace Toleralize2011_0
{
    enum elementTypeEnum { R, g, HI, H, BI, FI, F, B, K, KU, G, GU, L, c, E, J, I, U, N };
    enum passiveElementTypeEnum { R, g }
    enum reactiveElementTypeEnum { L, c };
    enum activeElementTypeEnum { E, J };
    enum acceptingElementTypeEnum { U, I };
    enum controlledElementTypeEnum { HI, H, BI, FI, F, B, K, KU, G, GU };

    static class FileOperations
    {
        static Type elementType = typeof(elementTypeEnum);
        static Type passiveElementType = typeof(passiveElementTypeEnum);
        static Type reactiveElementType = typeof(reactiveElementTypeEnum);
        static Type activeElementType = typeof(activeElementTypeEnum);
        static Type acceptingElementType = typeof(acceptingElementTypeEnum);
        static Type controlledElementType = typeof(controlledElementTypeEnum);

        const string workingDirectory = @"\Work";
        const string CirmulwFileName = "CIRMULW.exe";
        const string CalcsymFileName = "CALCSYM.exe";
        const string cirFileName = "CIR";
        const string outFileName = "OUT";
        const string clcFileName = "CLC";

        static string workingDirectoryPath = Application.StartupPath + workingDirectory;
        static string CirmulwPath = workingDirectoryPath + "\\" + CirmulwFileName;
        static string CalcsymPath = workingDirectoryPath + "\\" + CalcsymFileName;
        static string cirFilePath = Application.StartupPath + workingDirectory + "\\" + cirFileName;
        static string outFilePath = Application.StartupPath + workingDirectory + "\\" + outFileName;

        const int passiveElementParametersCount = 3;
        const int reactiveElementParametersCount = 3;
        const int activeElementParametersCount = 3;
        const int acceptingElementParametersCount = 2;
        const int controlledElementParametersCount = 5;
        const int nullerParametersCount = 4;

        static List<Element> getElements(List<string> elementStrings)
        {
            List<Element> elementsList = new List<Element>();
            foreach (string elementString in elementStrings)
            {
                string[] elementStringParts = elementString.Split(new char[] { ' ' },
                    StringSplitOptions.RemoveEmptyEntries);
                string elementTypeString = getElementType(elementStringParts[0]);
                //if (elementTypeString == "FI" || elementTypeString == "B")
                //    elementTypeString = "BI";
                string name = elementStringParts[0];
                int parametersCount = elementStringParts.Length;
                int knotsCount = parametersCount % 2 == 0 ? parametersCount - 2 : parametersCount - 1;
                string[] knots = new string[knotsCount];
                Array.Copy(elementStringParts, 1, knots, 0, knotsCount);
                string value = parametersCount % 2 == 0 ? elementStringParts[parametersCount - 1] : "";
                switch (elementTypeString)
                {
                    case "N":
                        elementsList.Add(new Nuller(name, knots));
                        break;
                    case "R":
                        elementsList.Add(new Resistor(name, knots, value));
                        break;
                    case "g":
                        elementsList.Add(new Conduction(name, knots, value));
                        break;
                    case "c":
                        elementsList.Add(new Capacity(name, knots, value));
                        break;
                    case "L":
                        elementsList.Add(new Inductance(name, knots, value));
                        break;
                    case "FI":
                    case "F":
                    case "B":
                    case "BI":
                        elementsList.Add(new BI(name, knots, value));
                        break;
                    case "H":
                    case "HI":
                    case "Hi":
                        elementsList.Add(new HI(name, knots, value));
                        break;
                    case "K":
                    case "KU":
                        elementsList.Add(new KU(name, knots, value));
                        break;
                    case "G":
                    case "GU":
                        elementsList.Add(new GU(name, knots, value));
                        break;
                    case "E":
                        {
                            AcceptingElement acceptor = getAcceptor(elementStrings, name);
                            elementsList.Add(new E(name, knots, value, acceptor));
                            break;
                        }
                    case "J":
                        {
                            AcceptingElement acceptor = getAcceptor(elementStrings, name);
                            elementsList.Add(new J(name, knots, value, acceptor));
                            break;
                        }
                }
            }
            return elementsList;
        }

        static AcceptingElement getAcceptor(List<string> elementStrings, string activeElementName)
        {
            foreach (string elementString in elementStrings)
            {
                string name = elementString.Substring(0, elementString.IndexOf(' '));
                string type = getElementType(name);
                string index = name.Substring(type.Length);
                string activeElementType = getElementType(activeElementName);
                string activeElementIndex = activeElementName.Substring(activeElementType.Length);
                if (Array.IndexOf(Enum.GetNames(acceptingElementType), type) >= 0)
                {
                    if (index == activeElementIndex)
                    {
                        string[] elementItems = elementString.Split(new char[] { ' ' },
                            StringSplitOptions.RemoveEmptyEntries);
                        string[] knots = new string[acceptingElementParametersCount];
                        Array.Copy(elementItems, 1, knots, 0, acceptingElementParametersCount);
                        switch (type)
                        {
                            case "I":
                                return new I(name, knots);
                            case "U":
                                return new U(name, knots);
                        }
                    }
                }
            }
            return null;
        }

        static bool isElementString(string fileString)
        {
            string[] elementStringParts = fileString.Split(new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);
            string elementTypeString = getElementType(elementStringParts[0]);
            if (elementTypeString == "N")
            {
                if (isSpecifiedElementString(elementStringParts, nullerParametersCount))
                    return true;
            }
            if (Array.IndexOf(Enum.GetNames(passiveElementType), elementTypeString) >= 0)
                if (isSpecifiedElementString(elementStringParts, passiveElementParametersCount))
                    return true;
            if (Array.IndexOf(Enum.GetNames(reactiveElementType), elementTypeString) >= 0)
                if (isSpecifiedElementString(elementStringParts, reactiveElementParametersCount))
                    return true;
            if (Array.IndexOf(Enum.GetNames(activeElementType), elementTypeString) >= 0)
                if (isSpecifiedElementString(elementStringParts, activeElementParametersCount))
                    return true;
            if (Array.IndexOf(Enum.GetNames(acceptingElementType), elementTypeString) >= 0)
                if (isSpecifiedElementString(elementStringParts, acceptingElementParametersCount))
                    return true;
            if (Array.IndexOf(Enum.GetNames(controlledElementType), elementTypeString) >= 0)
                if (isSpecifiedElementString(elementStringParts, controlledElementParametersCount))
                    return true;
            return false;
        }

        static bool isSpecifiedElementString(string[] elementStringParts, int requiredParametersCount)
        {
            int stringPartsCount = elementStringParts.Length;
            if (stringPartsCount != requiredParametersCount + 1)
                return false;
            return checkParameters(elementStringParts);
        }

        static bool checkParameters(string[] elementStringParts)
        {
            int count = elementStringParts.Length;
            count = count % 2 == 0 ? count - 1 : count;
            for (int i = 1; i < count; i++)
            {
                int number;
                if (!int.TryParse(elementStringParts[i], out number))
                    return false;
            }
            if (count < elementStringParts.Length)
            {
                double number;
                if (!double.TryParse(elementStringParts[count], out number))
                    return false;
            }
            return true;
        }

        public static string getElementType(string elementName)
        {
            int i;
            for (i = 0; i < elementName.Length; i++)
            {
                if (!Char.IsLetter(elementName[i]))
                    break;
            }
            return elementName.Substring(0, i);
        }

        public static void runCirmulw()
        {
            startProcess(CirmulwPath);
        }

        public static void runCalcsym()
        {
            startProcess(CalcsymPath);
        }

        public static void startProcess(string filePath)
        {
            Process process = new Process();
            ProcessStartInfo processInfo = new ProcessStartInfo(filePath);
            processInfo.UseShellExecute = false;
            processInfo.WorkingDirectory = workingDirectoryPath;
            processInfo.CreateNoWindow = true;
            process.StartInfo = processInfo;
            process.Start();
            while (!process.HasExited) ;
        }

        public static string getFormuleFromOutFile()
        {
            string formule = "";
            bool formuleExist = false;
            try
            {
                string[] fileStrings = File.ReadAllLines(outFilePath);
                int i;
                for (i = 0; i < fileStrings.Length; i++)
                {
                    if (fileStrings[i].Length >= 5)
                        if (fileStrings[i].Substring(0, 5) == "detan")
                            break;
                }
                for (i++; i < fileStrings.Length; i++)
                {
                    formule += fileStrings[i].Trim();
                    if (fileStrings[i].Contains(";"))
                    {
                        formuleExist = true;
                        break;
                    }
                }
                if (!formuleExist)
                    return "";
                return formule.Substring(0, formule.Length - 1);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string getResultFromCLCFile()
        {
            string[] fileStrings = File.ReadAllLines(workingDirectoryPath + "\\" + clcFileName);
            int i;
            for (i = 0; i < fileStrings.Length; i++)
            {
                if (fileStrings[i].Length > 6)
                    if (fileStrings[i].Substring(0, 6) == "detan=")
                    {
                        string result = fileStrings[i].Substring(fileStrings[i].IndexOf('=') + 1).Trim();
                        //if (!fileStrings[i].Contains("j"))
                        //    if (Convert.ToDouble(result) > 0.00001 && Convert.ToDouble(result) < 10000000)
                        //        result = string.Format("{0:F3}", Convert.ToDouble(result));
                        return result;
                    }
            }
            return "";
        }

        public static void writeFileCir(string[] elementStrings)
        {
            int elementStringsCount = elementStrings.Length;
            int fileStringsCount = elementStringsCount + 3;
            string[] fileStrings = new string[fileStringsCount];
            Array.Copy(elementStrings, 0, fileStrings, 2, elementStringsCount);
            fileStrings[0] = "1";
            fileStrings[1] = ".AC LOG 5 1000";
            fileStrings[fileStringsCount - 1] = ".END";
            Directory.CreateDirectory(workingDirectoryPath);
            //if(File.)
            File.WriteAllLines(cirFilePath, fileStrings);
        }

        public static void writeFileOut(string formule, AdvancedScheme scheme)
        {
            int elementCount = scheme.elementToValueDictionary.Count;
            int fileStringsCount = elementCount + 3;
            List<string> fileStrings = new List<string>();
            fileStrings.Add("1");
            fileStrings.Add("");
            fileStrings.Add("f=1000.000000;");
            fileStrings.Add("s=2*3.14159265358979323j*f;");
            foreach (KeyValuePair<Element, string> elementValuePair in scheme.elementToValueDictionary)
            {
                string value = elementValuePair.Value;
                fileStrings.Add(((ValueHavingElement)elementValuePair.Key).getFormattedName() + "=" + value + ";");
            }
            fileStrings.Add("");
            fileStrings.Add("detan =");
            fileStrings.Add(formule + ";");
            File.WriteAllLines(outFilePath, fileStrings.ToArray());
        }

        public static List<Element> getElementsListFromFile(string fileName)
        {
            List<Element> elementsList = new List<Element>();
            List<string> elementStrings = new List<string>();
            foreach (string fileString in File.ReadAllLines(fileName))
            {
                if (!string.IsNullOrEmpty(fileString))
                    if (isElementString(fileString))
                        elementStrings.Add(fileString);
            }
            elementsList = getElements(elementStrings);
            return elementsList;
        }
    }
}