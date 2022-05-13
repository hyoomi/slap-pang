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
    void Start()
    {
        //Gameover.SetActive(false);
        GameObject go= Gameover;
        base.SetFalse(go);
        Timer = GetComponent<Slider>();
        timeLeft = MaxTime;
    }


    void Update()
    {
        GameObject go = Gameover;
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            Timer.value = timeLeft / MaxTime;
        }
        else
        {
            //Gameover.SetActive(true);
            base.SetTrue(go);
            Time.timeScale = 0;
        }
    }
}
