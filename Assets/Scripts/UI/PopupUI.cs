using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    /* 게임 앱 종료 
     * Managers.Scene.CurrentScene;으로 get해온 BaseScene을 조건에 어떻게 매치해야할지 찾고있습니다,,
    public void GameEndButton()
    {
        BaseScene SceneType = Managers.Scene.CurrentScene;
        Debug.Log(SceneType);
        if (SceneType == typeof(LobbyScene))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        else if (SceneType == typeof(GameScene))
        {
            Managers.Scene.LoadScene(Define.Scene.Lobby);
        }
    }
    */
}
