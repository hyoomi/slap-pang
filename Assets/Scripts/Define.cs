using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    public enum UIEvent
    {
        Click,
        Drag,
    }
}
