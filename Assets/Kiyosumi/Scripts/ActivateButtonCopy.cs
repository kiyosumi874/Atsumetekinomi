using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// ボタンUIのinteractableを操作するためのクラス
/// </summary>
public class ActivateButtonCopy : MonoBehaviour
{
    [SerializeField]
    private GameObject firstSelectButton;   // 最初に選択するボタンオブジェクト

    /// <summary>
    /// ボタンの選択状態の設定
    /// </summary>
    /// <param name="activateFlag">有効かどうか</param>
    public void ActivateOrNotActivate(bool activateFlag)
    {
        //firstSelectButton.interactable = activateFlag;
        for(var i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Button>().interactable = activateFlag;
        }

        if(activateFlag)
        {
            EventSystem.current.SetSelectedGameObject(firstSelectButton.gameObject);
        }
    }
}
