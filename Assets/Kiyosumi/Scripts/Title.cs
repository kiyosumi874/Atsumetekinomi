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
            // 仕様上ボタンのアクティブが切り替わったら音が鳴るようになっているので、
            // 決定したときにも同じ音が鳴ってしまうため、例外は鳴らさないようにしている
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
                Debug.Log("移動した");
            }
        }
    }
}
