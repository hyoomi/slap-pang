using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceController : MonoBehaviour
{
    const int row = 8;
    const int col = 10;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] public CellController[] allCells;
    public int[] filled = new int[80];
    public int ballCount;
    public Define.SlideAction _slide;
    public static Action<Define.SlideAction> slideAction;

    void Start()
    {
        ballCount = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SpawnBall();

        if (Input.GetKeyDown(KeyCode.UpArrow))
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

    public void SpawnBall()
    {
        if (ballCount == 80)
        {
            Debug.Log("모든 Cell이 꽉 찼습니다.");
            return;
        }
        int spawnCell;

        while (true)
        {
            spawnCell = UnityEngine.Random.Range(0, allCells.Length);
            if (allCells[spawnCell].ball == null)
            {
                break;
            }
        }

        GameObject tempBall = Instantiate(ballPrefab, allCells[spawnCell].transform);
        BallController tempBallController = tempBall.GetComponent<BallController>();
        allCells[spawnCell].GetComponent<CellController>().ball = tempBallController;
        filled[spawnCell] = 1;
        ballCount++;
        // 랜덤한 Ball 이미지를 세팅할 것
    }    

    public void SlideUp()
    {
        for(int j = 0; j < 80; j++)
        {
            if (filled[j] == 0)
            {
                int firstBall = FindFirstBall_Up(j);
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
