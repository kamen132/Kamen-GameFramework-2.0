using System.Collections;
using KamenFramework.Runtime.Service.Base;

namespace KamenFramework.Runtime.Service.Coroutine
{
    public interface ICoroutineService: IService
    {
        void StopCoroutine(int taskId);

        int StartCoroutine(IEnumerator routine);

        void PauseCoroutine(int id);

        void ReusedCoroutine(int id);
    }
}