
namespace KamenFramework
{
    public abstract class MessageController<TMsg> : IMessageController<TMsg> where TMsg : MessageModel
    {
        protected IMessageService MessageService => KamenGame.Instance.MessageService;

        public abstract void Handle(TMsg msg);
    }
}