namespace KamenFramework
{
    public class SceneObjectContainer
    {
        public SceneType LastSceneType { get; private  set; } = SceneType.None;
        public SceneType SceneType { get; private set; } = SceneType.None;

        public void SetTarget(SceneType type)
        {
            LastSceneType = SceneType;
            SceneType = type;
        }
    }
}