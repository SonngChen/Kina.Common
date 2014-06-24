using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kina.Common.Components
{
    public enum LifeStyle
    {
        /// <summary>
        /// 标识对于每次的请求得到的都是一个新的实例（类似new）
        /// </summary>
        Transient,
        /// <summary>
        /// 标识一个组件只有一个实例被创建，所有请求的客户使用程序得到的都是同一个实例。
        /// </summary>
        Singleton
    }
}
