using System.Collections;
using System.Collections.Generic;
using KamenFramework.Runtime.Service.Base;

namespace KamenFramework.Runtime.Service.Coroutine
{
    public class CoroutineService: ServiceBase ,ICoroutineService
    {
        /// <summary>
        /// task标识
        /// </summary>
        private int mCoroutineTaskId;

        /// <summary>
        /// 所有正在运行的协程
        /// </summary>
        private Dictionary<int, CoroutineTask> mCoroutines = new Dictionary<int, CoroutineTask>();

        /// <summary>
        /// 开始一个协程
        /// </summary>
        /// <param name="routine"></param>
        /// <returns></returns>
        public int StartCoroutine(IEnumerator routine)
        {
            CoroutineTask task = new CoroutineTask(mCoroutineTaskId++);
            mCoroutines.Add(task.Id, task);
            GameRoot.StartCoroutine(task.StartCoroutineTask(routine));
            return task.Id;
        }
        
        /// <summary>
        /// 结束一个协程
        /// </summary>
        /// <param name="id"></param>
        public void StopCoroutine(int id)
        {
            if (mCoroutines.TryGetValue(id,out var task))
            {
                task.Stop();
                mCoroutines.Remove(id);
            }
        }

        /// <summary>
        /// 暂停一个协程
        /// </summary>
        /// <param name="id"></param>
        public void PauseCoroutine(int id)
        {
            if (mCoroutines.TryGetValue(id,out var task))
            {
                task.Pause();
            }
        }

        /// <summary>
        /// 恢复一个协程
        /// </summary>
        /// <param name="id"></param>
        public void ReusedCoroutine(int id)
        {
            if (mCoroutines.TryGetValue(id,out var task))
            {
                task.Resume();
            }
        }

        public override void Shut()
        {
            base.Shut();
            mCoroutines.Clear();
            mCoroutineTaskId = 0;
            GameRoot.StopAllCoroutines();
        }
    }
}