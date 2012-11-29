using System;
using System.Collections.Generic;
using System.Text;

namespace Toleralize2011_0
{
    /* Абстрактный элемент */
    public abstract class Element
    {
        /* Массив соответствий сежду строкой типа элемента и названием типа */
        public static Dictionary<string, string> typeStringToElementTypeDictionary = new Dictionary<string, string>()
        {
            {"R","Сопротивление"},{"g","Проводимоcть"},{"L","Индуктивность"},{"c","Емкость"},
            {"BI","ИТУТ"},{"HI","ИТУН"},{"KU","ИНУТ"},{"GU","ИНУН"},
            {"E","Источник напряжения"},{"J","Источник тока"},
            {"U","Приемник напряжения"},{"I","Приемник тока"},{"N","Нуллер"}
        };

        /* Суффикс, прибавляемый к имени элемента, чей допуск моделируется путем добавления к клону */
        protected static string clonnedSuffix = "cl";

        /* Индекс в названии элемента */
        public string index;
        /* Смежные узлы */
        public string[] knots;
        /* Название элемента */
        protected string name;

        public Element(string receivedName, string[] receivedKnots)
        {
            knots = receivedKnots;
            name = receivedName;
        }

        public Element(Element element)
        {
            name = element.name;
            for (int i = 0; i < element.knots.Length; i++)
                knots[i] = element.knots[i];
        }

        /* Получение для элемента строки формата SPICE */
        public override string ToString()
        {
            string elementString = getName();
            foreach (string knot in knots)
            {
                elementString += "  " + knot;
            }
            return elementString;
        }

        /* Получение имени элемента */
        public virtual string getName() { return name; }
        /* Получение форматированного имени (актуально для реактивных элементов) */
        public virtual string getFormattedName() { return getName(); }
        /* Получение корректного имени (актуально для BI) */
        public virtual string getRightName() { return getTypeString() + index; }
        /* Получение строки типа */
        public abstract string getTypeString();
        /* Получение форматированной стркоки типа (актуально для реактивных элементов) */
        public virtual string getFormattedType() { return getTypeString(); }
        /* Получение сичла полюсов */
        public abstract int getKnotsCount();
        /* Максимальный номер смежного узла */
        public virtual int getMaxKnot()
        {
            int maxKnot = 0;
            foreach (string knot in knots)
            {
                int knotInt = Convert.ToInt32(knot);
                if ((knotInt) > maxKnot)
                    maxKnot = knotInt;
            }
            return maxKnot;
        }
    }

    /* Абстрактный реальный элемент */
    public abstract class RealElement : Element
    {
        public RealElement(string receivedName, string[] receivedKnots)
            : base(receivedName, receivedKnots)
        {
            string typeString = getTypeString();
            int typeLength = typeString.Length;
            int i;
            for (i = 0; i < name.Length; i++)
                if (!char.IsLetter(name[i]))
                    break;
            index = receivedName.Substring(i);
        }

        public RealElement(Element element) : base(element) { }
    }

    /* Абстрактный элемент, имеющий параметр */
    public abstract class ValueHavingElement : RealElement
    {
        public string value;
        public ValueHavingElement(string receivedName, string[] receivedKnots, string receivedValue)
            : base(receivedName, receivedKnots)
        {
            value = receivedValue;
        }

        public ValueHavingElement(ValueHavingElement element)
            : base(element)
        {
            value = element.getValue();
        }

        public override string ToString()
        {
            string elementString = base.ToString();
            elementString += "  " + value;
            return elementString;
        }

        public string getValue()
        {
            return value;
        }

        public virtual string getFormattedValue()
        {
            return value;
        }

    }

    /* Абстрактный приемник */
    abstract class AcceptingElement : RealElement
    {
        public AcceptingElement(string receivedName, string[] receivedKnots)
            : base(receivedName, receivedKnots) { }

        public override int getKnotsCount() { return 2; }

        public abstract Nuller neutralizeToQ();
    }

