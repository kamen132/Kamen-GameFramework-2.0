namespace KamenFramework
{
    public class KamenGame : MonoSingleton<KamenGame>
    {
        protected override void OnInitialize()
        {
            base.OnInitialize();
            mMainSceneContainer=new SceneObjectContainer();
        }

        public IMessageService MessageService => ServiceManager.Instance.GetService<IMessageService>();
        public ICoroutineService CoroutineService => ServiceManager.Instance.GetService<ICoroutineService>();
        public IAudioService AudioService => ServiceManager.Instance.GetService<IAudioService>();
        public IUIService UIService => ServiceManager.Instance.GetService<IUIService>();
        public ISceneService SceneService => ServiceManager.Instance.GetService<ISceneService>();
        public IResourceService ResourceService => ServiceManager.Instance.GetService<IResourceService>();
        public SceneObjectContainer MainSceneContainer => mMainSceneContainer;
        private SceneObjectContainer mMainSceneContainer;
    }
}