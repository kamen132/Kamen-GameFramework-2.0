using System;

namespace KamenFramework
{
    public interface IEntity : IDisposable
    { 
        SceneObjectContainer SceneContainer { get; }
        IModel Model { get; }
        void SetSceneContainer(SceneObjectContainer sceneContainer);
        EntityType Type { get; }
        int Index { get;}
        bool IsAlive { get; }
        void Init(int index,IModel model);
        void Update();
        void FixedUpdate();
    }
}