namespace KamenFramework.Runtime.Tool.Singleton
{
    public delegate void OnSingletonInitializeEventHandler();
    public delegate void OnSingletonUnInitializeEventHandler();

    public class SingletonInterface
    {
        public virtual void Launch() { }
        public virtual void SingletonInterfaceOnInitialize() { }
        public virtual void SingletonInterfaceOnUnInitialize() { }
    }

    public class SingletonBase : SingletonInterface
    {
        public event OnSingletonInitializeEventHandler OnInitializeHandler;
        public event OnSingletonUnInitializeEventHandler OnUnInitializeHandler;

        private bool mIsInit = false;
        private bool mIsUninit = false;

        public sealed override void Launch() { }

        public sealed override void SingletonInterfaceOnInitialize()
        {
            if (!mIsInit)
            {
                mIsInit = true;
                OnInitialize();
                OnInitializeHandler?.Invoke();
            }
        }

        public sealed override void SingletonInterfaceOnUnInitialize()
        {
            if (!mIsUninit)
            {
                mIsUninit = true;
                OnUnInitializeHandler?.Invoke();
                OnUninitialize();
            }
        }

        protected virtual void OnInitialize() { }
        protected virtual void OnUninitialize() { }
    }
}