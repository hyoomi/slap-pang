using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

// Sound를 컨트롤하는 Manager
public class SoundManager
{
    public AudioMixer master;

    AudioSource[] _audioSources = new AudioSource[(int)Enum.GetNames(typeof(Define.Sound)).Length];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();
    //AudioClip[] comboSounds = new AudioClip[4];
    GameObject LobbyBGM, GameBGM,  combo1, combo2, combo3, combo4, GameOver;


    public void Init()
    {
        LobbyBGM = Resources.Load<GameObject>("Prefabs/LobbyBGM");
        GameBGM = Resources.Load<GameObject>("Prefabs/GameBGM");
        combo1 = Resources.Load<GameObject>("Prefabs/combo_1");
        combo2 = Resources.Load<GameObject>("Prefabs/combo_2");
        combo3 = Resources.Load<GameObject>("Prefabs/combo_3");
        combo4 = Resources.Load<GameObject>("Prefabs/combo_4");
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
       
        _audioClips.Add("combo1", combo1.GetComponent<AudioSource>().clip);
        _audioClips.Add("combo2", combo2.GetComponent<AudioSource>().clip);
        _audioClips.Add("combo3", combo3.GetComponent<AudioSource>().clip);
        _audioClips.Add("combo4", combo4.GetComponent<AudioSource>().clip);
        

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
         _audioClips.Clear();

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

    public void GameEnd(){
        _audioSources[(int)Define.Sound.Bgm].Stop();
        _audioSources[(int)Define.Sound.Bgm].PlayOneShot(GameOver.GetComponent<AudioSource>().clip);
    }

    public void PlaybyScene(){
        if(Managers.Scene.CurrentScene.SceneType == Define.Scene.Lobby){
            _audioSources[(int)Define.Sound.Bgm].clip = LobbyBGM.GetComponent<AudioSource>().clip;
            _audioSources[(int)Define.Sound.Bgm].Play();
        }
        else if(Managers.Scene.CurrentScene.SceneType == Define.Scene.Game){
            _audioSources[(int)Define.Sound.Bgm].clip = GameBGM.GetComponent<AudioSource>().clip;

            _audioSources[(int)Define.Sound.Bgm].Play();
        }
    }
}
