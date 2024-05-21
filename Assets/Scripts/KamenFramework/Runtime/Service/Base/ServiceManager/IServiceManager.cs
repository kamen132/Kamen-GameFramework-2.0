namespace KamenFramework
{
    public interface IServiceManager
    {
        T GetService<T>() where T : IService;

        void RegisteredService<T>(IService service);

        void RemoveService(string serviceName);
    }
}