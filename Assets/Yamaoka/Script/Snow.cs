using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GPU���g�p���A����~�点��
/// </summary>
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Snow : MonoBehaviour
{
    // ���_�A���̑���p��
    // 1�h���[�R�[����65000���_������̂ŁA�ꗱ4���_�̐�͂���1/4�܂ŏo����
    const int SNOW_NUM = 8000;  // �Ƃ肠���� 1/8�o��
    private Vector3[] vertices; // ���_
    private int[] triangles;    // ���_�����񂾎O�p�`
    private Color[] colors;
    private Vector2[] uvs;      // UV���W

    private float range;    // ����~�点��͈�
    private float rangeR;
    private Vector3 move = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        // �͈͓��̃����_���ȓ_�ɁA4���_���܂Ƃ߂�
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

        // 4���_����`�ɂ���悤�Ɍ���Ŗʂɂ���(�O�p�`2����)
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

        // UV���W(����̓e�N�X�`�����W�ȊO�ɂ��g�p����)
        uvs = new Vector2[SNOW_NUM * 4];
        for(int i = 0; i < SNOW_NUM; ++i)
        {
            uvs[i * 4 + 0] = new Vector2(0f, 0f);
            uvs[i * 4 + 1] = new Vector2(1f, 0f);
            uvs[i * 4 + 2] = new Vector2(0f, 1f);
            uvs[i * 4 + 3] = new Vector2(1f, 1f);
        }

        // ���b�V���Ɋi�[����
        Mesh mesh = new Mesh();
        mesh.name = "MeshSnowFlakes";
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;
        mesh.uv = uvs;
        // Mesh.bounds �́A���b�V���̋��E�̐ς�\���\���� -> ���b�V�����̂̋�ԓ��̎��ɉ������o�E���f�B���O�{�b�N�X
        // (Bounding Box -> �摜��f���̒��̕��̂��͂񂾕����̈�)
        // Frustom Culling����Ȃ����߂ɁAbounds�� 99999999 �����Ă���
        mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 99999999);
        var mf = GetComponent<MeshFilter>();
        mf.sharedMesh = mesh;
    }

    /// <summary>
    /// transform�̍X�V��ɌĂ΂�Ăق����̂ŁALateUpdate
    /// </summary>
    void LateUpdate()
    { 
        // �J�����̑O���T���v�Z
        // �����𒆐S�ɐႪ�~��
        var targetPosition = Camera.main.transform.TransformPoint(Vector3.forward * range);
        var mr = GetComponent<Renderer>();
        // �V�F�[�_�[�萔�̐ݒ�
        mr.material.SetFloat("_Range", range);
        mr.material.SetFloat("_RangeR", rangeR);
        mr.material.SetFloat("_Size", 0.1f);
        mr.material.SetVector("_MoveTotal", move);
        mr.material.SetVector("_CamUp", Camera.main.transform.up);
        mr.material.SetVector("_TargetPosition", targetPosition);
        // ����������𓮂���
        float x = 0f;
        float y = -2f;
        float z = 0f;
        move += new Vector3(x, y, z) * Time.deltaTime;
        // �z������(�������i���ɑ傫���Ȃ��ăI�[�o�[�t���[���N��������)
        move.x = Mathf.Repeat(move.x, range * 2f);
        move.y = Mathf.Repeat(move.y, range * 2f);
        move.z = Mathf.Repeat(move.z, range * 2f);
    }
}
