//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Toleralize2011_0
//{
//    interface ITask<T>
//    {
//        void Execute();
//    }

//    abstract class AbstractTask : ITask<AbstractTask>
//    {
//        Scheme scheme;

//        public AbstractTask(AdvancedScheme scheme)
//        {

//        }

//        public abstract void Execute();
//    }

//    abstract class AbstractNegativeOptionedTask : AbstractTask
//    {
//        bool negativeValues;

//        public AbstractNegativeOptionedTask(AdvancedScheme scheme, bool useNegativeValues) : base(scheme) { }
//    }

//    class TolerancesTask : AbstractNegativeOptionedTask
//    {
//        public TolerancesTask(AdvancedScheme scheme, bool useNegativeValues) : base(scheme, useNegativeValues) { }

//        public override void Execute()
//        {
//        }
//    }
//}
