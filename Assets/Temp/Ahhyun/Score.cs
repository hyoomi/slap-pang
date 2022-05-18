using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{   
    Text scoreText;
    
    // rich text color 지정을 위한 코드 문자열을 배열로 선언하였습니다.
    // 배열 인덱스 0 ~ 5까지 순서대로 "파랑색, 초록색, 주황색, 노랑색, 빨간색" 컬러 코드가 들어있습니다.
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
        string colorText = string.Format($"{{0:#,#}}", data); // 1000단위로 콤마를 찍어 string으로 저장
        colorText += endCode; // "890,123</color>"
        int j = 0;  // 몇번째 컬러코드를 사용할지 체크하는 변수
        for (int i = colorText.Length - 1; i >= 0; i--)  // 문자열 맨 뒤에서부터 검사
        {
            if (colorText[i]==',')  // 콤마를 발견했다면
            {
                colorText = colorText.Insert(i + 1, colorCode[j++]);  //콤마뒤에 컬러코드 삽입 "890,<color=#0000CD>123</color>"
                colorText = colorText.Insert(i + 1, endCode);  // 콤마뒤에 end코드 삽입 "890,</color><color=#0000CD>123</color>"       
            }
        }
        colorText = colorCode[j] + colorText;  // 가장 앞에 컬러코드 삽입 "<color=#006400>890,</color><color=#0000CD>123</color>" 
        return colorText;
    }

    // Text GetComponent는 Start 함수에서 딱 한번 호출
    void Start()
    {
        scoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //쉼표 출력을 위해 GetCommaScore 함수를 사용했습니다.
        // [박효민] 추가한 코드입니다! 아래 코드 사용하시면 작동해요~
        scoreText.text = GetCommaScore(Managers.Data.score); 
    }
}
