Shader "Custom/Scan_Shader"
{
    Properties
    {
        _PartialView ("Partial View", Range(0, 1)) = 0.5
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        fixed4 _Color;
        float _PartialView;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            float uvX = IN.uv_MainTex.x;

            // Calculate the cutoff position
            float cutoff = 1.0 - _PartialView;

            // Apply the cutoff to the mesh
            if (uvX < cutoff || uvX > (1.0 - cutoff))
            {
                c.a = 0.0;
            }

            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
