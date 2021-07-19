using SDG.Unturned;
using System.Collections.Generic;
using UnityEngine;

[Component]
public class ItemsComponent : MonoBehaviour
{
    public static void RefreshItems()
    {
        ItemsComponent.items.Clear();
        for (ushort num = 0; num < 65535; num += 1)
        {
            ItemAsset itemAsset = (ItemAsset)Assets.find(EAssetType.ITEM, num);
            bool flag = !string.IsNullOrEmpty((itemAsset != null) ? itemAsset.itemName : null) && !ItemsComponent.items.Contains(itemAsset);
            if (flag)
            {
                ItemsComponent.items.Add(itemAsset);
            }
        }
    }

    public void Start()
    {
        CoroutineComponent.ItemPickupCoroutine = base.StartCoroutine(ItemCoroutines.PickupItems());
    }

    public static List<ItemAsset> items = new List<ItemAsset>();
}

