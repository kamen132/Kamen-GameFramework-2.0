using System.Collections.Generic;
using KamenFramework.Runtime.Service.Base;
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

        public void AddModule()
        {
            
        }
        
        
        public T GetService<T>() where T : IService
        {
            throw new System.NotImplementedException();
        }

        public void Registered(IService service)
        {
            throw new System.NotImplementedException();
        }

        public void AddService(string serviceName, IService service)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveService(string serviceName)
        {
            throw new System.NotImplementedException();
        }
    }
}