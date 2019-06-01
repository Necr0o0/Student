Shader "Programowanie shaderow/Wyklad 2/3 - ShowNormal"
{
	Properties{

	}

    SubShader
    {
        CGPROGRAM

        #pragma surface surf Lambert

        struct Input
        {
        	float3 worldNormal;
        };

        void surf (Input IN, inout SurfaceOutput OUT)
        {
            OUT.Emission = IN.worldNormal;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
