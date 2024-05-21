using System;
using System.Collections.Generic;
using KamenFramework;

namespace KamenFramework
{
    public class EntityService<TSceneEntity> : IEntityService<TSceneEntity> where TSceneEntity : IEntity, new()
    {
        /// <summary>
        /// 场景容器
        /// </summary>
        private readonly SceneObjectContainer mSceneContainer;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sceneContext"></param>
        public EntityService(SceneObjectContainer sceneContext)
        {
            mSceneContainer = sceneContext;
        }

        /// <summary>
        /// 场景内所有的实体
        /// </summary>
        public List<TSceneEntity> Entities { get; } = new List<TSceneEntity>();
        private readonly Dictionary<int, TSceneEntity> Id2EntityMapping = new Dictionary<int, TSceneEntity>();
        private int NextEntityIndex = 0;

        /// <summary>
        /// 创建实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T CreateEntity<T>(IModel model=null) where T : TSceneEntity, new()
        {
            T entity = new T();
            entity.Init(NextEntityIndex++, model);
            entity.SetSceneContainer(mSceneContainer);
            Entities.Add(entity);
            Id2EntityMapping[entity.Index] = entity;
            return entity;
        }

        /// <summary>
        /// 尝试获取实体
        /// </summary>
        /// <param name="entityIndex"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool TryGetByIndex(int entityIndex, out IEntity entity)
        {
            if (Id2EntityMapping.TryGetValue(entityIndex, out var entityInstance))
            {
                entity = entityInstance;
                return true;
            }

            entity = default;
            return false;
        }

        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(IEntity entity)
        {
            int entityIndex = entity.Index;
            if (Id2EntityMapping.TryGetValue(entityIndex, out var entityInstance))
            {
                entityInstance.Dispose();
                Id2EntityMapping.Remove(entityIndex);
                Entities.Remove(entityInstance);
            }
        }

        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="entityIndex"></param>
        public void Remove(int entityIndex)
        {
            if (Id2EntityMapping.TryGetValue(entityIndex, out var entityInstance))
            {
                entityInstance.Dispose();
                Id2EntityMapping.Remove(entityIndex);
                Entities.Remove(entityInstance);
            }
        }

        /// <summary>
        /// 遍历实体
        /// </summary>
        /// <param name="action"></param>
        public void ForeachEntity(Action<IEntity> action)
        {
            foreach (var entity in Entities)
            {
                if (entity.IsAlive)
                {
                    action(entity);
                }
            }
        }
        
        public void OnUpdate()
        {
            foreach (var entity in Entities)
            {
                entity.Update();
            }
        }

        public void OnFixedUpdate()
        {
            foreach (var entity in Entities)
            {
                entity.FixedUpdate();
            }
        }
        
        public void Dispose()
        {
            Reset();
        }
        
        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            foreach (var iterator in Entities)
            {
                iterator.Dispose();
            }

            Entities.Clear();
            Id2EntityMapping.Clear();
            NextEntityIndex = 0;
        }
    }
}