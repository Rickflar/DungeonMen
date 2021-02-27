sampler s0;
texture2D lightMask;
sampler lightSampler : register(s1) = sampler_state {
	Texture = <lightMask>;
	AddressU = Clamp;
	AddressV = Clamp;
};

struct VSOutput
{
	float4 position		: SV_Position;
	float4 color		: COLOR0;
	float2 texCoord		: TEXCOORD0;
};

float4 PixelShaderFunction(VSOutput input) : SV_TARGET0
{
	float4 color = tex2D(s0, input.texCoord);

	if (!any(color)) return color;

	float step = 1.0 / 7;

	if (input.texCoord.x < (step * 1)) color = float4(1, 0, 0, 1);
	else if (input.texCoord.x < (step * 2)) color = float4(1, .5, 0, 1);
	else if (input.texCoord.x < (step * 3)) color = float4(1, 1, 0, 1);
	else if (input.texCoord.x < (step * 4)) color = float4(0, 1, 0, 1);
	else if (input.texCoord.x < (step * 5)) color = float4(0, 0, 1, 1);
	else if (input.texCoord.x < (step * 6)) color = float4(.3, 0, .8, 1);
	else                            color = float4(1, .8, 1, 1);

	return color;
}


technique Technique1
{
	pass Pass1
	{
		PixelShader = compile ps_3_0 PixelShaderFunction();
	}
}