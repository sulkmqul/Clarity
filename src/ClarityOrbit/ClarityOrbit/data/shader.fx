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
	float2 tex_div;
	float2 index;
};

RegistData RData : register(s0);

//�e�N�X�`��
Texture2D pix : register(t0);
SamplerState picsamp : register(s1);


/////////////////////////////////////////////////////////////////////////////////////////////////////////
//VretexShader
//��{�I�ɂ����g��
PS_IN VsDefault(VS_IN vsin)
{
	PS_IN outdata = (PS_IN)0;

	//outdata.pos = vsin.pos;
	outdata.pos = mul(vsin.pos, RData.WorldViewProj);
	outdata.col = RData.SelectCololor;

	outdata.tex = vsin.tex;

	return outdata;
}



/////////////////////////////////////////////////////////////////////////////////////////////////////////
//PixelShader
//��{�I�ɂ����g��
float4 PsGrid(PS_IN psin) : SV_TARGET
{
	float4 col = psin.col;	
	float border = 0.05;
	float sa = 1.0 - border;
	col.w = 0.0;
	
	if (psin.tex.x < border || psin.tex.x > sa)
	{
		col.w = 1.0;		
	}
	if (psin.tex.y < border || psin.tex.y > sa)
	{
		col.w = 1.0;
	}
	return col;
}

