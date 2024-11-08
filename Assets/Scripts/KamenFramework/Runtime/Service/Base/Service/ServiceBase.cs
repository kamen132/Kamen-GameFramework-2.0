using System.Collections;

namespace KamenFramework
{
    public abstract class ServiceBase : IService
    {
        public IEnumerator Init()
        {
            BeforeInit();
            yield return OnInit();
            AfterInit();
        }

        protected virtual IEnumerator OnInit()
        {
            KLogger.Log($"{GetType().FullName} OnInit");
            yield break;
        }

        public virtual void BeforeInit()
        {
            
        }

        public virtual void AfterInit()
        {
            
        }

        public virtual void Update()
        {

        }

        public virtual void FixUpdate()
        {

        }

        public virtual void Shut()
        {
            
        }
        public void Dispose()
        {
            
        }
    }
}