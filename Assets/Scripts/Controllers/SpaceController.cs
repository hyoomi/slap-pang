using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceController : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab; // 공 프리펩
    [SerializeField] public CellController[] allCells; // 총 80개의 cell
    public int[] filled = new int[80]; // cell에 공이 들어있으면 1, 없으면 0 (임시변수입니다)
    public int ballCount; // 공의 총 개수
    public Define.SlideAction _slide; // 지금 발생중인 슬라이드 행위를 체크
    public static Action<Define.SlideAction> slideAction; // 슬라이드에 따른 액션 (일단 무시해주세요)

    void Start()
    {
        ballCount = 0;
    }

    // 키를 입력받습니다
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 스페이스 누르면 공 하나 스폰(임시)
            SpawnBall();

        if (Input.GetKeyDown(KeyCode.UpArrow)) // 위 화살표 누르면 Up 슬라이드로 간주
        {
            _slide = Define.SlideAction.Up;
            slideAction(_slide);
            SlideUp();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _slide = Define.SlideAction.Down;
            slideAction(_slide);
            SlideDown();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _slide = Define.SlideAction.Left;
            slideAction(_slide);
            SlideLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _slide = Define.SlideAction.Right;
            slideAction(_slide);
            SlideRight();
        }
        else
        {
            _slide = Define.SlideAction.None;
        }
    }

    // 공을 스폰합니다
    public void SpawnBall()
    {
        // Cell이 꽉 찼으니 공을 스폰하지 않습니다
        if (ballCount >= 80)
        {
            Debug.Log("모든 Cell이 꽉 찼습니다.");
            return;
        }

        int spawnCell; // 공을 스폰할 Cell index를 받는 변수

        // 비어있는 cell을 찾을 때까지 while문을 돌립니
        while (true)
        {
            spawnCell = UnityEngine.Random.Range(0, allCells.Length); // 0~80사이의 랜덤값 추출
            if (allCells[spawnCell].ball == null) // 해당 cell이 비어있다면 while문 탈출
            {
                break;
            }
        }

        GameObject tempBall = Instantiate(ballPrefab, allCells[spawnCell].transform); // 해당 cell을 부모로 하여 ball을 instantiate합니다
        BallController tempBallController = tempBall.GetComponent<BallController>(); 
        allCells[spawnCell].GetComponent<CellController>().ball = tempBallController; // 해당 cell에 방금 만든 ball을 담습니다
        filled[spawnCell] = 1; // 해당 cell이 찼다는것을 표시해줍니다
        ballCount++; // 공 개스를 하나 늘립니다
    }    

    // 위로 슬라이드 할 경우
    // 동작 원리: 가장 위에있는 빈칸을 선택. 그 빈칸의 아래 칸들을 쭉 살피다가 공이 들어있는 칸을 발견하면 그 공을 선택된 빈칸으로 옮겨준다
    // 빈칸을 위에서부터 아래로 검사해야만 위쪽에 있는 빈칸쪽으로 공을 모을 수 있다.
    public void SlideUp()
    {
        for(int j = 0; j < 80; j++)
        {
            if (filled[j] == 0)
            {
                int firstBall = FindFirstBall_Up(j);
                if (firstBall != -1) // 공이 들어있는 Cell의 인덱스를 발견했다면
                {
                    filled[j] = 1; filled[firstBall] = 0;
                    allCells[firstBall].ball.transform.SetParent(allCells[j].transform); // 공의 부모를 빈칸으로 바꿔준다
                    allCells[j].ball = allCells[firstBall].ball; // 빈칸에다가 공을 넣어준다
                    allCells[j].ball.transform.localPosition = Vector3.zero; // 공의 위치를 초기화. 그러면 변경된 cell 중심에 공이 배치된다
                    allCells[firstBall].ball = null; // 원래 공이 들어있던 칸은 공을 비워준다
                }
            }
        }
    }

    public void SlideDown()
    {
        for (int j = 79; j >= 0; j--)
        {
            if (filled[j] == 0)
            {
                int firstBall = FindFirstBall_Down(j);
                
                if (firstBall != -1) // 공 발견하면
                {
                    filled[j] = 1; filled[firstBall] = 0;
                    allCells[firstBall].ball.transform.SetParent(allCells[j].transform);
                    allCells[j].ball = allCells[firstBall].ball;
                    allCells[j].ball.transform.localPosition = Vector3.zero;
                    allCells[firstBall].ball = null;
                }
            }
        }
    }

    public void SlideLeft() {
        for (int j = 0; j < 80; j++)
        {
            if (filled[j] == 0)
            {
                int firstBall = FindFirstBall_Left(j);
                if (firstBall != -1) // 공 발견하면
                {
                    filled[j] = 1; filled[firstBall] = 0;
                    allCells[firstBall].ball.transform.SetParent(allCells[j].transform);
                    allCells[j].ball = allCells[firstBall].ball;
                    allCells[j].ball.transform.localPosition = Vector3.zero;
                    allCells[firstBall].ball = null;
                }
            }
        }
    }

    public void SlideRight()
    {
        for (int j = 79; j >= 0; j--)
        {
            if (filled[j] == 0)
            {
                int firstBall = FindFirstBall_Right(j);

                if (firstBall != -1) // 공 발견하면
                {
                    filled[j] = 1; filled[firstBall] = 0;
                    allCells[firstBall].ball.transform.SetParent(allCells[j].transform);
                    allCells[j].ball = allCells[firstBall].ball;
                    allCells[j].ball.transform.localPosition = Vector3.zero;
                    allCells[firstBall].ball = null;
                }
            }
        }
    }

    // 위로 슬라이드 할거니 아래쪽에 있는 가장 가까운 공 체크
    public int FindFirstBall_Up(int index)
    {     
        int next = index + 8;

        if (next >= 80) return -1; // 가장 밑줄 범위 벗어나면 종료
        for (int i = next; i < 80; i += 8) // 한줄 조사. 위에서 아래로
        {
            if (filled[i] == 1)
                return i;
        }
        return -1;
    }

    // 아래로 슬라이드 할거니 위쪽에 있는 가장 가까운 공 체크
    public int FindFirstBall_Down(int index)
    {
        int next = index - 8;

        if (next < 0) return -1; // 가장 윗줄 범위 벗어나면 종료
        for (int i = next; i >= 0; i -= 8) // 한줄 조사. 아래서 위로
        {
            if (filled[i] == 1)
                return i;
        }
        return -1;
    }

    // 왼쪽으로 슬라이드 할거니 오른쪽에 있는 가장 가까운 공 체크
    public int FindFirstBall_Left(int index)
    {
        if (index % 8 == 7) return -1; // 가장 오른쪽에 있는 칸이면 종료
        int next = index + 1;
        for (int i = next; i % 8 != 0; i += 1) // 다음 줄로 넘어가기 전까지 오른쪽에 있는 칸 조사
        {
            if (i >= 80) return -1;
            if (filled[i] == 1)
                return i;           
        }
        return -1;
    }
    
    // 오른쪽으로 슬라이드 할거니 왼쪽에 있는 가장 가까운 공 체크
    public int FindFirstBall_Right(int index)
    {
        if (index % 8 == 0) return -1; //가장 왼쪽에 있는 칸이면 종료
        int next = index - 1;
        for (int i = next; i % 8 != 7; i -= 1) // 이전 줄로 넘어가기 전까지 왼쪽에 있는 칸 조사
        {
            if (i < 0) return -1;
            if (filled[i] == 1)
                return i;
        }
        return -1;
    }
}
