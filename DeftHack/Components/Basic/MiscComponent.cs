using SDG.Provider;
using SDG.Unturned;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;



[Component]
public class MiscComponent : MonoBehaviour
{
    [Initializer]
    public static void Initialize()
    {
        HotkeyComponent.ActionDict.Add("_VFToggle", delegate
        {
            MiscOptions.VehicleFly = !MiscOptions.VehicleFly;
        });
        HotkeyComponent.ActionDict.Add("_ToggleAimbot", delegate
        {
            AimbotOptions.Enabled = !AimbotOptions.Enabled;
        });
        HotkeyComponent.ActionDict.Add("_AimbotOnKey", delegate
        {
            AimbotOptions.OnKey = !AimbotOptions.OnKey;
        });
        HotkeyComponent.ActionDict.Add("_ToggleFreecam", delegate
        {
            MiscOptions.Freecam = !MiscOptions.Freecam;
        });
        HotkeyComponent.ActionDict.Add("_PanicButton", delegate
        {
            MiscOptions.PanicMode = !MiscOptions.PanicMode;
            bool panicMode = MiscOptions.PanicMode;
            if (panicMode)
            {

                PlayerCoroutines.DisableAllVisuals();
            }
            else
            {
                PlayerCoroutines.EnableAllVisuals();
            }
        });
        HotkeyComponent.ActionDict.Add("_SelectPlayer", delegate
        {
            Vector3 position = OptimizationVariables.MainPlayer.look.aim.position;
            Vector3 forward = OptimizationVariables.MainPlayer.look.aim.forward;
            bool enablePlayerSelection = RaycastOptions.EnablePlayerSelection;
            if (enablePlayerSelection)
            {
                foreach (GameObject gameObject in RaycastUtilities.Objects)
                {
                    Player component = gameObject.GetComponent<Player>();
                    bool flag = component != null;
                    if (flag)
                    {
                        bool flag2 = VectorUtilities.GetAngleDelta(position, forward, gameObject.transform.position) < RaycastOptions.SelectedFOV;
                        if (flag2)
                        {
                            RaycastUtilities.TargetedPlayer = component;
                            break;
                        }
                    }
                }
            }
        });

        HotkeyComponent.ActionDict.Add("_InstantDisconnect", delegate
        {
            Provider.disconnect();
        });

    }

    [OnSpy]
    public static void Disable()
    {
        bool wasNightVision = MiscOptions.WasNightVision;
        if (wasNightVision)
        {
            MiscComponent.NightvisionBeforeSpy = true;
            MiscOptions.NightVision = false;
        }
        bool freecam = MiscOptions.Freecam;
        if (freecam)
        {
            MiscComponent.FreecamBeforeSpy = true;
            MiscOptions.Freecam = false;
        }
    }

    [OffSpy]
    public static void Enable()
    {
        bool nightvisionBeforeSpy = MiscComponent.NightvisionBeforeSpy;
        if (nightvisionBeforeSpy)
        {
            MiscComponent.NightvisionBeforeSpy = false;
            MiscOptions.NightVision = true;
        }
        bool freecamBeforeSpy = MiscComponent.FreecamBeforeSpy;
        if (freecamBeforeSpy)
        {
            MiscComponent.FreecamBeforeSpy = false;
            MiscOptions.Freecam = true;
        }
    }

