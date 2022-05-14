using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Constants //상수값들
{
    public const int n = 70; //기본점수
    //구간별 p값
    public const int p0 = 3; // ~1000점
    public const int p1 = 10; // 1,000~10,000점
    public const int p2 = 120; // 10,000~100,000점
    public const int p3 = 240; // 100,000~1,000,000점
    public const int p4 = 700; // 1,000,000~ 10,000,000점
    public const int p5 = 1500; // 10,000,000~ 100,000,000점
    public const int p6 = 3000; // 100,000,000 ~ 1,000,000,000점
    public const int p7 = 10000; // 1,000,000,000점 ~ 
    
}

// 최고기록을 저장하고 불러오는 Manager
public class DataManager 
{
    public ulong score; // uint 최댓값: 4,294,967,295
    public int combo;
    public ulong pastscore;

    public void Init()
    {
        score = 0;
        pastscore = 0;
        combo = 0;
    }

    // explode로 4, 5, 6, ... 값이 입력됨
    // 몇 콤보인지 카운트하고 점수를 누적시키자 (기획ppt참고)
    public void Combo(int explode)
    {
        Debug.Log(explode + "개의 구슬이 폭발!");
        ++combo;
        CalculateScore(FirstScore(explode));

        Debug.Log(score + "점");
    }

    public void ComboCheck()
    {
        pastscore = score;
    }

    // 슬라이드를 했는데 아무것도 안터졌으면 콤보 초기화
    public void InitCombo() //콤보 
    {
        if(pastscore == score)
            combo = 0;
    }

    public int FirstScore(int explode)
    {
        if(score < 1000){
            return explode * (explode - 3) * Constants.n * Constants.p0;
        }
        else if(score >= 1000 && score < 10000){
            return explode * (explode - 3) * Constants.n * Constants.p1;
        }
        else if(score >= 10000 && score < 100000){
            return explode * (explode - 3) * Constants.n * Constants.p2;
        }
        else if(score >= 100000 && score < 1000000){
            return explode * (explode - 3) * Constants.n * Constants.p3;
        }
        else if(score >= 1000000 && score < 10000000){
            return explode * (explode - 3) * Constants.n * Constants.p4;
        }
        else if(score >= 10000000 && score < 100000000){
            return explode * (explode - 3) * Constants.n * Constants.p5;
        }
        else if(score >= 100000000 && score < 1000000000){
            return explode * (explode - 3) * Constants.n * Constants.p6;
        }
        else{
            return explode * (explode - 3) * Constants.n * Constants.p7;
        }
    }

    public void CalculateScore(int FirstScore)
    {
        if(combo < 2)
            score += (ulong)FirstScore;
        else if(combo == 2)
            score += (ulong)(FirstScore * 50);
        else if(combo == 3)
            score += (ulong)(FirstScore * 100);
        else if(combo == 4)
            score += (ulong)(FirstScore * 200);
        else if(combo > 4)
            score += (ulong)(FirstScore * 100 * (combo + 1));
    }

    public void Clear()
    {
        score = 0;
        pastscore = 0;
        combo = 0;
    }
}
