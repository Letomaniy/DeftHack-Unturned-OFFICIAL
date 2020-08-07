 
public static class ItemOptions
{
    
    [Save]
    public static bool AutoItemPickup = false;

    [Save]
    public static bool AutoForagePickup = false;

   
    [Save]
    public static int ItemPickupDelay = 1000;

  
    [Save]
    public static ItemOptionList ItemFilterOptions = new ItemOptionList();
 
    [Save]
    public static ItemOptionList ItemESPOptions = new ItemOptionList();
}

