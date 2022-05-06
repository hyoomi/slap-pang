using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtonEvent : MonoBehaviour
{
    bool isPauseActive = false;

    public void pauseBtn()
    {
        if(isPauseActive)
        {
            Time.timeScale = 1;
            isPauseActive = false;
        }
        else
        {
            Time.timeScale = 0;
            isPauseActive = true;
        }
        // inner text change
    }
}
