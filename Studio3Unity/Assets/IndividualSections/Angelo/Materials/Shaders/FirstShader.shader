// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/FirstShader"
{
	Properties
	{
		_Tint ("Tint", Color) = (1, 1, 1, 1)
		_MainTex ("Texture", 2D) = "white" {}
	}

	SubShader
	{
		pass
		{
			CGPROGRAM
			
			#pragma vertex MyVertexProgram
			#pragma fragment MyFragmentProgram	

			#include "UnityCG.cginc"

			float4 _Tint;
			sampler2D _MainTex;
			float4 _MainTex_ST;

			struct Interpolators 
			{
				float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
				// float3 localPosition : TEXCOORD0;
			};

			struct VertexData
			{
				float4 position : POSITION;
				float2 uv : TEXCOORD0;
			};

			Interpolators MyVertexProgram(VertexData v)
			{
				Interpolators i;
				//i.localPosition = v.position.xyz;
				i.position = UnityObjectToClipPos(v.position);
				i.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return i;
			}

			float4 MyFragmentProgram(Interpolators i) : SV_TARGET
			{
				//return _Tint;
				//return float4(i.localPosition + 0.5, 1) * _Tint;
				//return float4(i.uv, 1, 1);
				return tex2D(_MainTex, i.uv) * _Tint;
			}

			ENDCG
		}
	}
}
