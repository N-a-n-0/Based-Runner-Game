Shader "Unlit/Hologram"
{
    Properties
    {//kinda like public variables
        _MainTex ("Albedo Texture", 2D) = "white" {} // Variable_name ("Display name in Unity", 2D)  ="white"{}
        _TintColor("Tint Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100         //LOD STANDS FOR LEVEL OF DETAIL

        Pass
        {
            CGPROGRAM   //short for cgraphics code
            #pragma vertex vert
            #pragma fragment frag
          

            #include "UnityCG.cginc" //

            struct appdata //this is gonna be used to pass information about the vertices of our 3d model
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f // 
            {
                float2 uv : TEXCOORD0;
               
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _TintColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
               
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv) * _TintColor;
          
                return col;
            }
            ENDCG
        }
    }
}


//UNLIT Shader

//VERTEX FUNCTION: takes the shape of model, potentially modifies it.
//FRAGMENT FUNCTION: Applies colors to the shape output by the vert FUNCTION
//PROPERTY DATA: Colors, textures, values set by user in the inspector

// MESH -> VERTEX FUNCTION -> FRAGMENT FUNCTION -> IMAGE