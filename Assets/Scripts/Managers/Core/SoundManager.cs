using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager
{
    // 출력기기 -> AudioSource
    // 음원 -> AudioClip
    // 청취자? -> AudioListener

    AudioSource[] _audioSource = new AudioSource[(int)Define.Sound.MaxCount];
    Dictionary<string, AudioClip> _audioClip = new Dictionary<string, AudioClip>();



    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSource[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSource[(int)Define.Sound.BGM].loop = true;
        }     
    }

    public void Clear()
    {

        foreach(AudioSource audioSource in _audioSource)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClip.Clear();
    }


    public void Play(string path, Define.Sound type = Define.Sound.Effact, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch);
    }

    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effact, float pitch = 1.0f)
    {
        if (audioClip == null)
            return;

        if (type == Define.Sound.BGM)
        {
            AudioSource audioSource = _audioSource[(int)Define.Sound.BGM];

            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = _audioSource[(int)Define.Sound.Effact];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }


    AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effact)
    {

        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";

        AudioClip audioClip = null;

        if (type == Define.Sound.BGM)
        {
            audioClip = Managers.Resource.Load<AudioClip>(path);
        }
        else
        {          
            if (_audioClip.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Managers.Resource.Load<AudioClip>(path);
                _audioClip.Add(path, audioClip);
            }
        }

        if (audioClip == null)
            Debug.Log($"AudioClip Missing ! {path}");

        return audioClip;
    }
}
