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

    public class EnemyMoveComponent : KComponent
    {
        public EnemyMoveComponent(IEntity entity) : base(entity)
        {
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            //KLogger.Log("EnemyMoving!!!");
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            KLogger.Log("EnemyMoveComponent Dispose");
        }
    }
    public class EnemyEntity : BattleEntity
    {
        private GameObject mTarget;
        private EnemyMoveComponent EnemyMoveComponent;

        protected override void OnInit()
        {
            EnemyMoveComponent = AddComponent<EnemyMoveComponent>();
            mTarget = KamenGame.Instance.ObjectPoolService.Get("Assets/GameRes/Entity/Cube.prefab");
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            KamenGame.Instance.ObjectPoolService.Push(mTarget);
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