    public void Start()
    {
        MiscComponent.Instance = this;
        Provider.onClientConnected = (Provider.ClientConnected)Delegate.Combine(Provider.onClientConnected, new Provider.ClientConnected(delegate ()
        {
            bool alwaysCheckMovementVerification = MiscOptions.AlwaysCheckMovementVerification;
            if (alwaysCheckMovementVerification)
            {
                MiscComponent.CheckMovementVerification();
            }
            else
            {
                MiscOptions.NoMovementVerification = false;
            }
        }));
        SkinsUtilities.RefreshEconInfo();
        HotkeyComponent.ActionDict.Add("_Com1", delegate
        {
            ChatManager.sendChat(EChatMode.GLOBAL, "/" + BindOptions.Com1);
        });
        HotkeyComponent.ActionDict.Add("_Com2", delegate
        {
            ChatManager.sendChat(EChatMode.GLOBAL, "/" + BindOptions.Com2);
        });
        HotkeyComponent.ActionDict.Add("_Com3", delegate
        {
            ChatManager.sendChat(EChatMode.GLOBAL, "/" + BindOptions.Com3);
        });
        HotkeyComponent.ActionDict.Add("_Com4", delegate
        {
            ChatManager.sendChat(EChatMode.GLOBAL, "/" + BindOptions.Com4);
        });
        HotkeyComponent.ActionDict.Add("_Com5", delegate
        {
            ChatManager.sendChat(EChatMode.GLOBAL, "/" + BindOptions.Com5);
        }); 
        HotkeyComponent.ActionDict.Add("_AutoPickUp", delegate
        {
            ItemOptions.AutoItemPickup = !ItemOptions.AutoItemPickup;
        });
    }

