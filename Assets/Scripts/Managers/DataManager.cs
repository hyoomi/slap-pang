using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//----------삭제예정------------------//
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
    const int N = 70;

    public ulong score; //-------삭제예정-----------//
    public int p_state; 

    // 콤보. 연속적으로 공이 터짐 (1부터 시작)
    int _combo;
    public int COMBO
    {
        get { return _combo; }
        set
        {         
            if (value == 0) { _combo = 0; return; }

            if (_combo > 0)
            {
                Managers.Action.ComboAction(_combo);
                //Debug.Log(_combo + "콤보");
            }
            _combo++;
        }
    }

    // 점수
    ulong _score;
    public ulong SCORE
    {
        get { return _score; }
        set 
        {
            _score += value;
            if (_score >= 999999999999999) { _score = 999999999999999; return; }               
            if (_score < 1000)
                Section = 0;
            else if (_score >= 1000 && _score < 10000)
                Section = 1;
            else if (_score >= 10000 && _score < 100000)
                Section = 2;
            else if (_score >= 100000 && _score < 1000000)
                Section = 3;
            else if (_score >= 1000000 && _score < 10000000)
                Section = 4;
            else if (_score >= 10000000 && _score < 100000000)
                Section = 5;
            else if (_score >= 100000000 && _score < 1000000000)
                Section = 6;
            else
                Section = 7;          
        }
    }

    // 점수 구간 (0 ~ 7)
    int _section; 
    public int Section
    {
        get { return _section; }
        set
        {
            if (value == _section) return;
            _section = value;
            Managers.Action.SectionAction(_section);
            p_state = _section - 1;
            if (p_state < 0) p_state = 0;
        }
    }

    // 구간별 가중치 P
    int[] _p = new int[] { 3, 10, 120, 240, 700, 1500, 3000, 10000};
    public int P => _p[_section]; 
    
    // 초기화
    public void Init()
    {
        score = 0;
        _combo = 0;
        _score = 0;
        _section = 0;
    }

    // 네.... 점수가 망해버렸어요.... 기획변경으로 새로 만들었습니다......모두 수고 많으셨어요..!
    public void ExplodedSet(int explode)
    {
        //Debug.Log(explode + "개의 공 폭발 / " + COMBO + "콤보 / " + P );
        ulong tmpSCORE = (ulong)(explode * (explode - 3) * N * P);
        if (COMBO == 0)
            SCORE = tmpSCORE;
        if (COMBO == 1) // 1콤보
            SCORE = tmpSCORE * 3;
        else if (COMBO == 2) // 2콤보
            SCORE = tmpSCORE * 5;
        else // 3콤보 이상
            SCORE = tmpSCORE * 8;
    }


    //----------------- 삭제예정--------------------------------------------------

    // explode로 4, 5, 6, ... 값이 입력됨
    // 몇 콤보인지 카운트하고 점수를 누적시키자 (기획ppt참고)
    public void Combo(int explode)
    {
        CalculateScore(FirstScore(explode));
        Set_state_p();
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
        Debug.Log("Cal Score: " + _combo);
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
        _score = 0;
        _section = 0;
    }
}
