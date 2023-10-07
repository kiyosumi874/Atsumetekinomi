using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    /// <summary>
    /// �����f�[�^
    /// </summary>
    [System.Serializable]
    public class BGMSoundData
    {
        // �T�E���h��
        public string name;
        // �����f�[�^
        public AudioClip audioClip;
        // ����
        [Range(0.0f, 1.0f)]
        public float volume = 1.0f;
        // �O��Đ���������
        public float playedTime;
        // ���[�v�Đ������邩�ǂ���
        public bool soundLoop;
    }

    /// <summary>
    /// �����f�[�^
    /// </summary>
    [System.Serializable]
    public class SoundData
    {
        // �T�E���h��
        public string name;
        // �����f�[�^
        public AudioClip audioClip;
        // ����
        [Range(0.0f, 1.0f)]
        public float volume = 1.0f;
        // �O��Đ���������
        public float playedTime;
    }

    // �ő哯���Đ���
    [Range(5, 25)]
    public int maxSoundTrack = 10;

    // �ʖ�(name)���L�[�Ƃ����Ǘ��pDictionary
    private Dictionary<string, SoundData> soundDictionary = new Dictionary<string, SoundData>();

    // �ʖ�(name)���L�[�Ƃ���BGN�Ǘ��pDictionary
    private Dictionary<string, BGMSoundData> bgmSoundDictionary = new Dictionary<string, BGMSoundData>();

    // AudioSource��p�ӂ���
    private AudioSource[] audioSourceList;

    // BGM�p��AudioSource��p�ӂ���
    private AudioSource[] bgmAudioSourceList;

    [SerializeField]
    private SoundData[] soundDatas;

    [SerializeField]
    private BGMSoundData[] bgmSoundDatas;

    // �V���O���g���̃C���X�^���X
    public static SoundManager instance;

    private void Awake()
    {
        CheckInstance();

        // maxSoundTrack����AudioSource���i�[
        audioSourceList = new AudioSource[maxSoundTrack];

        // audioSourceList�z��̐�����AudioSource���������g�ɐ������āA�z��Ɋi�[
        for(var i = 0; i < audioSourceList.Length; i++)
        {
            audioSourceList[i] = gameObject.AddComponent<AudioSource>();
        }

        // soundDirectionary�ɃZ�b�g
        foreach(var soundData in soundDatas)
        {
            soundDictionary.Add(soundData.name, soundData);
        }

        /// BGM  ///
        // maxSoundTrack����AudioSource���i�[
        bgmAudioSourceList = new AudioSource[maxSoundTrack];

        // audioSourceList�z��̐�����AudioSource���������g�ɐ������āA�z��Ɋi�[
        for (var i = 0; i < bgmAudioSourceList.Length; i++)
        {
            bgmAudioSourceList[i] = gameObject.AddComponent<AudioSource>();
        }

        // soundDirectionary�ɃZ�b�g
        foreach (var bgmSoundData in bgmSoundDatas)
        {
            bgmSoundDictionary.Add(bgmSoundData.name, bgmSoundData);
        }
    }

    /// <summary>
    /// ���g�p��AudioSource�̎擾
    /// </summary>
    /// <returns>
    /// ���g�p��AudioSource(���ׂĎg�p���̏ꍇ�� null)
    /// </returns>
    private AudioSource GetUnusedAudioSource()
    {
        for(var i = 0; i < audioSourceList.Length; i++)
        {
            // ���g�p��AudioSource�����݂���Ƃ�
            if (!audioSourceList[i].isPlaying)
            {
                return audioSourceList[i];
            }
        }

        // ���g�p��AudioSource��������Ȃ��Ƃ�
        Debug.Log("���g�p��AudioSource�����݂��܂���");
        return null;
    }

    /// <summary>
    /// ���g�p��BGM�pAudioSource�̎擾
    /// </summary>
    /// <returns>
    /// ���g�p��AudioSource(���ׂĎg�p���̏ꍇ�� null)
    /// </returns>
    private AudioSource GetUnusedBGMAudioSource()
    {
        for (var i = 0; i < bgmAudioSourceList.Length; i++)
        {
            // ���g�p��AudioSource�����݂���Ƃ�
            if (!bgmAudioSourceList[i].isPlaying)
            {
                return bgmAudioSourceList[i];
            }
        }

        // ���g�p��AudioSource��������Ȃ��Ƃ�
        Debug.Log("���g�p��BGM�pAudioSource�����݂��܂���");
        return null;
    }

    /// <summary>
    /// �w�肳�ꂽAudioClip�𖢎g�p��AudioSource�ōĐ�
    /// </summary>
    /// <param name="clip">�����f�[�^</param>
    /// <param name="volume">����</param>
    /// SoundManager�����Ŏg�p
    public void PlaySound(AudioClip clip, float volume)
    {
        // ���g�p��AudioSource���擾
        var audioSource = GetUnusedAudioSource();

        // AudioSource�����݂��Ȃ��Ƃ�
        if(!audioSource)
        {
            Debug.Log("���g�p��AudioSource�����݂��܂���");
            return;
        }
        // �������擾
        audioSource.clip = clip;
        // ���ʂ��擾
        audioSource.volume = volume;
        // �����Đ�
        audioSource.Play();
    }

    /// <summary>
    /// �ݒ肳�ꂽ�ʖ�(�T�E���h��)�œo�^���ꂽAudioClip���Đ�
    /// </summary>
    /// <param name="name">�ݒ肵���ʖ�</param>
    public void PlaySound(string name)
    {
        // �Ǘ��pDirectionary����A�ʖ�(�T�E���h���j�Ō���
        if(soundDictionary.TryGetValue(name, out var soundData))
        {
            // �������Đ�����Ă���ꍇ
            //if(Time.realtimeSinceStartup - soundData.playedTime < soundData.audioClip.length)
            //{
            //    return;
            //}
            //// ����̍Đ��p�ɁA����̍Đ����Ԃ�ێ�����
            //soundData.playedTime = Time.realtimeSinceStartup;
            // ����������������A�Đ�
            PlaySound(soundData.audioClip, soundData.volume);
        }
        // ������������Ȃ�������
        else
        {
            Debug.LogWarning($"���̕ʖ��͓o�^����Ă��܂��� : {name}");
        }
    }

    /// <summary>
    /// �ݒ肳�ꂽ�ʖ�(�T�E���h��)�A�Ԋu�œo�^���ꂽAudioClip���Đ�
    /// </summary>
    /// <param name="name">�ݒ肵���ʖ�</param>
    /// <param name="intervalTime">�Đ��Ԋu</param>
    public void PlaySound(string name, float intervalTime)
    {
        // �Ǘ��pDirectionary����A�ʖ�(�T�E���h���j�Ō���
        if (soundDictionary.TryGetValue(name, out var soundData))
        {
            // �Đ��Ԋu�𒴂��Ă��Ȃ��Ƃ�
            if (Time.realtimeSinceStartup - soundData.playedTime < intervalTime)
            {
                return;
            }
            // ����̍Đ��p�ɁA����̍Đ����Ԃ�ێ�����
            soundData.playedTime = Time.realtimeSinceStartup;
            // ����������������A�Đ�
            PlaySound(soundData.audioClip, soundData.volume);
        }
        // ������������Ȃ�������
        else
        {
            Debug.LogWarning($"���̕ʖ��͓o�^����Ă��܂��� : {name}");
        }
    }


    /// <summary>
    /// �w�肳�ꂽAudioClip�𖢎g�p��AudioSource�ōĐ�
    /// </summary>
    /// <param name="clip">�����f�[�^</param>
    /// <param name="volume">����</param>
    /// SoundManager�����Ŏg�p
    public void PlayBGM(AudioClip clip, float volume, bool loop)
    {
        // ���g�p��AudioSource���擾
        var bgmAudioSource = GetUnusedBGMAudioSource();

        // AudioSource�����݂��Ȃ��Ƃ�
        if (!bgmAudioSource)
        {
            Debug.Log("���g�p��AudioSource�����݂��܂���");
            return;
        }
        // �������擾
        bgmAudioSource.clip = clip;
        // ���ʂ��擾
        bgmAudioSource.volume = volume;
        // �����Đ�
        bgmAudioSource.Play();
        // ���[�v�Đ��t���O�̐ݒ�
        bgmAudioSource.loop = loop;
    }

    /// <summary>
    /// �ݒ肳�ꂽ�ʖ�(�T�E���h��)�œo�^���ꂽAudioClip���Đ�
    /// </summary>
    /// <param name="name">�ݒ肵���ʖ�</param>
    public void PlayBGM(string name)
    {
        // �Ǘ��pDirectionary����A�ʖ�(�T�E���h���j�Ō���
        if (bgmSoundDictionary.TryGetValue(name, out var bgmSoundData))
        {
            // ����������������A�Đ�
            PlayBGM(bgmSoundData.audioClip, bgmSoundData.volume, bgmSoundData.soundLoop);
        }
        // ������������Ȃ�������
        else
        {
            Debug.LogWarning($"���̕ʖ��͓o�^����Ă��܂��� : {name}");
        }
    }

    /// <summary>
    /// �ݒ肳�ꂽ�ʖ�(�T�E���h��)�A�Ԋu�œo�^���ꂽAudioClip���Đ�
    /// </summary>
    /// <param name="name">�ݒ肵���ʖ�</param>
    /// <param name="intervalTime">�Đ��Ԋu</param>
    public void PlayBGM(string name, float intervalTime)
    {
        // �Ǘ��pDirectionary����A�ʖ�(�T�E���h���j�Ō���
        if (bgmSoundDictionary.TryGetValue(name, out var bgmSoundData))
        {
            // �Đ��Ԋu�𒴂��Ă��Ȃ��Ƃ�
            if (bgmSoundData.audioClip.length - bgmSoundData.playedTime < bgmSoundData.audioClip.length)
            {
                Debug.Log("�܂��Đ��ł��܂���B");
                return;
            }
            // ����̍Đ��p�ɁA����̍Đ����Ԃ�ێ�����
            bgmSoundData.playedTime = Time.realtimeSinceStartup;
            // ����������������A�Đ�
            PlayBGM(bgmSoundData.audioClip, bgmSoundData.volume, bgmSoundData.soundLoop);
        }
        // ������������Ȃ�������
        else
        {
            Debug.LogWarning($"���̕ʖ��͓o�^����Ă��܂��� : {name}");
        }
    }

    /// <summary>
    /// ���̃Q�[���I�u�W�F�N�g�ɃA�^�b�`����Ă��邩���ׂ�
    /// �A�^�b�`����Ă���ꍇ�͔j������B
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
