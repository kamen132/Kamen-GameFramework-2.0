using System;
using System.Collections.Generic;

namespace KamenFramework
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Entity : IEntity
    {
        /// <summary>
        /// 实体状态
        /// </summary>
        private enum EntityState
        {
            Alive,
            Disposed,
        }

        /// <summary>
        /// 实体状态
        /// </summary>
        private EntityState State = EntityState.Alive;

        /// <summary>
        /// 实体所在场景
        /// </summary>
        public SceneObjectContainer SceneContainer { get; private set; }

        public IModel Model { get; protected set; }

        /// <summary>
        /// 实体类型
        /// </summary>
        public EntityType Type { get; protected set; }

        /// <summary>
        /// 实体标识
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        /// 是否存在
        /// </summary>
        public bool IsAlive => State == EntityState.Alive;


        /// <summary>
        /// 实体上所有组件
        /// </summary>
        private Dictionary<string, IComponent> Components;

        /// <summary>
        /// 设置实体所在场景
        /// </summary>
        /// <param name="SceneContainer"></param>
        public void SetSceneContainer(SceneObjectContainer sceneContainer)
        {
            this.SceneContainer = sceneContainer;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        public Entity()
        {
            Components = new Dictionary<string, IComponent>();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index"></param>
        public void Init(int index,IModel model=null)
        {
            Index = index;
            Model = model;
            OnInit();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void OnInit()
        {

        }

        public virtual void Dispose()
        {
            State = EntityState.Disposed;
            ClearComponent();
        }

        /// <summary>
        /// 获取实体上组件
        /// </summary>
        /// <typeparam name="TKComponent"></typeparam>
        /// <returns></returns>
        public TKComponent GetComponent<TKComponent>() where TKComponent : class, IComponent
        {
            Type typeOfComponent = typeof(TKComponent);
            Components.TryGetValue(typeOfComponent.ToString(), out IComponent component);
            return component as TKComponent;
        }

        /// <summary>
        /// 获取实体上组件
        /// </summary>
        /// <typeparam name="TKComponent"></typeparam>
        /// <returns></returns>
        public TKComponent AddComponent<TKComponent>() where TKComponent : class, IComponent
        {
            Type typeOfComponent = typeof(TKComponent);
            var component = (IComponent) Activator.CreateInstance(typeOfComponent, this);
            if (!Components.TryGetValue(typeOfComponent.ToString(), out IComponent aa))
            {
                Components.Add(component.ToString(), component);
            }

            return component as TKComponent;
        }

        /// <summary>
        /// 移除实体上组件
        /// </summary>
        /// <typeparam name="TKComponent"></typeparam>
        public void RemoveComponent<TKComponent>() where TKComponent : class, IComponent
        {
            Type typeOfComponent = typeof(TKComponent);
            var component = (IComponent) Activator.CreateInstance(typeOfComponent, this);
            Components.Remove(component.ToString());
        }

        /// <summary>
        /// 清除实体上所有组件
        /// </summary>
        public void ClearComponent()
        {
            foreach (var component in Components)
            {
                Components.Remove(component.Key);
            }
        }

        public virtual void Update()
        {

        }

        public virtual void FixedUpdate()
        {

        }

        public override string ToString()
        {
            return $"单位[{this.Index}]";
        }
    }
}