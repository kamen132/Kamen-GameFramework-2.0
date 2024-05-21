using System.Collections;
using KamenFramework;

namespace KamenFramework
{
    public class KamenScene<SCENE_ENTITY> :IScene where SCENE_ENTITY : IEntity , new()
    {
        /// <summary>
        /// 场景容器
        /// </summary>
        public SceneObjectContainer SceneContext { get; private set; }
        
        /// <summary>
        /// 场景内实体管理器
        /// </summary>
        public EntityService<SCENE_ENTITY> EntityService { get; private set; }
        
        /// <summary>
        /// 场景类型
        /// </summary>
        public SceneType SceneType { get; protected set; }
        /// <summary>
        /// 场景状态
        /// </summary>
        public SceneStateType StateType { get; set; }
        
        public KamenScene(SceneObjectContainer sceneContext)
        {
            SceneContext = sceneContext;
            EntityService = new EntityService<SCENE_ENTITY>(SceneContext);
        }

        /// <summary>
        /// 遮挡层
        /// </summary>
        /// <param name="curSceneType"></param>
        /// <param name="targetSceneType"></param>
        /// <returns></returns>
        public virtual IEnumerator OnPreEnter(SceneType curSceneType, SceneType targetSceneType) { return null; }
        
        /// <summary>
        /// 状态机准备
        /// </summary>
        /// <param name="lastSceneType"></param>
        /// <param name="goSceneType"></param>
        /// <returns></returns>
        public virtual IEnumerator OnEntering(SceneType lastSceneType, SceneType goSceneType) { return null; }
        
        /// <summary>
        /// 加载场景，资源，面板准备
        /// </summary>
        /// <param name="curSceneType"></param>
        /// <param name="targetSceneType"></param>
        /// <returns></returns>
        public virtual IEnumerator OnPostEnter(SceneType curSceneType, SceneType targetSceneType) { return null; }
        
        /// <summary>
        /// 切场景前准备
        /// </summary>
        /// <param name="lastSceneType"></param>
        /// <param name="goSceneType"></param>
        /// <returns></returns>
        public virtual IEnumerator OnPreSwitch(SceneType lastSceneType, SceneType goSceneType) { return null; }
        
        /// <summary>
        /// 切场景
        /// </summary>
        /// <param name="lastSceneType"></param>
        /// <param name="goSceneType"></param>
        /// <returns></returns>
        public virtual IEnumerator OnSwitching(SceneType lastSceneType, SceneType goSceneType) { return null; }
        
        /// <summary>
        /// 结束切场景
        /// </summary>
        /// <param name="lastSceneType"></param>
        /// <param name="goSceneType"></param>
        /// <returns></returns>
        public virtual IEnumerator OnPostSwitch(SceneType lastSceneType, SceneType goSceneType) { return null; }
        
        /// <summary>
        /// 离开场景前
        /// </summary>
        /// <param name="goSceneType"></param>
        /// <returns></returns>
        public virtual IEnumerator OnPreExit(SceneType goSceneType) { return null; }
        
        /// <summary>
        /// 离开场景中
        /// </summary>
        /// <param name="goSceneType"></param>
        /// <returns></returns>
        public virtual IEnumerator OnExiting(SceneType goSceneType) { return null; }
        
        /// <summary>
        /// 离开场景后处理
        /// </summary>
        /// <param name="goSceneType"></param>
        /// <returns></returns>
        public virtual IEnumerator OnPostExit(SceneType goSceneType) { return null; }
        
        /// <summary>
        /// 切换场景PostEnter之后，Last场景的清除
        /// </summary>
        /// <param name="lastSceneType"></param>
        /// <returns></returns>
        public virtual IEnumerator OnClear(SceneType lastSceneType){ return null; }
        
        /// <summary>
        /// 更新场景内
        /// </summary>
        public virtual void OnUpdate()
        {
            EntityService.OnUpdate();
        }

        public virtual void OnFixUpdate()
        {
            EntityService.OnFixedUpdate();
        }

        public virtual void OnInit()
        {
            
        }

        public virtual void OnPreLeave() { }
        public virtual void OnLeave()
        {
        }
        public virtual void OnDestroy()
        {
            EntityService.Dispose();
        }
    }
}