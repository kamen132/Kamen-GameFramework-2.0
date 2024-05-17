using System.Collections;
using System.Collections.Generic;
using KamenFramework.Runtime.Service.Base;
using KamenFramework.Runtime.Service.Message.Basic.Controller;
using KamenFramework.Runtime.Service.Message.Basic.Message;
using KamenFramework.Runtime.Service.Resource;
using KamenFramework.Runtime.Tool.Singleton;

namespace KamenFramework.Runtime.Service.ServiceManager
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

        protected void AddModule()
        {
            RegisteredService<IService>(new MessageService());
            RegisteredService<IControllerService>(new MessageControllerService());
            RegisteredService<IResourceService>(new ResourceService());
        }

        public IEnumerator InitService()
        {
            foreach (var item in mModuleList)
            {
                yield return item.Init();
            }
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