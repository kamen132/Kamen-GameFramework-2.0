using System;
using System.Collections.Generic;
using System.Linq;
using KamenFramework.Runtime.Service.Base;
using KamenFramework.Runtime.Service.Message.Interface.Message;

namespace KamenFramework.Runtime.Service.Message.Basic.Message
{
    public class MessageService : ServiceBase ,IMessageService
    {
        private readonly Dictionary<string, List<IMessageHandle>> mDynamicCallbacks = new Dictionary<string, List<IMessageHandle>>();

        private readonly Dictionary<string, List<Action<object>>> mCallbacks = new Dictionary<string, List<Action<object>>>();

        public IDisposable Register<T>(Action<T> callback) where T : MessageModel
        {
            Type type = typeof(T);
            if (!mDynamicCallbacks.TryGetValue(type.FullName, out var dynamicCallbacks))
            {
                dynamicCallbacks = new List<IMessageHandle>();
                mDynamicCallbacks.Add(type.FullName, dynamicCallbacks);
            }

            MessageHandle<T> handle = new MessageHandle<T>(callback, OnDispose);
            dynamicCallbacks.Add(handle);
            return handle;
        }

        private void OnDispose(IMessageHandle handle)
        {
            Type key = handle.MessageType;
            if (mDynamicCallbacks.TryGetValue(key.FullName, out var callbacks))
            {
                callbacks.Remove(handle);
            }
        }

        public void Register(Type type, Action<object> callback)
        {
            if (!mCallbacks.TryGetValue(type.FullName, out var callbacks))
            {
                callbacks = new List<Action<object>>();
                mCallbacks.Add(type.FullName, callbacks);
            }

            callbacks.Add(callback);
        }

        public void Dispatch<T>(T msg) where T : MessageModel
        {
            HandleCallBacks(msg);
            HandleDynamicCallBacks(msg);
        }

        private void HandleDynamicCallBacks<T>(T msg) where T : MessageModel
        {
            string key = typeof(T).FullName;
            if (!mDynamicCallbacks.TryGetValue(key, out var callbacks))
            {
                return;
            }

            List<IMessageHandle> temp = callbacks.ToList();
            foreach (IMessageHandle item in temp)
            {
                item?.Handle(msg);
            }
        }

        private void HandleCallBacks<T>(T msg) where T : MessageModel
        {
            string key = typeof(T).FullName;
            if (!mCallbacks.TryGetValue(key, out var callbacks))
            {
                return;
            }

            List<Action<object>> temp = callbacks.ToList();
            foreach (Action<object> item in temp)
            {
                item?.Invoke(msg);
            }
        }
    }
}