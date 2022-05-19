using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    public void RunTime()
    {
        Time.timeScale = 1;
    }
}
