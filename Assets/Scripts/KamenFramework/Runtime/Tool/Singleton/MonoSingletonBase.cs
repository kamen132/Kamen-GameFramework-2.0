/*
* @Author: Kamen
* @Description:
* @Date: 2023年10月25日 星期三 15:10:07
* @Modify:
*/

using UnityEngine;

namespace KamenFramework.Runtime.Tool.Singleton
{
    public class MonoSingletonInterface : MonoBehaviour
    {
        public virtual void Launch() { }
        public virtual void MonoSingletonInterfaceOnInitialize() { }
        public virtual void MonoSingletonInterfaceOnUnInitialize() { }
    }

    public class MonoSingletonBase : MonoSingletonInterface
    {
        public event OnSingletonInitializeEventHandler OnInitializeHandler;
        public event OnSingletonUnInitializeEventHandler OnUnInitializeHandler;

        private bool mIsInit = false;
        private bool mIsUnInit = false;

        public sealed override void Launch() { }

        public sealed override void MonoSingletonInterfaceOnInitialize()
        {
            if (!mIsInit)
            {
                mIsInit = true;
                OnInitialize();
                OnInitializeHandler?.Invoke();
            }
        }

        public sealed override void MonoSingletonInterfaceOnUnInitialize()
        {
            if (!mIsUnInit)
            {
                mIsUnInit = true;
                OnUnInitializeHandler?.Invoke();
                OnUnInitialize();
            }
        }

        protected virtual void OnInitialize() { }
        protected virtual void OnUnInitialize() { }
    }
}