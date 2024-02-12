﻿using SoraDataEngine.Commons.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoraDataEngine.Runtime.Binding
{
    /// <summary>
    /// 属性侦听绑定器
    /// </summary>
    public class AttributeBinder
    {
        /// <summary>
        /// 实例
        /// </summary>
        public static AttributeBinder? Instance;

        /// <summary>
        /// 实例化
        /// </summary>
        public AttributeBinder()
        {
            Instance = RuntimeCore.AttributeBinder;
        }

        /// <summary>
        /// 添加一个事件绑定
        /// </summary>
        /// <param name="callback">回调函数，参数一为新的值，参数二为旧的值</param>
        /// <param name="target">目标属性</param>
        public void Bind(Action<object?, object?> callback, IAttribute target) 
        {
            target.OnValueChanged += callback;
        }
    }
}
