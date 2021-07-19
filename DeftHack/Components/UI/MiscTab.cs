using SDG.Unturned;
using System.Reflection;
using UnityEngine;

 
public static class MiscTab
{
    public static void g()
    {
        bool flag = !lb;
        int num = 210;
        lb = flag;
        int[] array = new int[]
        {
            1,
            2,
            3,
            4,
            5,
            6,
            7,
            8,
            9,
            10,
            11,
            12,
            13,
            14,
            15,
            16,
            17,
            18,
            19,
            21,
            22,
            23,
            24,
            26,
            27,
            28,
            29,
            30
        };
        int num2 = 0;
        for (; ; )
        {
            int num3 = num2;
            int num4 = array.Length;
            num = ((num & -209) | 4);
            if (num3 >= num4)
            {
                break;
            }
            for (; ; )
            {
                int layer = array[num2];
                Physics.IgnoreLayerCollision(LayerMasks.VEHICLE, layer, lb);
                int num5 = o;
                if ((8013 ^ num5 + num5 - (num5 * 168 + num5 * 344)) != 0)
                {
                    num2++;
                    int num6 = i;
                    if (~num6 != (int)((uint)(num6 * -1081081856 / 1183637 | num6 << 17) >> 17))
                    {
                        break;
                    }
                }
            }
        }
    }
    public static int i;
    public static int o;
    public static bool lb;

    public static int db;
    public static int nnX;
    public static EEngine znI;
    public static void Unc(bool bool_0)
    {
        bool fuck = bool_0;
        InteractableVehicle vehicle = Player.player.movement.getVehicle();
        if (vehicle != null)
        {
            vehicle.GetComponent<Rigidbody>().useGravity = !bool_0;
        }
    }

