using System;

namespace KamenFramework
{
    public interface IComponent : IDisposable
    {
         IEntity Entity { get; }
         void Update();
         void FixedUpdate();
    }
}