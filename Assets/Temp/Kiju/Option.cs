using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Option : MonoBehaviour
{
    public Slider slider;
    
    public void Panel_Set_Save(){ //옵션 저장할 때 Onclick에서 호출
        PlayerPrefs.Save();
        //Debug.Log(slider.value + "로 저장");
    }

    public void Sound_Control(){ //master 볼륨값을 슬라이더값에 맞게 조정
        float volume = slider.value;
        //Debug.Log(slider.value+ "," + PlayerPrefs.GetFloat("Volume"));
        if(volume == -40f){
            Managers.Sound.master.SetFloat("Master", -80);
        }
        else{
            Managers.Sound.master.SetFloat("Master", volume);
        }
        PlayerPrefs.SetFloat("Volume", slider.value);
    }

    void Start()
    {
        
    }

    void Update()
    {
        slider.value = PlayerPrefs.GetFloat("Volume");
    }
    
}
