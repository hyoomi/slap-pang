using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Option : MonoBehaviour
{
    private bool toggle = false;
    public GameObject panel;
    public AudioMixer master;
    public Slider slider;
    
    public void Panel_Set(){
        toggle = !toggle;
        panel.SetActive(toggle);
    }

    public void Sound_Control(){
        float volume = slider.value;
        if(volume == -40f){
            master.SetFloat("Master", -80);
        }
        else{
            master.SetFloat("Master", volume);
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
}
