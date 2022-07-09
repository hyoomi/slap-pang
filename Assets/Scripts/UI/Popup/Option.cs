using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Option : MonoBehaviour
{
    public Slider slider;
    [SerializeField] Text bestScore;

    public void Panel_Set_Save(){ //옵션 저장할 때 Onclick에서 호출
        PlayerPrefs.Save();
    }

    public void Sound_Control(){ //master 볼륨값을 슬라이더값에 맞게 조정
        float volume = slider.value;
        if(volume == -40f){
            Managers.Sound.master.SetFloat("Master", -80);
        }
        else{
            Managers.Sound.master.SetFloat("Master", volume);
        }
        PlayerPrefs.SetFloat("Volume", slider.value);
    }

    private void OnEnable()
    {
        bestScore.text = "최고 기록: " + Managers.Data.ColorScore(Managers.Data.BEST_SCORE) + "점";
    }

    void Update()
    {
        slider.value = PlayerPrefs.GetFloat("Volume");
    }
    
}
