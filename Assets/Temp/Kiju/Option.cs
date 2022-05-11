using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Option : MonoBehaviour
{
    public AudioMixer master;
    public Slider slider;


    public void Panel_Set(){ // 옵션 팝업 열 때 슬라이더를 볼륨값에 맞게 위치해주는 함수이나 한번에 작동하지 않는중입니다...
        slider.value = PlayerPrefs.GetFloat("Volume");
    }
    
    public void Panel_Set_Save(){ //옵션 저장할 때 Onclick에서 호출
        PlayerPrefs.Save();
        //Debug.Log(slider.value + "로 저장");
    }

    public void Panel_Set_No_Save(){ //옵션 저장하지 않을 때 Onclick에서 호출
        slider.value = PlayerPrefs.GetFloat("Volume");
        //Debug.Log(slider.value+ "로 복귀");
    }

    public void Sound_Control(){ //master 볼륨값을 슬라이더값에 맞게 조정
        float volume = slider.value;
        //Debug.Log(slider.value+ "," + PlayerPrefs.GetFloat("Volume"));
        if(volume == -40f){
            master.SetFloat("Master", -80);
        }
        else{
            master.SetFloat("Master", volume);
        }
        PlayerPrefs.SetFloat("Volume", slider.value);
    }

    void Start()
    {
        
    }

    void Update()
    {

    }
    
}
