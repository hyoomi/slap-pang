using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameoverScript : PopupUI
{
    public void ReStartButton()
    {
        Managers.Scene.LoadScene(Define.Scene.Game);
    }
}
