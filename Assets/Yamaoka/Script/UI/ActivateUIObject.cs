using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// UI�̕\���E��\���𐧌䂷��N���X
/// </summary>
public class ActivateUIObject : MonoBehaviour
{
    [SerializeField]
    private GameObject panel_1;   // �\��������UI
    [SerializeField]
    private GameObject panel_2;   // �\��������UI
    // �eUI��ActivateButton���擾
    [SerializeField]
    private ActivateButton select1;
    [SerializeField]
    private ActivateButton select2;

    private void Start()
    {
        panel_1.SetActive(false);
        panel_2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // UI�̕�����E��\���̐ؑ�
            panel_1.SetActive(!panel_1.activeSelf);

            // UI��ʂ��J�����Ƃ�
            if(panel_1.activeSelf)
            {
                // ���ꂼ��̃{�^���̑I����Ԃ�ݒ�
                select1.ActivateOrNotActivate(true);
                select2.ActivateOrNotActivate(false);
            }
            else
            {
                // �{�^���̑I����Ԃ�����
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }
}