    /* Абстрактный пассивный элемент */
    public abstract class PassiveElement : ValueHavingElement
    {
        /* Параметр в форме с плавающей запятой */
        protected string exponentialValue;
        /* Элемент, моделирующий допуск данного элемента */
        public PassiveElement cloneElement;
        public List<Element> clonnedElements;
        public PassiveElement(string receivedName, string[] receivedKnots, string receivedValue)
            : base(receivedName, receivedKnots, receivedValue)
        {
            exponentialValue = string.Format("{0:#.#####e+0}", Convert.ToDouble(value));
        }

        public PassiveElement(PassiveElement element) : base(element) { }

        public override string getFormattedValue() { return exponentialValue; }
        /* Моделирование допуска элемента путем добавления клона */
        public abstract void makeClonned(int maxKnot, string cloneValue);
        /* Удаление элемента */
        public abstract List<Element> getModifiedToInfin();
        /* Стягивание элемента */
        public abstract List<Element> getModifiedToZero();

        public override int getKnotsCount() { return 2; }
    }

    /* Абстрактный реактивный элемент */
    abstract class ReactiveElement : PassiveElement
    {
        /* Название элемента для формул */
        string complexName;

        public ReactiveElement(string receivedName, string[] receivedKnots, string receivedValue)
            : base(receivedName, receivedKnots, receivedValue)
        {
            complexName = getFormattedType() + index;
            exponentialValue = string.Format("s*{0:#.#####e+0}", Convert.ToDouble(value));
        }

        public ReactiveElement(ReactiveElement element)
            : base(element)
        {
            complexName = getFormattedType() + index;
            exponentialValue = string.Format("s*{0:#.#####e+0}", Convert.ToDouble(value));
        }

        public override string getFormattedName()
        {
            return getFormattedType() + index;
        }
    }

    /* Абстрактный управляемый источник */
    abstract class ControlledElement : PassiveElement
    {
        public ControlledElement(string receivedName, string[] receivedKnots, string receivedValue) : base(receivedName, receivedKnots, receivedValue) { }

        public ControlledElement(ControlledElement element) : base(element) { }

        public override List<Element> getModifiedToInfin()
        {
            Nuller nuller = new Nuller("N1", knots);
            List<Element> returnList = new List<Element>();
            returnList.Add(nuller);
            return returnList;
        }
        public override int getKnotsCount() { return 4; }
    }

    /* Абстрактный активный элемент */
    abstract class ActiveElement : ValueHavingElement
    {
        public AcceptingElement dependingElement;

        public ActiveElement(string receivedName, string[] receivedKnots, string receivedValue, AcceptingElement acceptor)
            : base(receivedName, receivedKnots, receivedValue)
        {
            dependingElement = acceptor;
        }
        public override int getKnotsCount() { return 2; }

        /* Нейтрализация элемента */
        public Element neutralizeToZ()
        {
            if (index != "1")
                return new E(name, knots, value, dependingElement);
            if (dependingElement == null)
                return new E(name, knots, value, dependingElement);
            string[] activeKnots = new string[2];
            Array.Copy(knots, activeKnots, 2);
            Array.Reverse(activeKnots);
            string[] acceptorKnots = dependingElement.knots;
            string[] nullerKnots = new string[4];
            Array.Copy(activeKnots, nullerKnots, 2);
            Array.Copy(acceptorKnots, 0, nullerKnots, 2, 2);
            return new Nuller("N1", nullerKnots);
        }

        public override int getMaxKnot()
        {
            int maxKnot = 0;
            foreach (string knot in knots)
            {
                int knotInt = Convert.ToInt32(knot);
                if (knotInt > maxKnot)
                    maxKnot = knotInt;
            }
            if (dependingElement == null)
                return maxKnot;
            foreach (string knot in dependingElement.knots)
            {
                int knotInt = Convert.ToInt32(knot);
                if (knotInt > maxKnot)
                    maxKnot = knotInt;
            }
            return maxKnot;
        }

        public abstract List<Element> neutralizeToQ();
    }

    /* Ниже реализованы все элементы и их методы */

    /* Нуллер */
    class Nuller : Element
    {
        public Nuller(string receivedName, string[] receivedKnots)
            : base(receivedName, receivedKnots) { }

        public override string getTypeString() { return "N"; }

