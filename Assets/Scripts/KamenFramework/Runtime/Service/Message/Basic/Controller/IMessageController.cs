namespace KamenFramework
{
    public interface IMessageController<in T> where T : MessageModel
    {
        void Handle(T msg);
    }
}