using KamenFramework;
using UnityEngine;

namespace Game.Message
{
    public class DemoMessageController : MessageController<DemoMsgModel>
    {
        public override void Handle(DemoMsgModel msg)
        {
            Debug.Log("DemoMessageController接受消息 信息内容:" +
                      $"msg1:{msg.Message1} msg2:{msg.Message2} msg3:{msg.Message3}");
        }
    }
}