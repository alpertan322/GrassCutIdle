Shader "Custom/ToonWithHighlightShadowRampOutline" {
    Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _HighlightColor ("Highlight Color", Color) = (1,1,1,1)
        _ShadowColor ("Shadow Color", Color) = (0,0,0,1)
        _ToonRamp ("Toon Ramp", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineWidth ("Outline Width", Range(0, 0.1)) = 0.01
    }
    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        LOD 100
        Cull Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityPBSLighting.cginc"
            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            struct v2f {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };
            sampler2D _MainTex;
            float4 _Color;
            float4 _HighlightColor;
            float4 _ShadowColor;
            sampler2D _ToonRamp;
            float4 _OutlineColor;
            float _OutlineWidth;
            v2f vert (appdata v) {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv = v.uv;
            UNITY_TRANSFER_FOG(o,o.vertex);
            return o;
            }
            fixed4 frag (v2f i) : SV_Target {
            fixed4 highlight = _HighlightColor;
            fixed4 shadow = _ShadowColor;
            float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
            float3 surfaceNormal = tex2D(_MainTex, i.uv).rgb * 2 - 1;
            float highlightAmount = dot(surfaceNormal, lightDirection);
            highlightAmount = pow(saturate(highlightAmount), 4);
            highlight *= highlightAmount;
            float shadowAmount = dot(-surfaceNormal, lightDirection);
            shadowAmount = pow(saturate(shadowAmount), 4);
            shadow *= shadowAmount;
            fixed4 ramp = tex2D(_ToonRamp, tex2D(_MainTex, i.uv).r);
            fixed4 outline = _OutlineColor;
            outline.a = tex2D(_MainTex, i.uv + _ScreenParams.zw * _OutlineWidth).a;
            fixed4 col = lerp(ramp, _Color, tex2D(_MainTex, i.uv).r);
            col.rgb += highlight.rgb + shadow.rgb + outline.rgb;
                col.rgb = col.rgb * col.a;
            return col;
}
ENDCG
}
}
FallBack "Diffuse"
}