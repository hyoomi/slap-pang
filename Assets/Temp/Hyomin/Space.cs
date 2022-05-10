using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab; // 공 프리펩
    [SerializeField] public CellController[] allCells; // 총 80개의 cell
    int ballCount; // 공의 총 개수
    public Define.SlideAction _slide; // 지금 발생중인 슬라이드 행위를 체크
    public Define.SlideAction Slide
    {
        get { return _slide; }
        set
        {
            if (_slide == value)
                return;
            _slide = value;
        }
    }
    void Start()
    {
        ballCount = 0;
        //filled.Initialize(); // 배열 초기화

        // 구슬 두줄
        for(int i = 0; i < 16; i++)
        {
            ballCount++;
            GameObject tempBall = Instantiate(ballPrefab, allCells[79 - i].transform);
        }
    }

    // 키를 입력받습니다
    void Update()
    {          
        if (Input.GetKeyDown(KeyCode.A)) // A키 누르면 콤보 터짐
        {
            Pop();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) // 위 화살표 누르면 Up 슬라이드로 간주
        {

            _slide = Define.SlideAction.Up;
            Managers.Action.slideAction(_slide);
            SlideUp();
            SpawnBall();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _slide = Define.SlideAction.Down;
            Managers.Action.slideAction(_slide);
            SlideDown();
            SpawnBall();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _slide = Define.SlideAction.Left;
            Managers.Action.slideAction(_slide);
            SlideLeft();
            SpawnBall();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _slide = Define.SlideAction.Right;
            Managers.Action.slideAction(_slide);
            SlideRight();
            SpawnBall();
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

        // 비어있는 cell을 찾을 때까지 while문을 돌립니다
        while (true)
        {
            spawnCell = UnityEngine.Random.Range(0, allCells.Length); // 0~80사이의 랜덤값 추출
            if (allCells[spawnCell].transform.childCount == 0) // 해당 cell이 비어있다면 while문 탈출
            {
                break;
            }
        }

        GameObject tempBall = Instantiate(ballPrefab, allCells[spawnCell].transform); // 해당 cell을 부모로 하여 ball을 instantiate합니다
        ballCount++; // 공 개수를 하나 늘립니다
    }

    // 위로 슬라이드 할 경우
    // 동작 원리: 가장 위에있는 빈칸을 선택. 그 빈칸의 아래 칸들을 쭉 살피다가 공이 들어있는 칸을 발견하면 그 공을 선택된 빈칸으로 옮겨준다
    // 빈칸을 위에서부터 아래로 검사해야만 위쪽에 있는 빈칸쪽으로 공을 모을 수 있다.
    public void SlideUp()
    {
        for (int j = 0; j < 80; j++)
        {
            if (allCells[j].transform.childCount == 0)
            {
                int firstBall = FindFirstBall_Up(j);
                if (firstBall != -1) // 공이 들어있는 Cell의 인덱스를 발견했다면
                {
                    allCells[firstBall].GetComponentInChildren<Ball>().SetParent(allCells[j].transform);
                }
            }
        }
    }

    public void SlideDown()
    {
        for (int j = 79; j >= 0; j--)
        {
            if (allCells[j].transform.childCount == 0)
            {
                int firstBall = FindFirstBall_Down(j);

                if (firstBall != -1) // 공 발견하면
                {
                    allCells[firstBall].GetComponentInChildren<Ball>().SetParent(allCells[j].transform);
                }
            }
        }
    }

    public void SlideLeft()
    {    
        for (int j = 0; j < 80; j++)
        {
            if (allCells[j].transform.childCount == 0)
            {
                int firstBall = FindFirstBall_Left(j);
                if (firstBall != -1) // 공 발견하면
                {    
                    allCells[firstBall].GetComponentInChildren<Ball>().SetParent(allCells[j].transform);
                }
            }
        }
    } 

    public void SlideRight()
    {
        for (int j = 79; j >= 0; j--)
        {
            if (allCells[j].transform.childCount == 0)
            {
                int firstBall = FindFirstBall_Right(j);

                if (firstBall != -1) // 공 발견하면
                {
                    allCells[firstBall].GetComponentInChildren<Ball>().SetParent(allCells[j].transform);
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
            if (allCells[i].transform.childCount > 0)
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
            if (allCells[i].transform.childCount > 0)
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
            if (allCells[i].transform.childCount > 0)
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
            if (allCells[i].transform.childCount > 0)
                return i;
        }
        return -1;
    }

    int popnumber = 0;
    Stack<Ball> ballList = new Stack<Ball>();

    
    public void Pop()
    {
        // 모든 셀 콤보 검사
        for (int i = 0; i < allCells.Length; i++)
        {
            if (allCells[i].transform.childCount == 0) continue;
            Ball ball = allCells[i].GetComponentInChildren<Ball>();
            if (ball.State == Define.BallState.Check) continue;
           
            popnumber = 1;
            ball.State = Define.BallState.Check;
            ballList.Push(ball); // 1 콤보 ball 저장

            // 왼오위아래 공이랑 같은 타입인지 체크 (콤보 측정중)
            if ((i - 1) % 8 != 7) // 이전줄로 올라가는거 방지
                PopBall(i - 1, ball.Type); 
            if ((i + 1) % 8 != 0) // 다음줄로 넘어가는거 방지
                PopBall(i + 1, ball.Type);
            PopBall(i - 8, ball.Type);
            PopBall(i + 8, ball.Type);

            Debug.Log(i + "의 콤보는 " + popnumber);

            // 4 콤보 미만이면 stack에서 pop 시킨다
            if (popnumber < 4 && ballList.Count > 0)
            {
                for (int j = 0; j < popnumber; j++)
                {
                    ballList.Pop();             
                }
            }
        }

        // 4 콤보 이상이면 공 터뜨리자
        int count = ballList.Count;
        for(int i = 0; i < count; i++)
        {
            Ball ball = ballList.Pop();
            ball.State = Define.BallState.Explode;
            ballCount--;
            Destroy(ball.gameObject);
            Destroy(ball);
        }
        Debug.Log(count+ballList.Count);

    }
    
    // index의 콤보 검사하는 함수
    public void PopBall(int index, Define.BallType type)
    {
        if (index < 0 || index >= allCells.Length)
            return;
        if (allCells[index].transform.childCount == 0)
            return;
        Ball ball = allCells[index].GetComponentInChildren<Ball>();
        if (ball.State == Define.BallState.Check)
            return;
        if (type != ball.Type)
            return;

        // 콤보 적립
        popnumber++;
        ballList.Push(ball); // 콤보 ball 저장
        ball.State = Define.BallState.Check; // 체크한 것 표시

        // 왼오위아래 공 콤보 검사
        if ((index - 1) % 8 != 7) // 이전줄로 올라가는거 방지
            PopBall(index - 1, type);
        if ((index + 1) % 8 != 0) // 다음줄로 넘어가는거 방지
            PopBall(index + 1, type);
        PopBall(index - 8, type);
        PopBall(index + 8, type);                   
    }
}
