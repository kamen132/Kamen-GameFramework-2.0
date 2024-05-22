using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace KamenFramework
{
    public class SceneService : ServiceBase,ISceneService
    {
         /// <summary>
        /// 正在切换的场景协程
        /// </summary>
        private readonly Dictionary<string, IEnumerator> SwitchSceneMap = new Dictionary<string, IEnumerator>();
        
        /// <summary>
        /// 所有场景
        /// </summary>
        private readonly Dictionary<SceneType, IScene> SceneMap = new Dictionary<SceneType, IScene>();

        /// <summary>
        /// 场景容器
        /// </summary>
        public SceneObjectContainer SceneContainer { get; private set; } = new SceneObjectContainer();
        
            
        /// <summary>
        /// 是否正在切换
        /// </summary>
        private bool InSwitching;

        protected override IEnumerator OnInit()
        {
            foreach (var layerMap in SceneMap)
            {
                var layer = layerMap.Value;
                layer.StateType = SceneStateType.Init;
                layer.OnInit();
                layer.StateType = SceneStateType.Pending;
            }

            return base.OnInit();
        }

        /// <summary>
        /// 添加场景
        /// </summary>
        /// <param name="layer"></param>
        /// <typeparam name="T"></typeparam>
        public void AddScene<T>(T layer) where T : class, IScene
        {
            SceneMap.Add(layer.SceneType, layer);
        }

        /// <summary>
        /// 尝试获取场景
        /// </summary>
        /// <param name="sceneType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T TryGetScene<T>(SceneType sceneType) where T : class, IScene
        {
            SceneMap.TryGetValue(sceneType, out var scene);
            return scene as T;
        }

        /// <summary>
        /// 切换场景
        /// </summary>
        /// <param name="type">场景类型</param>
        /// <param name="callback">切换场景完成后回调</param>
        /// <param name="callbackWithResult"></param>
        public void SwitchScene(SceneType type, Action callback = null, Action<bool> callbackWithResult = null)
        {
            if (InSwitching)
            {
                return;
            }

            IScene lastLayer = null;
            SceneMap.TryGetValue(type, out var switchLayer);
            if (switchLayer == null)
            {
                return;
            }

            InSwitching = true;
            SwitchSceneMap.Clear();
            SceneContainer.SetTarget(type);

            if (SceneContainer.LastSceneType != SceneType.None)
            {
                SceneMap.TryGetValue(SceneContainer.LastSceneType, out lastLayer);
            }

            if (null != lastLayer)
            {
                if (type != SceneContainer.LastSceneType)
                {
                    SwitchSceneMap.Add("ExitScene", ExitScene(lastLayer as IScene, switchLayer as IScene));
                    SwitchSceneMap.Add("SwitchScene", SwitchScene(switchLayer));
                    SwitchSceneMap.Add("ClearScene", ClearScene(lastLayer as IScene));
                }
            }
            else
            {
                SwitchSceneMap.Add("SwitchScene", SwitchScene(switchLayer));
            }


            KamenGame.Instance.CoroutineService.StartCoroutine(RunSwitchScene(callback, callbackWithResult));
        }

        /// <summary>
        /// 开始切换场景
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="callbackWithResult"></param>
        /// <returns></returns>
        IEnumerator RunSwitchScene(Action callback, Action<bool> callbackWithResult)
        {
            for (int i = 0; i < SwitchSceneMap.Count; ++i)
            {
                var switchValue = SwitchSceneMap.ElementAt(i);
                var switchRoutine = switchValue.Value;
                var routineName = switchValue.Key;
                KLogger.Log($"SwitchScening CurrentRun:[{routineName}],  CurScene:[{SceneContainer.LastSceneType}] -> TargetScene:[{SceneContainer.SceneType}]", Color.red);
                yield return switchRoutine;
                if (routineName.Equals("SwitchScene"))
                {
                    callbackWithResult?.Invoke(true);
                }
            }
            
            callback?.Invoke();
            InSwitching = false;
            Resources.UnloadUnusedAssets();
            GC.Collect();
            KLogger.Log($"--Switch Success CurScene:{SceneContainer.SceneType}--",GameHelper.ColorGreen);
        }

        /// <summary>
        /// 离开场景
        /// </summary>
        /// <param name="layer"></param>
        /// <param name="targetLayer"></param>
        /// <returns></returns>
        IEnumerator ExitScene(IScene layer, IScene targetLayer)
        {
            SceneType nextSceneType = targetLayer?.SceneType ?? SceneType.None;
            layer.StateType = SceneStateType.PreExit;

            {
                var enumerator = layer.OnPreExit(nextSceneType);
                if (null != enumerator)
                {
                    yield return enumerator;
                }
            }
            layer.StateType = SceneStateType.Exiting;

            {
                var enumerator = layer.OnExiting(nextSceneType);
                if (null != enumerator)
                {
                    yield return enumerator;
                }
            }

            layer.StateType = SceneStateType.PostExit;

            {
                var enumerator = layer.OnPostExit(nextSceneType);
                if (null != enumerator)
                {
                    yield return enumerator;
                }
            }
        }

        /// <summary>
        /// 切换场景
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        IEnumerator SwitchScene(IScene layer)
        {

            layer.StateType = SceneStateType.PreSwitch;
            {
                var enumerator = layer.OnPreSwitch(SceneContainer.LastSceneType, layer.SceneType);
                if (null != enumerator)
                {
                    yield return enumerator;
                }
            }


            layer.StateType = SceneStateType.Switching;

            {
                var enumerator = layer.OnSwitching(SceneContainer.LastSceneType, layer.SceneType);
                if (null != enumerator)
                {
                    yield return enumerator;
                }
            }



            layer.StateType = SceneStateType.PostSwitch;

            {
                var enumerator = layer.OnPostSwitch(SceneContainer.LastSceneType, layer.SceneType);
                if (null != enumerator)
                {
                    yield return enumerator;
                }
            }

            {
                var enumerator = EnterScene(layer);
                if (null != enumerator)
                {
                    yield return enumerator;
                }
            }

            InSwitching = false;
        }

        /// <summary>
        /// 进入场景
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        IEnumerator EnterScene(IScene layer)
        {

            layer.StateType = SceneStateType.PreEnter;
            {
                var enumerator = layer.OnPreEnter(SceneContainer.LastSceneType, layer.SceneType);
                if (null != enumerator)
                {
                    yield return enumerator;
                }
            }

            layer.StateType = SceneStateType.Entering;

            {
                var enumerator = layer.OnEntering(SceneContainer.LastSceneType, layer.SceneType);
                if (null != enumerator)
                {
                    yield return enumerator;
                }
            }


            layer.StateType = SceneStateType.PostEnter;
            {
                var enumerator = layer.OnPostEnter(SceneContainer.LastSceneType, layer.SceneType);
                if (null != enumerator)
                {
                    yield return enumerator;
                }
            }

            layer.StateType = SceneStateType.Update;
        }

        /// <summary>
        /// 清除场景
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        IEnumerator ClearScene(IScene layer)
        {
            layer.StateType = SceneStateType.Clear;

            {
                var enumerator = layer.OnClear(layer.SceneType);
                if (null != enumerator)
                {
                    yield return enumerator;
                }
            }
        }
        
        public override void Update()
        {
            foreach (var layer in SceneMap)
            {
                layer.Value.OnUpdate();
            }
        }

        public override void FixUpdate()
        {
            foreach (var layer in SceneMap)
            {
                layer.Value.OnFixUpdate();
            }
        }

        public override void Shut()
        {
            foreach (var scene in SceneMap)
            {
                scene.Value.OnPreLeave();
            }
            foreach (var scene in SceneMap)
            {
                scene.Value.OnLeave();
            }
            foreach (var scene in SceneMap)
            {
                scene.Value.OnDestroy();
            }
        }
    }
}