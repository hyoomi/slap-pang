using UnityEngine;
using UnityEngine.UI;


public class GameoverScript : PopupUI
{
    [SerializeField] Text bestScore;

    public void OnEnable()
    {
        bestScore.text = Managers.Data.ColorScore(Managers.Data.SCORE) + "점!";
    }
}
