using System.Collections;

namespace KamenFramework.Runtime.Service.Base
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
    }
}