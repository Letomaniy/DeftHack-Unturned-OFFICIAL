using SDG.Unturned;
using System.Collections.Generic;
using System.Reflection;

 
public class OV_PlayerDashboardInventoryUI
{
    [Override(typeof(PlayerDashboardInventoryUI), "updateNearbyDrops", BindingFlags.Static | BindingFlags.NonPublic, 0)]
    public static void updateNearbyDrops()
    {
        if (MiscOptions.NearbyItemRaycast)
        {
            OV_PlayerDashboardInventoryUI.areaItems.clear();
            OV_PlayerDashboardInventoryUI.regionsInRadius.Clear();
            Regions.getRegionsInRadius(Player.player.look.aim.position, 20f, OV_PlayerDashboardInventoryUI.regionsInRadius);
            OV_PlayerDashboardInventoryUI.itemsInRadius.Clear();
            ItemManager.getItemsInRadius(Player.player.look.aim.position, 400f, OV_PlayerDashboardInventoryUI.regionsInRadius, OV_PlayerDashboardInventoryUI.itemsInRadius);
            if (OV_PlayerDashboardInventoryUI.itemsInRadius.Count <= 0)
            {
                OV_PlayerDashboardInventoryUI.areaItems.resize(8, 3);
            }
            else
            {
                OV_PlayerDashboardInventoryUI.areaItems.resize(8, 0);
                byte b = 0;
                while (b < OV_PlayerDashboardInventoryUI.itemsInRadius.Count && OV_PlayerDashboardInventoryUI.areaItems.getItemCount() < 200)
                {
                    InteractableItem interactableItem = OV_PlayerDashboardInventoryUI.itemsInRadius[b];
                    if (interactableItem)
                    {
                        Item item = interactableItem.item;
                        if (item != null)
                        {
                            while (!OV_PlayerDashboardInventoryUI.areaItems.tryAddItem(item))
                            {
                                if (OV_PlayerDashboardInventoryUI.areaItems.height >= 200)
                                {
                                    goto IL_15F;
                                }
                                OV_PlayerDashboardInventoryUI.areaItems.resize(OV_PlayerDashboardInventoryUI.areaItems.width, (byte)(areaItems.height + 1));
                            }
                            ItemJar item2 = OV_PlayerDashboardInventoryUI.areaItems.getItem((byte)(areaItems.getItemCount() - 1));
                            item2.interactableItem = interactableItem;
                            interactableItem.jar = item2;
                        }
                    }
                    b += 1;
                }
            IL_15F:
                if (OV_PlayerDashboardInventoryUI.areaItems.height + 3 <= 200)
                {
                    OV_PlayerDashboardInventoryUI.areaItems.resize(OV_PlayerDashboardInventoryUI.areaItems.width, (byte)(areaItems.height + 3));
                }
            }
            Player.player.inventory.replaceItems(PlayerInventory.AREA, OV_PlayerDashboardInventoryUI.areaItems);
            SleekItems[] array = (SleekItems[])OV_PlayerDashboardInventoryUI.itemsfield.GetValue(null);
            array[PlayerInventory.AREA - PlayerInventory.SLOTS].clear();
            array[PlayerInventory.AREA - PlayerInventory.SLOTS].resize(OV_PlayerDashboardInventoryUI.areaItems.width, OV_PlayerDashboardInventoryUI.areaItems.height);
            for (int i = 0; i < OV_PlayerDashboardInventoryUI.areaItems.getItemCount(); i++)
            {
                array[PlayerInventory.AREA - PlayerInventory.SLOTS].addItem(OV_PlayerDashboardInventoryUI.areaItems.getItem((byte)i));
            }
            OV_PlayerDashboardInventoryUI.updateBoxAreasfield.Invoke(null, null);
            return;
        }
        OverrideUtilities.CallOriginal(null, null);
    }

    public static Items areaItems = new Items(PlayerInventory.AREA);
     
    public static List<InteractableItem> itemsInRadius = new List<InteractableItem>();
     
    public static List<RegionCoordinate> regionsInRadius = new List<RegionCoordinate>(4);
     
    public static FieldInfo itemsfield = typeof(PlayerDashboardInventoryUI).GetField("items", BindingFlags.Static | BindingFlags.NonPublic);
     
    public static MethodInfo updateBoxAreasfield = typeof(PlayerDashboardInventoryUI).GetMethod("updateBoxAreas", BindingFlags.Static | BindingFlags.NonPublic);
}

