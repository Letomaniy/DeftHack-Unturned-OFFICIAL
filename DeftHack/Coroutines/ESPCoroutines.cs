using SDG.Unturned;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


 
public static class ESPCoroutines
{ 
    public static IEnumerator DoChams()
    {
        for (; ; )
        {
            bool flag = !DrawUtilities.ShouldRun() || ESPCoroutines.UnlitChams == null;
            if (flag)
            {
                yield return new WaitForSeconds(1f);
            }
            else
            {
                try
                {
                    bool chamsEnabled = ESPOptions.ChamsEnabled;
                    if (chamsEnabled)
                    {
                        ESPCoroutines.EnableChams();
                    }
                    else
                    {
                        ESPCoroutines.DisableChams();
                    }
                }
                catch (Exception)
                {
                }
                yield return new WaitForSeconds(5f);
            }
        }
    }
     
    public static void DoChamsGameObject(GameObject pgo, Color32 front, Color32 behind)
    {
        bool flag = ESPCoroutines.UnlitChams == null;
        if (!flag)
        {
            Renderer[] componentsInChildren = pgo.GetComponentsInChildren<Renderer>();
            for (int i = 0; i < componentsInChildren.Length; i++)
            {
                bool flag2 = !(componentsInChildren[i].material.shader != ESPCoroutines.LitChams | ESPCoroutines.UnlitChams);
                if (!flag2)
                {
                    Material[] materials = componentsInChildren[i].materials;
                    for (int j = 0; j < materials.Length; j++)
                    {
                        materials[j].shader = (ESPOptions.ChamsFlat ? ESPCoroutines.UnlitChams : ESPCoroutines.LitChams);
                        materials[j].SetColor("_ColorVisible", new Color32(front.r, front.g, front.b, front.a));
                        materials[j].SetColor("_ColorBehind", new Color32(behind.r, behind.g, behind.b, behind.a));
                    }
                }
            }
        }
    }
     
    [OffSpy]
    public static void EnableChams()
    {
        bool flag = !ESPOptions.ChamsEnabled;
        if (!flag)
        {
            Color32 color = ColorUtilities.getColor("_ChamsFriendVisible");
            Color32 color2 = ColorUtilities.getColor("_ChamsFriendInvisible");
            Color32 color3 = ColorUtilities.getColor("_ChamsEnemyVisible");
            Color32 color4 = ColorUtilities.getColor("_ChamsEnemyInvisible");
            foreach (SteamPlayer steamPlayer in Provider.clients.ToArray())
            {
                Color32 front = FriendUtilities.IsFriendly(steamPlayer.player) ? color : color3;
                Color32 behind = FriendUtilities.IsFriendly(steamPlayer.player) ? color2 : color4;
                Player player = steamPlayer.player;
                bool flag2 = player == null || player == OptimizationVariables.MainPlayer || player.gameObject == null || player.life == null || player.life.isDead;
                if (!flag2)
                {
                    GameObject gameObject = player.gameObject;
                    ESPCoroutines.DoChamsGameObject(gameObject, front, behind);
                }
            }
        }
    }
     
    [OnSpy]
    public static void DisableChams()
    {
        bool flag = ESPCoroutines.Normal == null;
        if (!flag)
        {
            for (int i = 0; i < Provider.clients.ToArray().Length; i++)
            {
                Player player = Provider.clients.ToArray()[i].player;
                bool flag2 = player == null || player == OptimizationVariables.MainPlayer || player.life == null || player.life.isDead;
                if (!flag2)
                {
                    GameObject gameObject = player.gameObject;
                    Renderer[] componentsInChildren = gameObject.GetComponentsInChildren<Renderer>();
                    for (int j = 0; j < componentsInChildren.Length; j++)
                    {
                        Material[] materials = componentsInChildren[j].materials;
                        for (int k = 0; k < materials.Length; k++)
                        {
                            bool flag3 = materials[k].shader != ESPCoroutines.Normal;
                            if (flag3)
                            {
                                materials[k].shader = ESPCoroutines.Normal;
                            }
                        }
                    }
                }
            }
        }
    }
     
