using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using KamenFramework.Runtime.Service.Base;
using YooAsset;
using Object = UnityEngine.Object;

namespace KamenFramework.Runtime.Service.Resource
{
    public interface IResourceService : IService
    {
        T Load<T>(string path) where T : Object;
        void LoadAsync<T>(string path,Action<AssetHandle> completed) where T : Object;
        AssetHandle LoadAsync<T>(string path) where T : Object;
        IEnumerator LoadSceneAsync(string path, string packagePath);
    }
}