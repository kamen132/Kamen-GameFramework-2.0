using UnityEngine;

namespace KamenFramework
{
    public interface IObjectPoolService : IService
    {
        GameObject Get(string path, Transform parent = null, bool resetPos = true);
        T Get<T>(string path) where T : Object;
        void Push(GameObject obj);
        void Push(string path, Object obj);
    }
}