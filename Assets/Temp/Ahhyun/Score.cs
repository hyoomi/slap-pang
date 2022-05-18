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
        "<color=#0000CD>",
        "<color=#006400>",
        "<color=#FF8C00>",
        "<color=#FFD700>",
        "<color=#FF0000>",
    };

    string endCode = "</color>";

    public string GetCommaScore(ulong data)
    {
        //uint 최댓값 자릿수만큼 정규식으로 표현한 점수.. 
        return string.Format($"{{0:#,###,###,###}}", data);
    }

    // [박효민] 작성한 코드입니다! 이 함수 사용하시면 됩니다~ 변수명, 함수명 수정 OK!
    public string GetCommaScore2(ulong data)
    {
        string number = string.Format($"{{0:#,#}}", data); // 1000단위로 콤마를 찍어 string으로 저장
        number += endCode; // "890,123</color>"
        int j = 0;  // 몇번째 컬러코드를 사용할지 체크하는 변수
        for (int i = number.Length - 1; i >= 0; i--)  // 문자열 맨 뒤에서부터 검사
        {
            if (number[i]==',')  // 콤마를 발견했다면
            {
                number = number.Insert(i + 1, colorCode[j++]);  //콤마뒤에 컬러코드 삽입 "890,<color=#0000CD>123</color>"
                number = number.Insert(i, endCode);  // 콤마앞에 end코드 삽입 "890</color>,<color=#0000CD>123</color>"         
            }
        }
        number = colorCode[j] + number;  // 가장 앞에 컬러코드 삽입 "<color=#006400>890</color>,<color=#0000CD>123</color>" 
        return number;
    }

    // Text GetComponent는 Start 함수에서 딱 한번 호출
    void Start()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = GetCommaScore2(1234567890123); //테스트
    }

    // Update is called once per frame
    void Update()
    {
        //ScoreText.text = Managers.data.score.ToString();
        //쉼표 출력을 위해 GetCommaScore 함수를 사용했습니다
        //scoreText.text = GetCommaScore(Managers.Data.score).ToString();

        // [박효민] 추가한 코드입니다! 아래 코드 사용하시면 작동해요~
        //scoreText.text = GetCommaScore2(Managers.Data.score); 

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
