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
        // ルート(Canvas)のスケール値を取得
        float canvasScale = transform.root.localScale.z;
        var center = 0.5f * new Vector3(Screen.width, Screen.height);

        // 画面中心を原点としたターゲットのスクリーン座標を計算
        var pos = mainCamera.WorldToScreenPoint(target.position) - center;

        // カメラ後方にあるターゲットのスクリーン座標は、画面中心に対する点対称の座標にする
        if (pos.z < 0.0f)
        {
            pos.x = -pos.x;
            pos.y = -pos.y;

            // カメラと水平なターゲットのスクリーン座標を補正する
            if(Mathf.Approximately(pos.y, 0.0f))
            {
                pos.y = -center.y;
            }
        }

        // UI座標系の値をスクリーン座標系の値に変換する
        var halfSize = 0.5f * canvasScale * rectTransform.sizeDelta;
        float d = Mathf.Max(
            Mathf.Abs(pos.x / (center.x - halfSize.x)),
            Mathf.Abs(pos.y / (center.y - halfSize.y))
            );

        // ターゲットのスクリーン座標が画面外なら、画面端になるように調整する
        bool isOffscreen = (pos.z < 0.0f || d > 1.0f);
        if(isOffscreen)
        {
            pos.x /= d;
            pos.y /= d;
        }
        // スクリーン座標系の値をUI座標系の値に変換する
        rectTransform.anchoredPosition = pos / canvasScale;

        // Targetの方向を示す矢印の画像を表示、向きを計
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
