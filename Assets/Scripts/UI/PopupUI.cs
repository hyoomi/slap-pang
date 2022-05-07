using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupUI : BaseUI
{
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
}