    public static void Mcz()
    {
        nnX++;
        if (nnX > 3)
        {
            nnX = 0;
        }
        switch (nnX)
        {
            case 0:
                Unc(false);
                znI = EEngine.BOAT;
                return;
            case 1:
                Unc(true);
                znI = EEngine.HELICOPTER;
                return;
            case 2:
                Unc(false);
                znI = EEngine.CAR;
                return;
            case 3:
                Unc(true);
                znI = EEngine.PLANE;
                return;
            default:
                return;
        }
    }
    public static void Wn5()
    {
        InteractableVehicle vehicle = Player.player.movement.getVehicle();
        FieldInfo expr_27 = vehicle.asset.GetType().GetField("_engine", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        if (expr_27 == null)
        {
            return;
        }
        expr_27.SetValue(vehicle.asset, znI);
    }
    public static string car;
    public static EEngine cb; 
    public static void Tab()
    { 
        Prefab.ScrollView(new Rect(0f, 0f, 466f, 436f), "Прочее", ref StatsTab.ScrollPos, delegate ()
        {
            if (cb == EEngine.CAR)
            {
                car = "Машина";
            }
            else
            {
                if (cb == EEngine.PLANE)
                {
                    car = "Самолёт";
                }
                else
                {
                    if (cb == EEngine.HELICOPTER)
                    {
                        car = "Вертолёт";
                    }
                    else
                    {
                        if (cb == EEngine.BLIMP)
                        {
                            car = "Дирижабль";
                        }
                        else
                        {
                            if (cb == EEngine.TRAIN)
                            {
                                car = "Поезд";
                            }
                            else
                            {

                            }
                        }
                    }
                }
            }
            GUILayout.BeginHorizontal(new GUILayoutOption[0]);
            GUILayout.BeginVertical(new GUILayoutOption[]
                {
                    GUILayout.Width(230f)
        });
            Prefab.Toggle("Для транспорта", ref MiscOptions.EnVehicle, 17);
            if (MiscOptions.EnVehicle == true)
            {
                Prefab.Toggle("Полёт транспорта", ref MiscOptions.VehicleFly, 17);
                bool vehicleFly = MiscOptions.VehicleFly;
                if (vehicleFly)
                {
                    Prefab.Toggle("Максимальная скорость", ref MiscOptions.VehicleUseMaxSpeed, 17);
                    bool flag = !MiscOptions.VehicleUseMaxSpeed;
                    if (flag)
                    {
                        GUILayout.Space(2f);
                        GUILayout.Label("Множитель скорости: " + MiscOptions.SpeedMultiplier + "x", Prefab._TextStyle, new GUILayoutOption[0]);
                        GUILayout.Space(2f);
                        MiscOptions.SpeedMultiplier = (float)System.Math.Round(Prefab.Slider(0f, 10f, MiscOptions.SpeedMultiplier, 175), 2);
                        GUILayout.Space(4f);
                    }
                }
                GUILayout.Space(2f); 
                GUILayout.Space(2f);
                if (Prefab.Button("Заправить машину", 200f, 25f, new GUILayoutOption[0]))
                {
                    InteractableVehicle vehicle1 = Player.player.movement.getVehicle();
                    if (vehicle1 != null)
                    {
                        vehicle1.askFillFuel(2000);
                    }
                }
                GUILayout.Space(2f);
                if (Prefab.Button("Проезд сквозь объекты" + MoreMiscTab.a(lb), 200f, 25f, new GUILayoutOption[0]))
                {

                    g();
                }
                GUILayout.Label("_________________________", Prefab._TextStyle, new GUILayoutOption[0]);
            }
            Prefab.Toggle("Быстрое снятия строений", ref MiscOptions.CustomSalvageTime, 17); 
            Prefab.Toggle("Постройка в препядствиях", ref MiscOptions.BuildinObstacles, 17);
            Prefab.Toggle("Время суток", ref MiscOptions.SetTimeEnabled, 17);
            Prefab.Toggle("Зависание", ref MiscOptions.hang, 17); 
            bool noMovementVerification = MiscOptions.NoMovementVerification;
            if (noMovementVerification)
            {
                Prefab.Toggle("Полёт игрока", ref MiscOptions.PlayerFlight, 17);
                bool playerFlight = MiscOptions.PlayerFlight;
                if (playerFlight)
                {
                    GUILayout.Label("Множитель скорости: " + MiscOptions.FlightSpeedMultiplier + "x", Prefab._TextStyle, new GUILayoutOption[0]);
                    GUILayout.Space(2f);
                    MiscOptions.FlightSpeedMultiplier = (float)System.Math.Round(Prefab.Slider(0f, 10f, MiscOptions.FlightSpeedMultiplier, 175), 2);
                }
            }

            Prefab.Toggle("Дальность удара", ref MiscOptions.PunchSilentAim, 17);
            if (MiscOptions.PunchSilentAim)
            {
                MiscOptions.ExtendMeleeRange = true;
            }
            else
            {
                MiscOptions.ExtendMeleeRange = false;
            } 
            GUILayout.Space(5f);


            GUILayout.EndVertical();
            GUILayout.BeginVertical(new GUILayoutOption[0]);
            bool flag2 = Provider.isConnected && OptimizationVariables.MainPlayer != null;
            if (flag2)
            {
                bool flag3 = !OptimizationVariables.MainPlayer.look.isOrbiting;
                if (flag3)
                {
                    OptimizationVariables.MainPlayer.look.orbitPosition = Vector3.zero;
                }
                Prefab.Toggle("Свободная камера", ref MiscOptions.Freecam, 17);
                bool isOrbiting = OptimizationVariables.MainPlayer.look.isOrbiting;
                if (isOrbiting)
                {
                    bool flag4 = Prefab.Button("Вернуть камеру", 150f, 25f, new GUILayoutOption[0]);
                    if (flag4)
                    {

                        OptimizationVariables.MainPlayer.look.orbitPosition = Vector3.zero;
                    }
                }
            } 
            Prefab.Toggle("Автопроверка движения", ref MiscOptions.AlwaysCheckMovementVerification, 17);
            bool isConnected = Provider.isConnected;
            if (isConnected)
            {
                bool flag5 = Prefab.Button("Проверить движение", 150f, 25f, new GUILayoutOption[0]);
                if (flag5)
                {
                    MiscComponent.CheckMovementVerification();
                }
            } 
            bool extendMeleeRange = MiscOptions.ExtendMeleeRange;
            if (extendMeleeRange)
            {
                GUILayout.Space(2f);
                GUILayout.Label("Расстояние удара: " + MiscOptions.MeleeRangeExtension, Prefab._TextStyle, new GUILayoutOption[0]);
                GUILayout.Space(2f);
                MiscOptions.MeleeRangeExtension = (float)System.Math.Round(Prefab.Slider(0f, 7.5f, MiscOptions.MeleeRangeExtension, 175), 1);
            }
            GUILayout.Space(5f);
            if (MiscOptions.SetTimeEnabled)
            {
                GUILayout.Label("ТЕКУЩЕЕ ВРЕМЯ", Prefab._TextStyle, new GUILayoutOption[0]);
                GUILayout.Label("Время: " + MiscOptions.Time, Prefab._TextStyle, new GUILayoutOption[0]);
                GUILayout.Space(2f);
                MiscOptions.Time = (float)System.Math.Round(Prefab.Slider(0f, 0.9f, MiscOptions.Time, 175), 2);
                GUILayout.Space(8f);
            }
            GUILayout.Space(5f);
            if (MiscOptions.CustomSalvageTime)
            {
                GUILayout.Label("ВРЕМЯ СНЯТИЯ ПОСТРОЕК", Prefab._TextStyle, new GUILayoutOption[0]);
                GUILayout.Label("Время снятия: " + MiscOptions.SalvageTime + " секунда", Prefab._TextStyle, new GUILayoutOption[0]);
                GUILayout.Space(2f);
                MiscOptions.SalvageTime = (float)System.Math.Round(Prefab.Slider(0f, 10f, MiscOptions.SalvageTime, 175));
                if (MiscOptions.SalvageTime == 0)
                {
                    MiscOptions.SalvageTime = 1;
                }
                GUILayout.Space(8f);
            }
            GUILayout.Space(5f);
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

        });
        Prefab.MenuArea(new Rect(17f, 291f, 215f, 135f), "СПАМЕР", delegate
        {
            Prefab.Toggle("Включить", ref MiscOptions.SpammerEnabled, 17);
            GUILayout.Space(5f);
            MiscOptions.SpamText = Prefab.TextField(MiscOptions.SpamText, "Текст: ", 150);
            GUILayout.Space(10f);
            GUILayout.Label("Задержка: " + MiscOptions.SpammerDelay + "ms", Prefab._TextStyle, new GUILayoutOption[0]);
            GUILayout.Space(5f);
            MiscOptions.SpammerDelay = (int)Prefab.Slider(0f, 10000f, MiscOptions.SpammerDelay, 175);
        });
        Prefab.MenuArea(new Rect(235f, 271f, 221f, 155f), "Взаимодействия", delegate
        {
            Prefab.Toggle("Взаимодейвие через:", ref InteractionOptions.InteractThroughWalls, 17);
            bool flag6 = !InteractionOptions.InteractThroughWalls;
            if (!flag6)
            {
                Prefab.Toggle("Стены/Полы/т.д.", ref InteractionOptions.NoHitStructures, 17);
                Prefab.Toggle("Сейфы/Двери/т.д.", ref InteractionOptions.NoHitBarricades, 17);
                Prefab.Toggle("Предметы", ref InteractionOptions.NoHitItems, 17);
                Prefab.Toggle("Транспорт", ref InteractionOptions.NoHitVehicles, 17);
                Prefab.Toggle("Ресурсы", ref InteractionOptions.NoHitResources, 17);
                Prefab.Toggle("Землю/Здания", ref InteractionOptions.NoHitEnvironment, 17);
            }
        });
    }
}