    public static IEnumerator UpdateObjectList()
    {
        for (; ; )
        {
            bool flag = !DrawUtilities.ShouldRun();
            if (flag)
            {
                yield return new WaitForSeconds(2f);
            }
            else
            {
                List<ESPObject> objects = ESPVariables.Objects;
                objects.Clear();
                List<ESPTarget> targets = (from k in ESPOptions.PriorityTable.Keys
                                           orderby ESPOptions.PriorityTable[k] descending
                                           select k).ToList<ESPTarget>();
                int num;
                for (int i = 0; i < targets.Count; i = num + 1)
                {
                   
                    ESPTarget target = targets[i];
                    ESPVisual vis = ESPOptions.VisualOptions[(int)target];
                    bool flag2 = !vis.Enabled;
                    if (!flag2)
                    {
                        Vector2 pPos = OptimizationVariables.MainPlayer.transform.position;
                        switch (target)
                        {
                            case ESPTarget.Игроки:
                                {
                                    SteamPlayer[] objarray = (from p in Provider.clients
                                                              orderby VectorUtilities.GetDistance(pPos, p.player.transform.position) descending
                                                              select p).ToArray<SteamPlayer>();
                                    bool useObjectCap = vis.UseObjectCap;
                                    if (useObjectCap)
                                    {
                                        objarray = objarray.TakeLast(vis.ObjectCap).ToArray<SteamPlayer>();
                                    }
                                    for (int j = 0; j < objarray.Length; j = num + 1)
                                    {
                                        SteamPlayer sPlayer = objarray[j];
                                        Player plr = sPlayer.player;
                                        bool flag3 = plr.life.isDead || plr == OptimizationVariables.MainPlayer;
                                        if (!flag3)
                                        {
                                            objects.Add(new ESPObject(target, plr, plr.gameObject));
                                            sPlayer = null;
                                            plr = null;
                                        }
                                        num = j;
                                    }
                                    break;
                                }
                            case ESPTarget.Зомби:
                                {
                                    Zombie[] objarr = (from obj in ZombieManager.regions.SelectMany((ZombieRegion r) => r.zombies)
                                                       orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
                                                       select obj).ToArray<Zombie>();
                                    bool useObjectCap2 = vis.UseObjectCap;
                                    if (useObjectCap2)
                                    {
                                        objarr = objarr.TakeLast(vis.ObjectCap).ToArray<Zombie>();
                                    }
                                    for (int k2 = 0; k2 < objarr.Length; k2 = num + 1)
                                    {
                                        Zombie obj9 = objarr[k2];
                                        objects.Add(new ESPObject(target, obj9, obj9.gameObject));
                                        obj9 = null;
                                        num = k2;
                                    }
                                    break;
                                }
                            case ESPTarget.Предметы:
                                {
                                    InteractableItem[] objarr2 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableItem>()
                                                                  orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
                                                                  select obj).ToArray<InteractableItem>();
                                    bool useObjectCap3 = vis.UseObjectCap;
                                    if (useObjectCap3)
                                    {
                                        objarr2 = objarr2.TakeLast(vis.ObjectCap).ToArray<InteractableItem>();
                                    }
                                    for (int l = 0; l < objarr2.Length; l = num + 1)
                                    {
                                        InteractableItem obj2 = objarr2[l];
                                        bool flag4 = ItemUtilities.Whitelisted(obj2.asset, ItemOptions.ItemESPOptions) || !ESPOptions.FilterItems;
                                        if (flag4)
                                        {
                                            objects.Add(new ESPObject(target, obj2, obj2.gameObject));
                                        }
                                        obj2 = null;
                                        num = l;
                                    }
                                    break;
                                }
                            case ESPTarget.Турели:
                                {
                                    InteractableSentry[] objarr3 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableSentry>()
                                                                    orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
                                                                    select obj).ToArray<InteractableSentry>();
                                    bool useObjectCap4 = vis.UseObjectCap;
                                    if (useObjectCap4)
                                    {
                                        objarr3 = objarr3.TakeLast(vis.ObjectCap).ToArray<InteractableSentry>();
                                    }
                                    for (int m = 0; m < objarr3.Length; m = num + 1)
                                    {
                                        InteractableSentry obj3 = objarr3[m];
                                        objects.Add(new ESPObject(target, obj3, obj3.gameObject));
                                        obj3 = null;
                                        num = m;
                                    }
                                    break;
                                }
                            case ESPTarget.Кровати:
                                {
                                    InteractableBed[] objarr4 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableBed>()
                                                                 orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
                                                                 select obj).ToArray<InteractableBed>();
                                    bool useObjectCap5 = vis.UseObjectCap;
                                    if (useObjectCap5)
                                    {
                                        objarr4 = objarr4.TakeLast(vis.ObjectCap).ToArray<InteractableBed>();
                                    }
                                    for (int n = 0; n < objarr4.Length; n = num + 1)
                                    {
                                        InteractableBed obj4 = objarr4[n];
                                        objects.Add(new ESPObject(target, obj4, obj4.gameObject));
                                        obj4 = null;
                                        num = n;
                                    }
                                    break;
                                }
                            case ESPTarget.КлеймФлаги:
                                {
                                    InteractableClaim[] objarr5 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableClaim>()
                                                                   orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
                                                                   select obj).ToArray<InteractableClaim>();
                                    bool useObjectCap6 = vis.UseObjectCap;
                                    if (useObjectCap6)
                                    {
                                        objarr5 = objarr5.TakeLast(vis.ObjectCap).ToArray<InteractableClaim>();
                                    }
                                    for (int j2 = 0; j2 < objarr5.Length; j2 = num + 1)
                                    {
                                        InteractableClaim obj5 = objarr5[j2];
                                        objects.Add(new ESPObject(target, obj5, obj5.gameObject));
                                        obj5 = null;
                                        num = j2;
                                    }
                                    break;
                                }
                            case ESPTarget.Транспорт:
                                {
                                    InteractableVehicle[] objarr6 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableVehicle>()
                                                                     orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
                                                                     select obj).ToArray<InteractableVehicle>();
                                    bool useObjectCap7 = vis.UseObjectCap;
                                    if (useObjectCap7)
                                    {
                                        objarr6 = objarr6.TakeLast(vis.ObjectCap).ToArray<InteractableVehicle>();
                                    }
                                    for (int j3 = 0; j3 < objarr6.Length; j3 = num + 1)
                                    {
                                        InteractableVehicle obj6 = objarr6[j3];
                                        bool isDead = obj6.isDead;
                                        if (!isDead)
                                        {
                                            objects.Add(new ESPObject(target, obj6, obj6.gameObject));
                                            obj6 = null;
                                        }
                                        num = j3;
                                    }
                                    break;
                                }
                            case ESPTarget.Ящики:
                                {
                                    InteractableStorage[] objarr7 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableStorage>()
                                                                     orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
                                                                     select obj).ToArray<InteractableStorage>();
                                    bool useObjectCap8 = vis.UseObjectCap;
                                    if (useObjectCap8)
                                    {
                                        objarr7 = objarr7.TakeLast(vis.ObjectCap).ToArray<InteractableStorage>();
                                    }
                                    for (int j4 = 0; j4 < objarr7.Length; j4 = num + 1)
                                    {
                                        InteractableStorage obj7 = objarr7[j4];
                                        objects.Add(new ESPObject(target, obj7, obj7.gameObject));
                                        obj7 = null;
                                        num = j4;
                                    }
                                    break;
                                }
                            case ESPTarget.Генераторы:
                                {
                                    InteractableGenerator[] objarr8 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableGenerator>()
                                                                       orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
                                                                       select obj).ToArray<InteractableGenerator>();
                                    bool useObjectCap9 = vis.UseObjectCap;
                                    if (useObjectCap9)
                                    {
                                        objarr8 = objarr8.TakeLast(vis.ObjectCap).ToArray<InteractableGenerator>();
                                    }
                                    for (int j5 = 0; j5 < objarr8.Length; j5 = num + 1)
                                    {
                                        InteractableGenerator obj8 = objarr8[j5];
                                        objects.Add(new ESPObject(target, obj8, obj8.gameObject));
                                        obj8 = null;
                                        num = j5;
                                    }
                                    break;
                                }
                            case ESPTarget.Животные:
                                {
                                    Animal[] objarr8 = (from obj in UnityEngine.Object.FindObjectsOfType<Animal>()
                                                        orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
                                                        select obj).ToArray<Animal>();
                                    bool useObjectCap9 = vis.UseObjectCap;
                                    if (useObjectCap9)
                                    {
                                        objarr8 = objarr8.TakeLast(vis.ObjectCap).ToArray<Animal>();
                                    }
                                    for (int j5 = 0; j5 < objarr8.Length; j5 = num + 1)
                                    {
                                        Animal obj8 = objarr8[j5];
                                        objects.Add(new ESPObject(target, obj8, obj8.gameObject));
                                        obj8 = null;
                                        num = j5;
                                    }
                                    break;
                                }
                            case ESPTarget.Ловшуки:
                                {
                                    InteractableTrap[] objarr8 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableTrap>()
                                                                  orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
                                                                  select obj).ToArray<InteractableTrap>();
                                    bool useObjectCap9 = vis.UseObjectCap;
                                    if (useObjectCap9)
                                    {
                                        objarr8 = objarr8.TakeLast(vis.ObjectCap).ToArray<InteractableTrap>();
                                    }
                                    for (int j5 = 0; j5 < objarr8.Length; j5 = num + 1)
                                    {
                                        InteractableTrap obj8 = objarr8[j5];
                                        objects.Add(new ESPObject(target, obj8, obj8.gameObject));
                                        obj8 = null;
                                        num = j5;
                                    }
                                    break;
                                }
                            case ESPTarget.Аирдропы:
                                {
                                    Carepackage[] objarr8 = (from obj in UnityEngine.Object.FindObjectsOfType<Carepackage>()
                                                             orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
                                                             select obj).ToArray<Carepackage>();
                                    bool useObjectCap9 = vis.UseObjectCap;
                                    if (useObjectCap9)
                                    {
                                        objarr8 = objarr8.TakeLast(vis.ObjectCap).ToArray<Carepackage>();
                                    }
                                    for (int j5 = 0; j5 < objarr8.Length; j5 = num + 1)
                                    {
                                        Carepackage obj8 = objarr8[j5];
                                        objects.Add(new ESPObject(target, obj8, obj8.gameObject));
                                        obj8 = null;
                                        num = j5;
                                    }
                                    break;
                                }
                            case ESPTarget.Двери:
                                {
                                    InteractableDoorHinge[] objarr8 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableDoorHinge>()
                                                                       orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
                                                                       select obj).ToArray<InteractableDoorHinge>();
                                    bool useObjectCap9 = vis.UseObjectCap;
                                    if (useObjectCap9)
                                    {
                                        objarr8 = objarr8.TakeLast(vis.ObjectCap).ToArray<InteractableDoorHinge>();
                                    }
                                    for (int j5 = 0; j5 < objarr8.Length; j5 = num + 1)
                                    {
                                        InteractableDoorHinge obj8 = objarr8[j5];
                                        objects.Add(new ESPObject(target, obj8, obj8.gameObject));
                                        obj8 = null;
                                        num = j5;
                                    }
                                    break;
                                }
                            case ESPTarget.Ягоды:
                                {
                                    InteractableForage[] objarr8 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableForage>()
                                                                    orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
                                                                    select obj).ToArray<InteractableForage>();
                                    bool useObjectCap9 = vis.UseObjectCap;
                                    if (useObjectCap9)
                                    {
                                        objarr8 = objarr8.TakeLast(vis.ObjectCap).ToArray<InteractableForage>();
                                    }
                                    for (int j5 = 0; j5 < objarr8.Length; j5 = num + 1)
                                    {
                                        InteractableForage obj8 = objarr8[j5];
                                        objects.Add(new ESPObject(target, obj8, obj8.gameObject));
                                        obj8 = null;
                                        num = j5;
                                    }
                                    break;
                                }
                            case ESPTarget.Растения:
                                {
                                    InteractableFarm[] objarr8 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableFarm>()
                                                                  orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
                                                                  select obj).ToArray<InteractableFarm>();
                                    bool useObjectCap9 = vis.UseObjectCap;
                                    if (useObjectCap9)
                                    {
                                        objarr8 = objarr8.TakeLast(vis.ObjectCap).ToArray<InteractableFarm>();
                                    }
                                    for (int j5 = 0; j5 < objarr8.Length; j5 = num + 1)
                                    {
                                        InteractableFarm obj8 = objarr8[j5];
                                        objects.Add(new ESPObject(target, obj8, obj8.gameObject));
                                        obj8 = null;
                                        num = j5;
                                    }
                                    break;
                                }
                            case ESPTarget.C4:
                                {
                                    InteractableCharge[] objarr8 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableCharge>()
                                                                    orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
                                                                    select obj).ToArray<InteractableCharge>();
                                    bool useObjectCap9 = vis.UseObjectCap;
                                    if (useObjectCap9)
                                    {
                                        objarr8 = objarr8.TakeLast(vis.ObjectCap).ToArray<InteractableCharge>();
                                    }
                                    for (int j5 = 0; j5 < objarr8.Length; j5 = num + 1)
                                    {
                                        InteractableCharge obj8 = objarr8[j5];
                                        objects.Add(new ESPObject(target, obj8, obj8.gameObject));
                                        obj8 = null;
                                        num = j5;
                                    }
                                    break;
                                }
                            case ESPTarget.Fire:
                                {
                                    InteractableFire[] objarr8 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableFire>()
                                                                  orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
                                                                  select obj).ToArray<InteractableFire>();
                                    bool useObjectCap9 = vis.UseObjectCap;
                                    if (useObjectCap9)
                                    {
                                        objarr8 = objarr8.TakeLast(vis.ObjectCap).ToArray<InteractableFire>();
                                    }
                                    for (int j5 = 0; j5 < objarr8.Length; j5 = num + 1)
                                    {
                                        InteractableFire obj8 = objarr8[j5];
                                        objects.Add(new ESPObject(target, obj8, obj8.gameObject));
                                        obj8 = null;
                                        num = j5;
                                    }
                                    break;
                                }
                            case ESPTarget.Лампы:
                                {
                                    InteractableSpot[] objarr8 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableSpot>()
                                                                  orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
                                                                  select obj).ToArray<InteractableSpot>();
                                    bool useObjectCap9 = vis.UseObjectCap;
                                    if (useObjectCap9)
                                    {
                                        objarr8 = objarr8.TakeLast(vis.ObjectCap).ToArray<InteractableSpot>();
                                    }
                                    for (int j5 = 0; j5 < objarr8.Length; j5 = num + 1)
                                    {
                                        InteractableSpot obj8 = objarr8[j5];
                                        objects.Add(new ESPObject(target, obj8, obj8.gameObject));
                                        obj8 = null;
                                        num = j5;
                                    }
                                    break;
                                }
                            case ESPTarget.Топливо:
                                {
                                    InteractableObjectResource[] objarr8 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableObjectResource>()
                                                                            orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
                                                                            select obj).ToArray<InteractableObjectResource>();
                                    bool useObjectCap9 = vis.UseObjectCap;
                                    if (useObjectCap9)
                                    {
                                        objarr8 = objarr8.TakeLast(vis.ObjectCap).ToArray<InteractableObjectResource>();
                                    }
                                    for (int j5 = 0; j5 < objarr8.Length; j5 = num + 1)
                                    {
                                        InteractableObjectResource obj8 = objarr8[j5];
                                        objects.Add(new ESPObject(target, obj8, obj8.gameObject));
                                        obj8 = null;
                                        num = j5;
                                    }
                                    break;
                                }
                            case ESPTarget.Генератор_безопасной_зоны:
                                {
                                    InteractableSafezone[] objarr8 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableSafezone>()
                                                                      orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
                                                                      select obj).ToArray<InteractableSafezone>();
                                    bool useObjectCap9 = vis.UseObjectCap;
                                    if (useObjectCap9)
                                    {
                                        objarr8 = objarr8.TakeLast(vis.ObjectCap).ToArray<InteractableSafezone>();
                                    }
                                    for (int j5 = 0; j5 < objarr8.Length; j5 = num + 1)
                                    {
                                        InteractableSafezone obj8 = objarr8[j5];
                                        objects.Add(new ESPObject(target, obj8, obj8.gameObject));
                                        obj8 = null;
                                        num = j5;
                                    }
                                    break;
                                }
                            case ESPTarget.Генератор_Воздуха:
                                {
                                    InteractableOxygenator[] objarr8 = (from obj in UnityEngine.Object.FindObjectsOfType<InteractableOxygenator>()
                                                                        orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
                                                                        select obj).ToArray<InteractableOxygenator>();
                                    bool useObjectCap9 = vis.UseObjectCap;
                                    if (useObjectCap9)
                                    {
                                        objarr8 = objarr8.TakeLast(vis.ObjectCap).ToArray<InteractableOxygenator>();
                                    }
                                    for (int j5 = 0; j5 < objarr8.Length; j5 = num + 1)
                                    {
                                        InteractableOxygenator obj8 = objarr8[j5];
                                        objects.Add(new ESPObject(target, obj8, obj8.gameObject));
                                        obj8 = null;
                                        num = j5;
                                    }
                                    break;
                                }
                            case ESPTarget.NPC:
                                {
                                    ResourceManager[] objarr8 = (from obj in UnityEngine.Object.FindObjectsOfType<ResourceManager>()
                                                                 orderby VectorUtilities.GetDistance(pPos, obj.transform.position) descending
                                                                 select obj).ToArray<ResourceManager>();
                                    bool useObjectCap9 = vis.UseObjectCap;
                                    if (useObjectCap9)
                                    {
                                        objarr8 = objarr8.TakeLast(vis.ObjectCap).ToArray<ResourceManager>();
                                    }
                                    for (int j5 = 0; j5 < objarr8.Length; j5 = num + 1)
                                    {
                                        ResourceManager obj8 = objarr8[j5];
                                        objects.Add(new ESPObject(target, obj8, obj8.gameObject));
                                        obj8 = null;
                                        num = j5;
                                    }
                                    break;
                                }



                        }
                    }
                    num = i;
                }
                yield return new WaitForSeconds(5f);
                objects = null;
                targets = null;
            }
        }
    }
    public static InteractableObjectNPC[] Hcx;
     
    public static Shader LitChams;
     
    public static Shader UnlitChams;
      
    public static Shader Normal;
}

