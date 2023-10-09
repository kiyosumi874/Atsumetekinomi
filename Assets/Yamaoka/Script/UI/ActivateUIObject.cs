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
    private GameObject panel;   // �\��������UI

    public KeyCode openPanelKey;    // UI���J���Ƃ��ɉ����L�[��ݒ�

    [SerializeField]
    private ActivateButton select1;
    [SerializeField]
    private ActivateButton select2;

    private void Start()
    {
        panel.SetActive(false);
        //panel_2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(openPanelKey))
        {
            // UI�̕�����E��\���̐ؑ�
            panel.SetActive(!panel.activeSelf);

            // UI��ʂ��J�����Ƃ�
            if(panel.activeSelf)
            {
                // ���ꂼ��̃{�^���̑I����Ԃ�ݒ�
                select1.ActivateOrNotActivate(true);
                //select2.ActivateOrNotActivate(false);
            }
            else
            {
                // �{�^���̑I����Ԃ�����
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }
}
