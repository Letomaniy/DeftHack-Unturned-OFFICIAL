using SDG.Unturned;
using System.Collections;
using UnityEngine;


 
public static class ItemCoroutines
{ 
    public static IEnumerator PickupItems()
    {
        for (; ; )
        {
            bool flag = !DrawUtilities.ShouldRun() || !ItemOptions.AutoItemPickup;
            if (flag)
            {
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                Collider[] array = Physics.OverlapSphere(OptimizationVariables.MainPlayer.transform.position, 19f, RayMasks.ITEM);
                int num;
                for (int i = 0; i < array.Length; i = num + 1)
                {
                    Collider col = array[i];
                    bool flag2 = col == null || col.GetComponent<InteractableItem>() == null || col.GetComponent<InteractableItem>().asset == null;
                    if (!flag2)
                    {
                        InteractableItem item = col.GetComponent<InteractableItem>();
                        bool flag3 = !ItemUtilities.Whitelisted(item.asset, ItemOptions.ItemFilterOptions);
                        if (!flag3)
                        {
                            item.use();
                            col = null;
                            item = null;
                        }
                    }
                    num = i;
                }
                yield return new WaitForSeconds(ItemOptions.ItemPickupDelay / 1000);
                array = null;
            }
        }
    }
}
