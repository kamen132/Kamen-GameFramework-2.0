using System;
using System.Collections;

namespace KamenFramework
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
