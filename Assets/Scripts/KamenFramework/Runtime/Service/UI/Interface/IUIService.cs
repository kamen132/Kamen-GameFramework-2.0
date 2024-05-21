using UnityEngine;

namespace KamenFramework
{
    public interface IUIService : IService
    {
        KVIEW CreateView<KVIEW>(UILayer layer, bool isFullScreen) where KVIEW : UIView, new();
        KVIEW CreateView<KVIEW>(GameObject parentContainer, bool isFullScreen) where KVIEW : UIView, new();
    }
}