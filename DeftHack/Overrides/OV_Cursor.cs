using System.Reflection;




using UnityEngine;

 
public static class OV_Cursor
{
    
    [Override(typeof(Cursor), "set_lockState", BindingFlags.Static | BindingFlags.Public, 0)]
    public static void OV_set_lockState(CursorLockMode rMode)
    {
        bool flag = MenuComponent.IsInMenu && !PlayerCoroutines.IsSpying && (rMode == CursorLockMode.Confined || rMode == CursorLockMode.Locked);
        if (!flag)
        {
            OverrideUtilities.CallOriginal(null, new object[]
            {
                    rMode
            });
        }
    }
}

