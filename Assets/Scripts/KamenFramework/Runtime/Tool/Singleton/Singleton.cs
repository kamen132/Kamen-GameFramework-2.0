namespace KamenFramework
{
    public class Singleton<T> : SingletonBase where T : class, new()
    {
        private static T mInstance = null;
        private static readonly object mLock = new object();

        public static T Instance
        {
            get
            {
                if (null == mInstance)
                {
                    lock (mLock)
                    {
                        mInstance = new T(); //调用构造函数;
                        if (mInstance is SingletonInterface singleton)
                        {
                            singleton.SingletonInterfaceOnInitialize();
                        }
                    }
                }

                return mInstance;
            }
        }

        /// 构造函数;
        protected Singleton()
        {
            if (null == mInstance)
            {
                //todo 
                //LogHelper.PrintGreen($"[Singleton]{typeof(T)} singleton instance created.");
            }
        }
    }
}