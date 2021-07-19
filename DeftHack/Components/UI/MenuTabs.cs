 
public static class MenuTabs
{
    public static void AddTabs()
    {
        MenuTabOption.Add(new MenuTabOption("визуал", new MenuTabOption.MenuTab(VisualsTab.Tab)));
        MenuTabOption.Add(new MenuTabOption("аим бот", new MenuTabOption.MenuTab(AimbotTab.Tab)));
        MenuTabOption.Add(new MenuTabOption("оружие", new MenuTabOption.MenuTab(WeaponsTab.Tab)));
        MenuTabOption.Add(new MenuTabOption("стата", new MenuTabOption.MenuTab(StatsTab.Tab)));
        MenuTabOption.Add(new MenuTabOption("игроки", new MenuTabOption.MenuTab(PlayersTab.Tab)));
        MenuTabOption.Add(new MenuTabOption("скины", new MenuTabOption.MenuTab(SkinsTab.Tab)));
        MenuTabOption.Add(new MenuTabOption("прочее", new MenuTabOption.MenuTab(MiscTab.Tab)));
        MenuTabOption.Add(new MenuTabOption("опции", new MenuTabOption.MenuTab(MoreMiscTab.Tab)));
        MenuTabOption.Add(new MenuTabOption("инфо", new MenuTabOption.MenuTab(InfoTab.Tab)));
        MenuTabOption.Add(new MenuTabOption("цвета", new MenuTabOption.MenuTab(ColorsTab.Tab)));
        MenuTabOption.Add(new MenuTabOption("Кнопки", new MenuTabOption.MenuTab(HotkeyTab.Tab)));
        MenuTabOption.Add(new MenuTabOption("Бинды", new MenuTabOption.MenuTab(BindTab.Tab)));
    }
}

