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
        // 透明パスに描く
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        ZWrite Off  // Zは描かない
        Cull Off    // backface culling をしない
        Blend SrcAlpha OneMinusSrcAlpha // αブレンディング

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
            
            // 頂点シェーダー
            v2f vert (appdata_Custom v)
            {
                // 常に_TargetPositionの周りに登場するように計算
                float3 target = _TargetPosition;
                float3 trip;
                float3 mv = v.vertex.xyz;
                mv += _MoveTotal;
                            // 座標を足すだけはダメ、(頂点を)うまくリピートさせないといけない
                trip = floor(((target - mv) * _RangeR + 1) * 0.5);
                trip *= (_Range * 2);
                mv += trip;
                
                // 視線ベクトルを使ってビルボードにする
                // texcoordを利用して四隅を識別する
                float3 diff = _CamUp * _Size;
                float3 finalposition;
                float3 tv0 = mv;
                // 雪を揺らす(sinを重ね合わせる)
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

            // フラグメントシェーダー
            fixed4 frag (v2f i) : SV_Target
            {
                // テクスチャを引くだけ
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}
