using System.Collections;
using KamenFramework;
using UnityEngine;

namespace Test
{
    public class BattleScene : KamenScene<BattleEntity>
    {
        public BattleScene(SceneObjectContainer sceneContext) : base(sceneContext)
        {
            SceneType = SceneType.Battle;
        }

        public EnemyEntity CreateEnemy()
        {
            return EntityService.CreateEntity<EnemyEntity>();
        }
        
        public HeroEntity CreateHero()
        {
            return EntityService.CreateEntity<HeroEntity>();
        }

        public int GetSceneEntityCount()
        {
            return EntityService.Entities.Count;
        }

        public override IEnumerator OnPreExit(SceneType goSceneType)
        {
            KLogger.Log($"PreExit EntityCount:{GetSceneEntityCount()}", GameHelper.ColorRed);
            EntityService.Reset();
            return base.OnPreExit(goSceneType);
        }

        public override IEnumerator OnPostExit(SceneType goSceneType)
        {
            KLogger.Log($"PreExit EntityCount:{GetSceneEntityCount()}", GameHelper.ColorRed);
            return base.OnPostExit(goSceneType);
        }
    }

    public class EnemyEntity : BattleEntity
    {
        private GameObject mTarget;
        protected override void OnInit()
        {
            KamenGame.Instance.ResourceService.LoadAsync<GameObject>("Assets/GameRes/Entity/Cube.prefab", (handle) =>
            {
                mTarget = handle.InstantiateSync();
                handle.Release();
            });
        }

        public override void Dispose()
        {
            base.Dispose();
            UnityEngine.Object.Destroy(mTarget);
            mTarget = null;
        }
    }

    public class HeroEntity : BattleEntity
    {
        
    }
    public class BattleEntity : Entity
    {
        
    }
}