using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeButton : MonoBehaviour
{
    public void SceneChange(string nextSceneName)
    {
        SceneChanger.Instance.LoadSceneFaded(nextSceneName);
    }
}
