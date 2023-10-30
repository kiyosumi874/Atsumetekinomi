using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �؂̎��N���X
/// </summary>
public class Kinomi : MonoBehaviour
{
    /// <summary>
    /// �؂̎��̐����ꏊ�̃f�[�^
    /// </summary>
    public enum GenerationLocation
    {
        Near,    // ���̋߂�
        Middle,  // ����
        Far,     // �����牓��
    }

    public string kinomiName;  // �؂̎��̖��O
    public GenerationLocation generatLocation;  // �؂̎��̐����ꏊ

    public int score = 50;

    public static Kinomi instance;
    public ParticleSystem effect;

    private void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        // �؂̎���Y����]������
        this.gameObject.transform.Rotate(new Vector3(0, 100, 0) * Time.deltaTime);
    }

    public int GetKinomiScore()
    {
        return score;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //effect.Play();
            EffectManager.instance.PlayEffect(this.transform, effect.startColor);
            KinomiManager.instance.CountItem(kinomiName, 1);
            KinomiGenerator.instance.nowKinomiNum--;
            Destroy(this.gameObject);
        }

        if(collision.gameObject.tag == "Kinomi")
        {
            //effect.Play();
            EffectManager.instance.PlayEffect(this.transform, effect.startColor);
            KinomiGenerator.instance.nowKinomiNum--;
            Destroy(this.gameObject);
        }
    }
}
