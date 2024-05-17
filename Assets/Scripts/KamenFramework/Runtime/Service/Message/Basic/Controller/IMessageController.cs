using KamenFramework.Runtime.Service.Message.Basic.Message;

namespace KamenFramework.Runtime.Service.Message.Basic.Controller
{
    public interface IMessageController<in T> where T : MessageModel
    {
        void Handle(T msg);
    }
}