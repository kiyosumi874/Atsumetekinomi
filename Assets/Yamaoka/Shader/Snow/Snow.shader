// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Snow"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        // �����p�X�ɕ`��
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        ZWrite Off  // Z�͕`���Ȃ�
        Cull Off    // backface culling �����Ȃ�
        Blend SrcAlpha OneMinusSrcAlpha // ���u�����f�B���O

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            #include "UnityCG.cginc"

            uniform sampler2D _MainTex;

            struct appdata_Custom
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float4x4 _PrevInMatrix;
            float3 _TargetPosition;
            float _Range;
            float _RangeR;
            float _Size;
            float3 _MoveTotal;
            float3 _CamUp;
            
            // ���_�V�F�[�_�[
            v2f vert (appdata_Custom v)
            {
                // ���_TargetPosition�̎���ɓo�ꂷ��悤�Ɍv�Z
                float3 target = _TargetPosition;
                float3 trip;
                float3 mv = v.vertex.xyz;
                mv += _MoveTotal;
                            // ���W�𑫂������̓_���A(���_��)���܂����s�[�g�����Ȃ��Ƃ����Ȃ�
                trip = floor(((target - mv) * _RangeR + 1) * 0.5);
                trip *= (_Range * 2);
                mv += trip;
                
                // �����x�N�g�����g���ăr���{�[�h�ɂ���
                // texcoord�𗘗p���Ďl�������ʂ���
                float3 diff = _CamUp * _Size;
                float3 finalposition;
                float3 tv0 = mv;
                // ���h�炷(sin���d�ˍ��킹��)
                tv0.x += sin(mv.x * 0.2) * sin(mv.y * 0.3) * sin(mv.x * 0.9) * sin(mv.y * 0.8);
                tv0.z += sin(mv.x * 0.1) * sin(mv.y * 0.2) * sin(mv.x * 0.8) * sin(mv.y * 1.2);  
                {
                    float3 eyeVector = ObjSpaceViewDir(float4(tv0, 0));
                    float3 sideVector = normalize(cross(eyeVector, diff));
                    tv0 += (v.texcoord.x - 0.5f) * sideVector * _Size;
                    tv0 += (v.texcoord.y - 0.5f) * diff;
                    finalposition = tv0;
                }
                
                v2f o;
                o.pos = UnityObjectToClipPos(float4(finalposition, 1));
                o.uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, v.texcoord);
                return o;
            }

            // �t���O�����g�V�F�[�_�[
            fixed4 frag (v2f i) : SV_Target
            {
                // �e�N�X�`������������
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}
