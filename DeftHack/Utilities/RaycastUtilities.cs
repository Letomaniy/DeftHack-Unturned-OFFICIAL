using SDG.Framework.Utilities;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using UnityEngine;


 
public static class RaycastUtilities
{
   
    public static bool NoShootthroughthewalls(Transform transform)
    {
        Vector3 direction = AimbotCoroutines.GetAimPosition(transform, "Skull") - Player.player.look.aim.position;
        return PhysicsUtility.raycast(new Ray(Player.player.look.aim.position, direction), out RaycastHit raycastHit, direction.magnitude, RayMasks.DAMAGE_CLIENT, QueryTriggerInteraction.UseGlobal) && raycastHit.transform.IsChildOf(transform);
    }

 
    public static RaycastInfo GenerateOriginalRaycast(Ray ray, float range, int mask)
    {
        PhysicsUtility.raycast(ray, out RaycastHit hit, range, mask, QueryTriggerInteraction.UseGlobal);
        RaycastInfo raycastInfo = new RaycastInfo(hit)
        {
            direction = ray.direction
        };
        if (!(raycastInfo.transform == null))
        {
            if (raycastInfo.transform.CompareTag("Barricade"))
            {
                raycastInfo.transform = DamageTool.getBarricadeRootTransform(raycastInfo.transform);
            }
            else if (raycastInfo.transform.CompareTag("Structure"))
            {
                raycastInfo.transform = DamageTool.getStructureRootTransform(raycastInfo.transform);
            }
            if (raycastInfo.transform.CompareTag("Enemy"))
            {
                raycastInfo.player = DamageTool.getPlayer(raycastInfo.transform);
            }
            if (raycastInfo.transform.CompareTag("Zombie"))
            {
                raycastInfo.zombie = DamageTool.getZombie(raycastInfo.transform);
            }
            if (raycastInfo.transform.CompareTag("Animal"))
            {
                raycastInfo.animal = DamageTool.getAnimal(raycastInfo.transform);
            }
            raycastInfo.limb = DamageTool.getLimb(raycastInfo.transform);
            if (!RaycastOptions.UseRandomLimb)
            {
                if (RaycastOptions.UseCustomLimb)
                {
                    raycastInfo.limb = RaycastOptions.TargetLimb;
                }
            }
            else
            {
                ELimb[] array = (ELimb[])Enum.GetValues(typeof(ELimb));
                raycastInfo.limb = array[MathUtilities.Random.Next(0, array.Length)];
            }
            if (raycastInfo.transform.CompareTag("Vehicle"))
            {
                raycastInfo.vehicle = DamageTool.getVehicle(raycastInfo.transform);
            }
            else if (raycastInfo.zombie != null && raycastInfo.zombie.isRadioactive)
            {
                raycastInfo.material = EPhysicsMaterial.ALIEN_DYNAMIC;
            }
            else
            {
                raycastInfo.material = DamageTool.getMaterial(hit.point, raycastInfo.transform, raycastInfo.collider);
            }
            if (RaycastOptions.AlwaysHitHead)
            {
                raycastInfo.limb = ELimb.SKULL;
            }
            return raycastInfo;
        }
        return raycastInfo;
    }

     
    public static bool GenerateRaycast(out RaycastInfo info)
    {
        ItemGunAsset itemGunAsset = OptimizationVariables.MainPlayer.equipment.asset as ItemGunAsset;
        float num = (itemGunAsset != null) ? itemGunAsset.range : 15.5f;
        info = RaycastUtilities.GenerateOriginalRaycast(new Ray(OptimizationVariables.MainPlayer.look.aim.position, OptimizationVariables.MainPlayer.look.aim.forward), num, RayMasks.DAMAGE_CLIENT);
        if (RaycastOptions.EnablePlayerSelection && RaycastUtilities.TargetedPlayer != null)
        {
            GameObject gameObject = RaycastUtilities.TargetedPlayer.gameObject;
            bool flag = true;
            Vector3 position = OptimizationVariables.MainPlayer.look.aim.position;
            if (Vector3.Distance(position, gameObject.transform.position) > num)
            {
                flag = false;
            }
            if (!SphereUtilities.GetRaycast(gameObject, position, out Vector3 point))
            {
                flag = false;
            }
            if (flag)
            {
                info = RaycastUtilities.GenerateRaycast(gameObject, point, info.collider);
                return true;
            }
            if (RaycastOptions.OnlyShootAtSelectedPlayer)
            {
                return false;
            }
        }

        if (RaycastUtilities.GetTargetObject(RaycastUtilities.Objects, out GameObject @object, out Vector3 point2, num))
        {
            info = RaycastUtilities.GenerateRaycast(@object, point2, info.collider);
            return true;
        }
        return false;
    }
     
