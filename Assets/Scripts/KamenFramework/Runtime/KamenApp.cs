using UnityEngine;
using YooAsset;

namespace KamenFramework
{
    public class KamenApp : MonoBehaviour
    {
        [SerializeField] [Header("资源使用编译器")]
        public EPlayMode PlayMode = EPlayMode.EditorSimulateMode;

        public static KamenApp Instance;
        private void Awake()
        {
            Application.targetFrameRate = 60;
            Instance = this;
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