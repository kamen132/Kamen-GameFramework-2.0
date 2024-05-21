using System;
using System.Collections;
using YooAsset;
using Object = UnityEngine.Object;

namespace KamenFramework
{
    public interface IResourceService : IService
    {
        T Load<T>(string path) where T : Object;
        void LoadAsync<T>(string path,Action<AssetHandle> completed) where T : Object;
        AssetHandle LoadAsync<T>(string path) where T : Object;
        IEnumerator LoadSceneAsync(string path, string packagePath);
    }
}