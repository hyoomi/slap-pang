using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    // 인자로 받은 GameObject를 활성화
    public void SetTrue(GameObject go)
    {
        go.SetActive(true);
    }

    // 인자로 받은 GameObject를 비활성화
    public void SetFalse(GameObject go)
    {
        go.SetActive(false);
    }

    // 인자로 받은 팝업창을 Load, Instantiate
    public void ShowPopup(GameObject go)
    {
        Managers.UI.LoadUI<PopupUI>(go.name);
        Time.timeScale = 0;
    }

    // 로비에서 Touch! 버튼 클릭시 로그인
    public void Login()
    {
        Managers.Scene.Login = true;
    }

    // 해당 UI 삭제
    public void CloseUI()
    {
        Destroy(gameObject);
    }
}
