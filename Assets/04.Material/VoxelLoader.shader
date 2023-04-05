Shader "VoxelLoader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
		_MainTex("Texture", 2D) = "white" {}

		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0

		[Toggle(_EMISSION)] _EMISSION("Emission", float) = 1
		_EmissionColor("_EmissionColor", color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows
		#pragma shader_feature _EMISSION
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
		half4 _EmissionColor;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
           // fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			fixed4 col = tex2D(_MainTex, float2(IN.uv_MainTex.x, 1.5f / 2.0f));
			fixed4 c = _Color * col;
            o.Albedo = c.rgb;
#ifdef _EMISSION
			o.Emission = tex2D(_MainTex, float2(IN.uv_MainTex.x, 0.5 / 2.0f)) * _EmissionColor;
#endif	

		//	o.Emission = tex2D(_MainTex, IN.uv_MainTex + float2(0, -1.0f / 2.0f));
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
