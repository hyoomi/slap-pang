using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// (0) 슬라이드가 입력되면 -- Update();
// (1) ball 부모 바꿔주기 -- SlideUp();
// (2) 슬라이드 Action발생 (Ball 움직이기) -- Managers.Action.Slide = Define.SlideAction.Up;  
// (3) 새로운 ball 스폰 -- SpawnBall();
// (4) Ball.State==Explode체크하고 Ball 콤보 터뜨리기 -- Pop();

public class Space : MonoBehaviour
{
    const int ROW = 10; //(가로 10줄)
    const int COL = 8; //(세로 8줄) 
    const int DEF_CHILD = 0; // Cell의 기본 자식 수
    int COMBO = 4; // 4콤보 이상이면 터뜨린다. 
    int error = 0; // 무한루프 방지 임시 변수

    [SerializeField] GameObject ballPrefab; // 공 프리펩
    [SerializeField] GameObject bombPrefab; // 폭탄 프리펩
    [SerializeField] public Transform[] allCells; // 총 80개의 cell

    public static int ballCount; // 공의 총 개수를 업데이트 할 변수. 오류가 예상됨. 수정 예정
    bool gameover;

    #region Unity
    private void OnEnable()
    {
        Managers.Action.slideAction += OnSlide;
        Managers.Action.comboAction += ReduceBombTimer;
        Managers.Action.slideAction += LastChanceBomb;
        Managers.Action.sectionAction += SectionBomb;
        Managers.Action.clickedBomb += ExplodeBomb;       
        Managers.Action.comboAction += Managers.Sound.comboSound;      
    }

    private void OnDisable()
    {
        Managers.Action.slideAction -= OnSlide;
        Managers.Action.comboAction -= ReduceBombTimer;
        Managers.Action.slideAction -= LastChanceBomb;
        Managers.Action.sectionAction -= SectionBomb;
        Managers.Action.clickedBomb -= ExplodeBomb;       
        Managers.Action.comboAction -= Managers.Sound.comboSound;       
    }

    void Start()
    {
        //변수 초기화
        COMBO = 4;
        ballCount = 0;
        Playtime = 30f;
        lastChanceBomb_count = 0;
        gameover = false;
        _gameState = Define.GameState.Idle;

        // 게임 시작시 구슬 두줄 배치
        for (int i = 0; i < COL * 2; i++)
        {
            ballCount++;

            GameObject go = Instantiate(ballPrefab, allCells[allCells.Length - 1 - i]); // 해당 cell을 부모로 하여 ball을 instantiate합니다
            go.GetComponent<BallAndBomb>().CellIndex = allCells.Length - 1 - i;
        }
        
        TimeBomb();  //TimeBomb의 Timer 시작
    }

    void Update()
    {
        UpdateController();
    }
    #endregion


    #region Game_State_Control

    // GameState 상태도
    // Idle (GetInput)
    // Move -> Idle (SpawnFourBall) -> Spawn
    // Spawn -> Explode (ExplodeBall)
    // Explode -> Idle (Gameover || GetInput)
    //  Move판정은 OnSlide함수 내부에 존재함

    Define.GameState _pastState = Define.GameState.Idle;
    Define.GameState _gameState = Define.GameState.Idle;
    void UpdateController()
    {
        _pastState = _gameState;

        if (_pastState == Define.GameState.Idle)
        {
            GetInput();
        }
        else if(_pastState == Define.GameState.Move)
        {           
            _gameState = CheckGameState();
            if(_gameState == Define.GameState.Idle)
            {
                SpawnFourBall();
                _gameState = Define.GameState.Spawn;
            }
        }
        else if (_pastState == Define.GameState.Spawn)
        {           
            ExplodeBall();
            _gameState = Define.GameState.Explode;
        }
        else if (_pastState == Define.GameState.Explode)
        {
            _gameState = CheckGameState();

            if (_gameState == Define.GameState.Idle)
            {
                if (gameover)
                {
                    Managers.UI.LoadUI<PopupUI>("GameoverPopup");
                    _gameState = Define.GameState.Gameover;
                    return;
                }
                GetInput();
            }
        }
        else if (_pastState == Define.GameState.Gameover)
        {            
            return;
        }
    }

