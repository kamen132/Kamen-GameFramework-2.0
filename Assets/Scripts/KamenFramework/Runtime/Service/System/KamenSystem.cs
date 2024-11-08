using System.Collections.Generic;
using KamenFramework;
using UnityEngine;

namespace KamenGameFramewrok
{
    /// <summary>
    /// 系统
    /// </summary>
    public class KamenSystem
    {
        /// <summary>
        /// 系统内所有控制器
        /// </summary>
        private readonly List<IController> mControllers = new List<IController>();

        /// <summary>
        /// 系统管理器
        /// </summary>
        public KamenSystemManager Manager { get; }

        /// <summary>
        /// 是否已被加载
        /// </summary>
        public bool Loaded { get; private set; }

        /// <summary>
        /// 是否暂停
        /// </summary>
        public bool Paused { get; private set; }
        /// <summary>
        /// 是否已经初始化，有OnInitialize返回值决定
        /// </summary>
        public bool IsInitialized { get; private set; }

        /// <summary>
        /// 系统名
        /// </summary>
        public string SystemName => GetType().Name;

        /// <summary>
        /// 初始化系统
        /// </summary>
        /// <param name="systemMgr"></param>
        protected KamenSystem(KamenSystemManager systemMgr)
        {
            Manager = systemMgr;
        }

        /// <summary>
        /// 绑定控制器
        /// </summary>
        /// <param name="controller"></param>
        public void BindController(IController controller)
        {
            mControllers.Add(controller);
        }

        /// <summary>
        /// 当配置加载完毕后调用
        /// </summary>
        public virtual void OnConfigLoaded()
        {

        }

        /// <summary>
        /// 当所有系统初始化完毕后调用
        /// </summary>
        public virtual void OnAllSystemsInitialized()
        {

        }

        /// <summary>
        /// 所有系统完成加载
        /// </summary>
        public virtual void OnAllSystemsLoaded()
        {

        }

        /// <summary>
        /// 加载系统
        /// </summary>
        internal void Load()
        {
            if (!Loaded)
            {
                Loaded = true;
                KLogger.Log($"[{SystemName}].OnLoaded Start",Color.green);
                OnLoaded();
                KLogger.Log($"[{SystemName}].OnLoaded End",Color.green);

                foreach (IController controller in mControllers)
                {
                    controller.Load();
                }
                KLogger.Log($"[{SystemName}].controller.Load End",Color.green);
            }
        }
        
        /// <summary>
        /// 卸载系统
        /// </summary>
        public void Unload()
        {
            if (Loaded)
            {
                foreach (IController controller in mControllers)
                {
                    controller.Unload();
                }
                mControllers.Clear();
                OnUnloaded();
                Loaded = false;
            }
        }

        /// <summary>
        /// 系统被加载后回调
        /// </summary>
        protected virtual void OnLoaded()
        {

        }

        /// <summary>
        /// 系统被卸载后回调
        /// </summary>
        protected virtual void OnUnloaded()
        {
        }

        public void Update()
        {
            if (Loaded && IsInitialized && !Paused)
            {
                OnUpdate();
            }
        }

        public void FixUpdate()
        {
            if (Loaded && IsInitialized && !Paused)
            {
                OnFixUpdate();
            }
        }

        protected virtual void OnUpdate()
        {

        }

        protected virtual void OnFixUpdate()
        {

        }
    }
}