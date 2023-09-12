using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class SoundTest : MonoBehaviour
{
    [SerializeField]
    private SoundManager soundManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            // SoundManager�ŉ����Đ�
            soundManager.PlaySound("�ł��グ�ԉ�");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // SoundManager�ŉ����Đ�
            soundManager.PlaySound("�_�C�r���O");
        }
    }
}
