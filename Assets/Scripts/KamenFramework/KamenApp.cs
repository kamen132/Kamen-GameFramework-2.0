using System;
using UnityEngine;

namespace KamenFramework
{
    public class KamenApp : MonoBehaviour
    {
        private void Awake()
        {
            ServiceManager.Instance.AddService();
            StartCoroutine(ServiceManager.Instance.InitService());
        }

        private void Update()
        {
            ServiceManager.Instance.Update();
        }

        private void FixedUpdate()
        {
            ServiceManager.Instance.FixUpdate();
        }

        private void OnApplicationQuit()
        {
            ServiceManager.Instance.Shut();
        }
    }
}