using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem effect;

    private ParticleSystem clone;

    public static EffectManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void PlayEffect(Transform transform, Color color)
    {
        clone = Instantiate(effect, transform.position, transform.rotation);
        clone.startColor = color;
        clone.Play();
    }
}
