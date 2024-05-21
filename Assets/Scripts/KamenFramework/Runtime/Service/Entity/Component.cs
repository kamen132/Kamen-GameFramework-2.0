﻿namespace KamenFramework
{
    /// <summary>
    /// 实体组件
    /// </summary>
    public class KComponent : IComponent
    {
        /// <summary>
        /// 实体
        /// </summary>
        public IEntity Entity { get; private set; }

        public KComponent(IEntity entity)
        {
            Entity = entity;
        }
    }
}