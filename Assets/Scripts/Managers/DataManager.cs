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
    public int p_state;

    // 몇개의 공이 폭발할지 입력되면 콤보를 알린다
    int _combo;
    public int COMBO
    {
        get { return _combo; }
        set
        {         
            if (value == 0) { _combo = 0; return; }
            _combo++;
            Managers.Action.ComboAction = _combo;
            Debug.Log(_combo + "콤보");
        }
    }

    public void Init()
    {
        score = 0;
        _combo = 0;
    }

    // explode로 4, 5, 6, ... 값이 입력됨
    // 몇 콤보인지 카운트하고 점수를 누적시키자 (기획ppt참고)
    public void Combo(int explode)
    {
        CalculateScore(FirstScore(explode));
        Set_state_p(); // p 상태 확인 
    }

    public int FirstScore(int explode)
    {
        if (score < 1000)
        { 
            return explode * (explode - 3) * Constants.n * Constants.p0;
        }
        else if (score >= 1000 && score < 10000)
        {
            return explode * (explode - 3) * Constants.n * Constants.p1;
        }
        else if (score >= 10000 && score < 100000)
        {
            return explode * (explode - 3) * Constants.n * Constants.p2;
        }
        else if (score >= 100000 && score < 1000000)
        { 
            return explode * (explode - 3) * Constants.n * Constants.p3;
        }
        else if (score >= 1000000 && score < 10000000)
        { 
            return explode * (explode - 3) * Constants.n * Constants.p4;
        }
        else if (score >= 10000000 && score < 100000000)
        {
            return explode * (explode - 3) * Constants.n * Constants.p5;
        }
        else if (score >= 100000000 && score < 1000000000)
        {
            return explode * (explode - 3) * Constants.n * Constants.p6;
        }
        else
        {
            return explode * (explode - 3) * Constants.n * Constants.p7;
        }
    }


    public void CalculateScore(int FirstScore)
    {
        if (_combo < 2)
            score += (ulong)FirstScore;
        else if (_combo == 2)
            score += (ulong)(FirstScore * 50);
        else if (_combo == 3)
            score += (ulong)(FirstScore * 100);
        else if (_combo == 4)
            score += (ulong)(FirstScore * 200);
        else if (_combo > 4)
            score += (ulong)(FirstScore * 100 * (_combo + 1));
    }

    public void Set_state_p() //p 상태 설정 
    {
        if (score < 10000)
        {
            p_state = 0;
        }
        else if (score >= 10000 && score < 100000)
        {
            p_state = 1;
        }
        else if (score >= 100000 && score < 1000000)
        {
            p_state = 2;
        }
        else if (score >= 1000000 && score < 10000000)
        {
            p_state = 3;
        }
        else if (score >= 10000000 && score < 100000000)
        {
            p_state = 4;
        }
        else if (score >= 100000000 && score < 1000000000)
        {
            p_state = 5;
        }
        else if (score >= 1000000000)
        {
            p_state = 6;
        }
    }

    public void Clear()
    {
        score = 0;
        _combo = 0;
    }
}
