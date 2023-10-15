using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnceSoundPlayer : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    bool flag = false;
    public void PlayOnce()
    {
        if (!flag)
        {
            audioSource.Play();
            flag = true;
        }
    }
}
