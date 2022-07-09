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
    float[] p_time = new float[12] { 0, 2, 5, 5, 7, 12, 10, 11, 12, 12, 13, 11}; //P에 따른 추가시간 값 배열. 기획변경으로 수정됨

    void Start()
    {
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
            if (check) return;
            check = true;
            base.ShowPopup(go);
        }
    }

    void P_Addtime()  //p에 따른 시간 추가 함수
    {
        if (Managers.Data.p_state >= p_change_count && Managers.Data.p_state <= 11)
        // 만약 p가 p_change한 값보다 클 경우 (p가 두 단계 이상 점프 했을 때 대비 ) && p가 6보다 작을 때만 시간 추가
        {
            int dif = Managers.Data.p_state - p_change_count;
            for (int i = 0; i < dif+1; i++)
            {
                timeLeft -= p_time[Managers.Data.p_state - i];

                if(timeLeft < 0) //시간을 추가했을 때 Maxtime을 초과해도 더 늘림
                {
                    MaxTime += (-timeLeft);
                    timeLeft = 0;
                }
            }
            p_change_count = Managers.Data.p_state + 1;
        }      
    }
}
