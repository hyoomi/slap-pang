using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    
    Text scoreText;

    public string GetCommaScore(ulong data)
    {
        //uint 최댓값 자릿수만큼 정규식으로 표현한 점수.. 
        return string.Format("{0:#,###,###,###}",data);
    }

    // Text GetComponent는 Start 함수에서 딱 한번 호출
    void Start()
    {
        scoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //ScoreText.text = Managers.data.score.ToString();
        //쉼표 출력을 위해 GetCommaScore 함수를 사용했습니다
        scoreText.text = GetCommaScore(Managers.Data.score).ToString();
    }
}
