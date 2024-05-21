using System;
using Game.Message;
using KamenFramework;
using Test;
using UnityEngine;

public class FrameworkTest : MonoBehaviour
{
    private void Start()
    {
        var sceneService = KamenGame.Instance.SceneService;
        sceneService.AddScene(new BattleScene( KamenGame.Instance.MainSceneContainer));
        sceneService.AddScene(new DemoScene( KamenGame.Instance.MainSceneContainer));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            KamenGame.Instance.MessageService.Dispatch(new DemoMsgModel()
            {
                Message1 = "消息内容 Message1",
                Message2 = "消息内容 Message2",
            });
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            KamenGame.Instance.SceneService.SwitchScene(SceneType.Battle);
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            KamenGame.Instance.SceneService.SwitchScene(SceneType.Demo);
        }
    }
}