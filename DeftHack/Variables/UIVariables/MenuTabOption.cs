using System.Collections.Generic;
using System.Linq;

 
public class MenuTabOption
{
    
    public static void Add(MenuTabOption tab)
    {
        bool flag = !MenuTabOption.Contains(tab);
        if (flag)
        {
            MenuTabOption.tabs[MenuTabOption.cPageIndex].Add(tab);
            tab.page = MenuTabOption.cPageIndex;
            MenuTabOption.cTabIndex++;
            bool flag2 = MenuTabOption.cTabIndex % 9 == 0;
            if (flag2)
            {
                MenuTabOption.cTabIndex = 0;
                MenuTabOption.cPageIndex++;
            }
        }
    }
     
    public static bool Contains(MenuTabOption tab)
    {
        bool result = false;
        foreach (MenuTabOption menuTabOption in MenuTabOption.tabs.SelectMany((List<MenuTabOption> t) => t))
        {
            bool flag = tab.name == menuTabOption.name;
            if (flag)
            {
                result = true;
            }
        }
        return result;
    }
     
    public MenuTabOption(string name, MenuTabOption.MenuTab tab)
    {
        this.tab = tab;
        this.name = name;
    }
     
    public MenuTabOption.MenuTab tab;
     
    public string name;
     
    public bool enabled = false;
      
    public static MenuTabOption CurrentTab;
     
    public int page;
     
    public static int cTabIndex = 0;
     
    public static int cPageIndex = 0;
     
    public static List<MenuTabOption>[] tabs = new List<MenuTabOption>[]
    {
            new List<MenuTabOption>(),
            new List<MenuTabOption>()
    };
      
    public delegate void MenuTab();
}

