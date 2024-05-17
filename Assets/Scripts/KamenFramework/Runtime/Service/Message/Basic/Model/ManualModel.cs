using System;
using System.Collections.Generic;
using KamenFramework.Runtime.Service.Message.Basic.Message;
using KamenFramework.Runtime.Service.Message.Interface.Message;

namespace KamenFramework.Runtime.Service.Message.Basic.Model
{
    public abstract class ManualModel : IModel
    {
        private readonly List<IDisposable> mEntrustDisposables = new List<IDisposable>();

        ~ManualModel()
        {
            EntrustDisposablesClear();
        }

        protected void EntrustDisposable(IDisposable disposable)
        {
            mEntrustDisposables.Add(disposable);
        }

        protected IDisposable Register<T>(Action<T> callback) where T : MessageModel
        {
            IDisposable disposable = KamenGame.Instance.MessageService.Register(callback);
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
    }
}