using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_AudioManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static M_AudioManager Instance;

    // ���� Ŭ�� �迭
    [Serializable]
    public class Sound
    {
        public string name;         // ���� �̸�
        public AudioClip clip;      // ����� Ŭ��
        public bool loop;           // �ݺ� ��� ����
        [Range(0f, 1f)] public float volume = 0.7f;
    }

    public Sound[] sounds;             // ���� �迭
    private AudioSource[] audioSources; // ����� �ҽ� �迭

    private void Awake()
    {
        // �̱��� ���� ����
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

        // AudioSource ���� �� �ʱ�ȭ
        audioSources = new AudioSource[sounds.Length];
        for (int i = 0; i < sounds.Length; i++)
        {
            audioSources[i] = gameObject.AddComponent<AudioSource>();
            audioSources[i].clip = sounds[i].clip;
            audioSources[i].volume = sounds[i].volume;
            audioSources[i].loop = sounds[i].loop;
        }
    }

    // ���� ��� �Լ�
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

    // ���� ���� �Լ�
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
