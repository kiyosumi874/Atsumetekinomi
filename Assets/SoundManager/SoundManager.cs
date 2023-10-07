using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    /// <summary>
    /// 音源データ
    /// </summary>
    [System.Serializable]
    public class BGMSoundData
    {
        // サウンド名
        public string name;
        // 音源データ
        public AudioClip audioClip;
        // 音量
        [Range(0.0f, 1.0f)]
        public float volume = 1.0f;
        // 前回再生した時間
        public float playedTime;
        // ループ再生させるかどうか
        public bool soundLoop;
    }

    /// <summary>
    /// 音源データ
    /// </summary>
    [System.Serializable]
    public class SoundData
    {
        // サウンド名
        public string name;
        // 音源データ
        public AudioClip audioClip;
        // 音量
        [Range(0.0f, 1.0f)]
        public float volume = 1.0f;
        // 前回再生した時間
        public float playedTime;
    }

    // 最大同時再生数
    [Range(5, 25)]
    public int maxSoundTrack = 10;

    // 別名(name)をキーとした管理用Dictionary
    private Dictionary<string, SoundData> soundDictionary = new Dictionary<string, SoundData>();

    // 別名(name)をキーとしたBGN管理用Dictionary
    private Dictionary<string, BGMSoundData> bgmSoundDictionary = new Dictionary<string, BGMSoundData>();

    // AudioSourceを用意する
    private AudioSource[] audioSourceList;

    // BGM用のAudioSourceを用意する
    private AudioSource[] bgmAudioSourceList;

    [SerializeField]
    private SoundData[] soundDatas;

    [SerializeField]
    private BGMSoundData[] bgmSoundDatas;

    // シングルトンのインスタンス
    public static SoundManager instance;

    private void Awake()
    {
        CheckInstance();

        // maxSoundTrack分のAudioSourceを格納
        audioSourceList = new AudioSource[maxSoundTrack];

        // audioSourceList配列の数だけAudioSourceを自分自身に生成して、配列に格納
        for(var i = 0; i < audioSourceList.Length; i++)
        {
            audioSourceList[i] = gameObject.AddComponent<AudioSource>();
        }

        // soundDirectionaryにセット
        foreach(var soundData in soundDatas)
        {
            soundDictionary.Add(soundData.name, soundData);
        }

        /// BGM  ///
        // maxSoundTrack分のAudioSourceを格納
        bgmAudioSourceList = new AudioSource[maxSoundTrack];

        // audioSourceList配列の数だけAudioSourceを自分自身に生成して、配列に格納
        for (var i = 0; i < bgmAudioSourceList.Length; i++)
        {
            bgmAudioSourceList[i] = gameObject.AddComponent<AudioSource>();
        }

        // soundDirectionaryにセット
        foreach (var bgmSoundData in bgmSoundDatas)
        {
            bgmSoundDictionary.Add(bgmSoundData.name, bgmSoundData);
        }
    }

    /// <summary>
    /// 未使用のAudioSourceの取得
    /// </summary>
    /// <returns>
    /// 未使用のAudioSource(すべて使用中の場合は null)
    /// </returns>
    private AudioSource GetUnusedAudioSource()
    {
        for(var i = 0; i < audioSourceList.Length; i++)
        {
            // 未使用のAudioSourceが存在するとき
            if (!audioSourceList[i].isPlaying)
            {
                return audioSourceList[i];
            }
        }

        // 未使用のAudioSourceが見つからないとき
        Debug.Log("未使用のAudioSourceが存在しません");
        return null;
    }

    /// <summary>
    /// 未使用のBGM用AudioSourceの取得
    /// </summary>
    /// <returns>
    /// 未使用のAudioSource(すべて使用中の場合は null)
    /// </returns>
    private AudioSource GetUnusedBGMAudioSource()
    {
        for (var i = 0; i < bgmAudioSourceList.Length; i++)
        {
            // 未使用のAudioSourceが存在するとき
            if (!bgmAudioSourceList[i].isPlaying)
            {
                return bgmAudioSourceList[i];
            }
        }

        // 未使用のAudioSourceが見つからないとき
        Debug.Log("未使用のBGM用AudioSourceが存在しません");
        return null;
    }

    /// <summary>
    /// 指定されたAudioClipを未使用のAudioSourceで再生
    /// </summary>
    /// <param name="clip">音源データ</param>
    /// <param name="volume">音量</param>
    /// SoundManager内部で使用
    public void PlaySound(AudioClip clip, float volume)
    {
        // 未使用のAudioSourceを取得
        var audioSource = GetUnusedAudioSource();

        // AudioSourceが存在しないとき
        if(!audioSource)
        {
            Debug.Log("未使用のAudioSourceが存在しません");
            return;
        }
        // 音源を取得
        audioSource.clip = clip;
        // 音量を取得
        audioSource.volume = volume;
        // 音を再生
        audioSource.Play();
    }

    /// <summary>
    /// 設定された別名(サウンド名)で登録されたAudioClipを再生
    /// </summary>
    /// <param name="name">設定した別名</param>
    public void PlaySound(string name)
    {
        // 管理用Directionaryから、別名(サウンド名）で検索
        if(soundDictionary.TryGetValue(name, out var soundData))
        {
            // 音源が再生されている場合
            //if(Time.realtimeSinceStartup - soundData.playedTime < soundData.audioClip.length)
            //{
            //    return;
            //}
            //// 次回の再生用に、今回の再生時間を保持する
            //soundData.playedTime = Time.realtimeSinceStartup;
            // 音源が見つかったら、再生
            PlaySound(soundData.audioClip, soundData.volume);
        }
        // 音源が見つからなかったら
        else
        {
            Debug.LogWarning($"その別名は登録されていません : {name}");
        }
    }

    /// <summary>
    /// 設定された別名(サウンド名)、間隔で登録されたAudioClipを再生
    /// </summary>
    /// <param name="name">設定した別名</param>
    /// <param name="intervalTime">再生間隔</param>
    public void PlaySound(string name, float intervalTime)
    {
        // 管理用Directionaryから、別名(サウンド名）で検索
        if (soundDictionary.TryGetValue(name, out var soundData))
        {
            // 再生間隔を超えていないとき
            if (Time.realtimeSinceStartup - soundData.playedTime < intervalTime)
            {
                return;
            }
            // 次回の再生用に、今回の再生時間を保持する
            soundData.playedTime = Time.realtimeSinceStartup;
            // 音源が見つかったら、再生
            PlaySound(soundData.audioClip, soundData.volume);
        }
        // 音源が見つからなかったら
        else
        {
            Debug.LogWarning($"その別名は登録されていません : {name}");
        }
    }


    /// <summary>
    /// 指定されたAudioClipを未使用のAudioSourceで再生
    /// </summary>
    /// <param name="clip">音源データ</param>
    /// <param name="volume">音量</param>
    /// SoundManager内部で使用
    public void PlayBGM(AudioClip clip, float volume, bool loop)
    {
        // 未使用のAudioSourceを取得
        var bgmAudioSource = GetUnusedBGMAudioSource();

        // AudioSourceが存在しないとき
        if (!bgmAudioSource)
        {
            Debug.Log("未使用のAudioSourceが存在しません");
            return;
        }
        // 音源を取得
        bgmAudioSource.clip = clip;
        // 音量を取得
        bgmAudioSource.volume = volume;
        // 音を再生
        bgmAudioSource.Play();
        // ループ再生フラグの設定
        bgmAudioSource.loop = loop;
    }

    /// <summary>
    /// 設定された別名(サウンド名)で登録されたAudioClipを再生
    /// </summary>
    /// <param name="name">設定した別名</param>
    public void PlayBGM(string name)
    {
        // 管理用Directionaryから、別名(サウンド名）で検索
        if (bgmSoundDictionary.TryGetValue(name, out var bgmSoundData))
        {
            // 音源が見つかったら、再生
            PlayBGM(bgmSoundData.audioClip, bgmSoundData.volume, bgmSoundData.soundLoop);
        }
        // 音源が見つからなかったら
        else
        {
            Debug.LogWarning($"その別名は登録されていません : {name}");
        }
    }

    /// <summary>
    /// 設定された別名(サウンド名)、間隔で登録されたAudioClipを再生
    /// </summary>
    /// <param name="name">設定した別名</param>
    /// <param name="intervalTime">再生間隔</param>
    public void PlayBGM(string name, float intervalTime)
    {
        // 管理用Directionaryから、別名(サウンド名）で検索
        if (bgmSoundDictionary.TryGetValue(name, out var bgmSoundData))
        {
            // 再生間隔を超えていないとき
            if (bgmSoundData.audioClip.length - bgmSoundData.playedTime < bgmSoundData.audioClip.length)
            {
                Debug.Log("まだ再生できません。");
                return;
            }
            // 次回の再生用に、今回の再生時間を保持する
            bgmSoundData.playedTime = Time.realtimeSinceStartup;
            // 音源が見つかったら、再生
            PlayBGM(bgmSoundData.audioClip, bgmSoundData.volume, bgmSoundData.soundLoop);
        }
        // 音源が見つからなかったら
        else
        {
            Debug.LogWarning($"その別名は登録されていません : {name}");
        }
    }

    /// <summary>
    /// 他のゲームオブジェクトにアタッチされているか調べる
    /// アタッチされている場合は破棄する。
    /// </summary>
    void CheckInstance()
    {
        if(!instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if(instance != this)
        {
            Destroy(this.gameObject);
        }
    }
}
