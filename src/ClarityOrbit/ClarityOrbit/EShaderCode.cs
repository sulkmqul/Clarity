namespace Clarity.Engine
{
    /// <summary>
    /// Shader ID
    /// </summary>
    public enum EShaderCode : int
    {
		System_Default = -100,
		System_NoTexture = -99,
		System_TextureAnime = -98,
		System_TextureUseAlpha = -97,
		TileMap = 2,

    }

    
    /// <summary>
    /// Clarity Engine Aid Class
    /// </summary>
    public static partial class ClarityAid
    {
        
		public static void SetShaderCode(this Clarity.Engine.Element.ClarityObject obj, EShaderCode esh)
        {
            obj.ShaderID = (int)esh;
        }
	
    }

}