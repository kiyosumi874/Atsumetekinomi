using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Title : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject exceptionButton;

    GameObject prevSelectedGameObject = null;
    void Start()
    {
        prevSelectedGameObject = EventSystem.current.firstSelectedGameObject;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        if (EventSystem.current.currentSelectedGameObject != prevSelectedGameObject)
        {
            bool flag = false;
            // �d�l��{�^���̃A�N�e�B�u���؂�ւ�����特����悤�ɂȂ��Ă���̂ŁA
            // ���肵���Ƃ��ɂ������������Ă��܂����߁A��O�͖炳�Ȃ��悤�ɂ��Ă���
            if (EventSystem.current.currentSelectedGameObject == exceptionButton)
            {
                prevSelectedGameObject = EventSystem.current.currentSelectedGameObject;
                flag = true;
            }
            if (prevSelectedGameObject == exceptionButton)
            {
                prevSelectedGameObject = EventSystem.current.currentSelectedGameObject;
                flag = true;
            }
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                flag = true;
            }

            if (!flag)
            {
                audioSource.Play();
                prevSelectedGameObject = EventSystem.current.currentSelectedGameObject;
                Debug.Log("�ړ�����");
            }
        }
    }
}
