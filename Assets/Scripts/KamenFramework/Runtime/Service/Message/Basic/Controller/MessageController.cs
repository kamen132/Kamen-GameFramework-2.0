using KamenFramework.Runtime.Service.Message.Basic.Message;
using KamenFramework.Runtime.Service.Message.Interface.Message;

namespace KamenFramework.Runtime.Service.Message.Basic.Controller
{
    public abstract class MessageController<TMsg> : IMessageController<TMsg> where TMsg : MessageModel
    {
        protected IMessageService MessageService => KamenGame.Instance.MessageService;

        public abstract void Handle(TMsg msg);
    }
}