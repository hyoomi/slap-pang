using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class PauseButtonEvent : MonoBehaviour
{ 
    //일시멈춤 버튼의 텍스트와 Bool값
    [SerializeField] Text startPauseText;
    bool isPauseActive = false;

    //Popup을 띄우기 위한 패널
    public GameObject Panel;

    public void pauseBtn()
    {
        if (Panel != null) // 팝업을 띄우기 위한 if문
        {
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive);
        }
        if (isPauseActive)
        {
            Time.timeScale = 1;
            isPauseActive = false; 
            Debug.Log("Resume"); 
        }
        else
        {
            Time.timeScale = 0;
            isPauseActive = true;
            Debug.Log("Paused"); 
        }
        // inner text change
        startPauseText.text = isPauseActive ? "RESUME" : "PAUSE";
    }   
}
