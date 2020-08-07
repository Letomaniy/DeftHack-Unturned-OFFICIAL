using SDG.Unturned;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


 
public class RaycastCoroutines
{ 
    public static IEnumerator UpdateObjects()
    {
        for (; ; )
        {
            if (!DrawUtilities.ShouldRun())
            {
                RaycastUtilities.Objects = new GameObject[0];
                yield return new WaitForSeconds(1f);
            }
            else
            {
                try
                {
                    ItemGunAsset itemGunAsset = OptimizationVariables.MainPlayer.equipment.asset as ItemGunAsset;
                    float num = (itemGunAsset != null) ? itemGunAsset.range : 15.5f;
                    num += 10f;
                    GameObject[] array = (from c in Physics.OverlapSphere(OptimizationVariables.MainPlayer.transform.position, num)
                                          select c.gameObject).ToArray<GameObject>();
                    switch (RaycastOptions.Target)
                    {
                        case TargetPriority.Players:
                            {
                                RaycastCoroutines.CachedPlayers.Clear();
                                GameObject[] array2 = array;
                                for (int i = 0; i < array2.Length; i++)
                                {
                                    Player player = DamageTool.getPlayer(array2[i].transform);
                                    if (!(player == null) && !RaycastCoroutines.CachedPlayers.Contains(player) && !(player == OptimizationVariables.MainPlayer) && !player.life.isDead)
                                    {
                                        RaycastCoroutines.CachedPlayers.Add(player);
                                    }
                                }
                                RaycastUtilities.Objects = (from c in RaycastCoroutines.CachedPlayers
                                                            select c.gameObject).ToArray<GameObject>();
                                break;
                            }
                        case TargetPriority.Sentries:
                            RaycastUtilities.Objects = (from g in array
                                                        where g.GetComponent<InteractableSentry>() != null
                                                        select g).ToArray<GameObject>();
                            break;
                        case TargetPriority.Beds:
                            RaycastUtilities.Objects = (from g in array
                                                        where g.GetComponent<InteractableBed>() != null
                                                        select g).ToArray<GameObject>();
                            break;
                        case TargetPriority.ClaimFlags:
                            RaycastUtilities.Objects = (from g in array
                                                        where g.GetComponent<InteractableClaim>() != null
                                                        select g).ToArray<GameObject>();
                            break;
                        case TargetPriority.Storage:
                            RaycastUtilities.Objects = (from g in array
                                                        where g.GetComponent<InteractableStorage>() != null
                                                        select g).ToArray<GameObject>();
                            break;
                        case TargetPriority.Vehicles:
                            RaycastUtilities.Objects = (from g in array
                                                        where g.GetComponent<InteractableVehicle>() != null
                                                        select g).ToArray<GameObject>();
                            break;
                        case TargetPriority.Zombies:
                            RaycastUtilities.Objects = (from g in array
                                                        where g.GetComponent<Zombie>() != null
                                                        select g).ToArray<GameObject>();
                            break;
                    }
                }
                catch (Exception)
                {
                }
                yield return new WaitForSeconds(2f);
            }
        }
    }
     
    public static List<Player> CachedPlayers = new List<Player>();
}

