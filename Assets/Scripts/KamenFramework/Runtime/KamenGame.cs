using KamenFramework.Runtime.Service.Coroutine;
using KamenFramework.Runtime.Service.Message.Interface.Message;
using KamenFramework.Runtime.Service.ServiceManager;
using KamenFramework.Runtime.Tool.Singleton;

namespace KamenFramework.Runtime
{
    public class KamenGame : MonoSingleton<KamenGame>
    {
        public IMessageService MessageService => ServiceManager.Instance.GetService<IMessageService>();

        public ICoroutineService CoroutineService => ServiceManager.Instance.GetService<ICoroutineService>();
    }
}