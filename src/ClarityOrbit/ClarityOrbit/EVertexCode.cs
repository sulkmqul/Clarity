namespace Clarity.Engine
{
    /// <summary>
    /// Vertex ID
    /// </summary>
    public enum EVertexCode : int
    {
		System_Rect = -100,
		System_Line = -99,
		VGrid = 2,

    }

    
    /// <summary>
    /// Clarity Engine Aid Class
    /// </summary>
    public static partial class ClarityAid
    {
        
		public static void SetVertexCode(this Clarity.Engine.Element.ClarityObject obj, EVertexCode evc)
        {
            obj.VertexID = (int)evc;
        }
	
    }

}