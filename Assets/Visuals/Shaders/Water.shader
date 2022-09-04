Shader "Unlit/Water"
{
    Properties
    {
        _Waves ("Waves Height(X, Z), Frequency(Y, W)", vector) = (0,0,0,0)
        _Speed ("Waves Speed(X, Y)", vector) = (0,0,0,0)
		_Offset ("Fixed Y-Offset", float) = 0.0
		_Power ("Alpha Power", float) = 1.0
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
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
			};

			float4 _Waves;
			float4 _Speed;
			float _Offset;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				float x = IN.vertex.x;
				float offset = _Offset + IN.color.a * (
					_Waves.x * cos(_Waves.y * x + _Speed.x * _Time.y)
					+ _Waves.z * sin(_Waves.w * x + _Speed.y * _Time.y));
				float4 vertex = IN.vertex + float4(0.0, offset, 0.0, 0.0);
				OUT.vertex = UnityObjectToClipPos(vertex);
				OUT.color = IN.color;
				return OUT;
			}

			float _Power;
			uniform fixed4 _WorldColor1;
			uniform fixed4 _WorldColor2;

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 lerpColor = lerp(_WorldColor1, _WorldColor2, IN.color.r);
				lerpColor.a *= pow(IN.color.a, _Power);
				return lerpColor;
			}
		ENDCG
		}
	}
}