        public override int getKnotsCount()
        {
            return 4;
        }
    }

    /* Сопротивление */
    class Resistor : PassiveElement
    {
        public Resistor(string receivedName, string[] receivedKnots, string receivedValue) : base(receivedName, receivedKnots, receivedValue) { }

        public Resistor(Resistor element) : base(element) { }

        public override string getTypeString() { return "R"; }

        public override void makeClonned(int maxKnot, string cloneValue)
        {
            string[] clonnedKnots = { maxKnot.ToString(), knots[1] };
            string[] cloneCnots = { knots[0], maxKnot.ToString() };
            Resistor clonned = new Resistor(name + clonnedSuffix, clonnedKnots, value);
            Resistor clone = new Resistor(name, cloneCnots, cloneValue);
            cloneElement = clone;
            clonnedElements = new List<Element>() { clonned, clone };
        }

        public override int getKnotsCount() { return 2; }

        public override List<Element> getModifiedToInfin()
        {
            return null;
        }

        public override List<Element> getModifiedToZero()
        {
            string[] nullerKnots = new string[4];
            Array.Copy(knots, nullerKnots, getKnotsCount());
            Array.Copy(knots, 0, nullerKnots, getKnotsCount(), getKnotsCount());
            Nuller nuller = new Nuller("N1", nullerKnots);
            return new List<Element>(new Element[] { nuller });
        }
    }

    /* Проводимость */
    class Conduction : PassiveElement
    {
        public Conduction(string receivedName, string[] receivedKnots, string receivedValue) : base(receivedName, receivedKnots, receivedValue) { }

        public Conduction(Conduction element) : base(element) { }

        public override string getTypeString() { return "g"; }

        public override void makeClonned(int maxKnot, string cloneValue)
        {
            Conduction clonned = new Conduction(name + clonnedSuffix, knots, value);
            Conduction clone = new Conduction(name, knots, cloneValue);
            cloneElement = clone;
            clonnedElements = new List<Element>() { clonned, clone };
        }

        public override List<Element> getModifiedToInfin()
        {
            string[] nullerKnots = new string[4];
            Array.Copy(knots, nullerKnots, getKnotsCount());
            Array.Copy(knots, 0, nullerKnots, getKnotsCount(), getKnotsCount());
            Nuller nuller = new Nuller("N1", nullerKnots);
            return new List<Element>(new Element[] { nuller }); ;
        }

        public override List<Element> getModifiedToZero()
        {
            return null;
        }
    }

    /* Индуктивность */
    class Inductance : ReactiveElement
    {
        public Inductance(string receivedName, string[] receivedKnots, string receivedValue) : base(receivedName, receivedKnots, receivedValue) { }

        public Inductance(Inductance element) : base(element) { }

        public override void makeClonned(int maxKnot, string cloneValue)
        {
            string[] clonnedKnots = { maxKnot.ToString(), knots[1] };
            string[] cloneCnots = { knots[0], maxKnot.ToString() };
            Inductance clonned = new Inductance(name + clonnedSuffix, clonnedKnots, value);
            Inductance clone = new Inductance(name, cloneCnots, cloneValue);
            cloneElement = clone;
            clonnedElements = new List<Element>() { clonned, clone };
        }

        public override string getTypeString() { return "L"; }

        public override string getFormattedType() { return "Z"; }

        public override List<Element> getModifiedToInfin()
        {
            string[] nullerKnots = new string[4];
            Array.Copy(knots, nullerKnots, getKnotsCount());
            Array.Copy(knots, 0, nullerKnots, getKnotsCount(), getKnotsCount());
            Nuller nuller = new Nuller("N1", nullerKnots);
            return new List<Element>(new Element[] { nuller });
        }

        public override List<Element> getModifiedToZero()
        {
            return null;
        }
    }

    /* Емкость */
    class Capacity : ReactiveElement
    {
        public Capacity(string receivedName, string[] receivedKnots, string receivedValue) : base(receivedName, receivedKnots, receivedValue) { }

        public Capacity(Capacity element) : base(element) { }

