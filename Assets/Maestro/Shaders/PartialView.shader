Shader "Custom/PartialView"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _PartialView ("Partial View", Range(0, 1)) = 0.5
        _ObjectPos ("Object Position", Vector) = (0, 0, 0)
        _ObjectHeight ("Object Height", Vector) = (0, 0, 0)
    }

    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        LOD 200

        Blend SrcAlpha OneMinusSrcAlpha // Enable transparency blending

        CGPROGRAM
        #pragma surface surf Lambert alpha

        sampler2D _MainTex;
        float _PartialView;
        float3 _ObjectPos;
        float3 _ObjectHeight;
        fixed4 _Color;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
            float bottomDistance = IN.worldPos.y;

            // Calculate the cutoff range
            float cutoffStart = _ObjectPos.y - (_ObjectHeight.y / 2); // Add the object position to the cutoffStart
            float cutoffEnd = _ObjectPos.y - (_ObjectHeight.y / 2) + (_PartialView * _ObjectHeight.y); // Add the object position to the cutoffEnd

            // Apply the cutoff to the mesh
            if (bottomDistance < cutoffStart || bottomDistance > cutoffEnd)
            {
                discard;
            }

            // Set the albedo color with opacity
            o.Albedo = _Color.rgb;
            o.Alpha = _Color.a;
        }
        ENDCG
    }


    FallBack "Diffuse"
}
