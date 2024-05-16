using KamenFramework.Runtime.Service.Base;

namespace KamenFramework.Runtime.Service.ServiceManager
{
    public interface IServiceManager
    {
        T GetService<T>() where T : IService;

        void Registered(IService service);

        void AddService(string serviceName, IService service);

        void RemoveService(string serviceName);
    }
}