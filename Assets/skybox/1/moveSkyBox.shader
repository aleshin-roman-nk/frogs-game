Shader "Custom/MovingSkybox"
{
    Properties
    {
        _MainTex ("Cubemap (RGB)", Cube) = "white" {}
        _TexCoord ("Texture Coordinate", Range(0, 1)) = 0.0
    }
    SubShader
    {
        Tags { "Queue" = "Background" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float3 texcoord : TEXCOORD0;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float3 texcoord : TEXCOORD0;
            };

            samplerCUBE _MainTex;
            float _TexCoord;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord + float3(_TexCoord, 0.0, 0.0);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return texCUBE(_MainTex, i.texcoord);
            }
            ENDCG
        }
    }
    FallBack "RenderType"
}