    // 키보드를 입력받는 함수
    void GetInput()
    {       
        if (Input.GetKeyDown(KeyCode.UpArrow))
            Managers.Action.SlideAction(Define.SlideDir.Up);
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            Managers.Action.SlideAction(Define.SlideDir.Down);
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            Managers.Action.SlideAction(Define.SlideDir.Left);
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            Managers.Action.SlideAction(Define.SlideDir.Right);
        else
            Managers.Action.SlideAction(Define.SlideDir.None);
    }

    // GameState를 체크하는 함수
    Define.GameState CheckGameState()
    {
        for(int i = 0; i < allCells.Length; i++)
        {
            Ball ball = allCells[i].GetComponentInChildren<Ball>();
            if (ball == null) continue;
            if (ball.State == Define.BallState.Move) return Define.GameState.Move;
            if (ball.State == Define.BallState.Explode) return Define.GameState.Explode;
        }

        return Define.GameState.Idle;
    }
    #endregion

    public void SpawnFourBall()
    {
        SpawnBall();
        SpawnBall();
        SpawnBall();
        SpawnBall();
    }

    // 공 스폰
    public void SpawnBall()
    {        
        if (ballCount >= allCells.Length)  // Cell이 꽉 찼으니 공을 스폰하지 않습니다
        {
            if (!gameover)
            { gameover = true;  }             
            return;
        }

        int spawnCell; // 공을 스폰할 Cell index를 받는 변수       
        while (true)  // 비어있는 cell을 찾을 때까지 while문을 돌립니다
        {            
            spawnCell = UnityEngine.Random.Range(0, allCells.Length); // 0~80사이의 랜덤값 추출
            if (allCells[spawnCell].childCount == DEF_CHILD) // 해당 cell이 비어있다면 while문 탈출
                break;
            if (++error > 10000000)  // 무한루프 error 처리
            { 
                Debug.Log(ballCount + "Cell error");  
                error = 0; ballCount = allCells.Length;
                _gameState = Define.GameState.Idle; // <-- 테스트 필요
                return; }
        }

        GameObject go = Instantiate(ballPrefab, allCells[spawnCell]); // 해당 cell을 부모로 하여 ball instantiate
        go.GetComponent<BallAndBomb>().CellIndex = spawnCell;
        ballCount++; // 공 개수를 하나 늘립니다.
        
    }

    // 슬라이드 액션을 인식
    public void OnSlide(Define.SlideDir slide)
    {
        _gameState = Define.GameState.Move;
        switch (slide)
        {
            case Define.SlideDir.Up: SlideUp(); break;
            case Define.SlideDir.Down: SlideDown(); break;
            case Define.SlideDir.Left: SlideLeft(); break;
            case Define.SlideDir.Right: SlideRight(); break;
        }
    }

    #region Move_Ball
    // 위로 슬라이드 할 경우
    // 동작 원리: 가장 위에있는 빈칸을 선택. 그 빈칸의 아래 칸들을 쭉 살피다가 공이 들어있는 칸을 발견하면 그 공을 선택된 빈칸으로 옮겨준다
    // 빈칸을 위에서부터 아래로 검사해야만 위쪽에 있는 빈칸쪽으로 공을 모을 수 있다.
    public void SlideUp()
    {
        for (int j = 0; j < allCells.Length; j++)
        {
            if (allCells[j].childCount == DEF_CHILD)
            {
                int firstBall = FindFirstBall_Up(j);
                if (firstBall != -1) // 공이 들어있는 Cell의 인덱스를 발견했다면
                {
                    allCells[firstBall].GetComponentInChildren<BallAndBomb>().Move(allCells[j], j);

                }
            }
        }
    }

    public void SlideDown()
    {
        for (int j = allCells.Length - 1; j >= 0; j--)
        {
            if (allCells[j].childCount == DEF_CHILD)
            {
                int firstBall = FindFirstBall_Down(j);

                if (firstBall != -1) // 공 발견하면
                {
                    allCells[firstBall].GetComponentInChildren<BallAndBomb>().Move(allCells[j], j);
                }
            }
        }
    }

