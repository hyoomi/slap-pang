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

    public void Init()
    {
        score = 0;
        combo = 0;
    }

    // explode로 4, 5, 6, ... 값이 입력됨
    // 몇 콤보인지 카운트하고 점수를 누적시키자 (기획ppt참고)
    public void Combo(int explode)
    {
        Debug.Log(explode + "개의 구슬이 폭발!");
    }

    // 슬라이드를 했는데 아무것도 안터졌으면 콤보 초기화
    public void InitCombo()
    {

    }

    public void Clear()
    {

    }
}
