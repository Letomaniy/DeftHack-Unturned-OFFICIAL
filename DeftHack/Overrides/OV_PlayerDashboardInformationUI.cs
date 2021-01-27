//using SDG.Unturned;
//using System.Reflection;
//using UnityEngine;
//
//
// 
//public static class OV_PlayerDashboardInformationUI
//{
//   
//    public static Sleek mapDynamicContainer => (Sleek)OV_PlayerDashboardInformationUI.DynamicContainerInfo.GetValue(null);
//
// 
//    public static int GetMap()
//    {
//        PlayerInventory inventory = OptimizationVariables.MainPlayer.inventory;
//        bool flag = MiscOptions.GPS || inventory.has(1176) != null;
//        int result;
//        if (flag)
//        {
//            result = 1;
//        }
//        else
//        {
//            result = 0;
//        }
//        return result;
//    }
//
// 
//    [Initializer]
//    public static void Init()
//    {
//        OV_PlayerDashboardInformationUI.DynamicContainerInfo = typeof(PlayerDashboardInformationUI).GetField("mapDynamicContainer", ReflectionVariables.publicStatic);
//        OV_PlayerDashboardInformationUI.RefreshStaticMap = typeof(PlayerDashboardInformationUI).GetMethod("refreshStaticMap", BindingFlags.Static | BindingFlags.NonPublic);
//    }
//
// 
//    [OnSpy]
//    public static void Disable()
//    {
//        bool flag = !DrawUtilities.ShouldRun();
//        if (!flag)
//        {
//            OV_PlayerDashboardInformationUI.WasEnabled = MiscOptions.ShowPlayersOnMap;
//            OV_PlayerDashboardInformationUI.WasGPSEnabled = MiscOptions.Compass;
//            MiscOptions.ShowPlayersOnMap = false;
//            MiscOptions.GPS = false;
//            OV_PlayerDashboardInformationUI.RefreshStaticMap.Invoke(OptimizationVariables.MainPlayer.inventory, new object[]
//            {
//                    OV_PlayerDashboardInformationUI.GetMap()
//            });
//            OV_PlayerDashboardInformationUI.OV_refreshDynamicMap();
//        }
//    }
//
// 
//    [OffSpy]
//    public static void Enable()
//    {
//        bool flag = !DrawUtilities.ShouldRun();
//        if (!flag)
//        {
//            MiscOptions.ShowPlayersOnMap = OV_PlayerDashboardInformationUI.WasEnabled;
//            MiscOptions.GPS = OV_PlayerDashboardInformationUI.WasGPSEnabled;
//            OV_PlayerDashboardInformationUI.RefreshStaticMap.Invoke(OptimizationVariables.MainPlayer.inventory, new object[]
//            {
//                    OV_PlayerDashboardInformationUI.GetMap()
//            });
//            OV_PlayerDashboardInformationUI.OV_refreshDynamicMap();
//        }
//    }
// 
//    [Override(typeof(PlayerDashboardInformationUI), "searchForMapsInInventory", BindingFlags.Static | BindingFlags.NonPublic, 0)]
//    public static void OV_searchForMapsInInventory(ref bool enableChart, ref bool enableMap)
//    {
//        bool gps = MiscOptions.GPS;
//        if (gps)
//        {
//            enableMap = true;
//            enableChart = true;
//        }
//        else
//        {
//            OverrideUtilities.CallOriginal(null, new object[0]);
//        }
//    }
//
// 
//     
//    [Override(typeof(PlayerDashboardInformationUI), "refreshDynamicMap", BindingFlags.Static | BindingFlags.Public, 0)]
//    public static void OV_refreshDynamicMap()
//    {
//        OV_PlayerDashboardInformationUI.mapDynamicContainer.remove();
//        bool flag = !PlayerDashboardInformationUI.active;
//        if (!flag)
//        {
//            bool flag2 = PlayerDashboardInformationUI.noLabel.isVisible || !Provider.modeConfigData.Gameplay.Group_Map;
//            if (!flag2)
//            {
//                bool flag3 = LevelManager.levelType == ELevelType.ARENA;
//                if (flag3)
//                {
//                    SleekImageTexture sleekImageTexture = new SleekImageTexture(PlayerDashboardInformationUI.icons.load<Texture2D>("Arena_Area"))
//                    {
//                        positionScale_X = LevelManager.arenaTargetCenter.x / (Level.size - Level.border * 2) + 0.5f - LevelManager.arenaTargetRadius / (Level.size - Level.border * 2),
//                        positionScale_Y = 0.5f - LevelManager.arenaTargetCenter.z / (Level.size - Level.border * 2) - LevelManager.arenaTargetRadius / (Level.size - Level.border * 2),
//                        sizeScale_X = LevelManager.arenaTargetRadius * 2f / (Level.size - Level.border * 2),
//                        sizeScale_Y = LevelManager.arenaTargetRadius * 2f / (Level.size - Level.border * 2),
//                        backgroundColor = new Color(1f, 1f, 0f, 0.5f)
//                    };
//                    OV_PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture);
//                    SleekImageTexture sleekImageTexture2 = new SleekImageTexture((Texture2D)Resources.Load("Materials/Pixel"))
//                    {
//                        positionScale_Y = sleekImageTexture.positionScale_Y,
//                        sizeScale_X = sleekImageTexture.positionScale_X,
//                        sizeScale_Y = sleekImageTexture.sizeScale_Y,
//                        backgroundColor = new Color(1f, 1f, 0f, 0.5f)
//                    };
//                    OV_PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture2);
//                    SleekImageTexture sleekImageTexture3 = new SleekImageTexture((Texture2D)Resources.Load("Materials/Pixel"))
//                    {
//                        positionScale_X = sleekImageTexture.positionScale_X + sleekImageTexture.sizeScale_X,
//                        positionScale_Y = sleekImageTexture.positionScale_Y,
//                        sizeScale_X = 1f - sleekImageTexture.positionScale_X - sleekImageTexture.sizeScale_X,
//                        sizeScale_Y = sleekImageTexture.sizeScale_Y,
//                        backgroundColor = new Color(1f, 1f, 0f, 0.5f)
//                    };
//                    OV_PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture3);
//                    SleekImageTexture sleekImageTexture4 = new SleekImageTexture((Texture2D)Resources.Load("Materials/Pixel"))
//                    {
//                        sizeScale_X = 1f,
//                        sizeScale_Y = sleekImageTexture.positionScale_Y,
//                        backgroundColor = new Color(1f, 1f, 0f, 0.5f)
//                    };
//                    OV_PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture4);
//                    SleekImageTexture sleekImageTexture5 = new SleekImageTexture((Texture2D)Resources.Load("Materials/Pixel"))
//                    {
//                        positionScale_Y = sleekImageTexture.positionScale_Y + sleekImageTexture.sizeScale_Y,
//                        sizeScale_X = 1f,
//                        sizeScale_Y = 1f - sleekImageTexture.positionScale_Y - sleekImageTexture.sizeScale_Y,
//                        backgroundColor = new Color(1f, 1f, 0f, 0.5f)
//                    };
//                    OV_PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture5);
//                    SleekImageTexture sleekImageTexture6 = new SleekImageTexture(PlayerDashboardInformationUI.icons.load<Texture2D>("Arena_Area"))
//                    {
//                        positionScale_X = LevelManager.arenaCurrentCenter.x / (Level.size - Level.border * 2) + 0.5f - LevelManager.arenaCurrentRadius / (Level.size - Level.border * 2),
//                        positionScale_Y = 0.5f - LevelManager.arenaCurrentCenter.z / (Level.size - Level.border * 2) - LevelManager.arenaCurrentRadius / (Level.size - Level.border * 2),
//                        sizeScale_X = LevelManager.arenaCurrentRadius * 2f / (Level.size - Level.border * 2),
//                        sizeScale_Y = LevelManager.arenaCurrentRadius * 2f / (Level.size - Level.border * 2)
//                    };
//                    OV_PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture6);
//                    SleekImageTexture sleekImageTexture7 = new SleekImageTexture((Texture2D)Resources.Load("Materials/Pixel"))
//                    {
//                        positionScale_Y = sleekImageTexture6.positionScale_Y,
//                        sizeScale_X = sleekImageTexture6.positionScale_X,
//                        sizeScale_Y = sleekImageTexture6.sizeScale_Y
//                    };
//                    OV_PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture7);
//                    SleekImageTexture sleekImageTexture8 = new SleekImageTexture((Texture2D)Resources.Load("Materials/Pixel"))
//                    {
//                        positionScale_X = sleekImageTexture6.positionScale_X + sleekImageTexture6.sizeScale_X,
//                        positionScale_Y = sleekImageTexture6.positionScale_Y,
//                        sizeScale_X = 1f - sleekImageTexture6.positionScale_X - sleekImageTexture6.sizeScale_X,
//                        sizeScale_Y = sleekImageTexture6.sizeScale_Y
//                    };
//                    OV_PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture8);
//                    SleekImageTexture sleekImageTexture9 = new SleekImageTexture((Texture2D)Resources.Load("Materials/Pixel"))
//                    {
//                        sizeScale_X = 1f,
//                        sizeScale_Y = sleekImageTexture6.positionScale_Y
//                    };
//                    OV_PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture9);
//                    SleekImageTexture sleekImageTexture10 = new SleekImageTexture((Texture2D)Resources.Load("Materials/Pixel"))
//                    {
//                        positionScale_Y = sleekImageTexture6.positionScale_Y + sleekImageTexture6.sizeScale_Y,
//                        sizeScale_X = 1f,
//                        sizeScale_Y = 1f - sleekImageTexture6.positionScale_Y - sleekImageTexture6.sizeScale_Y
//                    };
//                    OV_PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture10);
//                }
//                foreach (SteamPlayer steamPlayer in Provider.clients)
//                {
//                    bool flag4 = steamPlayer.model == null;
//                    if (!flag4)
//                    {
//                        PlayerQuests quests = steamPlayer.player.quests;
//                        bool flag5 = steamPlayer.playerID.steamID != Provider.client && !quests.isMemberOfSameGroupAs(OptimizationVariables.MainPlayer) && (!MiscOptions.ShowPlayersOnMap || !DrawUtilities.ShouldRun() || PlayerCoroutines.IsSpying);
//                        if (!flag5)
//                        {
//                            SleekImageTexture sleekImageTexture11 = new SleekImageTexture
//                            {
//                                positionOffset_X = -10,
//                                positionOffset_Y = -10,
//                                positionScale_X = steamPlayer.player.transform.position.x / (Level.size - Level.border * 2) + 0.5f,
//                                positionScale_Y = 0.5f - steamPlayer.player.transform.position.z / (Level.size - Level.border * 2),
//                                sizeOffset_X = 20,
//                                sizeOffset_Y = 20
//                            };
//                            bool flag6 = !OptionsSettings.streamer;
//                            if (flag6)
//                            {
//                                sleekImageTexture11.texture = Provider.provider.communityService.getIcon(steamPlayer.playerID.steamID, false);
//                            }
//                            sleekImageTexture11.addLabel(steamPlayer.playerID.characterName, ESleekSide.RIGHT);
//                            sleekImageTexture11.shouldDestroyTexture = true;
//                            OV_PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture11);
//                            bool flag7 = !quests.isMarkerPlaced;
//                            if (!flag7)
//                            {
//                                SleekImageTexture sleekImageTexture12 = new SleekImageTexture(PlayerDashboardInformationUI.icons.load<Texture2D>("Marker"))
//                                {
//                                    positionScale_X = quests.markerPosition.x / (Level.size - Level.border * 2) + 0.5f,
//                                    positionScale_Y = 0.5f - quests.markerPosition.z / (Level.size - Level.border * 2),
//                                    positionOffset_X = -10,
//                                    positionOffset_Y = -10,
//                                    sizeOffset_X = 20,
//                                    sizeOffset_Y = 20,
//                                    backgroundColor = steamPlayer.markerColor
//                                };
//                                sleekImageTexture12.addLabel(steamPlayer.playerID.characterName, ESleekSide.RIGHT);
//                                OV_PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture12);
//                            }
//                        }
//                    }
//                }
//                bool flag8 = OptimizationVariables.MainPlayer == null;
//                if (!flag8)
//                {
//                    SleekImageTexture sleekImageTexture13 = new SleekImageTexture
//                    {
//                        positionOffset_X = -10,
//                        positionOffset_Y = -10,
//                        positionScale_X = OptimizationVariables.MainPlayer.transform.position.x / (Level.size - Level.border * 2) + 0.5f,
//                        positionScale_Y = 0.5f - OptimizationVariables.MainPlayer.transform.position.z / (Level.size - Level.border * 2),
//                        sizeOffset_X = 20,
//                        sizeOffset_Y = 20,
//                        isAngled = true,
//                        angle = OptimizationVariables.MainPlayer.transform.rotation.eulerAngles.y,
//                        texture = PlayerDashboardInformationUI.icons.load<Texture2D>("Player"),
//                        backgroundTint = ESleekTint.FOREGROUND
//                    };
//                    sleekImageTexture13.addLabel(string.IsNullOrEmpty(Characters.active.nick) ? Characters.active.name : Characters.active.nick, ESleekSide.RIGHT);
//                    OV_PlayerDashboardInformationUI.mapDynamicContainer.add(sleekImageTexture13);
//                }
//            }
//        }
//    }
//     
//    public static bool WasGPSEnabled;
//     
//    public static bool WasEnabled;
//     
//    public static MethodInfo RefreshStaticMap;
//     
//    public static FieldInfo DynamicContainerInfo;
//}
