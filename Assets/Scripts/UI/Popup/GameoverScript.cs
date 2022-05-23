using UnityEngine;
using UnityEngine.UI;


public class GameoverScript : PopupUI
{
    [SerializeField] Text bestScore;

    public void OnEnable()
    {
        Managers.Sound.GameEnd();
        bestScore.text = Managers.Data.ColorScore(Managers.Data.SCORE) + "점!";
    }

    public void OnDisable()
    {
        Managers.Sound.Stop();
    }
}
