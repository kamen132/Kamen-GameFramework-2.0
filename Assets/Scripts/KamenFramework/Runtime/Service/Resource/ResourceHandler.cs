using UnityEngine;
using YooAsset;

namespace KamenFramework
{
    public class ResourceHandler : MonoBehaviour
    {
        private AssetHandle mHandle;
        public string AssetPath { get; private set; }
        public void Init(string path, AssetHandle handle)
        {
            mHandle = handle;
            AssetPath = path;
        }
        private void OnDestroy()
        {
            mHandle?.Release();
        }
    }
}