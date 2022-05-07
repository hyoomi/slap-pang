using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Lobby;

        Debug.Log(Managers.Scene.GetSceneName(SceneType));
        //Managers.UI.LoadUI<PopupUI>("GameUI");      
        //Managers.UI.LoadUI<PopupUI>("StartButton");
    }
}
