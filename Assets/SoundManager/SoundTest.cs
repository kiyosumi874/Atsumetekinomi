using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class SoundTest : MonoBehaviour
{
    //[SerializeField]
    //private SoundManager soundManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            // SoundManager��SE���Đ�
            //soundManager.PlaySound("�ł��グ�ԉ�");
            SoundManager.instance.PlaySound("�ł��グ�ԉ�");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // SoundManager��SE���Đ�
            //soundManager.PlaySound("�_�C�r���O");
            SoundManager.instance.PlaySound("�_�C�r���O");
        }
        // BGM�̍Đ����@
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    // SoundManager��BGM���Đ�
        //    SoundManager.instance.PlayBGM("BGM");
        //}

    }
}
