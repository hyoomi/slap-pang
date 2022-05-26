// enum을 모아놓은 class. ex) Define.Scene
public class Define
{
    public enum Scene
    {
        Unknown,
        Lobby,
        Game,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        Cup,
        Glass,
        Plant,
        Plate,
        Stone,
        Combo
    }

    public enum UIEvent
    {
        Click,
        Drag,
    }

    public enum GameState
    {
        Idle,
        Move,
        Spawn,
        Explode,
        Gameover,
        None,
    }

    public enum BallState
    {
        Idle,
        Move,
        Explode,
        Check,
    }

    public enum SlideDir
    {
        None,
        Up,
        Down,
        Left,
        Right,
    }

    public enum BallType
    {
        Cup,
        Glass,
        Plant,
        Plate,
        Stone,
    }

    public enum BombType
    {
        Near,
        Same,
        LeftRight,
        UpDown,
    }
}
