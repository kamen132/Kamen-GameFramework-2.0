using KamenFramework.Runtime.Service.Base;

namespace KamenFramework.Runtime.Service.ServiceManager
{
    public interface IServiceManager
    {
        T GetService<T>() where T : IService;

        void RegisteredService<T>(IService service);

        void RemoveService(string serviceName);
    }
}