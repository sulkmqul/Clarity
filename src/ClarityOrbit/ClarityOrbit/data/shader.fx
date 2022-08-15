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
};


struct RegistData
{
	float4x4 WorldViewProj;
	float4 SelectCololor;		
	float2 TexOffset;	//LT index : texdiv * index
	float2 TexAreaSize;	//area size * texdiv
};

RegistData RData : register(s0);

//�e�N�X�`��
Texture2D pix : register(t0);
SamplerState picsamp : register(s1);


/////////////////////////////////////////////////////////////////////////////////////////////////////////
//VretexShader
PS_IN VsTileMap(VS_IN vsin)
{
	PS_IN outdata = (PS_IN)0;

	outdata.pos = mul(vsin.pos, RData.WorldViewProj);
	outdata.col = RData.SelectCololor;
	

	outdata.tex.x = RData.TexOffset.x + (vsin.tex.x * RData.TexAreaSize.x);
	outdata.tex.y = RData.TexOffset.y + (vsin.tex.y * RData.TexAreaSize.y);


	return outdata;
}



/////////////////////////////////////////////////////////////////////////////////////////////////////////
//PixelShader
float4 PsDefault(PS_IN psin) : SV_TARGET
{	
	float4 col = pix.Sample(picsamp, psin.tex);


	col.x *= psin.col.x;
	col.y *= psin.col.y;
	col.z *= psin.col.z;

	col.w *= psin.col.w;

	return col;
}

