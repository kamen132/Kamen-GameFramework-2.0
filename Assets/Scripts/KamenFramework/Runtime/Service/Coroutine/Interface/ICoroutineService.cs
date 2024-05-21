using System.Collections;

namespace KamenFramework
{
    public interface ICoroutineService: IService
    {
        void StopCoroutine(int taskId);

        int StartCoroutine(IEnumerator routine);

        void PauseCoroutine(int id);

        void ReusedCoroutine(int id);
    }
}