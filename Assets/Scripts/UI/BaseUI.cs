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
    }

    // 해당 UI 삭제
    // 수정할 예정! 일단 이걸로 사용해주세요
    public void CloseUI()
    {
        Destroy(gameObject);
    }
}
