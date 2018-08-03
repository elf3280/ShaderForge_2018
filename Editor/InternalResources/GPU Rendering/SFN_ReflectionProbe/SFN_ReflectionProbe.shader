Shader "Hidden/Shader Forge/SFN_ReflectionProbe"
{
	Properties
	{
		_OutputMask("Output Mask", Vector) = (1, 1, 1, 1)
		_VR("", 2D) = "black" { }
		_Mip("",2D)="Black"{}
	}
		SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		Pass
	{
		CGPROGRAM

#pragma vertex vert
#pragma fragment frag
#define UNITY_PASS_FORWARDBASE
#include "UnityCG.cginc"
#pragma target 3.0
		uniform float4 _OutputMask;
	uniform sampler2D _VR;
	uniform sampler2D _Mip;

	struct VertexInput
	{
		float4 vertex: POSITION;
		float2 texcoord0: TEXCOORD0;
	};
	struct VertexOutput
	{
		float4 pos: SV_POSITION;
		float2 uv: TEXCOORD0;
	};
	VertexOutput vert(VertexInput v)
	{
		VertexOutput o = (VertexOutput)0;
		o.uv = v.texcoord0;
		o.pos = UnityObjectToClipPos(v.vertex);
		return o;
	}
	float4 frag(VertexOutput i) : COLOR
	{

		// Read inputs
		float4 _vr = tex2D(_VR, i.uv);
		float4 _mip =tex2D(_Mip,i.uv);

		float4 skyData=UNITY_SAMPLE_TEXCUBE_LOD(unity_SpecCube0,_vr,_mip);
		// Operator
		float4 outputColor = float4(DecodeHDR(skyData,unity_SpecCube0_HDR),1);

		// Return
		return outputColor * _OutputMask;
	}
		ENDCG

	}
	}
}
