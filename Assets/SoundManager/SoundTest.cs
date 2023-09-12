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
            // SoundManagerでSEを再生
            //soundManager.PlaySound("打ち上げ花火");
            SoundManager.instance.PlaySound("打ち上げ花火");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // SoundManagerでSEを再生
            //soundManager.PlaySound("ダイビング");
            SoundManager.instance.PlaySound("ダイビング");
        }
        // BGMの再生方法
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    // SoundManagerでBGMを再生
        //    SoundManager.instance.PlayBGM("BGM");
        //}

    }
}
