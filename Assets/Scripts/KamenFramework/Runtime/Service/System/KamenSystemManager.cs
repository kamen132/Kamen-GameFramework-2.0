using System;
using System.Collections;
using System.Collections.Generic;
using KamenFramework;

namespace KamenGameFramewrok
{
    public partial class KamenSystemManager : ServiceBase, ISystemManager
    {
        /// <summary>
        /// 所有已被加载的系统
        /// </summary>
        private readonly Dictionary<Type, KamenSystem> LoadedSystem = new Dictionary<Type, KamenSystem>();
        private readonly List<Type> OrderedSystemTypes = new List<Type>(64);
        private readonly Dictionary<Type, int> SystemOrders = new Dictionary<Type, int>(64);
        private readonly Dictionary<Type, SystemInstanceHandler> SystemInstanceHandlers = new Dictionary<Type, SystemInstanceHandler>(64);
        private int Mark = 0;
        
        /// <summary>
        /// 本地配置加载后回调
        /// </summary>
        private readonly Action<KamenSystem> ConfigLoadedAction = system => system.OnConfigLoaded();

        /// <summary>
        /// 全部系统加载后回调
        /// </summary>
        private readonly Action<KamenSystem> AllSystemsLoadedAction = system => system.OnAllSystemsLoaded();

        /// <summary>
        /// 全部系统初始化完成回调
        /// </summary>
        private readonly Action<KamenSystem> AllSystemsInitializedAction = system => system.OnAllSystemsInitialized();
        

        protected override IEnumerator OnInit()
        {
            RegisterSystems();
            SortedSystemTypes();
            int systemCount = GetSupportedSystemCount();
            for (int i = 0; i < systemCount; i++)
            {
                InstallSystemByIndex(i);
            }
            for (int i = 0; i < systemCount; i++)
            {
                PreloadSystemByIndex(i);
            }
            return base.OnInit();
            
        }

        /// <summary>
        /// 获取系统
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetSystem<T>() where T : KamenSystem
        {
            Type type = typeof(T);
            LoadedSystem.TryGetValue(type, out KamenSystem sys);
            return sys as T;
        }

        /// <summary>
        /// 注册系统
        /// </summary>
        /// <param name="order"></param>
        /// <param name="installer"></param>
        /// <typeparam name="T"></typeparam>
        private void RegisterSystem<T>(int order, Action installer)
        {
            Type type = typeof(T);
            SystemInstanceHandlers[type] = new SystemInstanceHandler(installer);
            SystemOrders[type] = order * 100 + Mark++;
        }

        private void RegisterSystems()
        {
            //在这注册系统
            //OnRegisterSystem();
        }

        /// <summary>
        /// 系统加载优先级排序
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        private int SystemTypeSorter(Type left, Type right)
        {
            int orderLeft = SystemOrders[left];
            int orderRight = SystemOrders[right];
            return orderLeft - orderRight;
        }

        private void SortedSystemTypes()
        {
            OrderedSystemTypes.Clear();
            foreach (var iter in SystemOrders)
            {
                OrderedSystemTypes.Add(iter.Key);
            }

            OrderedSystemTypes.Sort(SystemTypeSorter);

        }

        public void InstallSystemByIndex(int index)
        {
            if (index >= 0 && index < OrderedSystemTypes.Count)
            {
                Type type = OrderedSystemTypes[index];
                if (LoadedSystem.ContainsKey(type))
                {
                    return;
                }
                var handlers = SystemInstanceHandlers[type];
                handlers.Installer.Invoke();
            }
        }

        public void PreloadSystemByIndex(int index)
        {
            if (index >= 0 && index < OrderedSystemTypes.Count)
            {
                Type type = OrderedSystemTypes[index];

                GetSystemByType(type).Load();

                if (index == OrderedSystemTypes.Count - 1)
                {
                    TriggerEvent(EnumSystemEventType.AllSystemsLoaded);
                }
            }
        }

        public int GetSupportedSystemCount()
        {
            return OrderedSystemTypes.Count;
        }

        private TSystem Acquire<TSystem>() where TSystem : KamenSystem
        {
            Type systemOfType = typeof(TSystem);

            KamenSystem ret = GetSystemByType(systemOfType);
            if (null == ret)
            {
                TSystem newInstance = (TSystem) Activator.CreateInstance(systemOfType, this);

                LoadedSystem.Add(systemOfType, newInstance);
                return newInstance;
            }
            else
            {
                return ret as TSystem;
            }
        }

        public void TriggerEvent(EnumSystemEventType eventType)
        {
            Action<KamenSystem> foreachSystemAction = null;
            switch (eventType)
            {
            case EnumSystemEventType.ConfigLoaded:
                foreachSystemAction = ConfigLoadedAction;
                break;
            case EnumSystemEventType.AllSystemsLoaded:
                foreachSystemAction = AllSystemsLoadedAction;
                break;
            case EnumSystemEventType.AllSystemsInitialized:
                foreachSystemAction = AllSystemsInitializedAction;
                break;
            }

            if (null != foreachSystemAction)
            {
                ForeachSystemRoutine(foreachSystemAction);
            }
        }

        internal void ForeachSystemRoutine(Action<KamenSystem> updateHandler)
        {
            foreach (var iter in LoadedSystem)
            {
                updateHandler?.Invoke(iter.Value);
            }
        }

        private KamenSystem GetSystemByType(Type systemOfType)
        {
            KamenSystem ret = null;

            if (LoadedSystem.TryGetValue(systemOfType, out ret))
            {
                return ret;
            }

            return null;
        }

        /// <summary>
        /// 卸载所有正在运行的系统
        /// </summary>
        public void UnloadAll()
        {
            List<Type> unloadTypes = new List<Type>(64);
            foreach (var iter in this.LoadedSystem.Values)
            {
                iter.Unload();
                unloadTypes.Add(iter.GetType());
            }

            foreach (Type unloadType in unloadTypes)
            {
                LoadedSystem.Remove(unloadType);
            }
        }

        public override void Update()
        {
            foreach (var sys in LoadedSystem)
            {
                sys.Value.Update();
            }
        }

        public override void FixUpdate()
        {
            foreach (var sys in LoadedSystem)
            {
                sys.Value.FixUpdate();
            }
        }
    }

    public class SystemInstanceHandler
    {
        public Action Installer { get; private set; }
        public SystemInstanceHandler(Action installer)
        {
            Installer = installer;
        }
    }

    public enum EnumSystemEventType
    {
        ConfigLoaded,
        AllSystemsLoaded,
        AllSystemsInitialized,
    }
}