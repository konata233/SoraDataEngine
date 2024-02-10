using SoraDataEngine.Commons.Condition;
using SoraDataEngine.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    internal class ConditionTest
    {
        public static void Test()
        {
            Condition c1 = new Condition
            {
                IsSatisfied = new Func<bool>(() => RandBool()),
            };
            Condition c2 = new Condition
            {
                IsSatisfied = new Func<bool>(() => RandBool()),
            };

            AggregateCondition condition = new AggregateCondition
            {
                ConditionType = AggregateConditionType.Any,
                Conditions = new List<ICondition>
                {
                    c1, c2
                }
            }.Build();
            
            //Console.WriteLine(c1.IsSatisfied().ToString() + c2.IsSatisfied().ToString());

            //Console.WriteLine(condition.IsSatisfied());

            condition.ConditionType = AggregateConditionType.None;
           // Console.WriteLine(condition.Build().IsSatisfied());

            condition.ConditionType = AggregateConditionType.All;
            //Console.WriteLine(condition.Build().IsSatisfied());
        }

        private static bool RandBool()
        {
            bool[] arr = { true, false };
            Random ran = new Random();
            return arr[ran.Next(2)];
        }
    }
}
