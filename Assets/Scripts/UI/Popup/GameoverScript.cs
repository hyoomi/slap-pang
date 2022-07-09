using UnityEngine;
using UnityEngine.UI;


public class GameoverScript : PopupUI
{
    [SerializeField] Text bestScore;

    public new void OnEnable()
    {
        base.OnEnable();
        Managers.Sound.GameEnd();
        bestScore.text = Managers.Data.ColorScore(Managers.Data.SCORE) + "점!";
    }

    public new void OnDisable()
    {
        base.OnDisable();
        Managers.Sound.Stop();
    }
}
