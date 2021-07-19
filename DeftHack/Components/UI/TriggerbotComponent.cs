using SDG.Unturned;
using System.Collections;
using System.Reflection;
using UnityEngine;


[Component]
public class TriggerbotComponent : MonoBehaviour
{
    [Initializer]
    public static void Init()
    {
        TriggerbotComponent.CurrentFiremode = typeof(UseableGun).GetField("firemode", BindingFlags.Instance | BindingFlags.NonPublic);
    }

    public void Start()
    {
        base.StartCoroutine(TriggerbotComponent.CheckTrigger());
    }
    public static IEnumerator CheckTrigger()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(0.1f);
            bool flag = !TriggerbotOptions.Enabled || !DrawUtilities.ShouldRun() || OptimizationVariables.MainPlayer.stance.stance == EPlayerStance.SPRINT || OptimizationVariables.MainPlayer.stance.stance == EPlayerStance.CLIMB || OptimizationVariables.MainPlayer.stance.stance == EPlayerStance.DRIVING;
            if (flag)
            {
                TriggerbotOptions.IsFiring = false;
            }
            else
            {
                PlayerLook look = OptimizationVariables.MainPlayer.look;
                Useable u = OptimizationVariables.MainPlayer.equipment.useable;
                Useable useable = u;
                Useable useable2 = useable;
                if (useable2 == null)
                {
                    TriggerbotOptions.IsFiring = false;
                }
                else
                {
                    UseableGun useableGun;
                    UseableGun gun;
                    UseableMelee useableMelee;
                    if ((useableGun = (useable2 as UseableGun)) != null)
                    {

                        gun = useableGun;
                        ItemGunAsset PAsset = (ItemGunAsset)OptimizationVariables.MainPlayer.equipment.asset;
                        RaycastInfo ri = RaycastUtilities.GenerateOriginalRaycast(new Ray(look.aim.position, look.aim.forward), PAsset.range, RayMasks.DAMAGE_CLIENT);
                        if (AimbotCoroutines.LockedObject != null && AimbotCoroutines.IsAiming)
                        {
                            Ray r = OV_UseableGun.GetAimRay(look.aim.position, AimbotCoroutines.GetAimPosition(AimbotCoroutines.LockedObject.transform, "Skull"));
                            ri = RaycastUtilities.GenerateOriginalRaycast(new Ray(r.origin, r.direction), PAsset.range, RayMasks.DAMAGE_CLIENT);
                            r = default(Ray);

                        }
                        bool Valid = ri.player == null;

                        if (RaycastOptions.Enabled)
                        {
                            Valid = RaycastUtilities.GenerateRaycast(out ri);
                        }
                        if (Valid)
                        {
                            TriggerbotOptions.IsFiring = false;
                            continue;
                        }
                        EFiremode fire = (EFiremode)TriggerbotComponent.CurrentFiremode.GetValue(gun);
                        bool flag4 = fire == EFiremode.AUTO;
                        if (flag4)
                        {
                            TriggerbotOptions.IsFiring = true;
                            continue;
                        }
                        TriggerbotOptions.IsFiring = !TriggerbotOptions.IsFiring;
                    }
                    else if ((useableMelee = (useable2 as UseableMelee)) != null)
                    {
                        ItemMeleeAsset MAsset = (ItemMeleeAsset)OptimizationVariables.MainPlayer.equipment.asset;
                        RaycastInfo ri2 = RaycastUtilities.GenerateOriginalRaycast(new Ray(look.aim.position, look.aim.forward), MAsset.range, RayMasks.DAMAGE_CLIENT);
                        bool flag5 = AimbotCoroutines.LockedObject != null && AimbotCoroutines.IsAiming;
                        if (flag5)
                        {
                            Ray r2 = OV_UseableGun.GetAimRay(look.aim.position, AimbotCoroutines.GetAimPosition(AimbotCoroutines.LockedObject.transform, "Skull"));
                            ri2 = RaycastUtilities.GenerateOriginalRaycast(new Ray(r2.origin, r2.direction), MAsset.range, RayMasks.DAMAGE_CLIENT);
                            r2 = default(Ray);
                        }
                        bool Valid2 = ri2.player != null;
                        bool enabled2 = RaycastOptions.Enabled;
                        if (enabled2)
                        {
                            Valid2 = RaycastUtilities.GenerateRaycast(out ri2);
                        }
                        bool flag6 = !Valid2;
                        if (flag6)
                        {
                            TriggerbotOptions.IsFiring = false;
                            continue;
                        }
                        bool isRepeated = MAsset.isRepeated;
                        if (isRepeated)
                        {
                            TriggerbotOptions.IsFiring = true;
                            continue;
                        }
                        TriggerbotOptions.IsFiring = !TriggerbotOptions.IsFiring;
                    }
                    useable2 = null;
                    useableGun = null;
                    gun = null;
                    look = null;
                    u = null;
                }
            }
        }
        yield break;
    }
     
    public static FieldInfo CurrentFiremode;
}

