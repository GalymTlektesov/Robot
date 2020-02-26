Shader "Unlit/InwardShader"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Cull Front
        
        CGPROGRAM
        #pragma surface surf Standard vertex:vert

        void vert(inout appdata_full v){
            v.normal.xyz = v.normal * -1;
        }

        sampler2D _MainTex;

        struct Input{
            float2 uv_MainTex;
        };
        
        void surf (Input IN, inout SurfaceOutputStandard o) {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
