using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{   
    Text scoreText;
    
    // rich text color 지정을 위한 코드 문자열을 배열로 선언하였습니다.
    // 배열 인덱스 0 ~ 5까지 순서대로 "파랑색, 초록색, 주황색, 노랑색, 빨간색" 컬러 코드가 들어있습니다.
    
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
        scoreText.text = Managers.Data.ColorScore(Managers.Data.SCORE); 
    }
}
