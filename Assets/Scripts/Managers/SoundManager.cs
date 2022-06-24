using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using System;

// Sound를 컨트롤하는 Manager
public class SoundManager
{
    public AudioMixer master;

    bool[] distinctSound = new bool[5];
    AudioSource[] _audioSources = new AudioSource[(int)Enum.GetNames(typeof(Define.Sound)).Length];
    AudioSource MarbleAS, StoneAS;
    GameObject  Button, LobbyBGM, GameBGM,  combo1, combo2, combo3, combo4, combo5, Stone, Marble, GameOver;


    public void Init()
    {
        Button = Resources.Load<GameObject>("Prefabs/Button1");
        LobbyBGM = Resources.Load<GameObject>("Prefabs/LobbyBGM");
        GameBGM = Resources.Load<GameObject>("Prefabs/GameBGM");
        combo1 = Resources.Load<GameObject>("Prefabs/combo_1");
        combo2 = Resources.Load<GameObject>("Prefabs/combo_2");
        combo3 = Resources.Load<GameObject>("Prefabs/combo_3");
        combo4 = Resources.Load<GameObject>("Prefabs/combo_4");
        combo5 = Resources.Load<GameObject>("Prefabs/combo_5");
        Stone = Resources.Load<GameObject>("Prefabs/Stone");
        Marble = Resources.Load<GameObject>("Prefabs/Marble");
        GameOver = Resources.Load<GameObject>("Prefabs/GameEnd");

        master = Resources.Load<AudioMixer>("Master");

        GameObject root = GameObject.Find("@Sound");
        if (root == null) 
        {
            root = new GameObject { name = "@Sound" };
            UnityEngine.Object.DontDestroyOnLoad(root);

        string[] soundNames = System.Enum.GetNames(typeof(Define.Sound)); // "Bgm", "Effect"
            for (int i = 0; i < soundNames.Length; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }
       
        explodeInit();

        MarbleAS = _audioSources[(int)Define.Sound.Marble];
        StoneAS = _audioSources[(int)Define.Sound.Stone];

        _audioSources[(int)Define.Sound.Bgm].outputAudioMixerGroup = master.FindMatchingGroups("BGM")[0];
        for(int i = (int)Define.Sound.Effect; i <= (int)Define.Sound.Combo; i++)
        {
        _audioSources[i].outputAudioMixerGroup = master.FindMatchingGroups("Effect")[0];
        }
        _audioSources[(int)Define.Sound.Bgm].loop = true; // bgm 재생기는 무한 반복 재생

        }
    }

    public void Stop()
    {
        foreach (AudioSource audioSource in _audioSources) //오디오재생 멈추고 재생기에서 음원 제거
        {
            audioSource.clip = null;
            audioSource.Stop();
        }

    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources) //오디오재생 멈추고 재생기에서 음원 제거
        {
            audioSource.clip = null;
            audioSource.Stop();
        }

    }

    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f) // 디폴트매개변수는 효과음
	{
        if (audioClip == null)
            return;

		if (type == Define.Sound.Bgm) // BGM 배경음악 재생
		{
			AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
			if (audioSource.isPlaying)
				audioSource.Stop();

			audioSource.pitch = pitch;
			audioSource.clip = audioClip;
			audioSource.Play();
		}
		else // Effect 효과음 재생
		{
			AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
			audioSource.pitch = pitch;
			audioSource.PlayOneShot(audioClip);
		}
	}
    
    public void PlaybyScene(){
        if(Managers.Scene.CurrentScene.SceneType == Define.Scene.Lobby)
            Play(LobbyBGM.GetComponent<AudioSource>().clip, Define.Sound.Bgm);
        else if(Managers.Scene.CurrentScene.SceneType == Define.Scene.Game)
            Play(GameBGM.GetComponent<AudioSource>().clip, Define.Sound.Bgm);
    }

    public void buttonSound()
    {
         _audioSources[(int)Define.Sound.Effect].PlayOneShot(Button.GetComponent<AudioSource>().clip);
    }

    public void explodeSound(Define.BallType type)
    {
        if(type == Define.BallType.Cup && distinctSound[0]){
            MarbleAS.PlayOneShot(Marble.GetComponent<AudioSource>().clip);
            distinctSound[0] = false;
        }
        else if(type == Define.BallType.Glass && distinctSound[1]){
            MarbleAS.PlayOneShot(Marble.GetComponent<AudioSource>().clip);
            distinctSound[1] = false;
        }
        else if(type == Define.BallType.Plant && distinctSound[2]){
            MarbleAS.PlayOneShot(Marble.GetComponent<AudioSource>().clip);
            distinctSound[2] = false;
        }
        else if(type == Define.BallType.Plate && distinctSound[3]){
            MarbleAS.PlayOneShot(Marble.GetComponent<AudioSource>().clip);
            distinctSound[3] = false;
        }
        else if(type == Define.BallType.Stone && distinctSound[4]){
            StoneAS.PlayOneShot(Stone.GetComponent<AudioSource>().clip);
            distinctSound[4] = false;
        }
    }

    public void comboSound(int combo)
    {
        AudioSource comboAudio = _audioSources[(int)Define.Sound.Combo];
        switch(combo)
        {
            case 0:
                break;
            case 1:
                comboAudio.clip = combo1.GetComponent<AudioSource>().clip;
                comboAudio.Play();
                break;
            case 2:
                comboAudio.clip = combo2.GetComponent<AudioSource>().clip;
                comboAudio.Play();
                break;
            case 3:
                comboAudio.clip = combo3.GetComponent<AudioSource>().clip;
                comboAudio.Play();
                break;
            case 4:
                comboAudio.clip = combo4.GetComponent<AudioSource>().clip;
                comboAudio.Play();
                break;
            default:
                comboAudio.clip = combo5.GetComponent<AudioSource>().clip;
                comboAudio.Play();
                break;
        }
    }

    public void explodeInit(Define.SlideDir slide = Define.SlideDir.None){
        distinctSound = new bool[] {true, true, true, true, true};
    }

    public void GameEnd(){
        _audioSources[(int)Define.Sound.Bgm].Stop();
        _audioSources[(int)Define.Sound.Effect].PlayOneShot(GameOver.GetComponent<AudioSource>().clip);
    }

}
