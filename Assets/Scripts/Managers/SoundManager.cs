using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

// Sound를 컨트롤하는 Manager
public class SoundManager
{
    AudioSource[] _audioSources = new AudioSource[(int)Enum.GetNames(typeof(Define.Sound)).Length];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>(); //효과음 로드시 성능 향상을 위해 dictionary를 사용한다고 합니다.
    GameObject LobbyBGM;

    //AudioSource LobbyBGM = Resources.Load<AudioSource>("Prefabs/LobbyBGM");

    public void Init()
    {
        // if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == Managers.Scene.GetSceneName(Define.Scene.Lobby))
        // {
        //     _audioSources = GameObject.AddComponent<AudioSource>();
        //     _audioSources.loop = true;
        // }
        LobbyBGM = Resources.Load<GameObject>("Prefabs/LobbyBGM");
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

            _audioSources[(int)Define.Sound.Bgm].clip = LobbyBGM.GetComponent<AudioSource>().clip;
            //_audioSources[(int)Define.Sound.Bgm].outputAudioMixerGroup = Option.master.FindMatchingGroups("Master")[0];
            _audioSources[(int)Define.Sound.Bgm].loop = true; // bgm 재생기는 무한 반복 재생
        }
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources) //오디오재생 멈추고 재생기에서 음원 제거
        {
            audioSource.clip = null;
            audioSource.Stop();
        }

        _audioClips.Clear(); //dictionary 제거
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
}
