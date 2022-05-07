using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
- <게임 시작3>에서 [GAME!] 문구가 뜬 후 2초뒤 [START]로 바꿔주기 (코루틴 사용)
- [일시정지]버튼 클릭시 빈 팝업 띄우기 
*/

public class GameStart_3 : MonoBehaviour
{
    [SerializeField] Text GameStartText;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartPrint());
    }

    IEnumerator StartPrint()
    {
        yield return new WaitForSeconds(2.0f);
        GameStartText.text = "START";
        // Change UI into Start Pop up 
        Debug.Log("executed coroutine");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
