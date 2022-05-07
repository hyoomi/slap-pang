using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : BaseUI
{
    public void StartButton()
    {
        Managers.Scene.LoadScene(Define.Scene.Game);
    }
}
