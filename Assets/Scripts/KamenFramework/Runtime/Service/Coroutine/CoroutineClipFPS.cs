using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace KamenFramework
{
    /// <summary>
    /// 分帧处理数据
    /// </summary>
    public class CoroutineClipFPS
    {
        private int mNumberOfDataToLoad = 7;
        private int mTotalDataCount;
        private Action<int> mAction;
        private string mName;
        private int mTaskId;
        private int mDelayTime;

        public void SetData(int numberToLoad, int totalCount, Action<int> action, string name = "",int delayTime=0)
        {
            mNumberOfDataToLoad = numberToLoad;
            mTotalDataCount = totalCount;
            mAction = action;
            mName = name;
            mDelayTime = delayTime;
        }

        public void RunTask()
        {
            ServiceManager.Instance.GetService<ICoroutineService>().StopCoroutine(mTaskId);
            mTaskId =  ServiceManager.Instance.GetService<ICoroutineService>().StartCoroutine(UpdateData());
        }

        private IEnumerator UpdateData()
        {
            yield return new WaitForSeconds(mDelayTime);
#if UNITY_EDITOR
            var stopwatch = new Stopwatch();
            stopwatch.Start();
#endif
            int iterations = mTotalDataCount / mNumberOfDataToLoad + 1; // 计算需要循环的次数
            for (int i = 0; i < iterations; i++)
            {
                for (int j = 0; j < mNumberOfDataToLoad; j++)
                {
                    int dataIndex = i * mNumberOfDataToLoad + j;
                    if (dataIndex >= mTotalDataCount) // 确保不超过总数据量
                    {
#if UNITY_EDITOR
                        stopwatch.Stop();
                        KLogger.Log($"{mName}刷新耗时 " + (float) stopwatch.Elapsed.TotalSeconds + " milliseconds");
#endif
                        yield break;
                    }

                    mAction?.Invoke(dataIndex);
                }

                yield return null;
            }

#if UNITY_EDITOR
            stopwatch.Stop();
            KLogger.Log($"{mName}刷新耗时 " + (float) stopwatch.Elapsed.TotalSeconds + " milliseconds");
#endif
        }
    }
}