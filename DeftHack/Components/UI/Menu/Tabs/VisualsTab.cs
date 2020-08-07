using UnityEngine;


 public static class VisualsTab
{
     
    public static void Tab()
    {
        Prefab.ScrollView(new Rect(0f, 0f, 225f, 436f), "ВХ", ref StatsTab.ScrollPos, delegate () 
        {
            Prefab.SectionTabButton("Игроки", delegate
            {
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                GUILayout.BeginVertical(new GUILayoutOption[]
                {
                        GUILayout.Width(240f)
                });
                VisualsTab.BasicControls(ESPTarget.Игроки);
                bool flag = !ESPOptions.VisualOptions[0].Enabled;
                if (!flag)
                {
                    GUILayout.EndVertical();
                    GUILayout.BeginVertical(new GUILayoutOption[0]);
                    Prefab.Toggle("Показывать оружие", ref ESPOptions.ShowPlayerWeapon, 17);
                    Prefab.Toggle("Показывать транспорт", ref ESPOptions.ShowPlayerVehicle, 17);
                     GUILayout.EndVertical();
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                }
            }, 0f, 20);
            Prefab.SectionTabButton("Зомби", delegate
            {
                VisualsTab.BasicControls(ESPTarget.Зомби);
            }, 0f, 20);
            Prefab.SectionTabButton("Транспорт", delegate
            {
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                GUILayout.BeginVertical(new GUILayoutOption[]
                {
                        GUILayout.Width(240f)
                });
                VisualsTab.BasicControls(ESPTarget.Транспорт);
                bool flag = !ESPOptions.VisualOptions[6].Enabled;
                if (!flag)
                {
                    GUILayout.EndVertical();
                    GUILayout.BeginVertical(new GUILayoutOption[0]);
                    Prefab.Toggle("Кол-во топлива", ref ESPOptions.ShowVehicleFuel, 17);
                    Prefab.Toggle("Кол-во прочности", ref ESPOptions.ShowVehicleHealth, 17);
                    Prefab.Toggle("Показывать закрытые", ref ESPOptions.ShowVehicleLocked, 17);
                    Prefab.Toggle("Фильтровать закрытые", ref ESPOptions.FilterVehicleLocked, 17);
                    GUILayout.EndVertical();
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                }
            }, 0f, 20);
            Prefab.SectionTabButton("Предметы", delegate
            {
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                GUILayout.BeginVertical(new GUILayoutOption[]
                {
                        GUILayout.Width(240f)
                });
                VisualsTab.BasicControls(ESPTarget.Предметы);
                bool flag = !ESPOptions.VisualOptions[2].Enabled;
                if (!flag)
                {
                    GUILayout.EndVertical();
                    GUILayout.BeginVertical(new GUILayoutOption[0]);
                    Prefab.Toggle("Фильтр предметов", ref ESPOptions.FilterItems, 17);
                    bool filterItems = ESPOptions.FilterItems;
                    if (filterItems)
                    {
                        GUILayout.Space(5f);
                        ItemUtilities.DrawFilterTab(ItemOptions.ItemESPOptions);
                    }
                    GUILayout.EndVertical();
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                }
            }, 0f, 20);
            Prefab.SectionTabButton("Ящики", delegate
            {
                VisualsTab.BasicControls(ESPTarget.Ящики);
            }, 0f, 20);
            Prefab.SectionTabButton("Кровати", delegate
            {
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                GUILayout.BeginVertical(new GUILayoutOption[]
                {
                        GUILayout.Width(240f)
                });
                VisualsTab.BasicControls(ESPTarget.Кровати);
                bool flag = !ESPOptions.VisualOptions[4].Enabled;
                if (!flag)
                {
                    GUILayout.EndVertical();
                    GUILayout.BeginVertical(new GUILayoutOption[0]);
                    Prefab.Toggle("Показать занятые", ref ESPOptions.ShowClaimed, 17);
                    GUILayout.EndVertical();
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                }
            }, 0f, 20);
            Prefab.SectionTabButton("Генераторы", delegate
            {
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                GUILayout.BeginVertical(new GUILayoutOption[]
                {
                        GUILayout.Width(240f)
                });
                VisualsTab.BasicControls(ESPTarget.Генераторы);
                bool flag = !ESPOptions.VisualOptions[8].Enabled;
                if (!flag)
                {
                    GUILayout.EndVertical();
                    GUILayout.BeginVertical(new GUILayoutOption[0]);
                    Prefab.Toggle("Кол-во топлива", ref ESPOptions.ShowGeneratorFuel, 17);
                    Prefab.Toggle("Статус работы", ref ESPOptions.ShowGeneratorPowered, 17);
                    GUILayout.EndVertical();
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                }
            }, 0f, 20);
            Prefab.SectionTabButton("Турели", delegate
            {
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                GUILayout.BeginVertical(new GUILayoutOption[]
                {
                        GUILayout.Width(240f)
                });
                VisualsTab.BasicControls(ESPTarget.Турели);
                bool flag = !ESPOptions.VisualOptions[3].Enabled;
                if (!flag)
                {
                    GUILayout.EndVertical();
                    GUILayout.BeginVertical(new GUILayoutOption[0]);
                    Prefab.Toggle("Показывать оружие", ref ESPOptions.ShowSentryItem, 17);
                    GUILayout.EndVertical();
                    GUILayout.FlexibleSpace();
                    GUILayout.EndHorizontal();
                }
            }, 0f, 20);
            Prefab.SectionTabButton("Клейм флаги", delegate
            {
                VisualsTab.BasicControls(ESPTarget.КлеймФлаги);
            }, 0f, 20);
            Prefab.SectionTabButton("Животные", delegate
            {
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                GUILayout.BeginVertical(new GUILayoutOption[]
                {
                        GUILayout.Width(240f)
                });
                BasicControls(ESPTarget.Животные);
                if (!ESPOptions.VisualOptions[3].Enabled)
                {
                    return;
                }
                GUILayout.EndVertical();
                GUILayout.BeginVertical(new GUILayoutOption[0]);
                GUILayout.EndVertical();
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }, 0f, 20);
            Prefab.SectionTabButton("Ловушки", delegate
            {
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                GUILayout.BeginVertical(new GUILayoutOption[]
                {
                        GUILayout.Width(240f)
                });
                BasicControls(ESPTarget.Ловшуки);
                if (!ESPOptions.VisualOptions[3].Enabled)
                {
                    return;
                }
                GUILayout.EndVertical();
                GUILayout.BeginVertical(new GUILayoutOption[0]);
                GUILayout.EndVertical();
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }, 0f, 20);
            Prefab.SectionTabButton("Двери", delegate
            {
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                GUILayout.BeginVertical(new GUILayoutOption[]
                {
                        GUILayout.Width(240f)
                });
                BasicControls(ESPTarget.Двери);
                if (!ESPOptions.VisualOptions[3].Enabled)
                {
                    return;
                }
                GUILayout.EndVertical();
                GUILayout.BeginVertical(new GUILayoutOption[0]);
                GUILayout.EndVertical();
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }, 0f, 20);
            Prefab.SectionTabButton("Аирдропы", delegate
            {
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                GUILayout.BeginVertical(new GUILayoutOption[]
                {
                        GUILayout.Width(240f)
                });
                BasicControls(ESPTarget.Аирдропы);
                if (!ESPOptions.VisualOptions[3].Enabled)
                {
                    return;
                }
                GUILayout.EndVertical();
                GUILayout.BeginVertical(new GUILayoutOption[0]);
                GUILayout.EndVertical();
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }, 0f, 20);
            Prefab.SectionTabButton("Ягоды", delegate
            {
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                GUILayout.BeginVertical(new GUILayoutOption[]
                {
                        GUILayout.Width(240f)
                });
                BasicControls(ESPTarget.Ягоды);
                if (!ESPOptions.VisualOptions[3].Enabled)
                {
                    return;
                }
                GUILayout.EndVertical();
                GUILayout.BeginVertical(new GUILayoutOption[0]);
                GUILayout.EndVertical();
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }, 0f, 20);
            Prefab.SectionTabButton("Растения", delegate
            {
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                GUILayout.BeginVertical(new GUILayoutOption[]
                {
                        GUILayout.Width(240f)
                });
                BasicControls(ESPTarget.Растения);
                if (!ESPOptions.VisualOptions[3].Enabled)
                {
                    return;
                }
                GUILayout.EndVertical();
                GUILayout.BeginVertical(new GUILayoutOption[0]);
                GUILayout.EndVertical();
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }, 0f, 20);
            Prefab.SectionTabButton("Взрывчатка", delegate
            {
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                GUILayout.BeginVertical(new GUILayoutOption[]
                {
                        GUILayout.Width(240f)
                });
                BasicControls(ESPTarget.C4);
                if (!ESPOptions.VisualOptions[3].Enabled)
                {
                    return;
                }
                GUILayout.EndVertical();
                GUILayout.BeginVertical(new GUILayoutOption[0]);
                GUILayout.EndVertical();
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }, 0f, 20);
            Prefab.SectionTabButton("Источники огня", delegate
            {
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                GUILayout.BeginVertical(new GUILayoutOption[]
                {
                          GUILayout.Width(240f)
                });
                BasicControls(ESPTarget.Fire);
                if (!ESPOptions.VisualOptions[3].Enabled)
                {
                    return;
                }
                GUILayout.EndVertical();
                GUILayout.BeginVertical(new GUILayoutOption[0]);
                GUILayout.EndVertical();
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }, 0f, 20);
            Prefab.SectionTabButton("Лампы", delegate
            {
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                GUILayout.BeginVertical(new GUILayoutOption[]
                {
                          GUILayout.Width(240f)
                });
                BasicControls(ESPTarget.Лампы);
                if (!ESPOptions.VisualOptions[3].Enabled)
                {
                    return;
                }
                GUILayout.EndVertical();
                GUILayout.BeginVertical(new GUILayoutOption[0]);
                GUILayout.EndVertical();
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }, 0f, 20);
            Prefab.SectionTabButton("Топливо", delegate
            {
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                GUILayout.BeginVertical(new GUILayoutOption[]
                {
                          GUILayout.Width(240f)
                });
                BasicControls(ESPTarget.Топливо);
                if (!ESPOptions.VisualOptions[3].Enabled)
                {
                    return;
                }
                GUILayout.EndVertical();
                GUILayout.BeginVertical(new GUILayoutOption[0]);
                GUILayout.EndVertical();
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }, 0f, 20);
            Prefab.SectionTabButton("Ген. СЗ", delegate
            {
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                GUILayout.BeginVertical(new GUILayoutOption[]
                {
                        GUILayout.Width(240f)
                });
                BasicControls(ESPTarget.Генератор_безопасной_зоны);
                if (!ESPOptions.VisualOptions[3].Enabled)
                {
                    return;
                }
                GUILayout.EndVertical();
                GUILayout.BeginVertical(new GUILayoutOption[0]);
                GUILayout.EndVertical();
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }, 0f, 20);
            Prefab.SectionTabButton("Ген. воздух", delegate
            {
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                GUILayout.BeginVertical(new GUILayoutOption[]
                {
                        GUILayout.Width(240f)
                });
                BasicControls(ESPTarget.Генератор_Воздуха);
                if (!ESPOptions.VisualOptions[3].Enabled)
                {
                    return;
                }
                GUILayout.EndVertical();
                GUILayout.BeginVertical(new GUILayoutOption[0]);
                GUILayout.EndVertical();
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }, 0f, 20);
         });
        Prefab.MenuArea(new Rect(230f, 0f, 236f, 180f), "ДРУГОЕ", delegate
        {
            Prefab.SectionTabButton("Радар", delegate
            {
                Prefab.Toggle("Радар", ref RadarOptions.Enabled, 17);
                bool enabled = RadarOptions.Enabled;
                if (enabled)
                {
                    Prefab.Toggle("Центрирование игрока", ref RadarOptions.TrackPlayer, 17);
                    Prefab.Toggle("Показывать игроков", ref RadarOptions.ShowPlayers, 17);
                    Prefab.Toggle("Показывать машины", ref RadarOptions.ShowVehicles, 17);
                    bool showVehicles = RadarOptions.ShowVehicles;
                    if (showVehicles)
                    {
                        Prefab.Toggle("Только открытые", ref RadarOptions.ShowVehiclesUnlocked, 17);
                    }
                    GUILayout.Space(5f);
                    GUILayout.Label("Зум радара: " + Mathf.Round(RadarOptions.RadarZoom), Prefab._TextStyle, new GUILayoutOption[0]);
                    Prefab.Slider(0f, 10f, ref RadarOptions.RadarZoom, 200);
                    bool flag2 = Prefab.Button("По умолчанию", 105f, 25f, new GUILayoutOption[0]);
                    if (flag2)
                    {
                        RadarOptions.RadarZoom = 1f;
                    }
                    GUILayout.Space(5f);
                    GUILayout.Label("Размер радара: " + Mathf.RoundToInt(RadarOptions.RadarSize), Prefab._TextStyle, new GUILayoutOption[0]);
                    Prefab.Slider(50f, 1000f, ref RadarOptions.RadarSize, 200);
                }
            }, 0f, 20);
            Prefab.Toggle("Игроки в ванише", ref ESPOptions.ShowVanishPlayers, 17);
            Prefab.Toggle("Показывать координаты", ref ESPOptions.ShowCoordinates, 17);
            Prefab.Toggle("Камера заднего вида", ref MirrorCameraOptions.Enabled, 17);
            GUILayout.Space(5f);
            if (MirrorCameraOptions.Enabled)
            {
                if (Prefab.Button("Вернуть", 100f, 25f, new GUILayoutOption[0]))
                {
                    MirrorCameraComponent.FixCam();
                }
            }

        });
        Prefab.MenuArea(new Rect(230f, 185f, 236f, 250f), "Переключатели", delegate
        {
            bool flag = Prefab.Toggle("ВХ", ref ESPOptions.Enabled, 17);
            if (flag)
            {
                bool flag2 = !ESPOptions.Enabled;
                if (flag2)
                {
                    for (int i = 0; i < ESPOptions.VisualOptions.Length; i++)
                    {
                        ESPOptions.VisualOptions[i].Glow = false;
                    }
                    SosiHui.BinaryOperationBinder.HookObject.GetComponent<ESPComponent>().OnGUI();
                }
            }
            Prefab.Toggle("Чамсы", ref ESPOptions.ChamsEnabled, 17);
            bool chamsEnabled = ESPOptions.ChamsEnabled;
            if (chamsEnabled)
            {
                Prefab.Toggle("Плоские чамсы", ref ESPOptions.ChamsFlat, 17);
            }
            Prefab.Toggle("Без дождя", ref MiscOptions.NoRain, 17);
            Prefab.Toggle("Без снега", ref MiscOptions.NoSnow, 17); 
            Prefab.Toggle("No Flash", ref MiscOptions.NoFlash, 17);
            Prefab.Toggle("ПНВ", ref MiscOptions.NightVision, 17);
            Prefab.Toggle("Компасс", ref MiscOptions.Compass, 17);
            Prefab.Toggle("Карта(GPS)", ref MiscOptions.GPS, 17); 
            Prefab.Toggle("Показ игроков на карте", ref MiscOptions.ShowPlayersOnMap, 17);
        });
    } 
    public static void BasicControls(ESPTarget esptarget)
    {
        ESPVisual espvisual = ESPOptions.VisualOptions[(int)esptarget];
        Prefab.Toggle("Активировать", ref espvisual.Enabled, 17);
        bool flag = !espvisual.Enabled;
        if (!flag)
        {
            Prefab.Toggle("Надписи", ref espvisual.Labels, 17);
            bool labels = espvisual.Labels;
            if (labels)
            {
                Prefab.Toggle("Показывать имя", ref espvisual.ShowName, 17);
                Prefab.Toggle("Показывать дистанцию", ref espvisual.ShowDistance, 17);
                Prefab.Toggle("Показывать угол", ref espvisual.ShowAngle, 17); 
            }
            Prefab.Toggle("Вох", ref espvisual.Boxes, 17);
            bool boxes = espvisual.Boxes;
            if (boxes)
            {
                Prefab.Toggle("2D Вох", ref espvisual.TwoDimensional, 17);
            }
            Prefab.Toggle("Обводка", ref espvisual.Glow, 17);
            Prefab.Toggle("Линия до объекта", ref espvisual.LineToObject, 17);
            Prefab.Toggle("Масштаб текста", ref espvisual.TextScaling, 17);
            bool textScaling = espvisual.TextScaling;
            if (textScaling)
            {
                espvisual.MinTextSize = Prefab.TextField(espvisual.MinTextSize, "Минимальный размер текста: ", 30, 0, 255);
                espvisual.MaxTextSize = Prefab.TextField(espvisual.MaxTextSize, "Максимальный размер текста: ", 30, 0, 255);
                GUILayout.Space(3f);
                GUILayout.Label("Масштабирование текста по расстоянию: " + Mathf.RoundToInt(espvisual.MinTextSizeDistance), Prefab._TextStyle, new GUILayoutOption[0]);
                Prefab.Slider(0f, 1000f, ref espvisual.MinTextSizeDistance, 200);
                GUILayout.Space(3f);
            }
            else
            {
                espvisual.FixedTextSize = Prefab.TextField(espvisual.FixedTextSize, "Фиксированный размер текста: ", 30, 0, 255);
            }
            Prefab.Toggle("Дистанция на всю карту", ref espvisual.InfiniteDistance, 17);
            bool flag2 = !espvisual.InfiniteDistance;
            if (flag2)
            {
                GUILayout.Label("ESP Расстояние: " + Mathf.RoundToInt(espvisual.Distance), Prefab._TextStyle, new GUILayoutOption[0]);
                Prefab.Slider(0f, 4000f, ref espvisual.Distance, 200);
                GUILayout.Space(3f);
            }
            Prefab.Toggle("Лимит объектов", ref espvisual.UseObjectCap, 17);
            bool useObjectCap = espvisual.UseObjectCap;
            if (useObjectCap)
            {
                espvisual.ObjectCap = Prefab.TextField(espvisual.ObjectCap, "Object cap:", 30, 0, 255);
            }
            espvisual.BorderStrength = Prefab.TextField(espvisual.BorderStrength, "Border Strength:", 30, 0, 255);
        }
    }
}