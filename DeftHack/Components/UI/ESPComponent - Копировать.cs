using HighlightingSystem;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;





[SpyComponent]
[Component]
public class ESPComponent : MonoBehaviour
{
   
    [Initializer]
    public static void Initialize()
    {
        for (int i = 0; i < ESPOptions.VisualOptions.Length; i++)
        {
            ESPTarget esptarget = (ESPTarget)i;
            Color32 color = Color.red;
            Color32 color2 = Color.white;
            switch (esptarget)
            {
                case ESPTarget.Зомби:
                    color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
                    break;
                case ESPTarget.Предметы:
                    color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
                    break;
                case ESPTarget.Турели:
                    color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
                    break;
                case ESPTarget.Кровати:
                    color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
                    break;
                case ESPTarget.КлеймФлаги:
                    color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
                    break;
                case ESPTarget.Транспорт:
                    color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
                    break;
                case ESPTarget.Ящики:
                    color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
                    break;
                case ESPTarget.Генераторы:
                    color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
                    break;
                case ESPTarget.Животные:
                    color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
                    break;
                case ESPTarget.Ловшуки:
                    color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
                    break;
                case ESPTarget.Аирдропы:
                    color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
                    break;
                case ESPTarget.Двери:
                    color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
                    break;
                case ESPTarget.Ягоды:
                    color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
                    break;
                case ESPTarget.Растения:
                    color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
                    break;
                case ESPTarget.C4:
                    color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
                    break;
                case ESPTarget.Fire:
                    color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
                    break;
                case ESPTarget.Лампы:
                    color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
                    break;
                case ESPTarget.Топливо:
                    color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
                    break;
                case ESPTarget.Генератор_безопасной_зоны:
                    color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
                    break;
                case ESPTarget.Генератор_Воздуха:
                    color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
                    break;
                case ESPTarget.NPC:
                    color = new Color32(byte.MaxValue, 0, 0, byte.MaxValue);
                    break;
            }
            ColorUtilities.addColor(new ColorVariable(string.Format("_{0}", esptarget), string.Format("ВХ - {0}", esptarget), color, false)); 
            ColorUtilities.addColor(new ColorVariable(string.Format("_{0}_Outline", esptarget), string.Format("ВХ - {0} (Контур)", esptarget), Color.black, false));
            ColorUtilities.addColor(new ColorVariable(string.Format("_{0}_Glow", esptarget), string.Format("ВХ - {0} (ОБВОДКА)", esptarget), Color.yellow, false));
        }
        ColorUtilities.addColor(new ColorVariable("_ESPFriendly", "Дружелюбные игроки", Color.green, false));
        ColorUtilities.addColor(new ColorVariable("_ChamsFriendVisible", "Чамсы - Видимый друг", Color.green, false));
        ColorUtilities.addColor(new ColorVariable("_ChamsFriendInisible", "Чамсы - Невидимый друг", Color.blue, false));
        ColorUtilities.addColor(new ColorVariable("_ChamsEnemyVisible", "Чамсы - Видимый враг", new Color32(byte.MaxValue, 165, 0, byte.MaxValue), false));
        ColorUtilities.addColor(new ColorVariable("_ChamsEnemyInvisible", "Чамсы - Невидимый враг", Color.red, false));
    }
     
    public void Start()
    {
        CoroutineComponent.ESPCoroutine = base.StartCoroutine(ESPCoroutines.UpdateObjectList());
        CoroutineComponent.ChamsCoroutine = base.StartCoroutine(ESPCoroutines.DoChams());
        ESPComponent.MainCamera = OptimizationVariables.MainCam;
    }
     
    public void Update()
    {
        bool noRain = MiscOptions.NoRain;
        if (noRain)
        {

            LevelLighting.rainyness = ELightingRain.NONE;
        }
        bool noSnow = MiscOptions.NoSnow;
        if (noSnow)
        {
            LevelLighting.snowyness = ELightingSnow.NONE;
        }

    }
     
