using System.Collections;

namespace KamenFramework
{
    public interface IScene
    {
        SceneType SceneType { get; }
        SceneStateType StateType { get; set; }
        IEnumerator OnPreEnter(SceneType curSceneType, SceneType targetSceneType);
        IEnumerator OnEntering(SceneType lastSceneType, SceneType goSceneType);
        IEnumerator OnPostEnter(SceneType curSceneType, SceneType targetSceneType);
        IEnumerator OnPreSwitch(SceneType lastSceneType, SceneType goSceneType);
        IEnumerator OnSwitching(SceneType lastSceneType, SceneType goSceneType);
        IEnumerator OnPostSwitch(SceneType lastSceneType, SceneType goSceneType);
        IEnumerator OnPreExit(SceneType goSceneType);
        IEnumerator OnExiting(SceneType goSceneType);
        IEnumerator OnPostExit(SceneType goSceneType);
        IEnumerator OnClear(SceneType lastSceneType);
        void OnUpdate();
        void OnFixUpdate();
        void OnInit();
        void OnPreLeave();
        void OnLeave();
        void OnDestroy();
    }
}