using System;

namespace Kina.Common.Components
{
    /// <summary>
    /// 组件容器接口
    /// </summary>
    public interface IComponentContainer
    {
        /// <summary>
        /// 注册一种类型
        /// </summary>
        /// <param name="implementationType"></param>
        /// <param name="life"></param>
        void RegisterType(Type implementationType, LifeStyle life = LifeStyle.Singleton);
        /// <summary>
        /// 注册一种映射关系
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="implementationType"></param>
        /// <param name="life"></param>
        void RegisterType(Type serviceType, Type implementationType, LifeStyle life = LifeStyle.Singleton);
  
        /// <summary>
        /// 注意一种映射关系
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementer"></typeparam>
        /// <param name="life"></param>
        void Register<TService, TImplementer>(LifeStyle life = LifeStyle.Singleton)
            where TService : class
            where TImplementer : class, TService;

        /// <summary>
        /// 注册一个已经存在的实例
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementer"></typeparam>
        /// <param name="instance"></param>
        void RegisterInstance<TService, TImplementer>(TImplementer instance)
            where TService : class
            where TImplementer : class, TService;

        /// <summary>
        /// 返回对应实例
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        TService Resolve<TService>() where TService : class;

        /// <summary>
        /// 返回对应实例
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        object Resolve(Type serviceType);
    }
}
