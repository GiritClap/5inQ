using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_AudioManager : MonoBehaviour
{
    // 싱글턴 인스턴스
    public static M_AudioManager Instance;

    // 사운드 클립 배열
    [Serializable]
    public class Sound
    {
        public string name;         // 사운드 이름
        public AudioClip clip;      // 오디오 클립
        public bool loop;           // 반복 재생 여부
        [Range(0f, 1f)] public float volume = 0.7f;
    }

    public Sound[] sounds;             // 사운드 배열
    private AudioSource[] audioSources; // 오디오 소스 배열

    private void Awake()
    {
        // 싱글턴 패턴 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // AudioSource 생성 및 초기화
        audioSources = new AudioSource[sounds.Length];
        for (int i = 0; i < sounds.Length; i++)
        {
            audioSources[i] = gameObject.AddComponent<AudioSource>();
            audioSources[i].clip = sounds[i].clip;
            audioSources[i].volume = sounds[i].volume;
            audioSources[i].loop = sounds[i].loop;
        }
    }

    // 사운드 재생 함수
    public void Play(string name, float volume = 0.7f, bool loop = false)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == name)
            {
                audioSources[i].Play();
                audioSources[i].volume = volume;
                audioSources[i].loop = loop;
                return;
            }
        }
        Debug.LogWarning($"Sound {name} not found!");
    }

    // 사운드 정지 함수
    public void Stop(string name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == name)
            {
                audioSources[i].Stop();
                return;
            }
        }
        Debug.LogWarning($"Sound {name} not found!");
    }
}
