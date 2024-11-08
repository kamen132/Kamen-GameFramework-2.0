
namespace KamenFramework
{
    public abstract class MessageController<TMsg> : IMessageController<TMsg> where TMsg : MessageModel
    {
        protected IMessageService MessageService =>  ServiceManager.Instance.GetService<IMessageService>();

        public abstract void Handle(TMsg msg);
    }
}