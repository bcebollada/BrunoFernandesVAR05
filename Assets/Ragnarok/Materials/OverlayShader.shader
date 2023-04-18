Shader "Unlit/OverlayShader"
{
    Properties
    {

    }
    SubShader
    {
        Pass
        {
            ZTest GEqual

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert(appdata v)
            {
                v2f o;
                //v.vertex += float4(0, 1, 0, 0) * sin(_Time.y);

                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            // "pixel" shader.
            fixed4 frag(v2f i) : SV_Target
            {
                return float4(1, 0, 0, 1);
            }
            ENDCG
        }
    }
}