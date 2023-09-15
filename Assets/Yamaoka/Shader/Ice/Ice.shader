Shader "Custom/Ice"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _ShadeColor("Shade Color", Color) = (0.5,0.5,0.5)
        _SpecularPower("Specular Power", Range(0, 30)) = 1
        _SpecularColor("Specular Color", Color) = (0.5,0.5,0.5)
        _FreezeRate("Freeze Rate", Range(0.0, 1.0)) = 0
        _FreezeRateMax("Freeze Rate Max", Range(0.0, 1.0)) = 0.7
        _FreezeBorder("Freeze Border", Range(0.0, 1.0)) = 0.05
        _FreezeBorderColor("Freeze Border Color", Color) = (0.5,0.5,0.5)
        _IceTex("Ice Texture", 2D) = "black" {}
        _DisolveTex("DisolveTex", 2D) = "white" {}
        [Normal] _IceNormalMap("Ice Normal Map", 2D) = "bump" {}
        _IceNormalMapScale("Ice Normal Map Scale", Range(0, 3)) = 0.5
        _IceSpecularPower("Ice Specular Power", Range(0, 30)) = 1
        _IceSpecularColor("Ice Specular Color", Color) = (0.5,0.5,0.5)
        _RimLightBorder("RimLight Border", Range(0.0, 1.0)) = 0.5
        _RimLightColor("Rim Light Color", Color) = (0.5,0.5,0.5)
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

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
                float3 lightDir : TEXCOORD2;
                float3 viewDir : TEXCOORD3;
                float2 uvIce : TEXCOORD4;
                half4 tangent : TEXCOORD5;
                half3 binormal : TEXCOORD6;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float3 _ShadeColor;
            float _SpecularPower;
            float3 _SpecularColor;
            float _FreezeRate;
            float _FreezeRateMax;
            float _FreezeBorder;
            float3 _FreezeBorderColor;
            sampler2D _IceTex;
            float4 _IceTex_ST;
            sampler2D _DisolveTex;
            sampler2D _IceNormalMap;
            float _IceNormalMapScale;
            float _IceSpecularPower;
            float3 _IceSpecularColor;
            float _RimLightBorder;
            float3 _RimLightColor;

            v2f vert(appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                o.uvIce = TRANSFORM_TEX(v.uv, _IceTex);

                o.normal = UnityObjectToWorldNormal(v.normal);
                o.tangent = mul(unity_ObjectToWorld, v.tangent.xyz);
                // binormalはtangentのwとunity_WorldTransformParams.wを掛ける（Unityの決まり）
                o.binormal = normalize(cross(v.normal.xyz, v.tangent.xyz) * v.tangent.w * unity_WorldTransformParams.w);
                o.binormal = mul(unity_ObjectToWorld, o.binormal);

                o.lightDir = normalize(WorldSpaceLightDir(v.vertex));
                o.viewDir = normalize(WorldSpaceViewDir(v.vertex));

                return o;
            }

            fixed4 frag(v2f i) : SV_Target {
                fixed4 col = tex2D(_MainTex, i.uv);

                half3 halfDir = normalize(i.lightDir + i.viewDir);

                // 拡散反射
                float diffuse = max(dot(i.normal, i.lightDir), 0);
                float3 shade = lerp(_ShadeColor, 1, diffuse);
                col.rgb *= shade;

                // 鏡面反射
                float specularRate = pow(max(0, dot(i.normal, halfDir)), _SpecularPower);
                float3 specular = specularRate * _SpecularColor;
                col.rgb += specular;

                fixed4 disolve = tex2D(_DisolveTex, i.uvIce);
                float freezeRate = lerp(0, _FreezeRateMax, step(disolve.r, _FreezeRate));

                float freezeBorderRate = 1 - smoothstep(0, _FreezeBorder, disolve.r - _FreezeRate);
                freezeBorderRate *= step(freezeRate, 0);
                col.rgb += _FreezeBorderColor * freezeBorderRate;

                // 氷のテクスチャをブレンド
                col = lerp(col, tex2D(_IceTex, i.uvIce), freezeRate);

                // 氷のノーマルマップを反映した法線を計算
                half3 localNormal = UnpackNormalWithScale(tex2D(_IceNormalMap, i.uvIce), _IceNormalMapScale);
                // 接空間のベクトルをワールド空間に変換
                float3 normal = i.tangent * localNormal.x + i.binormal * localNormal.y + i.normal * localNormal.z;
                i.normal = lerp(i.normal, normalize(normal), freezeRate);

                // 氷の鏡面反射
                specularRate = pow(max(0, dot(i.normal, halfDir)), _IceSpecularPower);
                specular = specularRate * _IceSpecularColor;
                col.rgb += lerp(0, specular, freezeRate);

                // リムライト
                float rimLightRate = 1 - smoothstep(0, _RimLightBorder, dot(i.normal, i.viewDir));
                rimLightRate = lerp(0, rimLightRate, freezeRate);
                col.rgb += lerp(0, _RimLightColor, rimLightRate);

                // 氷の拡散反射
                float iceShade = lerp(shade, 1, 0.5);
                col.rgb *= lerp(1, iceShade, freezeRate);

                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
