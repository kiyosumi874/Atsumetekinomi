using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ɖ߂��Ă����Ƃ��̏���
/// </summary>
public class NestArea : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        // �G���A�Ƀv���C���[�������Ă�����
        if(other.gameObject.tag == "Player")
        {
            ComboManager.instance.UseCombo();
            ComboManager.instance.GetNotComboScore();
            KinomiManager.instance.LostAllKinomi();
        }
    }
}