    public void SlideLeft()
    {    
        for (int j = 0; j < allCells.Length; j++)
        {
            if (allCells[j].childCount == DEF_CHILD)
            {
                int firstBall = FindFirstBall_Left(j);
                if (firstBall != -1) // 공 발견하면
                {    
                    allCells[firstBall].GetComponentInChildren<BallAndBomb>().Move(allCells[j], j);
                }
            }
        }
    } 

    public void SlideRight()
    {
        for (int j = allCells.Length - 1; j >= 0; j--)
        {
            if (allCells[j].childCount == DEF_CHILD)
            {
                int firstBall = FindFirstBall_Right(j);

                if (firstBall != -1) // 공 발견하면
                {
                    allCells[firstBall].GetComponentInChildren<BallAndBomb>().Move(allCells[j], j);
                }
            }
        }
    }

    // 위로 슬라이드 할거니 아래쪽에 있는 가장 가까운 공 체크
    public int FindFirstBall_Up(int index)
    {
        int next = index + COL;

        if (next >= allCells.Length) return -1; // 가장 밑줄 범위 벗어나면 종료
        for (int i = next; i < allCells.Length; i += COL) // 한줄 조사. 위에서 아래로
        {
            if (allCells[i].childCount > DEF_CHILD)
                return i;
        }
        return -1;
    }

    // 아래로 슬라이드 할거니 위쪽에 있는 가장 가까운 공 체크
    public int FindFirstBall_Down(int index)
    {
        int next = index - COL;

        if (next < 0) return -1; // 가장 윗줄 범위 벗어나면 종료
        for (int i = next; i >= 0; i -= COL) // 한줄 조사. 아래서 위로
        {
            if (allCells[i].childCount > DEF_CHILD)
                return i;
        }
        return -1;
    }

    // 왼쪽으로 슬라이드 할거니 오른쪽에 있는 가장 가까운 공 체크
    public int FindFirstBall_Left(int index)
    {
        if (index % COL == COL - 1) return -1; // 가장 오른쪽에 있는 칸이면 종료
        int next = index + 1;
        for (int i = next; i % COL != 0; i += 1) // 다음 줄로 넘어가기 전까지 오른쪽에 있는 칸 조사
        {
            if (i >= allCells.Length) return -1;
            if (allCells[i].childCount > DEF_CHILD)
                return i;
        }
        return -1;
    }

    // 오른쪽으로 슬라이드 할거니 왼쪽에 있는 가장 가까운 공 체크
    public int FindFirstBall_Right(int index)
    {
        if (index % COL == 0) return -1; //가장 왼쪽에 있는 칸이면 종료
        int next = index - 1;
        for (int i = next; i % COL != COL - 1; i -= 1) // 이전 줄로 넘어가기 전까지 왼쪽에 있는 칸 조사
        {
            if (i < 0) return -1;
            if (allCells[i].childCount > DEF_CHILD)
                return i;
        }
        return -1;
    }
    #endregion

    #region Explode_Ball
    int popnumber = 0;
    Stack<Ball> ballList = new Stack<Ball>();

    public void ExplodeBall()
    {
        // 모든 셀 콤보 검사
        for (int i = 0; i < allCells.Length; i++)
        {
            Ball ball = allCells[i].GetComponentInChildren<Ball>();
            if (ball == null) continue;
            if (ball.State == Define.BallState.Check) continue;

            popnumber = 1;
            ball.State = Define.BallState.Check;
            ballList.Push(ball); // 1 콤보 ball 저장

            // 왼오위아래 공이랑 같은 타입인지 체크 (콤보 측정중)
            if ((i - 1) % COL != COL - 1) // 이전줄로 올라가는거 방지
                CheckBall(i - 1, ball.Type);
            if ((i + 1) % COL != 0) // 다음줄로 넘어가는거 방지
                CheckBall(i + 1, ball.Type);
            CheckBall(i - COL, ball.Type);
            CheckBall(i + COL, ball.Type);

            // 4 콤보 미만이면 stack에서 pop 시킨다
            if (popnumber < COMBO && ballList.Count > 0)
            {
                for (int j = 0; j < popnumber; j++)
                    ballList.Pop();
                continue;
            }

            // 4콤보 이상일때
            Managers.Data.ExplodedSet(popnumber);
        }
       
        int count = ballList.Count;
        Managers.Data.COMBO = count; // 콤보 이벤트 발생

        // 스택에 쌓인 공 터뜨리기
        for (int i = 0; i < count; i++)
        {
            Ball ball = ballList.Pop();
            ball.State = Define.BallState.Explode;
        }

        // check 초기화
        for (int i = 0; i < allCells.Length; i++)
        {
            Ball ball = allCells[i].GetComponentInChildren<Ball>();
            if (ball == null) continue;
            if (ball.State == Define.BallState.Check) ball.State = Define.BallState.Idle;

        }
    }

