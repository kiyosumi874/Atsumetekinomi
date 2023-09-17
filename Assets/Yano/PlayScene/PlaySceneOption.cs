using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �V��ł���Ƃ��̐ݒ�
/// </summary>
[CreateAssetMenu(menuName ="CreatePlaySceneOption")]
public class PlaySceneOption : ScriptableObject
{
    [SerializeField] [Header("�Q�[���I���܂ł̎���")] private float playLimitTime;
    [SerializeField] [Header("�X�R�A�̃m���}")] private float scoreQuota;
    /// <summary>
    /// ��������
    /// </summary>
    public float getPlayLimitTime { get { return playLimitTime; } private set { } }
    /// <summary>
    /// �Q�[���N���A�ɕK�v�ȃX�R�A��
    /// </summary>
    public float getScoreQuota { get { return scoreQuota; } private set { } }
}
