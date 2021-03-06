using UnityEngine;

// 최고기록을 저장하고 불러오는 Manager
public class DataManager
{    
    const int N = 70;

    public Define.GameState PastState = Define.GameState.Idle;  // 이전 GameState
    public Define.GameState GameState = Define.GameState.Idle;  // 지금 GameState
    public int p_state;

    #region Property

    // 최고기록
    ulong _bestScore;
    public ulong BEST_SCORE
    {
        get { return _bestScore; }
        set
        {
            if(_bestScore < value)
            {
                _bestScore = value;
                Bestscore.Save();
            }
        }
    }

    // 콤보. 연속적으로 공이 터짐 (1부터 시작)
    int _combo;
    public int COMBO
    {
        get { return _combo; }
        set
        {         
            if (value == 0) { _combo = 0; return; }

            if (_combo > 0)
            {
                Managers.Action.ComboAction(_combo);
            }
            _combo++;
        }
    }

    // 점수
    ulong _score;
    public ulong SCORE
    {
        get { return _score; }
        set 
        {
            _score += value;
            if (_score >= 999999999999999) 
            { 
                _score = 999999999999999;
                BEST_SCORE = _score;
                return; 
            }

            BEST_SCORE = _score;

            if (_score < 1000)
                Section = 0;
            else if (_score >= 1000 && _score < 10000)
                Section = 1;
            else if (_score >= 10000 && _score < 100000)
                Section = 2;
            else if (_score >= 100000 && _score < 1000000)
                Section = 3;
            else if (_score >= 1000000 && _score < 10000000)
                Section = 4;
            else if (_score >= 10000000 && _score < 100000000)
                Section = 5;
            else if (_score >= 100000000 && _score < 300000000)
                Section = 6;
            else if (_score >= 300000000 && _score < 700000000)
                Section = 7;
            else if (_score >= 700000000 && _score < 1000000000)
                Section = 8;
            else if (_score >= 700000000 && _score < 2000000000)
                Section = 9;
            else if (_score >= 200000000 && _score < 4000000000)
                Section = 10;
            else
                Section = 11;

        }
    }

    // 점수 구간 (0 ~ 7)
    int _section; 
    public int Section
    {
        get { return _section; }
        set
        {
            if (value == _section) return;
            _section = value;
            Managers.Action.SectionAction(_section);
            p_state = _section - 1;
            if (p_state < 0) p_state = 0;
        }
    }

    // 구간별 가중치 P
    int[] _p = new int[] { 3, 10, 120, 240, 700, 1500, 3000, 7000, 14000, 21000, 33000, 42000};
    public int P => _p[_section];

    #endregion

    // 초기화
    public void Init()
    {
        PastState = Define.GameState.None;
        GameState = Define.GameState.None;
        _bestScore = 0;
        BEST_SCORE = Bestscore.Load().bestScore;
        _combo = 0;
        _score = 0;
        _section = 0;
    }

    // 점수 계산
    public void ExplodedBallSet(int explode)
    {
        ulong tmpSCORE = 0;

        tmpSCORE = (ulong)(explode * (explode - 3) * N * P);

        if (COMBO == 0)
            SCORE = tmpSCORE;
        else if (COMBO == 1) // 1콤보
            SCORE = tmpSCORE * 3;
        else if (COMBO == 2) // 2콤보
            SCORE = tmpSCORE * 5;
        else // 3콤보 이상
            SCORE = tmpSCORE * 8;
       
    }

    public void ExplodeBomb(int explode)
    {
        ulong tmpSCORE = 0;
        if (explode < 4)
        {
            tmpSCORE = (ulong)(explode * N * P);
        }
        else
        {
            tmpSCORE = (ulong)(explode * (explode - 3) * N * P);
        }

        SCORE = tmpSCORE;
    }

    // 점수에 색깔 입히기
    string[] colorCode = new string[] {
        "<color=#0000FF>",
        "<color=#228B22>",
        "<color=#FF4700>",
        "<color=#FF0000>",
        "<color=#FF0000>",
    };
    string endCode = "</color>";

    public string ColorScore(ulong data)
    {
        string colorText = (data == 0) ? "0" : string.Format($"{{0:#,#}}", data); // 1000단위로 콤마를 찍어 string으로 저장
        colorText += endCode; // "890,123</color>"
        int j = 0;  // 몇번째 컬러코드를 사용할지 체크하는 변수
        for (int i = colorText.Length - 1; i >= 0; i--)  // 문자열 맨 뒤에서부터 검사
        {
            if (colorText[i] == ',')  // 콤마를 발견했다면
            {
                if (j + 1 == colorCode.Length) { break; }
                colorText = colorText.Insert(i + 1, colorCode[j++]);  //콤마뒤에 컬러코드 삽입 "890,<color=#0000CD>123</color>"
                colorText = colorText.Insert(i + 1, endCode);  // 콤마뒤에 end코드 삽입 "890,</color><color=#0000CD>123</color>"       
            }
        }
        colorText = colorCode[j] + colorText;  // 가장 앞에 컬러코드 삽입 "<color=#006400>890,</color><color=#0000CD>123</color>" 
        return colorText;
    }

    public void Clear()
    {
        PastState = Define.GameState.Idle;
        GameState = Define.GameState.Idle;
        _bestScore = 0;
        BEST_SCORE = Bestscore.Load().bestScore;
        _combo = 0;
        _score = 0;
        _section = 0;
    }
}
