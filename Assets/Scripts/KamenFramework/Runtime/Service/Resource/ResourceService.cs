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
            EPlayMode playMode = EPlayMode.OfflinePlayMode;
            
#if UNITY_EDITOR
            playMode = KamenApp.Instance.PlayMode;
#endif
            if (playMode == EPlayMode.EditorSimulateMode)
            {
                var initParameters = new EditorSimulateModeParameters();
                var simulateManifestFilePath = EditorSimulateModeHelper.SimulateBuild(EDefaultBuildPipeline.BuiltinBuildPipeline, "DefaultPackage");
                initParameters.SimulateManifestFilePath = simulateManifestFilePath;
                yield return package.InitializeAsync(initParameters);
            }
            if (playMode == EPlayMode.OfflinePlayMode)
            {
                //单机模式
                var initParameters = new OfflinePlayModeParameters();
                yield return package.InitializeAsync(initParameters);
                var allShader = YooAssets.GetPackage("DefaultPackage").LoadAllAssetsSync<Shader>("Assets/GameRes/shader/FlashWhiteShader.shader");
                yield return allShader;
            }

            yield return base.OnInit();
        }

        public TObject LoadAsset<TObject>(string location) where TObject : Object
        {
            AssetHandle handle = YooAssets.LoadAssetSync(location);
            return LoadAsset<TObject>(location, handle);
        }

        private TObject LoadAsset<TObject>(string path, AssetHandle handle) where TObject : Object
        {
            var obj = handle.GetAssetObject<TObject>();
            if (obj is GameObject)
            {
                var ins = handle.InstantiateSync();
                ins.AddComponent<ResourceHandler>().Init(path, handle);
                return ins as TObject;
            }

            using (handle)
            {
                return obj;
            }
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
        
        private void UnloadUnusedAssets()
        {
            var package = YooAssets.GetPackage("DefaultPackage");
            package.UnloadUnusedAssets();
            KLogger.Log("--Clean Assets--", GameHelper.ColorGreen);
        }
    }
}