using System.Collections;
using System.Collections.Generic;

namespace KamenFramework
{
    public class ServiceManager : Singleton<ServiceManager>, IServiceManager
    {
        /// <summary>
        /// 模块字典-用于查找
        /// </summary>
        private readonly Dictionary<string, IService> mModuleMap = new Dictionary<string, IService>();

        /// <summary>
        /// 模块字典-用于遍历
        /// </summary>
        private readonly List<IService> mModuleList = new List<IService>();

        public void AddService()
        {
            RegisteredService<IMessageService>(new MessageService());
            RegisteredService<IControllerService>(new MessageControllerService());
            RegisteredService<IResourceService>(new ResourceService());
            RegisteredService<ISceneService>(new SceneService());
            RegisteredService<IAudioService>(new AudioService());
            RegisteredService<ICoroutineService>(new CoroutineService());
            RegisteredService<IObjectPoolService>(new ObjectPoolService());
            KLogger.Log("--service add success--", GameHelper.ColorGreen);
        }

        public IEnumerator InitService()
        {
            foreach (var item in mModuleList)
            {
                yield return item.Init();
            }

            KLogger.Log("--service init success--", GameHelper.ColorGreen);
        }
        public void Update()
        {
            foreach (var service in mModuleList)
            {
                service.Update();
            }
        }

        public void FixUpdate()
        {
            foreach (var service in mModuleList)
            {
                service.FixUpdate();
            }
        }

        public void Shut()
        {
            foreach (var service in mModuleList)
            {
                service.Shut();
            }
        }

        public T GetService<T>() where T : IService
        {
            IService module = FindModule(typeof(T).ToString());
            return (T) module;
        }

        public IService FindModule(string moduleName)
        {
            if (mModuleMap.TryGetValue(moduleName, out var module))
            {
                return module;
            }

            return null;
        }

        public void RegisteredService<T>(IService service)
        {
            string modelName = typeof(T).ToString();
            if (!mModuleMap.TryGetValue(modelName, out var moduleOld))
            {
                mModuleList.Add(service);
                mModuleMap.Add(modelName, service);
            }
        }

        public void RemoveService(string serviceName)
        {
            if (mModuleMap.TryGetValue(serviceName, out var moduleOld))
            {
                mModuleMap.Remove(serviceName);
            }
        }
    }
}