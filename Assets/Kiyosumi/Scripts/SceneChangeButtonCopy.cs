using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneChangeButtonCopy : MonoBehaviour
{
    public void SceneChange(string nextSceneName)
    {
        SceneChanger.Instance.LoadSceneFaded(nextSceneName);
    }
}
