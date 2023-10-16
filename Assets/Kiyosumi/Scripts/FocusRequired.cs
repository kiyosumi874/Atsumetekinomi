using System.Collections;
using System.Linq;                 // �ǉ�
using UnityEngine;
using UnityEngine.EventSystems;    // �ǉ�
using UnityEngine.UI;              // �ǉ�

public class FocusRequired : MonoBehaviour
{
    /// <summary>
    /// <see cref="Selectable"/> ���t�b�N����N���X�ł��B
    /// </summary>
    private class SelectionHooker : MonoBehaviour, IDeselectHandler
    {
        /// <summary>�e�R���|�[�l���g�B</summary>
        public FocusRequired Restrictor;

        /// <summary>
        /// �I���������ɂ���܂őI������Ă����I�u�W�F�N�g���o���Ă����B
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDeselect(BaseEventData eventData)
        {
            Restrictor.PreviousSelection = eventData.selectedObject;
        }
    }

    /// <summary>�I�������Ȃ��I�u�W�F�N�g�ꗗ�B</summary>
    [SerializeField] private GameObject[] NotSelectables;

    /// <summary>���O�܂őI������Ă����I�u�W�F�N�g�B</summary>
    private GameObject PreviousSelection = null;

    /// <summary>
    /// �I��Ώۂ̃I�u�W�F�N�g�ꗗ�B
    /// </summary>
    private GameObject[] _selectables;

    private void Awake()
    {
        // ���ׂĂ� Selectable ���擾����
        var selectableList = (FindObjectsOfType(typeof(Selectable)) as Selectable[]).ToList();

        // �I�����O������ꍇ�͊O��
        if (NotSelectables != null)
        {
            foreach (var item in NotSelectables)
            {
                var sel = item?.GetComponent<Selectable>();
                if (sel != null) selectableList.Remove(sel);
            }
        }

        _selectables = selectableList.Select(x => x.gameObject).ToArray();

        // �t�H�[�J�X���I�u�W�F�N�g�� SelectionHooker ���A�^�b�`
        foreach (var selectable in this._selectables)
        {
            var hooker = selectable.AddComponent<SelectionHooker>();
            hooker.Restrictor = this;
        }

        // �t�H�[�J�X����p�R���[�`�����X�^�[�g
        StartCoroutine(RestrictSelection());
    }

    /// <summary>
    /// �t�H�[�J�X���䏈���B
    /// </summary>
    /// <returns></returns>
    private IEnumerator RestrictSelection()
    {
        while (true)
        {
            // �ʂȃI�u�W�F�N�g��I������܂őҋ@
            yield return new WaitUntil(
                () => (EventSystem.current != null) && (EventSystem.current.currentSelectedGameObject != PreviousSelection));

            // �܂��I�u�W�F�N�g�𖢑I���A�܂��͋����X�g��I�����Ă���Ȃ牽�����Ȃ�
            if ((PreviousSelection == null) || _selectables.Contains(EventSystem.current.currentSelectedGameObject))
            {
                continue;
            }

            // �I�����Ă�����̂��Ȃ��Ȃ����A�܂��͋����Ă��Ȃ� Selectable ��I�������ꍇ�͑O�̑I���ɖ߂�
            EventSystem.current.SetSelectedGameObject(PreviousSelection);
        }
    }
}