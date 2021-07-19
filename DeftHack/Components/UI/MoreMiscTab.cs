
using SDG.Unturned;
using System.IO;
using System.Threading;
using UnityEngine;

 
public static class MoreMiscTab
{

    public static bool lb;

    public static string a(bool bool_0)
    {
        return ": " + (bool_0 ? "Вкл" : "Выкл");
    }
    public static string language(bool bool_0)
    {
        return ": " + (bool_0 ? "Russian" : "English");
    }
    public static string GetRandomIpAddress()
    {
        System.Random random = new System.Random();
        return $"{random.Next(1, 255)}.{random.Next(0, 255)}.{random.Next(0, 255)}.{random.Next(0, 255)}";
    } 
    public static void Tab()
    {
         
        Prefab.MenuArea(new Rect(0f, 0f, 466f, 436f), "Опции", delegate
        {
            GUILayout.BeginHorizontal(new GUILayoutOption[0]);
            GUILayout.BeginVertical(new GUILayoutOption[]
            {
                    GUILayout.Width(230f)
            });
             
            GUILayout.Space(2f);
            Prefab.Toggle("Авто подбор вещей", ref ItemOptions.AutoItemPickup, 17);
            Prefab.Toggle("Дальнее поднятие в инвентаре", ref MiscOptions.NearbyItemRaycast, 17); 
            GUILayout.Space(2f);
            GUILayout.Label("Задержка: " + ItemOptions.ItemPickupDelay + "мс", Prefab._TextStyle, new GUILayoutOption[0]);
            GUILayout.Space(2f);
            ItemOptions.ItemPickupDelay = (int)Prefab.Slider(0f, 3000f, ItemOptions.ItemPickupDelay, 175);
            GUILayout.Space(2f);
            ItemUtilities.DrawFilterTab(ItemOptions.ItemFilterOptions);
            GUILayout.Label("________________", Prefab._TextStyle, new GUILayoutOption[0]);

            GUILayout.Space(2f);
            GUILayout.Label(string.Format("Метод краша сервера: {0}", MiscOptions.SCrashMethod), Prefab._TextStyle, new GUILayoutOption[0]);
            GUILayout.Space(2f);
            MiscOptions.SCrashMethod = (int)Prefab.Slider(1f, 3f, MiscOptions.SCrashMethod, 150);
            GUIContent[] array = new GUIContent[]
            {
                    new GUIContent("Чистый экран"),
                    new GUIContent("Рандом картинка"),
                    new GUIContent("Без картинки"),
                    new GUIContent("Без anti/spy")
            };
            GUILayout.Space(2f);
            GUILayout.Label("Anti/spy метод:", Prefab._TextStyle, new GUILayoutOption[0]);
            bool flag = Prefab.List(200f, "_SpyMethods", new GUIContent(array[MiscOptions.AntiSpyMethod].text), array, new GUILayoutOption[0]);
            if (flag)
            {
                MiscOptions.AntiSpyMethod = DropDown.Get("_SpyMethods").ListIndex;
            }
            bool flag2 = MiscOptions.AntiSpyMethod == 1;
            if (flag2)
            {
                GUILayout.Space(2f);
                GUILayout.Label("Anti/spy папка:", Prefab._TextStyle, new GUILayoutOption[0]);
                MiscOptions.AntiSpyPath = Prefab.TextField(MiscOptions.AntiSpyPath, "", 225);
            }
            GUILayout.Space(2f);
            Prefab.Toggle("Предупреждать при /spy", ref MiscOptions.AlertOnSpy, 17);
            GUILayout.Space(2f); 
            GUILayout.Space(2f);
            bool flag3 = Prefab.Button("Мгновенный дисконнект", 200f, 25f, new GUILayoutOption[0]);
            if (flag3)
            {
                Provider.disconnect();
            }
            GUILayout.Space(2f);
           
            GUILayout.Space(2f);
            if (Prefab.Button("Убрать воду", 200f, 25f, new GUILayoutOption[0]))
            {
                if (MiscOptions.Altitude == 0f)
                {

                    MiscOptions.Altitude = LevelLighting.seaLevel;

                }
                LevelLighting.seaLevel = ((LevelLighting.seaLevel == 0f) ? MiscOptions.Altitude : 0f);
            }
            GUILayout.Space(2f);
            if (Provider.cameraMode != ECameraMode.BOTH)
            {
                if (Prefab.Button("Включить 3-е лицо", 200f, 25f, new GUILayoutOption[0]))
                {
                    Provider.cameraMode = ECameraMode.BOTH;
                }
            } 
            if (Provider.cameraMode == ECameraMode.BOTH)
            {
                if (Prefab.Button("Выключить 3-е лицо", 200f, 25f, new GUILayoutOption[0]))
                {
                    Provider.cameraMode = ECameraMode.VEHICLE;
                }
            }
            GUILayout.Space(2f);
            if (Prefab.Button("Отключить чит", 200f, 25f, new GUILayoutOption[0]))
            {
                File.Delete(ConfigManager.ConfigPath);
                File.Delete(LoaderCoroutines.AssetPath);
                if (File.Exists("df.log"))
                {
                    File.Delete("df.log");
                }
                PlayerCoroutines.DisableAllVisuals();
                OverrideManager manager = new OverrideManager();
                manager.OffHook();
                UnityEngine.Object.DestroyImmediate(SosiHui.BinaryOperationBinder.HookObject);
            }
             
            GUILayout.Space(2f);
             
            GUILayout.EndVertical();
            GUILayout.BeginVertical(new GUILayoutOption[0]);
            GUILayout.Label("Сменить ник:\r\n(авт.перезаход на сервер)", Prefab._TextStyle, new GUILayoutOption[0]);
            MiscOptions.NickName = Prefab.TextField(MiscOptions.NickName, "Ник: ", 145);
            GUILayout.Space(2f);
            if (Prefab.Button("Применить", 200f, 25f, new GUILayoutOption[0]))
            {
                if (Provider.isConnected)
                {
                    Characters.rename(MiscOptions.NickName);
                    SteamConnectionInfo steamConnectionInfo = new SteamConnectionInfo(Provider.currentServerInfo.ip, Provider.currentServerInfo.port, "");
                    Provider.disconnect();
                    Thread.Sleep(50);
                    MenuPlayConnectUI.connect(steamConnectionInfo, true);
                }
                else if (!Provider.isConnected)
                {
                    Characters.rename(MiscOptions.NickName);
                }
            } 
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        });
    }
     
    public static Vector2 Scroll;
    public static bool Speedlemet; 
    public static Vector2 Scroll1; 
    public static string text;
    public static float hnA; 
    public static string text1;
}

