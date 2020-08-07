using SDG.Unturned;


using UnityEngine;

 
public static class WeaponsTab
{
    
    public static void Tab()
    {
        Prefab.MenuArea(new Rect(0f, 0f, 466f, 436f), "ДЛЯ ОРУЖИЯ", delegate
        {
            GUILayout.BeginHorizontal(new GUILayoutOption[0]);
            GUILayout.BeginVertical(new GUILayoutOption[]
            {
                    GUILayout.Width(230f)
            });
            Prefab.Toggle("Нет отдачи", ref WeaponOptions.NoRecoil, 17);
            Prefab.Toggle("Нет разброса", ref WeaponOptions.NoSpread, 17);
            Prefab.Toggle("Нет увода", ref WeaponOptions.NoSway, 17);
            Prefab.Toggle("Нет баллистики", ref WeaponOptions.NoDrop, 17);
            Prefab.Toggle("Триггербот", ref TriggerbotOptions.Enabled, 17);
            Prefab.Toggle("Звук после убийства", ref WeaponOptions.OofOnDeath, 17);
            Prefab.Toggle("Автоперезарядка", ref WeaponOptions.AutoReload, 17);
            Prefab.Toggle("Показывать инфу о оружии", ref WeaponOptions.ShowWeaponInfo, 17);

            if (Prefab.Toggle("Стрелять всегда в голову", ref RaycastOptions.AlwaysHitHead, 17))
            {
                RaycastOptions.UseCustomLimb = false;
                RaycastOptions.UseRandomLimb = false;
            } 
            if (Prefab.Toggle("Случайная конечность", ref RaycastOptions.UseRandomLimb, 17))
            {
                RaycastOptions.UseCustomLimb = false;
                RaycastOptions.AlwaysHitHead = false;
            }
            if (!RaycastOptions.UseRandomLimb)
            {
                if (Prefab.Toggle("Кастомная конечность", ref RaycastOptions.UseCustomLimb, 17))
                {
                    RaycastOptions.UseRandomLimb = false;
                    RaycastOptions.AlwaysHitHead = false;
                }
            }
            GUILayout.Space(2f);
            GUIContent[] array = new GUIContent[]
            {
                    new GUIContent("Left Foot"),
                    new GUIContent("Left Leg"),
                    new GUIContent("Right Foot"),
                    new GUIContent("Right Leg"),
                    new GUIContent("Left Hand"),
                    new GUIContent("Left Arm"),
                    new GUIContent("Right Hand"),
                    new GUIContent("Right Arm"),
                    new GUIContent("Left Back"),
                    new GUIContent("Right Back"),
                    new GUIContent("Left Front"),
                    new GUIContent("Right Front"),
                    new GUIContent("Spine"),
                    new GUIContent("Skull")
            }; 
            GUILayout.Space(2f);
            bool flag2 = RaycastOptions.UseCustomLimb && !RaycastOptions.UseRandomLimb;
            if (flag2)
            {
                bool flag3 = Prefab.List(230f, "_TargetLimb", new GUIContent("Конечность: " + array[(int)RaycastOptions.TargetLimb].text), array, new GUILayoutOption[0]);
                if (flag3)
                {
                    RaycastOptions.TargetLimb = (ELimb)DropDown.Get("_TargetLimb").ListIndex;
                }
            }
            GUILayout.Space(2f);
          
            GUILayout.EndVertical();
            GUILayout.BeginVertical(new GUILayoutOption[]
            {
                    GUILayout.Width(230f)
            }); 
            Prefab.Toggle("Кратность увеличения(зум)", ref WeaponOptions.Zoom, 17);
            if (WeaponOptions.Zoom)
            {
                GUILayout.Space(2f);
                GUILayout.Label("Зум прицела: " + WeaponOptions.ZoomValue, Prefab._TextStyle, new GUILayoutOption[0]);
                WeaponOptions.ZoomValue = (int)Prefab.Slider(2f, 30f, WeaponOptions.ZoomValue, 200);
            }
             
            GUILayout.Space(2f);
             
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        });
    }
}

