using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    
    Text scoreText;
    string tmpString;
    // rich text color 지정을 위한 코드 문자열을 배열로 선언하였습니다.
    // 배열 인덱스 0 ~ 5까지 = "파랑색, 초록색, 주황색, 노랑색, 빨간색, 닫는 태그"가 들어있습니다.
    string[] colorCode = new string[] {
        "<color=#2E2EFE>",
        "<color=#72EB00>",
        "<color=#FB9120>",
        "<color=#FDE848>",
        "<color=#FF0000>",
        "</color>"};

    public string GetCommaScore(ulong data)
    {
        //uint 최댓값 자릿수만큼 정규식으로 표현한 점수.. 
        return string.Format("{0:#,###,###,###}",data);
    }

    // Text GetComponent는 Start 함수에서 딱 한번 호출
    void Start()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = "000"; //초기 출력은 000
    }

    // Update is called once per frame
    void Update()
    {
        //ScoreText.text = Managers.data.score.ToString();
        //쉼표 출력을 위해 GetCommaScore 함수를 사용했습니다
        scoreText.text = GetCommaScore(Managers.Data.score).ToString();

        //,를 기준으로 나누어 점수 색을 변화시도
        /*
        tmpString = GetCommaScore(Managers.Data.score).ToString();
        int txtLen = tmpString.Length;

        switch(txtLen){
            case <= 4:
            scoreText.text = colorCode[0] + tmpString + colorCode[5];
            case > 4:
            scoreText.text = colorCode[0] + tmpString.Substring(txtLen-1,4) + colorCode[5] + colorCode[1] + tmpString.Substring(4,0) + colorCode[5];
        }*/
    }
}
