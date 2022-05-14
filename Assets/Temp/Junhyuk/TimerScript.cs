using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : BaseUI
{
    Slider Timer;
    public float MaxTime = 8f;
    float timeLeft;
    public GameObject Gameover;
    bool check;

    void Start()
    {
        //Gameover.SetActive(false);
        GameObject go= Gameover;
        Timer = GetComponent<Slider>();
        timeLeft = 0;
        check = false;
    }


    void Update()
    {
        GameObject go = Gameover;
        if (timeLeft < MaxTime)
        {
            timeLeft += Time.deltaTime;
            Timer.value = timeLeft / MaxTime;
        }
        else
        {
            //Gameover.SetActive(true);
            if (check) return;
            check = true;
            base.ShowPopup(go);
        }
    }
}
