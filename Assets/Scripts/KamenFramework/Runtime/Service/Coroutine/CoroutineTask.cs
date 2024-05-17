using System;
using System.Collections;
using KamenFramework.Runtime.Tool.Log;

namespace KamenFramework.Runtime.Service.Coroutine
{
        /// <summary>
    /// 协程任务
    /// </summary>
    public class CoroutineTask
    {
        /// <summary>
        /// 协程id
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// 是否正在运行
        /// </summary>
        public bool Running { get; private set; }
        /// <summary>
        /// 是否暂停
        /// </summary>
        public bool Paused { get; private set; }
        
        public CoroutineTask(int id)
        {
            Id = id;
            Running = true;
            Paused = false;
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            Running = false;
        }
        
        /// <summary>
        /// 暂停
        /// </summary>
        public void Pause()
        {
            Paused = true;
        }

        /// <summary>
        /// 恢复
        /// </summary>
        public void Resume()
        {
            Paused = false;
        }

        /// <summary>
        /// 开始协程
        /// </summary>
        /// <param name="co"></param>
        /// <returns></returns>
        public IEnumerator StartCoroutineTask(IEnumerator co)
        {
            IEnumerator coroutine = co;
            while (Running)
            {
                if (Paused)
                {
                    yield return null;
                }
                else
                {
                    if (coroutine != null)
                    {
                        try
                        {
                            coroutine.MoveNext();
                        }
                        catch (Exception e)
                        {
                            KLogger.LogError(e.Message + "\n" + e.StackTrace);
                        }

                        yield return coroutine.Current;
                    }
                    else
                        Running = false;
                }
            }

            KamenGame.Instance.CoroutineService.StopCoroutine(Id);
        }
    }
}