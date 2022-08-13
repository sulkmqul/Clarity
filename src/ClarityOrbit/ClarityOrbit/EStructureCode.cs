namespace Clarity.Engine
{
    /// <summary>
    /// Clarity Engine Structure IDs
    /// </summary>
    public enum EStructureCode : int
    {
		Manager = 1,
		Layer = 2,
		Grid = 3,
		Control = 4,
		Infomation = 5,

    }

    
    /// <summary>
    /// Clarity Engine Aid Class
    /// </summary>
    public static partial class ClarityAid
    {
        
		public static void AddElement(EStructureCode id, Clarity.BaseElement data)
		{
			Clarity.Engine.ClarityEngine.AddManage((long)id, data);
		}
	
    }

}