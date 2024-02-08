using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Commons.Condition
{
    public class AggregateCondition : ICondition
    {
        /// <summary>
        /// 条件
        /// </summary>
        public IEnumerable<ICondition> Conditions { get; set; }

        /// <summary>
        /// <b>注意：</b>每次更新 ConditionType，都需要手动 Build
        /// </summary>
        public AggregateConditionType ConditionType { get; set; }

        public Func<bool> IsSatisfied { get; set; }

        /// <summary>
        /// 手动创建此类的实例
        /// 如果使用此方法构造，请
        /// AggregateCondition instance = new AggregateCondition{ Conditions = ..., ConditionType = ...}.Build(...);
        /// 
        /// </summary>
        public AggregateCondition()
        {
            Conditions = new List<ICondition>();
            IsSatisfied = new Func<bool>(() => true);
        }

        /// <summary>
        /// 自动创建此类的实例，不需要手动 Build()
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="type"></param>
        public AggregateCondition(IEnumerable<ICondition> conditions, AggregateConditionType type = AggregateConditionType.All)
        {
            Conditions = conditions;
            IsSatisfied = IsSatisfiedBuilder(type);
        }

        /// <summary>
        /// 手动构建此类的实例
        /// </summary>
        /// <param name="customizedIsSatisfiedFunc">自定义检测条件是否满足的函数</param>
        /// <returns></returns>
        public AggregateCondition Build(Func<bool> customizedIsSatisfiedFunc)
        {
            IsSatisfied = customizedIsSatisfiedFunc;
            return this;
        }

        /// <summary>
        /// 手动构建此类的实例
        /// </summary>
        /// <returns></returns>
        public AggregateCondition Build()
        {
            IsSatisfied = IsSatisfiedBuilder(ConditionType);
            return this;
        }

        /// <summary>
        /// 添加一个条件
        /// </summary>
        /// <param name="condition"></param>
        public void AddCondition(ICondition condition) 
        {
            Conditions.Append(condition);
        }

        /// <summary>
        /// 获得默认的 IsSatisfied Func<bool>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private Func<bool> IsSatisfiedBuilder(AggregateConditionType type)
        {
            switch (type)
            {
                case AggregateConditionType.None:
                    // 任何一个子条件都不满足，则此条件满足
                    return new Func<bool>(
                            () =>
                            {
                                bool ret = true;
                                foreach (ICondition condition in Conditions)
                                {
                                    if (condition.IsSatisfied())
                                    {
                                        ret = false;
                                        return ret;
                                    }
                                }
                                return ret;
                            }
                        );
                case AggregateConditionType.Any:
                    // 满足任何一个子条件，则此条件满足
                    return new Func<bool>(
                        () => 
                        {
                            bool ret = false;
                            foreach (ICondition condition in Conditions)
                            {
                                if (condition.IsSatisfied())
                                {
                                    ret = true;
                                    return ret;
                                }
                            }
                            return ret;
                        }
                    );
                case AggregateConditionType.All:
                    // 满足所有子条件，则此条件满足
                    return new Func<bool>(
                        () =>
                        {
                            bool ret = true;
                            foreach (ICondition condition in Conditions)
                            {
                                if (!condition.IsSatisfied())
                                {
                                    ret = false;
                                    return ret;
                                }
                            }
                            return ret;
                        }
                    );
                default:
                    // 默认：恒满足
                    return new Func<bool>(() => true);
            }
        }
    }
}
