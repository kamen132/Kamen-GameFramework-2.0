using System;
using System.Collections.Generic;
using UnityEngine;

namespace KamenFramework
{
    public interface IUpdateService :IService
    {
        void AddTick(string name, ITickHandler tick);
        void RemoveTick(string name);
    }
    public class UpdateService : ServiceBase ,IUpdateService
    {
        private readonly Dictionary<string, ITickHandler> mTickMap = new Dictionary<string, ITickHandler>();
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
            if (Status == GameStatus.Pause)
            {
                return;
            }

            foreach (var tick in mTickMap)
            {
                tick.Value.Handle(Time.deltaTime);
            }
        }
    }

    public enum GameStatus
    {
        Normal,
        Pause,
    }
}