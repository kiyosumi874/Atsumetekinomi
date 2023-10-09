using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// UIの表示・非表示を制御するクラス
/// </summary>
public class ActivateUIObject : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;   // 表示する画面UI

    public KeyCode openPanelKey;    // UIを開くときに押すキーを設定

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
            // UIの補油時・非表示の切替
            panel.SetActive(!panel.activeSelf);

            // UI画面が開いたとき
            if(panel.activeSelf)
            {
                // それぞれのボタンの選択状態を設定
                select1.ActivateOrNotActivate(true);
                //select2.ActivateOrNotActivate(false);
            }
            else
            {
                // ボタンの選択状態を解除
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }
}
