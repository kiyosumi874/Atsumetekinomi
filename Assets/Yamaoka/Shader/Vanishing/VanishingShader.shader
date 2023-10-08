Shader"Custom/VanishingShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _StrengthX("StrengthX", Float) = 0
        _StrengthY("StrengthY", Float) = 0
        _Alpha("Alpha", Range(0, 1)) = 1
    }
    SubShader
    {
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Fog{ Mode Off }
        Tags { "Queue"="Transparent" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img     // ビルトインの頂点シェーダー
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _StrengthX;
            float _StrengthY;
            float _Alpha;

            float rand(float2 co)
            {
                return frac(sin(dot(co.xy, float2(12.9898, 78.233))) * 43758.5453);
            
            }

            fixed4 frag (v2f_img i) : SV_Target
            {
                float r = rand(i.uv);
                float2 uv = i.uv + r * float2(_StrengthX, -_StrengthY);
                fixed4 c = tex2D(_MainTex, uv);
                c.a *= _Alpha;
                return c;
            }
            ENDCG
        }
    }
}
