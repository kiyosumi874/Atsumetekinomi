using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GPUを使用し、雪を降らせる
/// </summary>
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Snow : MonoBehaviour
{
    // 頂点、その他を用意
    // 1ドローコールで65000頂点扱えるので、一粒4頂点の雪はその1/4まで出せる
    const int SNOW_NUM = 8000;  // とりあえず 1/8個出す
    private Vector3[] vertices; // 頂点
    private int[] triangles;    // 頂点を結んだ三角形
    private Color[] colors;
    private Vector2[] uvs;      // UV座標

    private float range;    // 雪を降らせる範囲
    private float rangeR;
    private Vector3 move = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        // 範囲内のランダムな点に、4頂点をまとめる
        range = 16f;
        rangeR = 1.0f / range;
        vertices = new Vector3[SNOW_NUM * 4];
        for(var i = 0; i < SNOW_NUM; ++i)
        {
            float x = Random.Range(-range, range);
            float y = Random.Range(-range, range);
            float z = Random.Range(-range, range);

            var point = new Vector3(x, y, z);
            vertices[i * 4 + 0] = point;
            vertices[i * 4 + 1] = point;
            vertices[i * 4 + 2] = point;
            vertices[i * 4 + 3] = point;
        }

        // 4頂点を矩形にするように結んで面にする(三角形2つずつ)
        triangles = new int[SNOW_NUM * 6];
        for(int i = 0; i < SNOW_NUM; ++i)
        {
            triangles[i * 6 + 0] = i * 4 + 0;
            triangles[i * 6 + 1] = i * 4 + 1;
            triangles[i * 6 + 2] = i * 4 + 2;

            triangles[i * 6 + 3] = i * 4 + 2;
            triangles[i * 6 + 4] = i * 4 + 1;
            triangles[i * 6 + 5] = i * 4 + 3;
        }

        // UV座標(今回はテクスチャ座標以外にも使用する)
        uvs = new Vector2[SNOW_NUM * 4];
        for(int i = 0; i < SNOW_NUM; ++i)
        {
            uvs[i * 4 + 0] = new Vector2(0f, 0f);
            uvs[i * 4 + 1] = new Vector2(1f, 0f);
            uvs[i * 4 + 2] = new Vector2(0f, 1f);
            uvs[i * 4 + 3] = new Vector2(1f, 1f);
        }

        // メッシュに格納する
        Mesh mesh = new Mesh();
        mesh.name = "MeshSnowFlakes";
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;
        mesh.uv = uvs;
        // Mesh.bounds は、メッシュの境界体積を表す構造体 -> メッシュ自体の空間内の軸に沿ったバウンディングボックス
        // (Bounding Box -> 画像や映像の中の物体を囲んだ部分領域)
        // Frustom Cullingされないために、boundsに 99999999 を入れている
        mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 99999999);
        var mf = GetComponent<MeshFilter>();
        mf.sharedMesh = mesh;
    }

    /// <summary>
    /// transformの更新後に呼ばれてほしいので、LateUpdate
    /// </summary>
    void LateUpdate()
    { 
        // カメラの前方Ⅰを計算
        // ここを中心に雪が降る
        var targetPosition = Camera.main.transform.TransformPoint(Vector3.forward * range);
        var mr = GetComponent<Renderer>();
        // シェーダー定数の設定
        mr.material.SetFloat("_Range", range);
        mr.material.SetFloat("_RangeR", rangeR);
        mr.material.SetFloat("_Size", 0.1f);
        mr.material.SetVector("_MoveTotal", move);
        mr.material.SetVector("_CamUp", Camera.main.transform.up);
        mr.material.SetVector("_TargetPosition", targetPosition);
        // 生成した雪を動かす
        float x = 0f;
        float y = -2f;
        float z = 0f;
        move += new Vector3(x, y, z) * Time.deltaTime;
        // 循環させる(数字が永遠に大きくなってオーバーフローを起こすため)
        move.x = Mathf.Repeat(move.x, range * 2f);
        move.y = Mathf.Repeat(move.y, range * 2f);
        move.z = Mathf.Repeat(move.z, range * 2f);
    }
}
