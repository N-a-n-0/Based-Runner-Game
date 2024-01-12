Shader "Unlit/CurvedUnlit"
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
	}
	SubShader
	{
		Tags { "Queue" = "Transparent" "RenderType" = "Transparency"}
		LOD 250


		ZWrite on
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