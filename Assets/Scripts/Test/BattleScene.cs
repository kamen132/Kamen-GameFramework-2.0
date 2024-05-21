using KamenFramework;

namespace Test
{
    public class BattleScene : KamenScene<BattleEntity>
    {
        public BattleScene(SceneObjectContainer sceneContext) : base(sceneContext)
        {
            SceneType = SceneType.Battle;
        }
    }

    public class BattleEntity : Entity
    {
        
    }
}