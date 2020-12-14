Shader "Custom/dukong"
{
	Properties
	{
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Metallic("Metallic", Range(0,1)) = 0
		_Smoothness("Smoothness", Range(0,1)) = 0.5
		_BumpMap("Normalmap", 2D) = "bump"{}
		_Color("Color", Color) = (1,1,1,1)
		_StencilTest("StencilNum", int) = 6
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		Blend SrcAlpha OneMinusSrcAlpha

		Stencil{
			Ref 1
			Comp[_StencilTest]
		}

		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows

		sampler2D _MainTex;
		sampler2D _BumpMap;
		float _Metallic;
		float _Smoothness;
		fixed4 _Color;

		struct Input {
			float2 uv_MainTex;
			float2 uv_BumpMap;
		};

		void surf(Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			fixed4 n = tex2D(_BumpMap, IN.uv_BumpMap);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
			o.Metallic = _Metallic;
			o.Smoothness = _Smoothness;
		}

		ENDCG
	}
	FallBack "Diffuse"
}
