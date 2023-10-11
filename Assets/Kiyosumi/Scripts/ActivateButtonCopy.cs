using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// �{�^��UI��interactable�𑀍삷�邽�߂̃N���X
/// </summary>
public class ActivateButtonCopy : MonoBehaviour
{
    [SerializeField]
    private GameObject firstSelectButton;   // �ŏ��ɑI������{�^���I�u�W�F�N�g

    /// <summary>
    /// �{�^���̑I����Ԃ̐ݒ�
    /// </summary>
    /// <param name="activateFlag">�L�����ǂ���</param>
    public void ActivateOrNotActivate(bool activateFlag)
    {
        //firstSelectButton.interactable = activateFlag;
        for(var i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Button>().interactable = activateFlag;
        }

        if(activateFlag)
        {
            EventSystem.current.SetSelectedGameObject(firstSelectButton.gameObject);
        }
    }
}
