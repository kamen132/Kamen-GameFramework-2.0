

using System;
using System.Collections;

using UnityEngine;
using YooAsset;
using Object = UnityEngine.Object;

namespace KamenFramework
{
    public class ResourceService : ServiceBase, IResourceService
    {
        private readonly float UnloadUnusedAssetTime = 30.0f;
        private float mUnloadUnusedAssetTimer;

        protected override IEnumerator OnInit()
        {
            YooAssets.Initialize();
            var package = YooAssets.CreatePackage("DefaultPackage");
            YooAssets.SetDefaultPackage(package);
            var initParameters = new OfflinePlayModeParameters();
            yield return package.InitializeAsync(initParameters);
        }

        public T Load<T>(string path) where T : Object
        {
            return YooAssets.LoadAssetSync<T>(path).GetAssetObject<T>();
        }

        public void LoadAsync<T>(string path, Action<AssetHandle> completed) where T : Object
        {
            AssetHandle handle = YooAssets.LoadAssetAsync<T>(path);
            handle.Completed += completed;
        }

        public AssetHandle LoadAsync<T>(string path) where T : Object
        {
            AssetHandle handle = YooAssets.LoadAssetAsync<T>(path);
            return handle;
        }

        public IEnumerator LoadSceneAsync(string path,string packagePath)
        {
            var package = YooAssets.GetPackage(packagePath);
            var sceneMode = UnityEngine.SceneManagement.LoadSceneMode.Single;
            SceneHandle handle = package.LoadSceneAsync(path, sceneMode);
            yield return handle;
        }

        public override void Update()
        {
            mUnloadUnusedAssetTimer += Time.deltaTime;
            if (mUnloadUnusedAssetTimer >= UnloadUnusedAssetTime)
            {
                try
                {
                    UnloadUnusedAssets();
                }
                catch (Exception ex)
                {
                    KLogger.LogError(ex);
                }

                mUnloadUnusedAssetTimer = 0f;
            }
        }

        // 卸载所有引用计数为零的资源包。
        // 可以在切换场景之后调用资源释放方法或者写定时器间隔时间去释放。
        private void UnloadUnusedAssets()
        {
            var package = YooAssets.GetPackage("DefaultPackage");
            package.UnloadUnusedAssets();
        }
    }
}