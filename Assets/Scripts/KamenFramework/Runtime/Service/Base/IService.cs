using System.Collections;

namespace KamenFramework.Runtime.Service.Base
{
    public interface IService
    {
        IEnumerator Init();
        void BeforeInit();
        void AfterInit();
        void Update();
        void FixUpdate();
    }
}
