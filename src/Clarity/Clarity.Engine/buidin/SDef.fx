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
	float4 Col;
	float2 tex_div;
	float2 tex_offset;

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
	outdata.col = RData.Col;

	outdata.tex = vsin.tex;

	return outdata;
}


//TexAnimation
//��{�I�ɂ����g��
PS_IN VsTextureAnimation(VS_IN vsin)
{
	PS_IN outdata = (PS_IN)0;

	//outdata.pos = vsin.pos;
	outdata.pos = mul(vsin.pos, RData.WorldViewProj);
	outdata.col = RData.Col;

	//outdata.tex.x = (RData.anime_index.x + vsin.tex.x) * RData.tex_div.x;
	//outdata.tex.y = (RData.anime_index.y + vsin.tex.y) * RData.tex_div.y;

	outdata.tex.x = RData.tex_offset.x + (vsin.tex.x * RData.tex_div.x);
	outdata.tex.y = RData.tex_offset.y + (vsin.tex.y * RData.tex_div.y);


	return outdata;
}


/////////////////////////////////////////////////////////////////////////////////////////////////////////
//PixelShader
//��{�I�ɂ����g��
float4 PsDefault(PS_IN psin) : SV_TARGET
{
	//return float4(0.5f, 0.5f, 1.0f, 1.0f);
	float4 col = pix.Sample(picsamp, psin.tex);


	col.x *= psin.col.x;
	col.y *= psin.col.y;
	col.z *= psin.col.z;

	col.w *= psin.col.w;



	return col;
}


/////////////////////////////////////////////////////////////////////////////////////////////////////////
//PixelShader
//??{?I??????g??
float4 PsHitLight(PS_IN psin) : SV_TARGET
{
	float4 col = pix.Sample(picsamp, psin.tex);

	col.x += 0.3f;
	col.y += 0.3f;
	col.z += 0.3f;
	col.w *= psin.col.w;



	return col;
}


/////////////////////////////////////////////////////////////////////////////////////////////////////////
//PixelShader
//??{?I??????g??
float4 PsNoTex(PS_IN psin) : SV_TARGET
{
	//return float4(0.5f, 0.5f, 1.0f, 1.0f);
	float4 col = psin.col;


	return col;
}

///////////////////////////////////////////////////////////////////////////////////////////
float4 PsTextureAlphaOnlyBind(PS_IN psin) : SV_TARGET
{
	//return float4(0.5f, 0.5f, 1.0f, 1.0f);
	float4 col = pix.Sample(picsamp, psin.tex);

	col.x = psin.col.x;
	col.y = psin.col.y;
	col.z = psin.col.z;
	col.w *= psin.col.w;

	return col;
}
