using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUI : MonoBehaviour
{   
    public void SetTure(GameObject go)
    {
        go.SetActive(true);
    }

    public void SetFalse(GameObject go)
    {
        go.SetActive(false);
    }    
    
    public void ShowPopup(GameObject go)
    {
        Managers.UI.LoadUI<PopupUI>(go.name);
    }
}
