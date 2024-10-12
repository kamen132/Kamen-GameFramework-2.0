using System;

namespace KamenFramework
{
    /// <summary>
    /// 消息内容接口
    /// </summary>
    public interface IModel : IDisposable
    {
        string Guid { get; set; }
    }
}