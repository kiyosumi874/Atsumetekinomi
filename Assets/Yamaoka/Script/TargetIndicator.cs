using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class TargetIndicator : MonoBehaviour
{
    [SerializeField]
    private Transform target = default;
    [SerializeField]
    private Image arrow = default;

    private Camera mainCamera;
    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        // ���[�g(Canvas)�̃X�P�[���l���擾
        float canvasScale = transform.root.localScale.z;
        var center = 0.5f * new Vector3(Screen.width, Screen.height);

        // ��ʒ��S�����_�Ƃ����^�[�Q�b�g�̃X�N���[�����W���v�Z
        var pos = mainCamera.WorldToScreenPoint(target.position) - center;

        // �J��������ɂ���^�[�Q�b�g�̃X�N���[�����W�́A��ʒ��S�ɑ΂���_�Ώ̂̍��W�ɂ���
        if (pos.z < 0.0f)
        {
            pos.x = -pos.x;
            pos.y = -pos.y;

            // �J�����Ɛ����ȃ^�[�Q�b�g�̃X�N���[�����W��␳����
            if(Mathf.Approximately(pos.y, 0.0f))
            {
                pos.y = -center.y;
            }
        }

        // UI���W�n�̒l���X�N���[�����W�n�̒l�ɕϊ�����
        var halfSize = 0.5f * canvasScale * rectTransform.sizeDelta;
        float d = Mathf.Max(
            Mathf.Abs(pos.x / (center.x - halfSize.x)),
            Mathf.Abs(pos.y / (center.y - halfSize.y))
            );

        // �^�[�Q�b�g�̃X�N���[�����W����ʊO�Ȃ�A��ʒ[�ɂȂ�悤�ɒ�������
        bool isOffscreen = (pos.z < 0.0f || d > 1.0f);
        if(isOffscreen)
        {
            pos.x /= d;
            pos.y /= d;
        }
        // �X�N���[�����W�n�̒l��UI���W�n�̒l�ɕϊ�����
        rectTransform.anchoredPosition = pos / canvasScale;

        // Target�̕������������̉摜��\���A�������v
        arrow.enabled = isOffscreen;
        if(isOffscreen)
        {
            arrow.rectTransform.eulerAngles = new Vector3(
                0.0f,
                0.0f,
                Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg
                );
        }
    }
}
