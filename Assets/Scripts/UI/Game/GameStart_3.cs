using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
- <게임 시작3>에서 [GAME!] 문구가 뜬 후 2초뒤 [START]로 바꿔주기 (코루틴 사용)
- [일시정지]버튼 클릭시 빈 팝업 띄우기 
*/

public class GameStart_3 : BaseUI
{
    private IEnumerator CPrint; // coroutine for print

    // Start is called before the first frame update 
    [SerializeField]
    public float sec = 2.0f; 
    public GameObject GameStart;
    // sec가 지나면 StartImage에 넣어둔 Sprite로 변경되도록 하였습니다.
    public Sprite StartImage;
    

    void Start()
    {
        Managers.Data.GameState = Define.GameState.None;  // GameState 변화 방지
        CPrint = StartPrint(sec);
        StartCoroutine(CPrint);
    }

    IEnumerator StartPrint(float sec)
    {
        yield return new WaitForSeconds(sec);
        GetComponent<Image>().sprite = StartImage;

        //wait for another sec and then Close "Start" UI 
        Coroutine CloseStart = StartCoroutine(CloseStartUI(sec));
        yield return new WaitForSeconds(sec);
        StopCoroutine(CloseStart);
    }

    IEnumerator CloseStartUI(float sec)
    {
        yield return new WaitForSeconds(sec); 
        this.CloseUI();
        Managers.Data.GameState = Define.GameState.Idle; // GameState 변화 허용
    }
}
