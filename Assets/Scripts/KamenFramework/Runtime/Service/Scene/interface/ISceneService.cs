using System;

namespace KamenFramework
{
    public interface ISceneService : IService
    {
        void AddScene<T>(T layer) where T : class, IScene;

        /// <summary>
        /// 获取场景
        /// </summary>
        /// <param name="sceneType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T TryGetScene<T>(SceneType sceneType) where T : class, IScene;

        /// <summary>
        /// 切换场景
        /// </summary>
        /// <param name="type"></param>
        /// <param name="callback"></param>
        /// <param name="callbackWithResult"></param>
        void SwitchScene(SceneType type, Action callback = null, Action<bool> callbackWithResult = null);
    }
}