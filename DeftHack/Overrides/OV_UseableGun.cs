using SDG.Unturned;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


 
public class OV_UseableGun
{
 
    [Initializer]
    public static void Load()
    {
        OV_UseableGun.BulletsField = typeof(UseableGun).GetField("bullets", ReflectionVariables.publicInstance);
    }

    
    public static bool IsRaycastInvalid(RaycastInfo info)
    {
        return info.player == null && info.zombie == null && info.animal == null && info.vehicle == null && info.transform == null;
    }

 
    [Override(typeof(UseableGun), "ballistics", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
    public void OV_ballistics()
    {
        Useable useable = OptimizationVariables.MainPlayer.equipment.useable;
        bool isServer = Provider.isServer;
        if (isServer)
        {
            OverrideUtilities.CallOriginal(useable, new object[0]);
        }
        else
        {
            bool flag = Time.realtimeSinceStartup - PlayerLifeUI.hitmarkers[0].lastHit > PlayerUI.HIT_TIME;
            if (flag)
            {
                PlayerLifeUI.hitmarkers[0].hitBuildImage.isVisible = false;
                PlayerLifeUI.hitmarkers[0].hitCriticalImage.isVisible = false;
                PlayerLifeUI.hitmarkers[0].hitEntitiyImage.isVisible = false;
            }
            ItemGunAsset itemGunAsset = (ItemGunAsset)OptimizationVariables.MainPlayer.equipment.asset;
            PlayerLook look = OptimizationVariables.MainPlayer.look;
            bool flag2 = itemGunAsset.projectile != null;
            if (!flag2)
            {
                List<BulletInfo> list = (List<BulletInfo>)OV_UseableGun.BulletsField.GetValue(useable);
                bool flag3 = list.Count == 0;
                if (!flag3)
                {
                    RaycastInfo raycastInfo = null;
                    bool enabled = RaycastOptions.Enabled;
                    if (enabled)
                    {
                        RaycastUtilities.GenerateRaycast(out raycastInfo);
                    }
                    bool ballistics = Provider.modeConfigData.Gameplay.Ballistics;
                    if (ballistics)
                    {
                        bool flag4 = raycastInfo == null;
                        if (flag4)
                        {
                            bool noAimbotDrop = AimbotOptions.NoAimbotDrop;
                            if (noAimbotDrop)
                            {
                                bool flag5 = AimbotCoroutines.IsAiming && AimbotCoroutines.LockedObject != null;
                                if (flag5)
                                {
                                    Vector3 aimPosition = AimbotCoroutines.GetAimPosition(AimbotCoroutines.LockedObject.transform, "Skull");
                                    Ray aimRay = OV_UseableGun.GetAimRay(look.aim.position, aimPosition);
                                    float maxDistance = (float)VectorUtilities.GetDistance(look.aim.position, aimPosition);
                                    bool flag6 = !Physics.Raycast(aimRay, out RaycastHit raycastHit, maxDistance, RayMasks.DAMAGE_SERVER);
                                    if (flag6)
                                    {
                                        raycastInfo = RaycastUtilities.GenerateOriginalRaycast(aimRay, itemGunAsset.range, RayMasks.ENEMY);
                                    }
                                }
                            }
                            bool flag7 = WeaponOptions.NoDrop && raycastInfo == null;
                            if (flag7)
                            {
                                for (int i = 0; i < list.Count; i++)
                                {
                                    BulletInfo bulletInfo = list[i];
                                    Ray ray = new Ray(bulletInfo.pos, bulletInfo.dir);
                                    RaycastInfo info = DamageTool.raycast(ray, itemGunAsset.ballisticTravel, RayMasks.DAMAGE_CLIENT);
                                    bool flag8 = OV_UseableGun.IsRaycastInvalid(info);
                                    if (flag8)
                                    {
                                        bulletInfo.pos += bulletInfo.dir * itemGunAsset.ballisticTravel;
                                    }
                                    else
                                    {
                                        EPlayerHit newHit = OV_UseableGun.CalcHitMarker(itemGunAsset, ref info);
                                        PlayerUI.hitmark(0, Vector3.zero, false, newHit);
                                        OptimizationVariables.MainPlayer.input.sendRaycast(info, ERaycastInfoUsage.Gun);
                                        bulletInfo.steps = 254;
                                    }
                                }
                                for (int j = list.Count - 1; j >= 0; j--)
                                {
                                    BulletInfo bulletInfo2 = list[j];
                                    BulletInfo bulletInfo3 = bulletInfo2;
                                    bulletInfo3.steps += 1;
                                    bool flag9 = bulletInfo2.steps >= itemGunAsset.ballisticSteps;
                                    if (flag9)
                                    {
                                        list.RemoveAt(j);
                                    }
                                }
                                return;
                            }
                            bool flag10 = raycastInfo == null;
                            if (flag10)
                            {
                                OverrideUtilities.CallOriginal(useable, new object[0]);
                                return;
                            }
                        }
                        for (int k = 0; k < list.Count; k++)
                        {
                            BulletInfo bulletInfo4 = list[k];
                            double distance = VectorUtilities.GetDistance(OptimizationVariables.MainPlayer.transform.position, raycastInfo.point);
                            bool flag11 = bulletInfo4.steps * itemGunAsset.ballisticTravel < distance;
                            if (!flag11)
                            {
                                EPlayerHit newHit2 = OV_UseableGun.CalcHitMarker(itemGunAsset, ref raycastInfo);
                                PlayerUI.hitmark(0, Vector3.zero, false, newHit2);
                                OptimizationVariables.MainPlayer.input.sendRaycast(raycastInfo, ERaycastInfoUsage.Gun);
                                bulletInfo4.steps = 254;
                            }
                        }
                        for (int l = list.Count - 1; l >= 0; l--)
                        {
                            BulletInfo bulletInfo5 = list[l];
                            BulletInfo bulletInfo6 = bulletInfo5;
                            bulletInfo6.steps += 1;
                            bool flag12 = bulletInfo5.steps >= itemGunAsset.ballisticSteps;
                            if (flag12)
                            {
                                list.RemoveAt(l);
                            }
                        }
                    }
                    else
                    {
                        bool flag13 = raycastInfo != null;
                        if (flag13)
                        {
                            for (int m = 0; m < list.Count; m++)
                            {
                                EPlayerHit newHit3 = OV_UseableGun.CalcHitMarker(itemGunAsset, ref raycastInfo);
                                PlayerUI.hitmark(0, Vector3.zero, false, newHit3);
                                OptimizationVariables.MainPlayer.input.sendRaycast(raycastInfo, ERaycastInfoUsage.Gun);
                            }
                            list.Clear();
                        }
                        else
                        {
                            OverrideUtilities.CallOriginal(useable, new object[0]);
                        }
                    }
                }
            }
        }
    }
     
    public static EPlayerHit CalcHitMarker(ItemGunAsset PAsset, ref RaycastInfo ri)
    {
        EPlayerHit eplayerHit = EPlayerHit.NONE;
        bool flag = ri == null || PAsset == null;
        EPlayerHit result;
        if (flag)
        {
            result = eplayerHit;
        }
        else
        {
            bool flag2 = ri.animal || ri.player || ri.zombie;
            if (flag2)
            {
                eplayerHit = EPlayerHit.ENTITIY;
                bool flag3 = ri.limb == ELimb.SKULL;
                if (flag3)
                {
                    eplayerHit = EPlayerHit.CRITICAL;
                }
            }
            else
            {
                bool flag4 = ri.transform;
                if (flag4)
                {
                    bool flag5 = ri.transform.CompareTag("Barricade") && PAsset.barricadeDamage > 1f;
                    if (flag5)
                    {
                        InteractableDoorHinge component = ri.transform.GetComponent<InteractableDoorHinge>();
                        bool flag6 = component != null;
                        if (flag6)
                        {
                            ri.transform = component.transform.parent.parent;
                        }
                        bool flag7 = !ushort.TryParse(ri.transform.name, out ushort id);
                        if (flag7)
                        {
                            return eplayerHit;
                        }
                        ItemBarricadeAsset itemBarricadeAsset = (ItemBarricadeAsset)Assets.find(EAssetType.ITEM, id);
                        bool flag8 = itemBarricadeAsset == null || (!itemBarricadeAsset.isVulnerable && !PAsset.isInvulnerable);
                        if (flag8)
                        {
                            return eplayerHit;
                        }
                        bool flag9 = eplayerHit == EPlayerHit.NONE;
                        if (flag9)
                        {
                            eplayerHit = EPlayerHit.BUILD;
                        }
                    }
                    else
                    {
                        bool flag10 = ri.transform.CompareTag("Structure") && PAsset.structureDamage > 1f;
                        if (flag10)
                        {
                            bool flag11 = !ushort.TryParse(ri.transform.name, out ushort id2);
                            if (flag11)
                            {
                                return eplayerHit;
                            }
                            ItemStructureAsset itemStructureAsset = (ItemStructureAsset)Assets.find(EAssetType.ITEM, id2);
                            bool flag12 = itemStructureAsset == null || (!itemStructureAsset.isVulnerable && !PAsset.isInvulnerable);
                            if (flag12)
                            {
                                return eplayerHit;
                            }
                            bool flag13 = eplayerHit == EPlayerHit.NONE;
                            if (flag13)
                            {
                                eplayerHit = EPlayerHit.BUILD;
                            }
                        }
                        else
                        {
                            bool flag14 = ri.transform.CompareTag("Resource") && PAsset.resourceDamage > 1f;
                            if (flag14)
                            {
                                bool flag15 = !ResourceManager.tryGetRegion(ri.transform, out byte x, out byte y, out ushort index);
                                if (flag15)
                                {
                                    return eplayerHit;
                                }
                                ResourceSpawnpoint resourceSpawnpoint = ResourceManager.getResourceSpawnpoint(x, y, index);
                                bool flag16 = resourceSpawnpoint == null || resourceSpawnpoint.isDead || !PAsset.hasBladeID(resourceSpawnpoint.asset.bladeID);
                                if (flag16)
                                {
                                    return eplayerHit;
                                }
                                bool flag17 = eplayerHit == EPlayerHit.NONE;
                                if (flag17)
                                {
                                    eplayerHit = EPlayerHit.BUILD;
                                }
                            }
                            else
                            {
                                bool flag18 = PAsset.objectDamage > 1f;
                                if (flag18)
                                {
                                    InteractableObjectRubble component2 = ri.transform.GetComponent<InteractableObjectRubble>();
                                    bool flag19 = component2 == null;
                                    if (flag19)
                                    {
                                        return eplayerHit;
                                    }
                                    ri.section = component2.getSection(ri.collider.transform);
                                    bool flag20 = component2.isSectionDead(ri.section) || (!component2.asset.rubbleIsVulnerable && !PAsset.isInvulnerable);
                                    if (flag20)
                                    {
                                        return eplayerHit;
                                    }
                                    bool flag21 = eplayerHit == EPlayerHit.NONE;
                                    if (flag21)
                                    {
                                        eplayerHit = EPlayerHit.BUILD;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    bool flag22 = ri.vehicle && !ri.vehicle.isDead && PAsset.vehicleDamage > 1f;
                    if (flag22)
                    {
                        bool flag23 = ri.vehicle.asset != null && (ri.vehicle.asset.isVulnerable || PAsset.isInvulnerable);
                        if (flag23)
                        {
                            bool flag24 = eplayerHit == EPlayerHit.NONE;
                            if (flag24)
                            {
                                eplayerHit = EPlayerHit.BUILD;
                            }
                        }
                    }
                }
            }
            result = eplayerHit;
        }
        return result;
    }
     
    public static Ray GetAimRay(Vector3 origin, Vector3 pos)
    {
        Vector3 direction = VectorUtilities.Normalize(pos - origin);
        return new Ray(pos, direction);
    }
     
    public static FieldInfo BulletsField;
}

