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
    int p_change_count = 1;

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
            if (DataManager.p_state == 1 && p_change_count == 1)
            {
                timeLeft -= 10f;
                p_change_count++;
            }
            else if (DataManager.p_state == 2 && p_change_count ==2)
            {
                timeLeft -= 10f;
                p_change_count++;

            }
            else if (DataManager.p_state == 3 && p_change_count == 3)
            {
                timeLeft -= 10f;
                p_change_count++;
            }
            else if (DataManager.p_state == 4 && p_change_count == 4)
            {
                timeLeft -= 5f;
                p_change_count++;

            }
            else if (DataManager.p_state == 5 && p_change_count == 5)
            {
                timeLeft -= 5f;
                p_change_count++;
            }
            else if (DataManager.p_state == 6 && p_change_count == 6)
            {
                timeLeft -= 5f;
                p_change_count++;
            }


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
