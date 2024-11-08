using System;
using System.Collections.Generic;

namespace KamenFramework
{
    public abstract class ManualModel : MessageModel, IModel
    {
        private readonly List<IDisposable> mEntrustDisposables = new List<IDisposable>();
        protected IMessageService MessageService => ServiceManager.Instance.GetService<IMessageService>();
        
        protected void EntrustDisposable(IDisposable disposable)
        {
            mEntrustDisposables.Add(disposable);
        }

        protected IDisposable Register<T>(Action<T> callback) where T : MessageModel
        {
            IDisposable disposable = MessageService.Register(callback);
            EntrustDisposable(disposable);
            return disposable;
        }

        public void EntrustDisposablesClear()
        {
            foreach (IDisposable entrustDisposable in mEntrustDisposables)
            {
                entrustDisposable.Dispose();
            }
            mEntrustDisposables.Clear();
        }

        public void Dispose()
        {
            OnDispose();
            EntrustDisposablesClear();
        }

        protected virtual void OnDispose()
        {
            
        }

        public string Guid { get; set; }
    }
}