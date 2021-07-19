using UnityEngine;
public static class AimbotTab
{
   
    public static void Tab()
    {
        Prefab.MenuArea(new Rect(0f, 0f, 466f, 436f), "АИМ", delegate
        {
            GUILayout.BeginHorizontal(new GUILayoutOption[0]);
            GUILayout.BeginVertical(new GUILayoutOption[]
            {
                    GUILayout.Width(230f)
            });

            GUILayout.Space(2f);
            Prefab.Toggle("Тихий АИМ", ref RaycastOptions.Enabled, 17);
            GUILayout.Space(10f);
            bool enabled = RaycastOptions.Enabled;
            if (enabled)
            {
                Prefab.Toggle("Авто радиус сферы", ref SphereOptions.SpherePrediction, 17);
                GUILayout.Space(5f);
                bool flag1 = !SphereOptions.SpherePrediction;
                if (flag1)
                {
                    GUILayout.Label("Радиус сферы: " + System.Math.Round(SphereOptions.SphereRadius, 2) + "m", Prefab._TextStyle, new GUILayoutOption[0]);
                    Prefab.Slider(0f, 16f, ref SphereOptions.SphereRadius, 200);
                } 
                GUILayout.Space(5f);
                GUIContent[] array12 = new GUIContent[]
                {
                        new GUIContent("Игроки"),
                        new GUIContent("Зомби"),
                        new GUIContent("Турели"),
                        new GUIContent("Кровати"),
                        new GUIContent("Клейм флаг"),
                        new GUIContent("Ящики"),
                        new GUIContent("Транспорт")
                };
                 
                Prefab.Toggle("Не стрелять через стены", ref RaycastOptions.NoShootthroughthewalls, 17);
                GUILayout.Space(2f);
                Prefab.Toggle("Использовать FOV", ref RaycastOptions.SilentAimUseFOV, 17);
                bool silentAimUseFOV = RaycastOptions.SilentAimUseFOV;
                if (silentAimUseFOV)
                {
                    Prefab.Toggle("Отображать FOV", ref RaycastOptions.ShowSilentAimUseFOV, 17);

                    GUILayout.Space(2f);
                    GUILayout.Label("FOV: " + RaycastOptions.SilentAimFOV, Prefab._TextStyle, new GUILayoutOption[0]);
                    RaycastOptions.SilentAimFOV = Prefab.Slider(1f, 180f, RaycastOptions.SilentAimFOV, 200);
                    if (RaycastOptions.SilentAimFOV == 1)
                    {
                        RaycastOptions.SilentAimFOV = 2;
                    }
                }
                else
                {
                    RaycastOptions.ShowSilentAimUseFOV = false;
                }

                if (Prefab.List(200f, "_TargetPriority", new GUIContent("Приоритет: " + array12[(int)RaycastOptions.Target].text), array12, new GUILayoutOption[0]))
                {
                    RaycastOptions.Target = (TargetPriority)DropDown.Get("_TargetPriority").ListIndex;
                }


                GUILayout.Space(5f); 

            }

            GUILayout.EndVertical();


            GUILayout.BeginVertical(new GUILayoutOption[0]);

            Prefab.Toggle("АИМ", ref AimbotOptions.Enabled, 17); 
            Prefab.Toggle("Плавность", ref AimbotOptions.Smooth, 17);
            Prefab.Toggle("По кнопке(F)", ref AimbotOptions.OnKey, 17);
            GUILayout.Space(3f);
            if (AimbotOptions.Smooth)
            {
                GUILayout.Label("Скорость аима: " + AimbotOptions.AimSpeed, Prefab._TextStyle, new GUILayoutOption[0]);
                AimbotOptions.AimSpeed = (int)Prefab.Slider(1f, AimbotOptions.MaxSpeed, AimbotOptions.AimSpeed, 200);
            }

            if (AimbotOptions.TargetMode == TargetMode.FOV)
            {
                Prefab.Toggle("Использовать FOV", ref AimbotOptions.UseFovAim, 17);
                if (AimbotOptions.UseFovAim)
                {
                    Prefab.Toggle("Отображать FOV", ref RaycastOptions.ShowAimUseFOV, 17);
                    AimbotOptions.TargetMode = TargetMode.FOV;
                    GUILayout.Label("FOV: " + AimbotOptions.FOV, Prefab._TextStyle, new GUILayoutOption[0]);
                    AimbotOptions.FOV = (int)Prefab.Slider(1f, 180f, AimbotOptions.FOV, 200);
                    if (AimbotOptions.FOV == 1)
                    {
                        AimbotOptions.FOV = 3;
                    }
                }
                else
                {
                    RaycastOptions.ShowAimUseFOV = false;
                }
            }
            else if (AimbotOptions.TargetMode == TargetMode.Distance)
            {
                GUILayout.Label("Дистанция: " + AimbotOptions.Distance, Prefab._TextStyle, new GUILayoutOption[0]);
                AimbotOptions.Distance = (int)Prefab.Slider(50f, 1000f, AimbotOptions.Distance, 200);
            }




            GUIContent[] array = new GUIContent[]
            {
                    new GUIContent("Дистанция"),
                    new GUIContent("FOV")
            };
            bool flag = Prefab.List(200f, "_TargetMode", new GUIContent("Наводится: " + array[(int)AimbotOptions.TargetMode].text), array, new GUILayoutOption[0]);
            if (flag)
            {
                AimbotOptions.TargetMode = (TargetMode)DropDown.Get("_TargetMode").ListIndex;
            }
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        });
    }
}

