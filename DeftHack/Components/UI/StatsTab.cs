
using SDG.Provider;
using SDG.Unturned;
using UnityEngine;

 
public static class StatsTab
{
    
    public static void Tab()
    {
        Prefab.ScrollView(new Rect(0f, 0f, 250f, 436f), "Статистика", ref StatsTab.ScrollPos, delegate ()
        {
            for (int i = 0; i < StatsTab.StatLabels.Length; i++)
            {
                string text = StatsTab.StatLabels[i];
                bool flag = Prefab.Button(text, 205f, 25f, new GUILayoutOption[0]);
                if (flag)
                {
                    StatsTab.Selected = i;
                }
                GUILayout.Space(3f);
            }
            GUILayout.Label("Получение достижений", Prefab._TextStyle, new GUILayoutOption[0]);
            if (Prefab.Button("Welcome to PEI", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("pei");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("A Bridge Too Far", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("bridge");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Mastermind", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("mastermind");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Offense", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("offense");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Defense", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("defense");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Support", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("support");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Experienced + Schooled", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("experienced");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Hoarder + Scavenger", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("hoarder");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Outdoors + Camper", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("outdoors");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Psychopath + Murderer", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("psychopath");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Survivor", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("survivor");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Berries", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("berries");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Accident Prone", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("accident_prone");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Behind the Wheel", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("wheel");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Welcome to the Yukon", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("yukon");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Welcome to Washington", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("washington");

            }
            GUILayout.Space(3f);
            if (Prefab.Button("Fishing", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("fishing");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Crafting", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("crafting");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Farming", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("farming");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Headshot", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("headshot");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Sharpshooter", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("sharpshooter");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Hiking", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("hiking");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Roadtrip", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("roadtrip");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Champion", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("champion");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Fortified", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("fortified");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Welcome to Russia", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("russia");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Villain", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("villain");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Helping Hand", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("Quest");
            } 
            if (Prefab.Button("Unturned", 205f, 25f, new GUILayoutOption[0])) 
            {
                Provider.provider.achievementsService.setAchievement("unturned");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Forged + Hardened", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("forged");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Graduation", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("Educated");
            } 
            if (Prefab.Button("Soulcrystal", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("soulcrystal");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Paragon", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("paragon");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Mk. II", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("mk2");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Ensign", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("ensign");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Lieutenant", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("lieutenant");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Major", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("major");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Welcome to Hawaii", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("hawaii");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Extinguished", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("Boss_Magma");
            }
            GUILayout.Space(3f);
            if (Prefab.Button("Secrets of Neuschwanstein", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("Zweihander");
            } 
            if (Prefab.Button("Получить все достижения", 205f, 25f, new GUILayoutOption[0]))
            {
                Provider.provider.achievementsService.setAchievement("Educated");
                Provider.provider.achievementsService.setAchievement("Zweihander");
                Provider.provider.achievementsService.setAchievement("Boss_Magma");
                Provider.provider.achievementsService.setAchievement("Quest");
                Provider.provider.achievementsService.setAchievement("pei");
                Provider.provider.achievementsService.setAchievement("bridge");
                Provider.provider.achievementsService.setAchievement("mastermind");
                Provider.provider.achievementsService.setAchievement("offense");
                Provider.provider.achievementsService.setAchievement("defense");
                Provider.provider.achievementsService.setAchievement("support");
                Provider.provider.achievementsService.setAchievement("experienced");
                Provider.provider.achievementsService.setAchievement("hoarder");
                Provider.provider.achievementsService.setAchievement("outdoors");
                Provider.provider.achievementsService.setAchievement("psychopath");
                Provider.provider.achievementsService.setAchievement("survivor");
                Provider.provider.achievementsService.setAchievement("berries");
                Provider.provider.achievementsService.setAchievement("accident_prone");
                Provider.provider.achievementsService.setAchievement("wheel");
                Provider.provider.achievementsService.setAchievement("yukon");
                Provider.provider.achievementsService.setAchievement("fishing");
                Provider.provider.achievementsService.setAchievement("washington");
                Provider.provider.achievementsService.setAchievement("crafting");
                Provider.provider.achievementsService.setAchievement("farming");
                Provider.provider.achievementsService.setAchievement("headshot");
                Provider.provider.achievementsService.setAchievement("sharpshooter");
                Provider.provider.achievementsService.setAchievement("hiking");
                Provider.provider.achievementsService.setAchievement("roadtrip");
                Provider.provider.achievementsService.setAchievement("champion");
                Provider.provider.achievementsService.setAchievement("fortified");
                Provider.provider.achievementsService.setAchievement("russia");
                Provider.provider.achievementsService.setAchievement("villain");
                Provider.provider.achievementsService.setAchievement("unturned");
                Provider.provider.achievementsService.setAchievement("forged");
                Provider.provider.achievementsService.setAchievement("soulcrystal");
                Provider.provider.achievementsService.setAchievement("paragon");
                Provider.provider.achievementsService.setAchievement("mk2");
                Provider.provider.achievementsService.setAchievement("ensign");
                Provider.provider.achievementsService.setAchievement("major");
                Provider.provider.achievementsService.setAchievement("lieutenant");
                Provider.provider.achievementsService.setAchievement("hawaii");
            }
            GUILayout.Space(3f);
        }, 20, new GUILayoutOption[0]);
        Rect area;
        area = new Rect(260f, 0f, 196f, 250f);
        Prefab.MenuArea(area, "Модиффикатор", delegate
        {
            bool flag = StatsTab.Selected == 0;
            if (!flag)
            {
                string text = StatsTab.StatLabels[StatsTab.Selected];
                Provider.provider.statisticsService.userStatisticsService.getStatistic(StatsTab.StatNames[StatsTab.Selected], out int num);
                GUILayout.Label(text, Prefab._TextStyle, new GUILayoutOption[0]);
                GUILayout.Space(4f);
                GUILayout.Label(string.Format("Текущий: {0}", num), Prefab._TextStyle, new GUILayoutOption[0]);
                GUILayout.Space(3f);
                StatsTab.Amount = Prefab.TextField(StatsTab.Amount, "Модиффицировать: ", 50);
                GUILayout.Space(2f);
                bool flag2 = !int.TryParse(StatsTab.Amount, out int num2);
                if (!flag2)
                {
                    bool flag3 = Prefab.Button("Принять", 75f, 25f, new GUILayoutOption[0]);
                    if (flag3)
                    {
                        for (int i = 1; i <= num2; i++)
                        {
                            Provider.provider.statisticsService.userStatisticsService.setStatistic(StatsTab.StatNames[StatsTab.Selected], num + i);
                        }
                    }
                }
            }
        });
        Prefab.MenuArea(new Rect(260f, 260f, 196f, 174f), "Счётчик убийств", delegate
        {
            GUILayout.Label("Возьмите в руки оружие\nсо счётчиком убийств!", Prefab._TextStyle, new GUILayoutOption[0]);
            GUILayout.Space(5f);
            if (Prefab.Button("Найти оружие", 167f, 25f) && Player.player)
            {
                ItemAsset asset = Player.player.equipment.asset;
                if (asset != null)
                {
                    Player.player.equipment.getUseableStatTrackerValue(out EStatTrackerType estatTrackerType, out int num);

                    name = asset.itemName.ToString();
                    id = asset.id.ToString();
                    count = num.ToString();
                    labels = "Оружие:";
                }

            }
            name = Prefab.TextField(name, labels, 100);
            GUILayout.Space(7f);
            count = Prefab.TextField(count, "Убийства:", 100);
            GUILayout.Space(7f);
            if (Prefab.Button("Изменить", 167f, 25f) && ushort.TryParse(id, out ushort itemID) && int.TryParse(count, out int newValue))
            {
                MiscComponent.incrementStatTrackerValue(itemID, newValue);
            }
        });
    }
    public static string id;
    public static string count;
    public static string name = "Оружие: ";
    public static string labels; 
    public static int Selected = 0; 
    public static Vector2 ScrollPos; 
    public static string Amount = ""; 
    public static string[] StatLabels = new string[]
    {
            "None",
            "Normal Zombie Kills",
            "Player Kills",
            "Items Found",
            "Resources Found",
            "Experience Found",
            "Mega Zombie Kills",
            "Player Deaths",
            "Animal Kills",
            "Blueprints Found",
            "Fishies Found",
            "Plants Taken",
            "Accuracy",
            "Headshots",
            "Foot Traveled",
            "Vehicle Traveled",
            "Arena Wins",
            "Buildables Taken",
            "Throwables Found"
    };
     
    public static string[] StatNames = new string[]
    {
            "None",
            "Kills_Zombies_Normal",
            "Kills_Players",
            "Found_Items",
            "Found_Resources",
            "Found_Experience",
            "Kills_Zombies_Mega",
            "Deaths_Players",
            "Kills_Animals",
            "Found_Crafts",
            "Found_Fishes",
            "Found_Plants",
            "Accuracy_Shot",
            "Headshots",
            "Travel_Foot",
            "Travel_Vehicle",
            "Arena_Wins",
            "Found_Buildables",
            "Found_Throwables"
    };
}
