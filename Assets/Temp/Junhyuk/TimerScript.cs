using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    Slider Timer;
    public float MaxTime = 8f;
    float timeLeft;
    public GameObject Gameover;
    void Start()
    {
        Gameover.SetActive(false);
        Timer = GetComponent<Slider>();
        timeLeft = MaxTime;
    }


    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            Timer.value = timeLeft / MaxTime;
        }
        else
        {
            Gameover.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
