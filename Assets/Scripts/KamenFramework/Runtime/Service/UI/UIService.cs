using UnityEngine;

namespace KamenFramework.Service.UI
{
    //todo
    public class UIService : ServiceBase ,IUIService
    {
        public KVIEW CreateView<KVIEW>(UILayer layer, bool isFullScreen) where KVIEW : UIView, new()
        {
            throw new System.NotImplementedException();
        }

        public KVIEW CreateView<KVIEW>(GameObject parentContainer, bool isFullScreen) where KVIEW : UIView, new()
        {
            throw new System.NotImplementedException();
        }
    }
}