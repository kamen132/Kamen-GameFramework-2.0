
namespace KamenFramework
{
    public class KamenGame : MonoSingleton<KamenGame>
    {
        /// <summary>
        /// 消息事件服务
        /// </summary>
        public IMessageService MessageService => ServiceManager.Instance.GetService<IMessageService>();

        /// <summary>
        /// 协程服务
        /// </summary>
        public ICoroutineService CoroutineService => ServiceManager.Instance.GetService<ICoroutineService>();

        /// <summary>
        /// 声音服务
        /// </summary>
        public IAudioService AudioService => ServiceManager.Instance.GetService<IAudioService>();

        /// <summary>
        /// 声音服务
        /// </summary>
        public IUIService UIService => ServiceManager.Instance.GetService<IUIService>();

        /// <summary>
        /// 场景管理
        /// </summary>
        public ISceneService SceneService => ServiceManager.Instance.GetService<ISceneService>();

        /// <summary>
        /// 加载服务
        /// </summary>
        public IResourceService ResourceService => ServiceManager.Instance.GetService<IResourceService>();

        /// <summary>
        /// 对象池
        /// </summary>
        public IObjectPoolService ObjectPoolService => ServiceManager.Instance.GetService<IObjectPoolService>();

        public IUpdateService UpdateService => ServiceManager.Instance.GetService<IUpdateService>();
        
        public SceneObjectContainer MainSceneContainer => mMainSceneContainer;
        private SceneObjectContainer mMainSceneContainer;

        protected override void OnInitialize()
        {
            base.OnInitialize();
            mMainSceneContainer = new SceneObjectContainer();
        }
    }
}