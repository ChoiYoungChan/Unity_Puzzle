using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonClass<SoundManager>
{
    [System.Serializable]
    public struct SoundInfo
    {
        public string id;
        public AudioClip clip;
        public SoundType type;
        public bool loop;
        [Range(0, 1)] public float volume;
    }

    public enum SoundType { SoundEffect, Music }

    [SerializeField] SoundInfo[] _soundInfos;
    Dictionary<string, SoundInfo> _soundInfoDict = new Dictionary<string, SoundInfo>();
    Dictionary<string, AudioSource> _loopingAudioSourceDict = new Dictionary<string, AudioSource>();

    void Awake()
    {
        // サウンドを辞書型に登録.
        for (int i = 0; i < _soundInfos.Length; i++)
        {
            _soundInfoDict.Add(_soundInfos[i].id, _soundInfos[i]);
        }
    }

    public void Play(string id)
    {
        if (!GameManager.Instance.GetSoundOn()) return;

        AudioSource audioSource = GetAudioSource(id);
        SoundInfo soundInfo = _soundInfoDict[id];

        audioSource.clip = soundInfo.clip;
        audioSource.volume = soundInfo.volume;
        audioSource.loop = _soundInfoDict[id].loop;
        if (audioSource.isPlaying) return;
        audioSource.Play();

        // ループでなければ、Destroyの予約をする
        if (!_soundInfoDict[id].loop)
            StartCoroutine(FinishPlayback_Coroutine(audioSource));
    }

    public void Pause()
    {
        foreach (var audioSource in _loopingAudioSourceDict)
        {
            audioSource.Value.Pause();
        }
    }

    AudioSource GetAudioSource(string id)
    {
        // Loopの場合は、AudioSourceがあれば返す。なければ作って登録。
        if (_soundInfoDict[id].loop && _loopingAudioSourceDict.ContainsKey(id))
            return _loopingAudioSourceDict[id];

        print("### SoundId : " + id);

        GameObject obj = new GameObject("sound_" + id);
        obj.transform.SetParent(transform);

        AudioSource audioSource = obj.AddComponent<AudioSource>();

        if (_soundInfoDict[id].loop)
            _loopingAudioSourceDict.Add(id, audioSource);

        return audioSource;
    }

    IEnumerator FinishPlayback_Coroutine(AudioSource audioSource)
    {
        yield return new WaitUntil(() => !audioSource.isPlaying);
        Destroy(audioSource.gameObject);
    }
}
