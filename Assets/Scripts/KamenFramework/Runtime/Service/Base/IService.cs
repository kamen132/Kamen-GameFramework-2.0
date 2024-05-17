using System;
using System.Collections;

namespace KamenFramework.Runtime.Service.Base
{
    public interface IService : IDisposable
    {
        IEnumerator Init();
        void BeforeInit();
        void AfterInit();
        void Update();
        void FixUpdate();
        void Shut();
    }
}
