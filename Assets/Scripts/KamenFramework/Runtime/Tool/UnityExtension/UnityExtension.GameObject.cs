using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KamenFramework.Runtime.Tool.UnityExtension
{
    public static partial class UnityExtension
    {
        public static T GetOrAddComponent<T>(this GameObject o) where T : Component
        {
            T component = o.GetComponent<T>();
            if (component != null)
            {
                return component;
            }

            return o.AddComponent<T>();
        }

        public static Component GetOrAddComponent(this GameObject o, Type type)
        {
            Component component = o.GetComponent(type);
            if (component != null)
            {
                return component;
            }

            return o.AddComponent(type);
        }

        public delegate bool Searcher<in T>(T obj, out bool goContinue);

        /// <summary>
        /// 搜索子节点
        /// </summary>
        public static List<GameObject> SearchChildren(this GameObject obj, Searcher<GameObject> search)
        {
            if (obj == null || search == null)
            {
                return null;
            }

            List<GameObject> result = new List<GameObject>();

            Predicate<GameObject> queryer = (go) =>
            {
                bool goContinue;
                if (search(go, out goContinue))
                {
                    result.Add(go);
                }

                return goContinue;
            };

            obj.AdvancedChildrenForeach(queryer);

            return result;
        }

        /// <summary>
        /// 子节点遍历
        /// </summary>
        public static void AdvancedChildrenForeach(this GameObject obj, Predicate<GameObject> action)
        {
            if (obj == null || action == null)
            {
                return;
            }

            for (int i = 0; i < obj.transform.childCount; ++i)
            {
                GameObject child = obj.transform.GetChild(i).gameObject;
                bool recursively = action.Invoke(child);
                if (recursively)
                {
                    AdvancedChildrenForeach(child, action);
                }
            }
        }

        public static bool SetParent(this GameObject obj, GameObject goParent, bool isResetTransform = true)
        {
            if (goParent == null || obj == null)
            {
                return false;
            }

            obj.transform.SetParent(goParent.transform, false);
            if (isResetTransform)
            {
                obj.transform.ResetTransform();
            }

            return true;
        }

        /// <summary>
        /// 立即销毁GameObject
        /// </summary>
        /// <param name="obj">目标资源</param>
        public static void DestroyImmediate(this GameObject obj)
        {
            if (obj != null)
            {
                UnityEngine.Object.DestroyImmediate(obj);
            }
        }

        /// <summary>
        /// 销毁GameObject
        /// </summary>
        /// <param name="obj">目标资源</param>
        public static void Destroy(this GameObject obj)
        {
            if (obj != null)
            {
                UnityEngine.Object.Destroy(obj);
            }
        }

        /// <summary>
        /// 设置UI是否可被交互操作
        /// </summary>
        /// <param name="UIGO">挂有interactable属性的GameObject</param>
        /// <param name="interactable">是否可以交互</param>
        public static void SetInteractable(this GameObject uiObj, bool interactable)
        {
            Selectable selectable = uiObj.GetComponent<Selectable>();
            if (null != selectable)
            {
                selectable.interactable = interactable;
            }
        }

        /// <summary>
        /// 设置子节点是否激活
        /// </summary>
        /// <param name="parent">父节点</param>
        /// <param name="path">子节点路径</param>
        /// <param name="active">是否激活</param>
        public static void SetChildrenActive(this GameObject parent, string path, bool active)
        {
            Transform child = parent.transform.Find(path);
            if (null != child)
            {
                child.gameObject.SetActive(active);
            }
        }

        /// <summary>
        /// 充值Transform
        /// </summary>
        /// <param name="obj"></param>
        public static void ResetTransform(this GameObject obj)
        {
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            obj.transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }
}