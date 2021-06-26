struct VS_IN
{
	float4 pos : POSITION;
	float4 col : COLOR;
	float2 tex : TEXCOORD;
};


struct PS_IN
{
	float4 pos : SV_POSITION;	
	float4 col : COLOR;
	float2 tex : TEXCOORD;
	float2 tex2 : TEXCOORD2;
};


struct RegistData
{
	float4x4 WorldViewProj;
	float4 Col;
	float2 ScrollOffset;
	float2 ScrollOffset2;

	float2 ScrollSize;
	float2 ScrollSize2;
	

};

RegistData RData : register(s0);

//texture no 1
Texture2D pix : register(t0);
//texture no 2
Texture2D pix2 : register(t1);
SamplerState picsamp : register(s1);


/////////////////////////////////////////////////////////////////////////////////////////////////////////
//VretexShader
PS_IN VsTexScroll(VS_IN vsin)
{
	PS_IN outdata = (PS_IN)0;

	//outdata.pos = vsin.pos;
	outdata.pos = mul(vsin.pos, RData.WorldViewProj);
	outdata.col = RData.Col;

	outdata.tex = vsin.tex;
	outdata.tex2 = vsin.tex;

	outdata.tex.x *= RData.ScrollSize.x;
	outdata.tex.y *= RData.ScrollSize.y;

	outdata.tex.x += RData.ScrollOffset.x;
	outdata.tex.y += RData.ScrollOffset.y;


	
	outdata.tex2.x *= RData.ScrollSize2.x;
	outdata.tex2.y *= RData.ScrollSize2.y;

	outdata.tex2.x += RData.ScrollOffset2.x;
	outdata.tex2.y += RData.ScrollOffset2.y;
	
	
	return outdata;
}



/////////////////////////////////////////////////////////////////////////////////////////////////////////
//PixelShader
float4 PsTexScroll(PS_IN psin) : SV_TARGET
{
	//return float4(1.0f, 0.0f, 1.0f, 1.0f);

	float4 col = 0;
	float2 cc = psin.tex;		
	col = pix.Sample(picsamp, cc);


	float2 cc2 = psin.tex2;
	if (cc.y > 1.0)
	{
		cc2.y -= 1.0f;
		col = pix2.Sample(picsamp, cc2);
	}
	
	if (cc.y < 0.0)
	{
		cc2.y += 1.0f;
		col = pix2.Sample(picsamp, cc2);
	}
	

	//float4 col = pix2.Sample(picsamp, psin.tex);

	col.w *= psin.col.w;
	return col;
}
