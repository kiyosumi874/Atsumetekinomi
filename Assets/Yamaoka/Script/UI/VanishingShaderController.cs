using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanishingShaderController : MonoBehaviour
{
    [SerializeField] float strengthX = 0f;
    [SerializeField] float strengthY = 0f;
    [SerializeField] float alpha = 1f;
    [SerializeField] Material material;

    private void Update()
    {
        UpdateMaterial();
    }

    /// <summary>
    /// �V�F�[�_�[�萔�̍X�V
    /// </summary>
    public void UpdateMaterial()
    {
        if(material == null)
        {
            return;
        }

        material.SetFloat("_StrengthX", strengthX);
        material.SetFloat("_StrengthY", strengthY);
        material.SetFloat("_Alpha", alpha);
    }
}