    public static RaycastInfo GenerateRaycast(GameObject Object, Vector3 Point, Collider col)
    {
        ELimb limb = RaycastOptions.TargetLimb;
        if (RaycastOptions.UseRandomLimb)
        {
            ELimb[] array = (ELimb[])Enum.GetValues(typeof(ELimb));
            limb = array[MathUtilities.Random.Next(0, array.Length)];
        }
        EPhysicsMaterial material = (col == null) ? EPhysicsMaterial.NONE : DamageTool.getMaterial(Point, Object.transform, col);
        return new RaycastInfo(Object.transform)
        {
            point = Point,
            direction = OptimizationVariables.MainPlayer.look.aim.forward,
            limb = limb,
            material = material,
            player = Object.GetComponent<Player>(),
            zombie = Object.GetComponent<Zombie>(),
            vehicle = Object.GetComponent<InteractableVehicle>()
        };
    }
     
    public static bool GetTargetObject(GameObject[] Objects, out GameObject Object, out Vector3 Point, double Range)
    {
        double num = Range + 1f;
        double num2 = 180f;
        Object = null;
        Point = Vector3.zero;
        Vector3 position = OptimizationVariables.MainPlayer.look.aim.position;
        Vector3 forward = OptimizationVariables.MainPlayer.look.aim.forward;
        foreach (GameObject gameObject in Objects)
        {
            if (!(gameObject == null))
            {
                Vector3 position2 = gameObject.transform.position;
                Player component = gameObject.GetComponent<Player>();
                if (!component || (!component.life.isDead && !FriendUtilities.IsFriendly(component) && (!RaycastOptions.NoShootthroughthewalls || RaycastUtilities.NoShootthroughthewalls(gameObject.transform))))
                {
                    Zombie component2 = gameObject.GetComponent<Zombie>();
                    if (!component2 || !component2.isDead)
                    {
                        if (gameObject.GetComponent<RaycastComponent>() == null)
                        {
                            gameObject.AddComponent<RaycastComponent>();
                        }
                        else
                        {
                            double distance = VectorUtilities.GetDistance(position, position2);
                            if (distance <= Range)
                            {
                                if (RaycastOptions.SilentAimUseFOV)
                                {
                                    double angleDelta = VectorUtilities.GetAngleDelta(position, forward, position2);
                                    if (angleDelta > RaycastOptions.SilentAimFOV || angleDelta > num2)
                                    {
                                        goto IL_12A;
                                    }
                                    num2 = angleDelta;
                                }
                                else if (distance > num)
                                {
                                    goto IL_12A;
                                }
                                if (SphereUtilities.GetRaycast(gameObject, position, out Vector3 vector))
                                {
                                    Object = gameObject;
                                    num = distance;
                                    Point = vector;
                                }
                            }
                        }
                    }
                }
            }
        IL_12A:;
        }
        return Object != null;
    }
     
    public static GameObject[] Objects = new GameObject[0];
     
    public static List<GameObject> AttachedObjects = new List<GameObject>();
      
    public static Player TargetedPlayer;
}

