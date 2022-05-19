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
    int p_change_count = 1; // p0 -> p1 포함해서 1
    float[] p_time = new float[7] { 0, 3, 6, 10, 15, 25, 40 }; //P에 따른 추가시간 값 배


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
            P_Addtime();
         
        }
        else
        {
            //Gameover.SetActive(true);
            if (check) return;
            check = true;
            base.ShowPopup(go);
        }
    }
    void P_Addtime()  //p에 따른 시간 추가 함수
    {
        if (Managers.Data.p_state >= p_change_count && Managers.Data.p_state <= 6)
        // 만약 p가 p_change한 값보다 클 경우 (p가 두 단계 이상 점프 했을 때 대비 ) && p가 6보다 작을 때만 시간 추가
        {
            int dif = Managers.Data.p_state - p_change_count;
            for (int i = 0; i < dif+1; i++)
            {
                timeLeft -= p_time[Managers.Data.p_state - i];
                //if(timeLeft < 0) //시간을 추가했을 때 Maxtime을 초과한다면 초과한 시간은 버림
                //{
                //    timeLeft = 0;
                //}
                if(timeLeft < 0) //시간을 추가했을 때 Maxtime을 초과해도 더 늘림
                {
                    MaxTime += (-timeLeft);
                    timeLeft = 0;
                }

                Debug.Log(p_time[Managers.Data.p_state - i] + "초 추가!");
            }
            p_change_count = Managers.Data.p_state + 1;
        }
        //if (Managers.Data.p_state == 1 && p_change_count == 1)
        //{
        //    timeLeft -= 3f;
        //    p_change_count++;
        //    Debug.Log("p1: 시간 3초 증가!");
        //}
        //else if (Managers.Data.p_state == 2 && p_change_count ==2)
        //{
        //    timeLeft -= 6f;
        //    p_change_count++;
        //    Debug.Log("p2: 시간 6초 증가!");

        //}
        //else if (Managers.Data.p_state == 3 && p_change_count == 3)
        //{
        //    timeLeft -= 10f;
        //    p_change_count++;
        //    Debug.Log("p3: 시간 10초 증가!");
        //}
        //else if (Managers.Data.p_state == 4 && p_change_count == 4)
        //{
        //    timeLeft -= 15f;
        //    p_change_count++;
        //    Debug.Log("p4: 시간 15초 증가!");

        //}
        //else if (Managers.Data.p_state == 5 && p_change_count == 5)
        //{
        //    timeLeft -= 25f;
        //    p_change_count++;
        //    Debug.Log("p5: 시간 25초 증가!");
        //}
        //else if (Managers.Data.p_state == 6 && p_change_count == 6)
        //{
        //    timeLeft -= 40f;
        //    p_change_count++;
        //    Debug.Log("p6: 시간 40초 증가!");
        //}
        }
    }
