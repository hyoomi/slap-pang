using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStart_3 : BaseUI
{
    private IEnumerator CPrint; // coroutine for print

    [SerializeField]
    public float sec = 2.0f; 
    public GameObject GameStart;
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
