using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