    public void Update()
    {
        if (Camera.main != null && OptimizationVariables.MainCam == null)
        {
            OptimizationVariables.MainCam = Camera.main;
        }
        bool flag2 = !OptimizationVariables.MainPlayer;
        if (!flag2)
        {
            bool flag3 = !DrawUtilities.ShouldRun();
            if (!flag3)
            {
                if (MiscOptions.hang)
                {
                    Player.player.movement.pluginGravityMultiplier = 0f;
                }
                else
                {
                    Player.player.movement.pluginGravityMultiplier = 1f;
                }
                Provider.provider.statisticsService.userStatisticsService.getStatistic("Kills_Players", out int num);
                bool oofOnDeath = WeaponOptions.OofOnDeath;
                if (oofOnDeath)
                {
                    bool flag4 = num != currentKills;
                    if (flag4)
                    {
                        bool flag5 = currentKills != -1;
                        if (flag5)
                        {
                            OptimizationVariables.MainPlayer.GetComponentInChildren<AudioSource>().PlayOneShot(AssetVariables.Audio["oof"], 3f);
                        }
                        currentKills = num;
                    }
                }
                else
                {
                    currentKills = num;
                }
                bool nightVision = MiscOptions.NightVision;
                if (nightVision)
                {
                    LevelLighting.vision = ELightingVision.MILITARY;
                    LevelLighting.updateLighting();
                    PlayerLifeUI.updateGrayscale();
                    MiscOptions.WasNightVision = true;
                }
                else
                {
                    bool wasNightVision = MiscOptions.WasNightVision;
                    if (wasNightVision)
                    {
                        LevelLighting.vision = ELightingVision.NONE;
                        LevelLighting.updateLighting();
                        PlayerLifeUI.updateGrayscale();
                        MiscOptions.WasNightVision = false;
                    }
                }
                bool isDead = OptimizationVariables.MainPlayer.life.isDead;
                if (isDead)
                {
                    MiscComponent.LastDeath = OptimizationVariables.MainPlayer.transform.position;
                }
                if (MiscOptions.NoFlash)
                {
                    if (MiscOptions.NoFlash && ((Color)typeof(PlayerUI).GetField("stunColor", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).a > 0f)
                    {
                        Color c = (Color)typeof(PlayerUI).GetField("stunColor", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
                        c.a = 0f;
                        typeof(PlayerUI).GetField("stunColor", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, c);
                    }
                }
            }
        }
    }

    public void FixedUpdate()
    {
        bool flag = !OptimizationVariables.MainPlayer;
        if (!flag)
        {
            MiscComponent.VehicleFlight();
            MiscComponent.PlayerFlight();
        }
    }

    public static void PlayerFlight()
    {
        Player mainPlayer = OptimizationVariables.MainPlayer;
        bool flag = !MiscOptions.PlayerFlight;
        if (flag)
        {
            ItemCloudAsset itemCloudAsset = mainPlayer.equipment.asset as ItemCloudAsset;
            mainPlayer.movement.itemGravityMultiplier = ((itemCloudAsset != null) ? itemCloudAsset.gravity : 1f);
        }
        else
        {
            mainPlayer.movement.itemGravityMultiplier = 0f;
            float flightSpeedMultiplier = MiscOptions.FlightSpeedMultiplier;
            bool flag2 = HotkeyUtilities.IsHotkeyHeld("_FlyUp");
            if (flag2)
            {
                mainPlayer.transform.position += mainPlayer.transform.up / 5f * flightSpeedMultiplier;
            }
            bool flag3 = HotkeyUtilities.IsHotkeyHeld("_FlyDown");
            if (flag3)
            {
                mainPlayer.transform.position -= mainPlayer.transform.up / 5f * flightSpeedMultiplier;
            }
            bool flag4 = HotkeyUtilities.IsHotkeyHeld("_FlyLeft");
            if (flag4)
            {
                mainPlayer.transform.position -= mainPlayer.transform.right / 5f * flightSpeedMultiplier;
            }
            bool flag5 = HotkeyUtilities.IsHotkeyHeld("_FlyRight");
            if (flag5)
            {
                mainPlayer.transform.position += mainPlayer.transform.right / 5f * flightSpeedMultiplier;
            }
            bool flag6 = HotkeyUtilities.IsHotkeyHeld("_FlyForward");
            if (flag6)
            {
                mainPlayer.transform.position += mainPlayer.transform.forward / 5f * flightSpeedMultiplier;
            }
            bool flag7 = HotkeyUtilities.IsHotkeyHeld("_FlyBackward");
            if (flag7)
            {
                mainPlayer.transform.position -= mainPlayer.transform.forward / 5f * flightSpeedMultiplier;
            }
        }
    }

    public static void VehicleFlight()
    {
        InteractableVehicle vehicle = OptimizationVariables.MainPlayer.movement.getVehicle();
        bool flag = vehicle == null;
        if (!flag)
        {
            Rigidbody component = vehicle.GetComponent<Rigidbody>();
            bool flag2 = component == null;
            if (!flag2)
            {
                bool vehicleFly = MiscOptions.VehicleFly;
                if (vehicleFly)
                {
                    float num = MiscOptions.VehicleUseMaxSpeed ? (vehicle.asset.speedMax * Time.fixedDeltaTime) : (MiscOptions.SpeedMultiplier / 3f);
                    component.useGravity = false;
                    component.isKinematic = true;
                    Transform transform = vehicle.transform;
                    bool flag3 = HotkeyUtilities.IsHotkeyHeld("_VFStrafeUp");
                    if (flag3)
                    {
                        transform.position += new Vector3(0f, num * 0.65f, 0f);
                    }
                    bool flag4 = HotkeyUtilities.IsHotkeyHeld("_VFStrafeDown");
                    if (flag4)
                    {
                        transform.position -= new Vector3(0f, num * 0.65f, 0f);
                    }
                    bool flag5 = HotkeyUtilities.IsHotkeyHeld("_VFStrafeLeft");
                    if (flag5)
                    {
                        component.MovePosition(transform.position - transform.right * num);
                    }
                    bool flag6 = HotkeyUtilities.IsHotkeyHeld("_VFStrafeRight");
                    if (flag6)
                    {
                        component.MovePosition(transform.position + transform.right * num);
                    }
                    bool flag7 = HotkeyUtilities.IsHotkeyHeld("_VFMoveForward");
                    if (flag7)
                    {
                        component.MovePosition(transform.position + transform.forward * num);
                    }
                    bool flag8 = HotkeyUtilities.IsHotkeyHeld("_VFMoveBackward");
                    if (flag8)
                    {
                        component.MovePosition(transform.position - transform.forward * num);
                    }
                    bool flag9 = HotkeyUtilities.IsHotkeyHeld("_VFRotateRight");
                    if (flag9)
                    {
                        transform.Rotate(0f, 1f, 0f);
                    }
                    bool flag10 = HotkeyUtilities.IsHotkeyHeld("_VFRotateLeft");
                    if (flag10)
                    {
                        transform.Rotate(0f, -1f, 0f);
                    }
                    bool flag11 = HotkeyUtilities.IsHotkeyHeld("_VFRollLeft");
                    if (flag11)
                    {
                        transform.Rotate(0f, 0f, 2f);
                    }
                    bool flag12 = HotkeyUtilities.IsHotkeyHeld("_VFRollRight");
                    if (flag12)
                    {
                        transform.Rotate(0f, 0f, -2f);
                    }
                    bool flag13 = HotkeyUtilities.IsHotkeyHeld("_VFRotateUp");
                    if (flag13)
                    {
                        vehicle.transform.Rotate(-2f, 0f, 0f);
                    }
                    bool flag14 = HotkeyUtilities.IsHotkeyHeld("_VFRotateDown");
                    if (flag14)
                    {
                        vehicle.transform.Rotate(2f, 0f, 0f);
                    }
                }
                else
                {
                    component.useGravity = true;
                    component.isKinematic = false;
                }
            }
        }
    }

    public static void CheckMovementVerification()
    {
        MiscComponent.Instance.StartCoroutine(MiscComponent.CheckVerification(OptimizationVariables.MainPlayer.transform.position));
    }
    public static void incrementStatTrackerValue(ushort itemID, int newValue)
    {
        if (Player.player == null)
        {
            return;
        }
        SteamPlayer owner = Player.player.channel.owner;
        if (owner == null)
        {
            return;
        }
        if (!owner.getItemSkinItemDefID(itemID, out int num))
        {
            return;
        }
        if (!owner.getTagsAndDynamicPropsForItem(num, out string text, out string text2))
        {
            return;
        }
        DynamicEconDetails dynamicEconDetails;
        dynamicEconDetails = new DynamicEconDetails(text, text2);
        if (dynamicEconDetails.getStatTrackerValue(out EStatTrackerType estatTrackerType, out int num2))
        {
            if (!owner.modifiedItems.Contains(itemID))
            {
                owner.modifiedItems.Add(itemID);
            }
            int i = 0;
            while (i < owner.skinItems.Length)
            {
                if (owner.skinItems[i] != num)
                {
                    i++;
                }
                else
                {
                    if (i < owner.skinDynamicProps.Length)
                    {
                        owner.skinDynamicProps[i] = dynamicEconDetails.getPredictedDynamicPropsJsonForStatTracker(estatTrackerType, newValue);
                        return;
                    }
                    break;
                }
            }
            return;
        }
    }
    public static IEnumerator CheckVerification(Vector3 LastPos)
    {
        bool flag = Time.realtimeSinceStartup - MiscComponent.LastMovementCheck < 0.8f;
        if (flag)
        {
            yield break;
        }
        MiscComponent.LastMovementCheck = Time.realtimeSinceStartup;
        OptimizationVariables.MainPlayer.transform.position = new Vector3(0f, -1337f, 0f);
        yield return new WaitForSeconds(3f);
        bool flag2 = VectorUtilities.GetDistance(OptimizationVariables.MainPlayer.transform.position, LastPos) < 10.0;
        if (flag2)
        {
            MiscOptions.NoMovementVerification = false;
        }
        else
        {
            MiscOptions.NoMovementVerification = true;
            OptimizationVariables.MainPlayer.transform.position = LastPos + new Vector3(0f, 5f, 0f);
        }
        yield break;
    }

    public static Vector3 LastDeath;
    public static MiscComponent Instance;
    public static float LastMovementCheck;
    public static bool FreecamBeforeSpy;
    public static bool NightvisionBeforeSpy;
    public static List<PlayerInputPacket> ClientsidePackets;
    public static FieldInfo Primary = typeof(PlayerEquipment).GetField("_primary", BindingFlags.Instance | BindingFlags.NonPublic);
    public static FieldInfo Sequence = typeof(PlayerInput).GetField("sequence", BindingFlags.Instance | BindingFlags.NonPublic);
    public static FieldInfo CPField = typeof(PlayerInput).GetField("clientsidePackets", BindingFlags.Instance | BindingFlags.NonPublic);
    public int currentKills = -1;
    public bool _isBroken;
}

