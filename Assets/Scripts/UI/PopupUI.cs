using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopupUI : BaseUI
{
    // 해당 팝업을 띄운다
    public void ShowPopup()
    {
        GameObject go = this.gameObject;
        Managers.UI.LoadUI<PopupUI>(go.name);
        
    }

    // 해당 팝업을 내린다
    public void ClosePopup()
    {
        Destroy(gameObject);
    }

    public void Awake()
    {
        Time.timeScale = 0;
    }

    public void OnEnable()
    {
        Managers.Action.popupAction = true;
    }

    public void OnDisable()
    {
        Managers.Action.popupAction = false;
    }

    public void RunTime()
    {
        Time.timeScale = 1;
    }

    // 게임 재시작
    public void ReStartButton()
    {
        Managers.Scene.LoadScene(Define.Scene.Game);
    }

    // 게임 로비화면으로 이동
    public void GoToLobbyButton()
    {
        Managers.Scene.LoadScene(Define.Scene.Lobby);
    }

    public void GameEndButton()
    {
        Bestscore.Save();
        if (Managers.Scene.CurrentScene.SceneType == Define.Scene.Lobby)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        else
        {
            Managers.Scene.LoadScene(Define.Scene.Lobby);
        }
    }
}
