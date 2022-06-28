using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : PopupUI
{
    [SerializeField]
    List<GameObject> TutorialPopup;
    int page;

    private void Start()
    {
        page = 1;

        foreach (var tut in TutorialPopup){
            tut.SetActive(false);
        }

        TutorialPopup[0].SetActive(true);
    }

    public void NextTutorial()
    {
        if(page >= TutorialPopup.Count)
        {
            ClosePopup();
            return;
        }

        TutorialPopup[page].SetActive(true);
        page++;
    }
}
