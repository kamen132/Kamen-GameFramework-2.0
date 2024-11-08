//using UniRx;

namespace KamenGameFramewrok
{
    /// <summary>
    /// 系统所属控制器
    /// </summary>
    /// <typeparam name="SYSTEM"></typeparam>
    public class KamenController<SYSTEM> : IController where SYSTEM  : KamenSystem 
    {
        /// <summary>
        /// 所属系统
        /// </summary>
        public SYSTEM ParentSystem { get; private set; }
        
        /// <summary>
        /// 控制器内监听
        /// </summary>
        //public CompositeDisposable Disposables { get; private set; } = new CompositeDisposable();
        
        public KamenController(SYSTEM system)
        {
            this.ParentSystem = system;
            system.BindController(this);
        }
        
        public void Load()
        {
            OnLoaded();
        }

        public void Unload()
        {
            OnUnloaded();
            //Disposables.Clear();
        }

        protected virtual void OnLoaded()
        {

        }

        protected virtual void OnUnloaded()
        {
            
        }
    }
}