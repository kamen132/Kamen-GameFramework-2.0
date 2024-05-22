using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace KamenFramework
{
    public class ObjectPoolService : ServiceBase, IObjectPoolService
    {
        private readonly Dictionary<string, Queue<Object>> Pool = new Dictionary<string, Queue<Object>>();
        private Transform mPoolRoot;
        protected override IEnumerator OnInit()
        {
            mPoolRoot = new GameObject("PoolRoot").transform;
            mPoolRoot.SetParent(GameHelper.GetRoot().transform);
            return base.OnInit();
        }

        public GameObject Get(string path, Transform parent = null, bool resetPos = true)
        {
            GameObject prefab = Get<GameObject>(path);
            if (resetPos)
            {
                prefab.transform.localPosition = Vector3.zero;
            }

            prefab.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
            return prefab;
        }

        public T Get<T>(string path) where T : Object
        {
            T obj = default;
            if (!Pool.TryGetValue(path, out var source) || !source.Any())
            {
                obj = KamenGame.Instance.ResourceService.LoadAsset<T>(path);
            }
            else
            {
                obj = source.Dequeue() as T;
            }

            return obj;
        }
        
        public void Push(GameObject obj, bool isMove)
        {
            if (obj == null)
            {
                KLogger.LogError($"obj is null cant add to pool");
                return;
            }

            var mono = obj.GetComponent<ResourceHandler>();
            if (mono == null)
            {
                KLogger.LogError("obj is dont have ResourceHandler cant add to pool");
                return;
            }

            var path = mono.AssetPath;
            if (isMove)
            {
                obj.transform.SetParent(mPoolRoot);
            }
            else
            {
                obj.transform.position = new Vector3(9999, 9999);
            }

            Push(path, obj);
        }

        public void Push(string path, Object obj)
        {
            if (obj == null)
            {
                KLogger.LogError($"obj is null cant add to pool path:{path}");
                return;
            }

            if (!Pool.TryGetValue(path, out var queue))
            {
                queue = new Queue<Object>();
                Pool.Add(path, queue);
            }
            
            queue.Enqueue(obj);
        }
    }
}