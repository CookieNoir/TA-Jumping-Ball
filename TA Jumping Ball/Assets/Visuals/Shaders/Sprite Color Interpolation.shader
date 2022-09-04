Shader "Unlit/Sprite Color Interpolation"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"RenderType"="Transparent" 
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
			};

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color;
				return OUT;
			}

			sampler2D _MainTex;
			uniform fixed4 _WorldColor1;
			uniform fixed4 _WorldColor2;

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 initialColor = tex2D (_MainTex, IN.texcoord) * IN.color;
				fixed4 lerpColor = lerp(_WorldColor1, _WorldColor2, initialColor.r);
				lerpColor.a *= initialColor.a;
				return lerpColor;
			}
		ENDCG
		}
	}
}