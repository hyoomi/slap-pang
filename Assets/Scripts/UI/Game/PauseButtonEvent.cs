using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class PauseButtonEvent : BaseUI
{
    //일시멈춤 버튼의 텍스트와 Bool값
    [SerializeField] Text startPauseText;

    public void PauseBtn(GameObject go)
    {
        bool IsPauseActive = go.activeSelf;
        if (go != null) // 팝업을 띄우기 위한 if문
        {
            SetTrue(go);
        }

        if (IsPauseActive)
        {
            Time.timeScale = 1;
            SetFalse(go);
        }
        else
        {
            Time.timeScale = 0;
            SetTrue(go);
        }
        // inner text change
        startPauseText.text = IsPauseActive ? "FAUSE" : "RESUME";
    }   
}