        public override void makeClonned(int maxKnot, string cloneValue)
        {
            Capacity clonned = new Capacity(name + clonnedSuffix, knots, value);
            Capacity clone = new Capacity(name, knots, cloneValue);
            cloneElement = clone;
            clonnedElements = new List<Element>() { clonned, clone };
        }

        public override string getTypeString() { return "c"; }

        public override string getFormattedType() { return "y"; }

        public override List<Element> getModifiedToInfin()
        {
            string[] nullerKnots = new string[4];
            Array.Copy(knots, nullerKnots, getKnotsCount());
            Array.Copy(knots, 0, nullerKnots, getKnotsCount(), getKnotsCount());
            Nuller nuller = new Nuller("N1", nullerKnots);
            return new List<Element>(new Element[] { nuller }); ;
        }

        public override List<Element> getModifiedToZero()
        {
            return null;
        }
    }

    /* ИТУТ */
    class BI : ControlledElement
    {
        public BI(string receivedName, string[] receivedKnots, string receivedValue) : base(receivedName, receivedKnots, receivedValue) { }

        public BI(BI element) : base(element) { }

        public override void makeClonned(int maxKnot, string cloneValue)
        {
            string max = maxKnot.ToString();
            string[] clonnedKnots = { knots[0], knots[1], max, knots[3] };
            string[] cloneKnots = { knots[0], knots[1], knots[2], max };
            BI clonned = new BI(name + clonnedSuffix, clonnedKnots, value);
            BI clone = new BI(name, cloneKnots, cloneValue);
            cloneElement = clone;
            clonnedElements = new List<Element>() { clonned, clone };
        }

        public override string getName()
        {
            return "FI" + index;
        }

        public override string getFormattedName() { return getRightName(); }
        public override string getTypeString() { return "BI"; }

        public override List<Element> getModifiedToZero()
        {
            string[] knotsPair = new string[2];
            Array.Copy(knots, 2, knotsPair, 0, 2);
            string[] nullerKnots = new string[4];
            Array.Copy(knotsPair, nullerKnots, 2);
            Array.Copy(knotsPair, 0, nullerKnots, 2, 2);
            Nuller nuller = new Nuller("N1", nullerKnots);
            List<Element> returnList = new List<Element>();
            returnList.Add(nuller);
            return returnList;
        }
    }

    /* ИТУН */
    class HI : ControlledElement
    {
        public HI(string receivedName, string[] receivedKnots, string receivedValue) : base(receivedName, receivedKnots, receivedValue) { }

        public HI(HI element) : base(element) { }

        public override void makeClonned(int maxKnot, string cloneValue)
        {
            string max1 = maxKnot.ToString();
            string max2 = (maxKnot + 1).ToString();
            string[] clonnedKnots = { max1, knots[1], max2, knots[3] };
            string[] cloneKnots = { knots[0], max1, knots[2], max2 };
            HI clonned = new HI(name + clonnedSuffix, clonnedKnots, value);
            HI clone = new HI(name, cloneKnots, cloneValue);
            cloneElement = clone;
            clonnedElements = new List<Element>() { clonned, clone };
        }

        public override string getTypeString() { return "HI"; }

        public override List<Element> getModifiedToZero()
        {
            string[] knotsPair1 = new string[2];
            string[] knotsPair2 = new string[2];
            string[] nuller1Knots = new string[4];
            string[] nuller2Knots = new string[4];
            Array.Copy(knots, knotsPair1, 2);
            Array.Copy(knots, 2, knotsPair2, 0, 2);
            Array.Copy(knotsPair1, nuller1Knots, 2);
            Array.Copy(knotsPair1, 0, nuller1Knots, 2, 2);
            Nuller nuller1 = new Nuller("N1", nuller1Knots);
            Array.Copy(knotsPair2, nuller2Knots, 2);
            Array.Copy(knotsPair2, 0, nuller2Knots, 2, 2);
            Nuller nuller2 = new Nuller("N1", nuller2Knots);
            List<Element> returnList = new List<Element>();
            returnList.Add(nuller2);
            returnList.Add(nuller2);
            return returnList;
        }
    }

    /* ИНУТ */
    class KU : ControlledElement
    {
        public KU(string receivedName, string[] receivedKnots, string receivedValue) : base(receivedName, receivedKnots, receivedValue) { }

