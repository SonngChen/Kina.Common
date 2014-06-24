using System;

namespace Kina.Common.Components
{
    /// <summary>
    /// 组件特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentAttribute : Attribute
    {
        public LifeStyle LifeStyle { get; private set; }
        
        public ComponentAttribute() : this(LifeStyle.Transient) { }
        
        public ComponentAttribute(LifeStyle lifeStyle)
        {
            LifeStyle = lifeStyle;
        }
    }
}