        // index의 콤보 검사하는 함수
        public void CheckBall(int index, Define.BallType type)
    {
        if (index < 0 || index >= allCells.Length)
            return;
        Ball ball = allCells[index].GetComponentInChildren<Ball>();
        if (ball == null) return;
        if (ball.State == Define.BallState.Check)
            return;
        if (type != ball.Type)
            return;

        // 콤보 적립
        popnumber++;
        ballList.Push(ball); // 콤보 ball 저장
        ball.State = Define.BallState.Check; // 체크한 것 표시

        // 왼오위아래 공 콤보 검사
        if ((index - 1) % COL != COL - 1) // 이전줄로 올라가는거 방지
            CheckBall(index - 1, type);
        if ((index + 1) % COL != 0) // 다음줄로 넘어가는거 방지
            CheckBall(index + 1, type);
        CheckBall(index - COL, type);
        CheckBall(index + COL, type);                   
    }
    #endregion

    #region Bomb
    // 폭탄을 스폰해주는 임시 함수
    public void SpawnBomb()
    {
        // Cell이 꽉 찼으니 공을 스폰하지 않습니다
        if (ballCount >= allCells.Length)
        {
            return;
        }

        int spawnCell; // 공을 스폰할 Cell index를 받는 변수

        // 비어있는 cell을 찾을 때까지 while문을 돌립니다
        while (true)
        {
            spawnCell = UnityEngine.Random.Range(0, allCells.Length); // 0~80사이의 랜덤값 추출
            if (allCells[spawnCell].childCount == DEF_CHILD) // 해당 cell이 비어있다면 while문 탈출
            {
                break;
            }
            if (++error > 10000000) { Debug.Log(ballCount+"Cell error"); error = 0; ballCount = allCells.Length;  return; }
        }

        GameObject go = Instantiate(bombPrefab, allCells[spawnCell]); // 해당 cell을 부모로 하여 ball을 instantiate합니다
        go.GetComponent<BallAndBomb>().CellIndex = spawnCell;
        ballCount++;
    }

    #region Bomb_Type
    // 시간에 따른 폭탄
    // 30초가 지나면 다음번 슬라이드 때 폭탄 생성
    // 2콤보 이상부터는 콤보당 -5초씩
    float Playtime = 30f;
    public void TimeBomb()
    {
        StartCoroutine(SpawnbombDelay());
    }