    public void OnGUI()
    {
         
            bool flag = Event.current.type != EventType.Repaint || !ESPOptions.Enabled;
            if (!flag)
            {
                bool flag2 = !DrawUtilities.ShouldRun();
                if (!flag2)
                {
                    GUI.depth = 1;
                    bool flag3 = ESPComponent.MainCamera == null;
                    if (flag3)
                    {
                        ESPComponent.MainCamera = OptimizationVariables.MainCam;
                    }
                    Vector3 position = OptimizationVariables.MainPlayer.transform.position;
                    Vector3 position2 = OptimizationVariables.MainPlayer.look.aim.position;
                    Vector3 forward = OptimizationVariables.MainPlayer.look.aim.forward;
                    for (int i = 0; i < ESPVariables.Objects.Count; i++)
                    {
                        ESPObject espobject = ESPVariables.Objects[i];
                        ESPVisual espvisual = ESPOptions.VisualOptions[(int)espobject.Target];
                        GameObject gobject = espobject.GObject;
                        bool flag4 = !espvisual.Enabled;
                        if (flag4)
                        {
                            Highlighter component = gobject.GetComponent<Highlighter>();
                            bool flag5 = component != null && component != TrajectoryComponent.Highlighted;
                            if (flag5)
                            {
                                component.ConstantOffImmediate();
                            }
                        }
                        else
                        {
                            bool flag6 = espobject.Target == ESPTarget.Предметы && ESPOptions.FilterItems;
                            if (flag6)
                            {
                                bool flag7 = !ItemUtilities.Whitelisted(((InteractableItem)espobject.Object).asset, ItemOptions.ItemESPOptions);
                                if (flag7)
                                {
                                    goto IL_C13;
                                }
                            }
                            Color color = ColorUtilities.getColor(string.Format("_{0}", espobject.Target));
                            LabelLocation location = espvisual.Location;
                            bool flag8 = gobject == null;
                            if (!flag8)
                            {
                                Vector3 position3 = gobject.transform.position;
                                double distance = VectorUtilities.GetDistance(position3, position);
                                bool flag9 = distance < 0.5 || (distance > espvisual.Distance && !espvisual.InfiniteDistance);
                                if (!flag9)
                                {
                                    Vector3 vector = ESPComponent.MainCamera.WorldToScreenPoint(position3);
                                    bool flag10 = vector.z <= 0f;
                                    if (!flag10)
                                    {
                                        Vector3 localScale = gobject.transform.localScale;
                                        ESPTarget target = espobject.Target;
                                        Bounds bounds;
                                        if (target > ESPTarget.Зомби)
                                        {
                                            if (target != ESPTarget.Транспорт)
                                            {
                                                bounds = gobject.GetComponent<Collider>().bounds;
                                            }
                                            else
                                            {
                                                bounds = gobject.transform.Find("Model_0").GetComponent<MeshRenderer>().bounds;
                                                Transform transform = gobject.transform.Find("Model_1");
                                                bool flag11 = transform != null;
                                                if (flag11)
                                                {
                                                    bounds.Encapsulate(transform.GetComponent<MeshRenderer>().bounds);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            bounds = new Bounds(new Vector3(position3.x, position3.y + 1f, position3.z), new Vector3(localScale.x * 2f, localScale.y * 3f, localScale.z * 2f));
                                        }
                                        int textSize = DrawUtilities.GetTextSize(espvisual, distance);
                                        double num = System.Math.Round(distance);
                                        string text = string.Format("<size={0}>", textSize);
                                        string text2 = string.Format("<size={0}>", textSize);
                                        switch (espobject.Target)
                                        {
                                            case ESPTarget.Игроки:
                                                {
                                                    Player player = (Player)espobject.Object;
                                                    bool isDead = player.life.isDead;
                                                    if (isDead)
                                                    {
                                                        goto IL_C13;
                                                    }
                                                    bool showName = espvisual.ShowName;
                                                    if (showName)
                                                    {
                                                        text2 = text2 + ESPComponent.GetSteamPlayer(player).playerID.characterName + "\n";
                                                    }
                                                    bool flag12 = RaycastUtilities.TargetedPlayer == player && RaycastOptions.EnablePlayerSelection;
                                                    if (flag12)
                                                    {
                                                        text2 += "[Цель]\n";
                                                    }
                                                    bool showPlayerWeapon = ESPOptions.ShowPlayerWeapon;
                                                    if (showPlayerWeapon)
                                                    {
                                                        text2 = text2 + ((player.equipment.asset != null) ? player.equipment.asset.itemName : "Руки") + "\n";
                                                    }
                                                    bool showPlayerVehicle = ESPOptions.ShowPlayerVehicle;
                                                    if (showPlayerVehicle)
                                                    {
                                                        text2 += ((player.movement.getVehicle() != null) ? (player.movement.getVehicle().asset.name + "\n") : "Нет транспорта\n");
                                                    }
                                                    bounds.size /= 2f;
                                                    bounds.size = new Vector3(bounds.size.x, bounds.size.y * 1.25f, bounds.size.z);
                                                    bool flag13 = FriendUtilities.IsFriendly(player) && ESPOptions.UsePlayerGroup;
                                                    if (flag13)
                                                    {
                                                        color = ColorUtilities.getColor("_ESPFriendly");
                                                    }
                                                    break;
                                                }
                                            case ESPTarget.Зомби:
                                                {
                                                    bool isDead2 = ((Zombie)espobject.Object).isDead;
                                                    if (isDead2)
                                                    {
                                                        goto IL_C13;
                                                    }
                                                    bool showName2 = espvisual.ShowName;
                                                    if (showName2)
                                                    {
                                                        text2 += "Зомби\n";
                                                    }
                                                    break;
                                                }
                                            case ESPTarget.Предметы:
                                                {
                                                    InteractableItem interactableItem = (InteractableItem)espobject.Object;
                                                    bool showName3 = espvisual.ShowName;
                                                    if (showName3)
                                                    {
                                                        text2 = text2 + interactableItem.asset.itemName + "\n";
                                                    }
                                                    break;
                                                }
                                            case ESPTarget.Турели:
                                                {
                                                    InteractableSentry interactableSentry = (InteractableSentry)espobject.Object;
                                                    bool showName4 = espvisual.ShowName;
                                                    if (showName4)
                                                    {
                                                        text2 += "Турели\n";
                                                        text += "Турели\n";
                                                    }
                                                    bool showSentryItem = ESPOptions.ShowSentryItem;
                                                    if (showSentryItem)
                                                    {
                                                        text = text + ESPComponent.SentryName(interactableSentry.displayItem, false) + "\n";
                                                        text2 = text2 + ESPComponent.SentryName(interactableSentry.displayItem, true) + "\n";
                                                    }
                                                    break;
                                                }
                                            case ESPTarget.Кровати:
                                                {
                                                    InteractableBed bed = (InteractableBed)espobject.Object;
                                                    bool showName5 = espvisual.ShowName;
                                                    if (showName5)
                                                    {
                                                        text2 += "Кровать\n";
                                                        text += "Кровать\n";
                                                    }
                                                    bool showClaimed = ESPOptions.ShowClaimed;
                                                    if (showClaimed)
                                                    {
                                                        text2 = text2 + ESPComponent.GetOwned(bed, true) + "\n";
                                                        text = text + ESPComponent.GetOwned(bed, false) + "\n";
                                                    }
                                                    break;
                                                }
                                            case ESPTarget.КлеймФлаги:
                                                {
                                                    bool showName6 = espvisual.ShowName;
                                                    if (showName6)
                                                    {
                                                        text2 += "Клейм Флаг\n";
                                                    }
                                                    break;
                                                }
                                            case ESPTarget.Транспорт:
                                                {
                                                    InteractableVehicle interactableVehicle = (InteractableVehicle)espobject.Object;
                                                    bool flag14 = interactableVehicle.health == 0;
                                                    if (flag14)
                                                    {
                                                        goto IL_C13;
                                                    }
                                                    bool flag15 = ESPOptions.FilterVehicleLocked && interactableVehicle.isLocked;
                                                    if (flag15)
                                                    {
                                                        goto IL_C13;
                                                    }
                                                    interactableVehicle.getDisplayFuel(out ushort num2, out ushort num3);
                                                    float num4 = Mathf.Round(100f * (interactableVehicle.health / (float)interactableVehicle.asset.health));
                                                    float num5 = Mathf.Round(100f * (num2 / (float)num3));
                                                    bool showName7 = espvisual.ShowName;
                                                    if (showName7)
                                                    {
                                                        text2 = text2 + interactableVehicle.asset.name + "\n";
                                                        text = text + interactableVehicle.asset.name + "\n";
                                                    }
                                                    bool showVehicleHealth = ESPOptions.ShowVehicleHealth;
                                                    if (showVehicleHealth)
                                                    {
                                                        text2 += string.Format("Прочность: {0}%\n", num4);
                                                        text += string.Format("Прочность: {0}%\n", num4);
                                                    }
                                                    bool showVehicleFuel = ESPOptions.ShowVehicleFuel;
                                                    if (showVehicleFuel)
                                                    {
                                                        text2 += string.Format("Топливо: {0}%\n", num5);
                                                        text += string.Format("Топливо: {0}%\n", num5);
                                                    }
                                                    bool showVehicleLocked = ESPOptions.ShowVehicleLocked;
                                                    if (showVehicleLocked)
                                                    {
                                                        text2 = text2 + ESPComponent.GetLocked(interactableVehicle, true) + "\n";
                                                        text = text + ESPComponent.GetLocked(interactableVehicle, false) + "\n";
                                                    }
                                                    break;
                                                }
                                            case ESPTarget.Ящики:
                                                {
                                                    InteractableStorage InteractableStorage = (InteractableStorage)espobject.Object;
                                                    if (espvisual.ShowName)
                                                    {
                                                        text2 += "Ящик\n";
                                                        text2 = text2 + "ID: " + InteractableStorage.name + "\n";
                                                        if (InteractableStorage.name == "1374")
                                                        {
                                                            text2 += "Аирдроп\n";
                                                        }
                                                        else
                                                        {
                                                            if (InteractableStorage.name == "366")
                                                            {
                                                                text2 += "ИМЯ: " + "Кленовый Ящик\n";
                                                            }
                                                            else
                                                            {
                                                                if (InteractableStorage.name == "1202")
                                                                {
                                                                    text2 += "ИМЯ: " + "Кленовая Стойка для оружия\n";
                                                                }
                                                                else
                                                                {
                                                                    if (InteractableStorage.name == "1205")
                                                                    {
                                                                        text2 += "ИМЯ: " + "Кленовый Стенд\n";
                                                                    }
                                                                    else
                                                                    {
                                                                        if (InteractableStorage.name == "1245")
                                                                        {
                                                                            text2 += "ИМЯ: " + "Кленовый кухонный шкаф\n";
                                                                        }
                                                                        else
                                                                        {
                                                                            if (InteractableStorage.name == "1251")
                                                                            {
                                                                                text2 += "ИМЯ: " + "Кленовая Кухонная раковина\n";
                                                                            }
                                                                            else
                                                                            {
                                                                                if (InteractableStorage.name == "1278")
                                                                                {
                                                                                    text2 += "ИМЯ: " + "Кленовый шкаф\n";
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (InteractableStorage.name == "1410")
                                                                                    {
                                                                                        text2 += "ИМЯ: " + "Кленовая подставка для трофеев\n";
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (InteractableStorage.name == "1410")
                                                                                        {
                                                                                            text2 += "ИМЯ: " + "Кленовая подставка для трофеев\n";
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (InteractableStorage.name == "367")
                                                                                            {
                                                                                                text2 += "ИМЯ: " + "Берёзовый ящик\n";
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if (InteractableStorage.name == "1203")
                                                                                                {
                                                                                                    text2 += "ИМЯ: " + "Берёзовая стойка для оружия\n";
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    if (InteractableStorage.name == "1206")
                                                                                                    {
                                                                                                        text2 += "ИМЯ: " + "Берёзовый стенд\n";
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        if (InteractableStorage.name == "1246")
                                                                                                        {
                                                                                                            text2 += "ИМЯ: " + "Берёзовый кухонный шкаф\n";
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            if (InteractableStorage.name == "1252")
                                                                                                            {
                                                                                                                text2 += "ИМЯ: " + "Берёзовая раковина\n";
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                if (InteractableStorage.name == "1411")
                                                                                                                {
                                                                                                                    text2 += "ИМЯ: " + "Берёзовая подставка для трофеев\n";
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    if (InteractableStorage.name == "368")
                                                                                                                    {
                                                                                                                        text2 += "ИМЯ: " + "Сосновый ящик\n";
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        if (InteractableStorage.name == "1204")
                                                                                                                        {
                                                                                                                            text2 += "ИМЯ: " + "Сосновая стойка для оружия\n";
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            if (InteractableStorage.name == "1207")
                                                                                                                            {
                                                                                                                                text2 += "ИМЯ: " + "Сосновый стенд\n";
                                                                                                                            }
                                                                                                                            else
                                                                                                                            {
                                                                                                                                if (InteractableStorage.name == "1247")
                                                                                                                                {
                                                                                                                                    text2 += "ИМЯ: " + "Сосновый кухонный шкаф\n";
                                                                                                                                }
                                                                                                                                else
                                                                                                                                {
                                                                                                                                    if (InteractableStorage.name == "1253")
                                                                                                                                    {
                                                                                                                                        text2 += "ИМЯ: " + "Сосновая раковина\n";
                                                                                                                                    }
                                                                                                                                    else
                                                                                                                                    {
                                                                                                                                        if (InteractableStorage.name == "1280")
                                                                                                                                        {
                                                                                                                                            text2 += "ИМЯ: " + "Сосновый шкаф\n";
                                                                                                                                        }
                                                                                                                                        else
                                                                                                                                        {
                                                                                                                                            if (InteractableStorage.name == "1412")
                                                                                                                                            {
                                                                                                                                                text2 += "ИМЯ: " + "Сосновая подставка для трофеев\n";
                                                                                                                                            }
                                                                                                                                            else
                                                                                                                                            { 
                                                                                                                                                if (InteractableStorage.name == "328")
                                                                                                                                                {
                                                                                                                                                    text2 += "ИМЯ: " + "Сейф\n";
                                                                                                                                                }
                                                                                                                                                else
                                                                                                                                                {
                                                                                                                                                    if (InteractableStorage.name == "1220")
                                                                                                                                                    {
                                                                                                                                                        text2 += "ИМЯ: " + "Железная стойка для оружия\n";
                                                                                                                                                    }
                                                                                                                                                    else
                                                                                                                                                    {
                                                                                                                                                        if (InteractableStorage.name == "1221")
                                                                                                                                                        {
                                                                                                                                                            text2 += "ИМЯ: " + "Железный стенд\n";
                                                                                                                                                        }
                                                                                                                                                        else
                                                                                                                                                        {
                                                                                                                                                            if (InteractableStorage.name == "1248")
                                                                                                                                                            {
                                                                                                                                                                text2 += "ИМЯ: " + "Железный кухонный шкаф\n";
                                                                                                                                                            }
                                                                                                                                                            else
                                                                                                                                                            {
                                                                                                                                                                if (InteractableStorage.name == "1254")
                                                                                                                                                                {
                                                                                                                                                                    text2 += "ИМЯ: " + "Железная раковина\n";
                                                                                                                                                                }
                                                                                                                                                                else
                                                                                                                                                                {
                                                                                                                                                                    if (InteractableStorage.name == "1281")
                                                                                                                                                                    {
                                                                                                                                                                        text2 += "ИМЯ: " + "Железный шкаф\n";
                                                                                                                                                                    }
                                                                                                                                                                    else
                                                                                                                                                                    {
                                                                                                                                                                        if (InteractableStorage.name == "1413")
                                                                                                                                                                        {
                                                                                                                                                                            text2 += "ИМЯ: " + "Железная подставка для трофеев\n";
                                                                                                                                                                        }
                                                                                                                                                                        else
                                                                                                                                                                        {

                                                                                                                                                                        }
                                                                                                                                                                    }
                                                                                                                                                                }
                                                                                                                                                            }
                                                                                                                                                        }
                                                                                                                                                    }
                                                                                                                                                }
                                                                                                                                            }
                                                                                                                                        }
                                                                                                                                    }
                                                                                                                                }
                                                                                                                            }
                                                                                                                        }
                                                                                                                    }
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                break;
                                            case ESPTarget.Генераторы:
                                                {
                                                    if (espvisual.ShowName)
                                                    {
                                                        text2 += "Генератор\n";
                                                    }
                                                    InteractableGenerator interactableGenerator = (InteractableGenerator)espobject.Object;
                                                    float num6 = Mathf.Round(100f * (interactableGenerator.fuel / (float)interactableGenerator.capacity));
                                                    bool showGeneratorFuel = ESPOptions.ShowGeneratorFuel;
                                                    if (showGeneratorFuel)
                                                    {
                                                        text2 += string.Format("Топливо: {0}%\n", num6);
                                                        text += string.Format("Топливо: {0}%\n", num6);
                                                    }
                                                    bool showGeneratorPowered = ESPOptions.ShowGeneratorPowered;
                                                    if (showGeneratorPowered)
                                                    {
                                                        text2 = text2 + ESPComponent.GetPowered(interactableGenerator, true) + "\n";
                                                        text = text + ESPComponent.GetPowered(interactableGenerator, false) + "\n";
                                                    }
                                                    break;
                                                }
                                            case ESPTarget.Животные:
                                                Animal animal = (Animal)espobject.Object;
                                                if (espvisual.ShowName)
                                                {
                                                    text2 += "Животное\n";
                                                    text2 = text2 + animal.asset.animalName + "\n";
                                                }
                                                break;
                                            case ESPTarget.Ловшуки:
                                                InteractableTrap InteractableTrap = (InteractableTrap)espobject.Object;
                                                if (espvisual.ShowName)
                                                {
                                                    text2 += "Ловушка\n";
                                                }
                                                break;
                                            case ESPTarget.Двери:
                                                if (espvisual.ShowName)
                                                {
                                                    text2 += "Дверь\n";
                                                }
                                                break;
                                            case ESPTarget.Аирдропы:
                                                Carepackage carepackage = (Carepackage)espobject.Object;
                                                if (espvisual.ShowName)
                                                {
                                                    text2 += "Аирдроп\n";
                                                }
                                                break;
                                            case ESPTarget.Ягоды:
                                                if (espvisual.ShowName)
                                                {
                                                    text2 += "Ягода\n";

                                                }
                                                break;
                                            case ESPTarget.Растения:
                                                InteractableFarm interactableFarm = (InteractableFarm)espobject.Object;
                                                if (espvisual.ShowName)
                                                {
                                                    text2 += "Растение\n";
                                                    if (interactableFarm.name == "330")
                                                    {
                                                        text2 = text2 + "Саженец моркови" + "\n";
                                                    }
                                                    else
                                                    {
                                                        if (interactableFarm.name == "336")
                                                        {
                                                            text2 = text2 + "Саженец кукурузы" + "\n";
                                                        }
                                                        else
                                                        {

                                                            if (interactableFarm.name == "339")
                                                            {
                                                                text2 = text2 + "Саженец капусты" + "\n";
                                                            }
                                                            else
                                                            {
                                                                if (interactableFarm.name == "341")
                                                                {
                                                                    text2 = text2 + "Саженец помидора" + "\n";
                                                                }
                                                                else
                                                                {
                                                                    if (interactableFarm.name == "343")
                                                                    {
                                                                        text2 = text2 + "Саженец картошки" + "\n";
                                                                    }
                                                                    else
                                                                    {
                                                                        if (interactableFarm.name == "345")
                                                                        {
                                                                            text2 = text2 + "Саженец пшеницы" + "\n";
                                                                        }
                                                                        else
                                                                        {
                                                                            if (interactableFarm.name == "1104")
                                                                            {
                                                                                text2 = text2 + "Саженец янтаря" + "\n";
                                                                            }
                                                                            else
                                                                            {
                                                                                if (interactableFarm.name == "1105")
                                                                                {
                                                                                    text2 = text2 + "Саженец синих ягод" + "\n";
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (interactableFarm.name == "1106")
                                                                                    {
                                                                                        text2 = text2 + "Саженец зелёных ягод" + "\n";
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (interactableFarm.name == "1107")
                                                                                        {
                                                                                            text2 = text2 + "Саженец лиловых ягод" + "\n";
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (interactableFarm.name == "1108")
                                                                                            {
                                                                                                text2 = text2 + "Саженец алых ягод" + "\n";
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if (interactableFarm.name == "1109")
                                                                                                {
                                                                                                    text2 = text2 + "Саженец бирюзовых ягод" + "\n";
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    if (interactableFarm.name == "1110")
                                                                                                    {
                                                                                                        text2 = text2 + "Саженец жёлтых ягод" + "\n";
                                                                                                    }
                                                                                                    else
                                                                                                    {

                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                break;
                                            case ESPTarget.C4:
                                                InteractableCharge InteractableCharge = (InteractableCharge)espobject.Object;
                                                if (espvisual.ShowName)
                                                {
                                                    text2 += "C4\n";
                                                    if (InteractableCharge.name == "1241")
                                                    {
                                                        text2 = text2 + "Breaching Charge" + "\n";
                                                    }
                                                    else
                                                    {
                                                        if (InteractableCharge.name == "1393")
                                                        {
                                                            text2 = text2 + "Precision Charge" + "\n";
                                                        }
                                                        else
                                                        {

                                                        }
                                                    }
                                                }
                                                break;
                                            case ESPTarget.Fire:
                                                if (espvisual.ShowName)
                                                {
                                                    text2 += "Источник огня\n";
                                                }
                                                break;
                                            case ESPTarget.Лампы:
                                                if (espvisual.ShowName)
                                                {
                                                    text2 += "Лампа\n";
                                                }
                                                break;
                                            case ESPTarget.Топливо:
                                                if (espvisual.ShowName)
                                                {
                                                    text2 += "Топливная бочка\n";
                                                }
                                                break;
                                            case ESPTarget.Генератор_безопасной_зоны:
                                                InteractableSafezone safezone = (InteractableSafezone)espobject.Object;
                                                if (espvisual.ShowName)
                                                {
                                                    text2 += "Генератор сейф зоны\n";
                                                }
                                                break;
                                            case ESPTarget.Генератор_Воздуха:

                                                if (espvisual.ShowName)
                                                {
                                                    text2 += "Генератор воздуха\n";
                                                }
                                                break;
                                            case ESPTarget.NPC:

                                                if (espvisual.ShowName)
                                                {
                                                    text2 += "NPC\n";
                                                }
                                                break;
                                        }
                                        bool flag16 = text == string.Format("<size={0}>", textSize);
                                        if (flag16)
                                        {
                                            text = null;
                                        }
                                        bool showDistance = espvisual.ShowDistance;
                                        if (showDistance)
                                        {
                                            text2 += string.Format("{0}m\n", num);
                                            bool flag17 = text != null;
                                            if (flag17)
                                            {
                                                text += string.Format("{0}m\n", num);
                                            }
                                        }
                                        bool showAngle = espvisual.ShowAngle;
                                        if (showAngle)
                                        {
                                            double num7 = System.Math.Round(VectorUtilities.GetAngleDelta(position2, forward, position3), 2);
                                            text2 += string.Format("Угол: {0}°\n", num7);
                                            bool flag18 = text != null;
                                            if (flag18)
                                            {
                                                text += string.Format("{0}°\n", num7);
                                            }
                                        }
                                        text2 += "</size>";
                                        bool flag19 = text != null;
                                        if (flag19)
                                        {
                                            text += "</size>";
                                        }
                                        Vector3[] boxVectors = DrawUtilities.GetBoxVectors(bounds);
                                        Vector2[] rectangleLines = DrawUtilities.GetRectangleLines(ESPComponent.MainCamera, bounds, color);
                                        Vector3 v2 = DrawUtilities.Get2DW2SVector(ESPComponent.MainCamera, rectangleLines, location);
                                        bool flag20;
                                        if (MirrorCameraOptions.Enabled)
                                        {
                                            flag20 = rectangleLines.Any((Vector2 v) => MirrorCameraComponent.viewport.Contains(v));
                                        }
                                        else
                                        {
                                            flag20 = false;
                                        }
                                        bool flag21 = flag20;
                                        if (flag21)
                                        {
                                            Highlighter component2 = gobject.GetComponent<Highlighter>();
                                            bool flag22 = component2 != null;
                                            if (flag22)
                                            {
                                                component2.ConstantOffImmediate();
                                            }
                                        }
                                        else
                                        {
                                            bool boxes = espvisual.Boxes;
                                            if (boxes)
                                            {
                                                bool twoDimensional = espvisual.TwoDimensional;
                                                if (twoDimensional)
                                                {
                                                    DrawUtilities.PrepareRectangleLines(rectangleLines, color);
                                                }
                                                else
                                                {
                                                    DrawUtilities.PrepareBoxLines(boxVectors, color);
                                                    v2 = DrawUtilities.Get3DW2SVector(ESPComponent.MainCamera, bounds, location);
                                                }
                                            }
                                            bool glow = espvisual.Glow;
                                            if (glow)
                                            {
                                                Highlighter highlighter = gobject.GetComponent<Highlighter>() ?? gobject.AddComponent<Highlighter>();
                                                highlighter.occluder = true;
                                                highlighter.overlay = true;
                                                highlighter.ConstantOnImmediate(ColorUtilities.getColor(string.Format("_{0}_Glow", espobject.Target)));
                                                ESPComponent.Highlighters.Add(highlighter);
                                            }
                                            else
                                            {
                                                Highlighter component3 = gobject.GetComponent<Highlighter>();
                                                bool flag23 = component3 != null && component3 != TrajectoryComponent.Highlighted;
                                                if (flag23)
                                                {
                                                    component3.ConstantOffImmediate();
                                                }
                                            }
                                            bool labels = espvisual.Labels;
                                            if (labels)
                                            {
                                                DrawUtilities.DrawLabel(ESPComponent.ESPFont, location, v2, text2, espvisual.CustomTextColor ? ColorUtilities.getColor(string.Format("_{0}_Text", espobject.Target)) : color, ColorUtilities.getColor(string.Format("_{0}_Outline", espobject.Target)), espvisual.BorderStrength, text, 12);
                                            }
                                            bool lineToObject = espvisual.LineToObject;
                                            if (lineToObject)
                                            {
                                                ESPVariables.DrawBuffer2.Enqueue(new ESPBox2
                                                {
                                                    Color = color,
                                                    Vertices = new Vector2[]
                                                    {
                                                        new Vector2(Screen.width / 2, Screen.height),
                                                        new Vector2(vector.x, Screen.height - vector.y)
                                                    }
                                                });
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    IL_C13:;
                    }
                    ESPComponent.GLMat.SetPass(0);
                    GL.PushMatrix();
                    GL.LoadProjectionMatrix(ESPComponent.MainCamera.projectionMatrix);
                    GL.modelview = ESPComponent.MainCamera.worldToCameraMatrix;
                    GL.Begin(1);
                    for (int j = 0; j < ESPVariables.DrawBuffer.Count; j++)
                    {
                        ESPBox espbox = ESPVariables.DrawBuffer.Dequeue();
                        GL.Color(espbox.Color);
                        Vector3[] vertices = espbox.Vertices;
                        for (int k = 0; k < vertices.Length; k++)
                        {
                            GL.Vertex(vertices[k]);
                        }
                    }
                    GL.End();
                    GL.PopMatrix();
                    GL.PushMatrix();
                    GL.Begin(1);
                    for (int l = 0; l < ESPVariables.DrawBuffer2.Count; l++)
                    {
                        ESPBox2 espbox2 = ESPVariables.DrawBuffer2.Dequeue();
                        GL.Color(espbox2.Color);
                        Vector2[] vertices2 = espbox2.Vertices;
                        bool flag24 = true;
                        for (int m = 0; m < vertices2.Length; m++)
                        {
                            bool flag25 = m < vertices2.Length - 1;
                            if (flag25)
                            {
                                Vector2 b = vertices2[m];
                                Vector2 a = vertices2[m + 1];
                                bool flag26 = Vector2.Distance(a, b) > Screen.width / 2;
                                if (flag26)
                                {
                                    flag24 = false;
                                    break;
                                }
                            }
                        }
                        bool flag27 = !flag24;
                        if (!flag27)
                        {
                            for (int n = 0; n < vertices2.Length; n++)
                            {
                                GL.Vertex3(vertices2[n].x, vertices2[n].y, 0f);
                            }
                        }
                    }
                    GL.End();
                    GL.PopMatrix();
                }
            }
        
    } 
    [OnSpy]
    public static void DisableHighlighters()
    {
        foreach (Highlighter highlighter in ESPComponent.Highlighters)
        {
            highlighter.occluder = false;
            highlighter.overlay = false;
            highlighter.ConstantOffImmediate();
        }
        ESPComponent.Highlighters.Clear();
    }
     
    public static string SentryName(Item DisplayItem, bool color)
    {
        return (DisplayItem != null) ? Assets.find(EAssetType.ITEM, DisplayItem.id).name : (color ? "<color=#ff0000ff>Нет предмета</color>" : "Нет предмета");
    }
     
    public static string GetLocked(InteractableVehicle Vehicle, bool color)
    {
        return Vehicle.isLocked ? (color ? "<color=#ff0000ff>Закрыто</color>" : "Закрыто") : (color ? "<color=#00ff00ff>Открыто</color>" : "Открыто");
    }
     
    public static string GetPowered(InteractableGenerator Generator, bool color)
    {
        return Generator.isPowered ? (color ? "<color=#00ff00ff>Работает</color>" : "Работает") : (color ? "<color=#ff0000ff>Не работает</color>" : "Не работает");
    }
     
    public static string GetOwned(InteractableBed bed, bool color)
    {
        return bed.isClaimed ? (color ? "<color=$00ff00ff>Занята</color>" : "Занята") : (color ? "<color=#ff0000ff>Свободна</color>" : "Свободна");
    }
     
    public static SteamPlayer GetSteamPlayer(Player player)
    {
        foreach (SteamPlayer steamPlayer in Provider.clients)
        {
            bool flag = steamPlayer.player == player;
            if (flag)
            {
                return steamPlayer;
            }
        }
        return null;
    }
     
    public static Material GLMat;
      
    public static Font ESPFont;
     
    public static List<Highlighter> Highlighters = new List<Highlighter>();
     
    public static Camera MainCamera;
}

