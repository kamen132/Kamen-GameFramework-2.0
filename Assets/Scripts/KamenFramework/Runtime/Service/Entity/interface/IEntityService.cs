using System;
using System.Collections.Generic;

namespace KamenFramework
{
    public interface IEntityService<SCENE_ENTITY> : IDisposable where SCENE_ENTITY : IEntity, new()
    {
        bool TryGetByIndex(int entityIndex, out IEntity entity);

        T CreateEntity<T>(IModel model = null) where T : SCENE_ENTITY, new();

        void Reset();

        void Remove(IEntity entity);

        void Remove(int entityIndex);

        void ForeachEntity(Action<IEntity> action);

        List<SCENE_ENTITY> Entities { get; }
    }
}