    IEnumerator SpawnbombDelay()
    {
        Playtime = 30f;
        float time = 0f;
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Playtime -= 1f;
            time += 1f;
            if(Playtime <= 0f)
            {
                //Debug.Log(time + "초만에 TimeBomb 생성!");
                SpawnBomb();
                TimeBomb();
                yield break;
            }
        }       
    }

    public void ReduceBombTimer(int combo)
    {
        if (combo >= 2)
            Playtime -= 5f;
    }

    // Last chance 폭탄
    // 게임오버 직전: 구슬이 80퍼 이상 찰 경우, [Todo]시간 초과할 경우..?
    // 1회 제공후에 Last Chance 폭탄은 제공되지 않는다.
    static int lastChanceBomb_count = 0; //여태 생성된 lastchancebomb 갯수
    public void LastChanceBomb(Define.SlideDir slide)
    { 
        if (ballCount >= 64 && lastChanceBomb_count < 1)
        {
            SpawnBomb();
            lastChanceBomb_count++;
            Debug.Log("LastChance Bomb 생성!");
        }
    }

    // 점수에 따른 폭탄
    // 구간이 변할 때마다 폭탄 1개씩 추가
    public void SectionBomb(int section)
    {
        SpawnBomb();
        Debug.Log("section bomb");

    }
    #endregion

    // 클릭한 폭탄이 터지는 함수
    public void ExplodeBomb(Bomb bomb)
    {
        int index = bomb.CellIndex;
        int tmp;

        // 주변 반경 2칸 폭발
        if (bomb.Type == Define.BombType.Near)
        {
            // i, i-1, i-9, i-8, i-7, i+1, i+9, i+8, i+7          
            int[] left = { -9, -1, 7 };
            int[] middle = { -8, 8 };
            int[] right = { -7, 1, 9 };
            for(int i = 0; i < left.Length; i++)
            {
                tmp = index + left[i];
                if (tmp < 0 || tmp >= allCells.Length) continue;

                if (tmp % COL == COL - 1) // 이전줄로 올라가는거 방지
                    continue;
                BallAndBomb bb = allCells[tmp].GetComponentInChildren<BallAndBomb>();
                if (bb != null && bb.State != Define.BallState.Explode)
                    bb.State = Define.BallState.Explode;                  
            }
            for(int i = 0; i < middle.Length; i++)
            {
                tmp = index + middle[i];
                if (tmp < 0 || tmp >= allCells.Length) continue;
                BallAndBomb bb = allCells[tmp].GetComponentInChildren<BallAndBomb>();
                if (bb != null && bb.State != Define.BallState.Explode)
                    bb.State = Define.BallState.Explode;
            }
            for(int i = 0; i < right.Length; i++)
            {
                tmp = index + right[i];
                if (tmp < 0 || tmp >= allCells.Length) continue;
                if (tmp % COL == 0) // 다음줄로 내려가는거 방지
                    continue;
                BallAndBomb bb = allCells[tmp].GetComponentInChildren<BallAndBomb>();
                if (bb != null && bb.State != Define.BallState.Explode)
                    bb.State = Define.BallState.Explode;
            }
        }

        // 같은 종료 폭탄 폭발
        else if(bomb.Type == Define.BombType.Same)
        {
            for (int i = 0; i < allCells.Length; i++)
            {
                if (i < 0 || i >= allCells.Length) continue;
                Ball ball = allCells[i].GetComponentInChildren<Ball>();
                if (ball != null && ball.Type == bomb.BallType && ball.State != Define.BallState.Explode)
                    ball.State = Define.BallState.Explode;
            }
        }

        // 가로 한줄 폭발
        else if (bomb.Type == Define.BombType.LeftRight)
        {
            for (int i = 1; i < COL; i++)
            {
                tmp = index - i;
                if (tmp < 0 || tmp >= allCells.Length) continue;
                if (tmp % COL == COL - 1) // 이전줄로 올라가는거 방지
                    break;
                BallAndBomb bb = allCells[tmp].GetComponentInChildren<BallAndBomb>();
                if (bb != null && bb.State != Define.BallState.Explode)
                    bb.State = Define.BallState.Explode;
            }
            for (int i = 1; i < COL; i++)
            {
                tmp = index + i;
                if (tmp < 0 || tmp >= allCells.Length) continue;
                if (tmp % COL == 0) // 다음줄로 내러가는거 방지
                    break;
                BallAndBomb bb = allCells[tmp].GetComponentInChildren<BallAndBomb>();
                if (bb != null && bb.State != Define.BallState.Explode)
                    bb.State = Define.BallState.Explode;
            }
        }

        // 세로 한줄 폭발
        else if (bomb.Type == Define.BombType.UpDown)
        {
            for (int i = 1; i < ROW; i++)
            {
                tmp = index - COL * i;
                if (tmp < 0 || tmp >= allCells.Length) continue;

                BallAndBomb bb = allCells[tmp].GetComponentInChildren<BallAndBomb>();
                if (bb != null && bb.State != Define.BallState.Explode)
                    bb.State = Define.BallState.Explode;
            }
            for (int i = 1; i < ROW; i++)
            {
                tmp = index + COL * i;
                if (tmp < 0 || tmp >= allCells.Length) continue;
                BallAndBomb bb = allCells[tmp].GetComponentInChildren<BallAndBomb>();
                if (bb != null && bb.State != Define.BallState.Explode)
                    bb.State = Define.BallState.Explode;
            }
        }
    }
    #endregion
}
