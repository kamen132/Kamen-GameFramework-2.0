using KamenFramework;

namespace KamenGameFramewrok
{
    /// <summary>
    /// 游戏系统管理器
    /// </summary>
    public interface ISystemManager 
    {
        /// <summary>
        /// 获取系统
        /// </summary>
        /// <typeparam name="T"></typeparam>
        T GetSystem<T>() where T : KamenSystem;

        void InstallSystemByIndex(int index);

        /// <summary>
        /// 预加载系统
        /// </summary>
        /// <param name="index"></param>
        void PreloadSystemByIndex(int index);

        /// <summary>
        /// 获取系统数量
        /// </summary>
        /// <returns></returns>
        int GetSupportedSystemCount();

        /// <summary>
        /// 触发系统重要事件
        /// </summary>
        /// <param name="eventType"></param>
        void TriggerEvent(EnumSystemEventType eventType);

        void UnloadAll();

    }
}