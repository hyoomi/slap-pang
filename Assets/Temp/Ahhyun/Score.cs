using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] Text scorePrefab;
    //public Text ScoreText;

    public string GetCommaScore(ulong data)
    {
        //uint 최댓값 자릿수만큼 정규식으로 표현한 점수.. 
        return string.Format("{0:#,###,###,###}",data);
    }

    // Update is called once per frame
    void Update()
    {
        //ScoreText.text = Managers.data.score.ToString();
        //쉼표 출력을 위해 GetCommaScore 함수를 사용했습니다
        scorePrefab.text = GetCommaScore(Managers.Data.score).ToString();
    }
}
