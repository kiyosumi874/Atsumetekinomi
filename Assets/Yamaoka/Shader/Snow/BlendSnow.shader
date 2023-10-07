// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader"Custom/BlendSnow"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _SnowTex ("Snow Texture", 2D) = "gray" {}
        _Direction ("Direction", Vector) = (0, 1, 0)
        _Amount ("Amount", Range(0, 1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            //struct appdata
            //{
            //    float4 vertex : POSITION;
            //    float2 uv : TEXCOORD0;
            //};

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 normal : NORMAL;
                float2 uv_Main : TEXCOORD0;
                float2 uv_Snow : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _SnowTex;
            float4 _SnowTex_ST;

            v2f vert (appdata_full v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv_Main = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.uv_Snow = TRANSFORM_TEX(v.texcoord, _SnowTex);
                o.normal = mul(unity_ObjectToWorld, v.normal);
                return o;
            }

            float3 _Direction;
            fixed _Amount;

            fixed4 frag (v2f i) : COLOR
            {
                fixed val = dot(normalize(i.normal), _Direction);
                
                if(val < 1 - _Amount)
                {
                    val = 0;
                }
                
                fixed4 tex1 = tex2D(_MainTex, i.uv_Main);
                fixed4 tex2 = tex2D(_SnowTex, i.uv_Snow);
                return lerp(tex1, tex2, val);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