        public KU(KU element) : base(element) { }

        public override void makeClonned(int maxKnot, string cloneValue)
        {
            string max = maxKnot.ToString();
            string[] clonnedKnots = { max, knots[1], knots[2], knots[3] };
            string[] cloneKnots = { knots[0], max, knots[2], knots[3] };
            KU clonned = new KU(name + clonnedSuffix, clonnedKnots, value);
            KU clone = new KU(name, cloneKnots, cloneValue);
            cloneElement = clone;
            clonnedElements = new List<Element>() { clonned, clone };
        }

        public override string getTypeString() { return "KU"; }

        public override List<Element> getModifiedToZero()
        {
            string[] knotsPair = new string[2];
            string[] nullerKnots = new string[4]; ;
            Array.Copy(knots, knotsPair, 2);
            Array.Copy(knotsPair, nullerKnots, 2);
            Array.Copy(knotsPair, 0, nullerKnots, 2, 2);
            Nuller nuller = new Nuller("N1", nullerKnots);
            List<Element> returnList = new List<Element>();
            returnList.Add(nuller);
            return returnList;
        }
    }

    /* ИНУН */
    class GU : ControlledElement
    {
        public GU(string receivedName, string[] receivedKnots, string receivedValue) : base(receivedName, receivedKnots, receivedValue) { }

        public GU(GU element) : base(element) { }

        public override void makeClonned(int maxKnot, string cloneValue)
        {
            GU clonned = new GU(name + clonnedSuffix, knots, value);
            GU clone = new GU(name, knots, cloneValue);
            cloneElement = clone;
            clonnedElements = new List<Element>() { clonned, clone };
        }

        public override string getTypeString() { return "GU"; }

        public override List<Element> getModifiedToZero()
        {
            return null;
        }
    }

    /* Генератор ЭДС */
    class E : ActiveElement
    {
        public E(string receivedName, string[] receivedKnots, string receivedValue, AcceptingElement acceptor) : base(receivedName, receivedKnots, receivedValue, acceptor) { }

        public override string getTypeString() { return "E"; }

        public override List<Element> neutralizeToQ()
        {
            string[] nullerKnots = new string[4];
            Array.Copy(knots, nullerKnots, 2);
            Array.Copy(knots, 0, nullerKnots, 2, 2);
            Nuller nuller1 = new Nuller("N2", nullerKnots);
            List<Element> returnList = new List<Element>();
            returnList.Add(nuller1);
            if (dependingElement != null)
            {
                Nuller nuller2 = dependingElement.neutralizeToQ();
                if (nuller2 != null)
                    returnList.Add(nuller2);
            }
            return returnList;
        }
    }

    /* Генератор тока */
    class J : ActiveElement
    {
        public J(string receivedName, string[] receivedKnots, string receivedValue, AcceptingElement acceptor) : base(receivedName, receivedKnots, receivedValue, acceptor) { }

        public override string getTypeString() { return "J"; }

        public override List<Element> neutralizeToQ()
        {
            if (dependingElement == null)
                return null;
            Nuller nuller = dependingElement.neutralizeToQ();
            if (nuller == null)
                return null;
            List<Element> returnList = new List<Element>();
            returnList.Add(dependingElement.neutralizeToQ());
            return returnList;
        }
    }

    /* Приемник напряжения */
    class U : AcceptingElement
    {
        public U(string receivedName, string[] receivedKnots)
            : base(receivedName, receivedKnots) { }

        public override string getTypeString() { return "U"; }

        public override Nuller neutralizeToQ()
        {
            return null;
        }
    }

    /* Приемник тока */
    class I : AcceptingElement
    {
        public I(string receivedName, string[] receivedKnots)
            : base(receivedName, receivedKnots) { }

        public override string getTypeString() { return "I"; }

        public override Nuller neutralizeToQ()
        {
            string[] nullerKnots = new string[4];
            Array.Copy(knots, nullerKnots, 2);
            Array.Copy(knots, 0, nullerKnots, 2, 2);
            return new Nuller("N2", nullerKnots);
        }
    }
}