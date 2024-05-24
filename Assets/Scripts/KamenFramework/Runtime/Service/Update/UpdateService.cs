using System;
using System.Collections.Generic;
using UnityEngine;

namespace KamenFramework.Service.Update
{
    public interface IUpdateService :IService
    {
        void AddTick(string name, ITickHandler tick);
        void RemoveTick(string name);
    }
    public class UpdateService : ServiceBase ,IUpdateService
    {
        private readonly Dictionary<string, ITickHandler> mTickMap = new Dictionary<string, ITickHandler>();
        private double mPauseTime;
        private DateTime mLastTime;
        public GameStatus Status { get; set; }

        public void AddTick(string name,ITickHandler tick)
        {
            if (mTickMap.TryGetValue(name,out var old))
            {
                KLogger.LogError($"tick map has same key add fail name:{name}");
                return;
            }
            mTickMap.Add(name, tick);
        }

        public void RemoveTick(string name)
        {
            mTickMap.Remove(name);
        }
        public override void Update()
        {
            DateTime nowTime = DateTime.Now;
            if (Status == GameStatus.Pause)
            {
                mLastTime = nowTime;
                return;
            }

            TimeSpan difTime = nowTime - mLastTime;
            if (!(difTime.TotalMilliseconds < 100.0))
            {
                mLastTime = nowTime;
                foreach (var tick in mTickMap)
                {
                    float passedTime = (float) ((difTime.TotalMilliseconds - mPauseTime) / 1000.0) * Time.timeScale;
                    mPauseTime = 0.0;
                    tick.Value.Handle(passedTime);
                }
            }
        }
    }

    public enum GameStatus
    {
        Normal,
        Pause,
    }
}