using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeButtonCopy : MonoBehaviour
{
    public void SceneChange(string nextSceneName)
    {
        SceneChanger.Instance.LoadSceneFaded(nextSceneName);
    }
}
