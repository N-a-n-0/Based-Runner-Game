Shader "Custom/CurvedLit"
{ 
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_TintColor("Tint Color", Color) = (1, 1, 1, 1)
		_Emission("Emission", Range(0, 3)) = 0
		CutoutThresh("Cutout Threshold", Range(0.0,1.0)) = 0.0
		Distance("Distance", Float) = 0
		Amplitude("Amplitude", Float) = 0
		Speed("Speed", Float) = 0
		Amount("Amount", Range(0.0,1.0)) = 0

		[Header(Surface options)] // Creates a text header
        // [MainTexture] and [MainColor] allow Material.mainTexture and Material.color to use the correct properties
        [MainTexture] _ColorMap("Color", 2D) = "white" {}
        [MainColor] _ColorTint("Tint", Color) = (1, 1, 1, 1)
        _Smoothness("Smoothness", Float) = 0
	}
	SubShader
	{
		 Tags {"Queue"="Transparent+1"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "RenderPipeline"="UniversalPipeline"}
			/*
		LOD 250
		 Tags {"RenderPipeline" = "UniversalPipeline"}

        // Shaders can have several passes which are used to render different data about the material
        // Each pass has it's own vertex and fragment function and shader variant keywords
		ZWrite on
		Cull Off
		//Blend [_SrcBlend] [_DstBlend]
		ZWrite [_ZWrite]
        Pass {
            Name "ForwardLit" // For debugging
            Tags{"LightMode" = "UniversalForward"} // Pass specific tags. 
            // "UniversalForward" tells Unity this is the main lighting pass of this shader

            HLSLPROGRAM // Begin HLSL code

            #define _SPECULAR_COLOR

            // Shader variant keywords
            // Unity automatically discards unused variants created using "shader_feature" from your final game build,
            // however it keeps all variants created using "multi_compile"
            // For this reason, multi_compile is good for global keywords or keywords that can change at runtime
            // while shader_feature is good for keywords set per material which will not change at runtime

            // Global URP keywords
#if UNITY_VERSION >= 202120
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE
#else
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
#endif
            #pragma multi_compile_fragment _ _SHADOWS_SOFT

            // Register our programmable stage functions
			   #pragma vertex Vertex
            #pragma fragment Fragment

				
          #include "MyLitForwardLitPass.hlsl"
            ENDHLSL
        }


			ZWrite on
		
        Pass {
            // The shadow caster pass, which draws to shadow maps
            Name "ShadowCaster"
            Tags{"LightMode" = "ShadowCaster"}

            ColorMask 0 // No color output, only depth

            HLSLPROGRAM
            #pragma vertex Vertex
            #pragma fragment Fragment

            #include "MyLitShadowCasterPass.hlsl"
            ENDHLSL
        }

		*/
		
		
		Blend SrcAlpha OneMinusSrcAlpha
		Pass
		{
			
			CGPROGRAM
		

			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _TintColor;
			float _Emission;
			float CutoutThresh;
			float Distance;
			float Amplitude;
			float Speed;
			float Amount;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float4 color : COLOR;
			};

			struct v2f 
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 color : TEXCOORD2;
				float4 vertex : SV_POSITION;
			};

			float4 _MainTex_ST;
			float _CurveStrength;

			v2f vert(appdata v)
			{
				v2f o;
				v.vertex.x += sin(_Time.y * Speed + v.vertex.y *   Amplitude) * Distance * Amount;//PART OF THAT WEIRD EFFECT

				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				float dist = UNITY_Z_0_FAR_FROM_CLIPSPACE(o.vertex.z);
				o.vertex.y -= _CurveStrength * dist * dist * _ProjectionParams.x;

				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.color = v.color;

				UNITY_TRANSFER_FOG(o, o.vertex);

				return o;
			}

			fixed4 frag(v2f i) : SV_Target
	{

		// Sample the texture
		fixed4 col = tex2D(_MainTex, i.uv) * _TintColor;

		// Add emission glow with scaling based on original color
		col.rgb += _Emission * col.rgb;

	clip(col.r - CutoutThresh);   //PART OF THAT WEIRD EFFECT

		UNITY_APPLY_FOG(i.fogCoord, col);

		return col;
	}

			ENDCG
		}





	}
}