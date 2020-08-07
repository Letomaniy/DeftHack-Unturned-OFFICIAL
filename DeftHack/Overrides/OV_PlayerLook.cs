using SDG.Unturned;
using System.Reflection;



 
public class OV_PlayerLook
{
 
    [Override(typeof(PlayerLook), "onDamaged", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
    public static void OV_onDamaged(byte damage)
    {
        bool noFlinch = MiscOptions.NoFlinch;
        if (!noFlinch)
        {
            OverrideUtilities.CallOriginal(null, new object[]
            {
                    damage
            });
        }
    }
}

