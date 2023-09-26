using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Kinomi/KinomiData")]
public class KinomiSourceData : ScriptableObject
{
    public enum GenerationLocation
    {
        Near,    // ���̋߂�
        Far,     // �����牓��
        Middle   // ����
    }

    [SerializeField]
    private int id;  // �؂̎����ʗpID
    [SerializeField]
    private string name;  // �؂̎��̖��O
    [SerializeField]
    private GenerationLocation location;  // �؂̎��̐����ꏊ

    /// <summary>
    /// �؂̎���ID���擾
    /// </summary>
    public int kinomiID
    {
        get { return id; }
    }
    /// <summary>
    /// �؂̎��̖��O���擾
    /// </summary>
    public string kinomiName
    {
        get { return name; }
    }
    /// <summary>
    /// �؂̎��̐����ꏊ
    /// </summary>
    public GenerationLocation kinomiGenerationLocation
    {
        get { return location; }
    }
}
