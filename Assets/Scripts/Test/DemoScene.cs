using KamenFramework;

namespace Test
{
    public class DemoScene : KamenScene<DemoEntity>
    {
        public DemoScene(SceneObjectContainer sceneContext) : base(sceneContext)
        {
            SceneType = SceneType.Demo;
        }
    }

    public class DemoEntity : Entity
    {
        
    }
}