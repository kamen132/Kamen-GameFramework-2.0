namespace KamenFramework
{
    public enum SceneType
    {
        None,
        Demo,
        Battle,
    }
    
    public enum SceneStateType
    {
        Init = 0,           //启动场景
        Pending,            //等待
        AfterLogin,         //登录之后
        PreEnter,           //遮挡层
        Entering,           //状态机准备
        PostEnter,          //加载场景，资源，面板准备
        PreSwitch,          //切场景前准备
        Switching,          //切场景
        PostSwitch,         //结束切场景
        PreExit,            //离开场景前
        Exiting,            //离开场景中
        PostExit,           //离开场景后处理
        Update,             //场景更新，可按照delta注册不同频率的回调
        Clear,              //切换场景PostEnter之后，Last场景的清除
    }
}