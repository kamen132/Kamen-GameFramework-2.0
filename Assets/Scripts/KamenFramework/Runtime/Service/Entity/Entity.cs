using System;
using System.Collections.Generic;
using System.Linq;

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

        public virtual IModel Model { get; protected set; }

        /// <summary>
        /// 实体类型
        /// </summary>
        public virtual EntityType EntityType { get; protected set; }

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
        private Dictionary<string, IComponent> Components = new Dictionary<string, IComponent>();

        /// <summary>
        /// 设置实体所在场景
        /// </summary>
        /// <param name="SceneContainer"></param>
        public void SetSceneContainer(SceneObjectContainer sceneContainer)
        {
            this.SceneContainer = sceneContainer;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="index"></param>
        public void Init(int index,IModel model=null)
        {
            Index = index;
            Model = model;
            if (LoadPrefab())
            {
                OnInit();
            }
        }

        protected virtual bool LoadPrefab()
        {
            return true;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void OnInit()
        {

        }

        public void Dispose()
        {
            State = EntityState.Disposed;
            OnDispose();
            ClearComponent();
            Model?.Dispose();
        }

        protected virtual void OnDispose()
        {
            
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

        public TKComponent TryGetOrAddComponent<TKComponent>() where TKComponent : class, IComponent
        {
            var component = GetComponent<TKComponent>();
            if (component == null)
            {
                return AddComponent<TKComponent>();
            }

            return component;
        }

        /// <summary>
        /// 获取实体上组件
        /// </summary>
        /// <typeparam name="TKComponent"></typeparam>
        /// <returns></returns>
        public TKComponent AddComponent<TKComponent>() where TKComponent : class, IComponent
        {
            Type typeOfComponent = typeof(TKComponent);
            var component = (KComponent) Activator.CreateInstance(typeOfComponent, this);
            if (!Components.TryGetValue(typeOfComponent.ToString(), out IComponent saveComponent))
            {
                Components.Add(component.ToString(), component);
                return component as TKComponent;
            }

            return saveComponent as TKComponent;
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
            for (int i = 0; i < Components?.Count; i++)
            {
                var comp = Components.ElementAt(i);
                comp.Value.Dispose();
            }
            Components?.Clear();
            Components = null;
        }

        public virtual void Update()
        {
            if (!IsAlive)
            {
                return;
            }
            
            foreach (var component in Components)
            {
                component.Value.Update();
            }
        }

        public virtual void FixedUpdate()
        {
            foreach (var component in Components)
            {
                component.Value.FixedUpdate();
            }
        }

        public override string ToString()
        {
            return $"单位[{this.Index}]";
        }
    }
}