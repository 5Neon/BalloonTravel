Shader "Roystan/ToonShader"
{
Properties
{
	//색
	_Color("Color", Color) = (1,1,1,1)

	//텍스쳐
	_MainTex("Main Texture", 2D) = "white" {}

	//반사 크기 제어
	_Glossiness("Glossiness", Float) = 32
	
	//반사광 색
	[HDR]
	_RimColor("Rim Color", Color) = (1,1,1,1)

	//반사광 크기
	_RimAmount("Rim Amount", Range(0, 1)) = 0.716

	//반사광 경계
	_RimThreshold("Rim Threshold", Range(0, 1)) = 0.1
	
	//광량 제어
	[HDR]
	_ColorBrightness("ColorBrightbess", Color) = (35,35,35,1)

	//그림자 경계선 제어
    _ShdowSmooth("ShadowSmooth",Float) = 0.01
}

SubShader
{
	Pass
	{
		//패스값 설정
		Tags
		{
			"LightMode" = "ForwardBase"
			"PassFlags" = "OnlyDirectional"
			"RenderType"= "Opaque"
		}
		
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma multi_compile_fwdbase
		
		#include "UnityCG.cginc"
		#include "Lighting.cginc"
		#include "AutoLight.cginc"

		struct appdata
		{
			float4 vertex : POSITION;				
			float4 uv : TEXCOORD0;
			float3 normal : NORMAL;
		};
		

		struct v2f
		{
				float4 pos : SV_POSITION;
				float3 worldNormal : NORMAL;
				float2 uv : TEXCOORD0;
				float3 viewDir : TEXCOORD1;	
				SHADOW_COORDS(2)
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				//버택스값을 월드 스페이스에서 쉐도우맵 스페이스로 변환
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.worldNormal = UnityObjectToWorldNormal(v.normal);		
				o.viewDir = WorldSpaceViewDir(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				TRANSFER_SHADOW(o)
				return o;
			}
			
			float4 _Color;

			float _Glossiness;

			float4 _RimColor;
			float _RimAmount;
			float _RimThreshold;

			float4 _ColorBrightness;
			float _ShdowSmooth;

			float4 frag (v2f i) : SV_Target
			{
				float3 normal = normalize(i.worldNormal);
				float3 viewDir = normalize(i.viewDir);

				//방향광에서 조명 계산
				float NdotL = dot(_WorldSpaceLightPos0, normal);

				//그림자 맵 샘플링
				float shadow = SHADOW_ATTENUATION(i);

				//빛 명암 분할 및 강도
				float lightIntensity = smoothstep(0, _ShdowSmooth, NdotL * shadow);	
				float4 light = lightIntensity * _LightColor0;
				
				//정반사 계산
				float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
				float NdotH = dot(normal, halfVector);

				//광택
				float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);
				float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
				float4 specular = specularIntensitySmooth * 1; //				
				
				//반사광 계산
				float rimDot = 1 - dot(viewDir, normal);
				float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
				rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
				float4 rim = rimIntensity * _RimColor;

				float4 sample = tex2D(_MainTex, i.uv);

				//float shadow = SHADOW_ATTENUATION(i);

				//return float4(0, 0, 0, (1 - shadow) * _Alpha);

				return (light + _ColorBrightness + specular + rim) * _Color * sample;
			}
			ENDCG
		}

		//그림자 부여
		UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
		//UsePass "Toon/Lighted/FORWARD"
		//UsePass "Toon/Basic Outline/OUTLINE"
		//UsePass "Shaders/SomeShader/SHADOWCASTER"
		//UsePass "Shaders/SomeOtherShader/OUTLINEPASS"
	}
}