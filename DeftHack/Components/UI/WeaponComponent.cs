using SDG.Unturned;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;





 
[Component]
[SpyComponent]
public class WeaponComponent : MonoBehaviour
{ 
    public static byte Ammo()
    {
        return (byte)WeaponComponent.AmmoInfo.GetValue(OptimizationVariables.MainPlayer.equipment.useable);
    }
     
    [Initializer]
    public static void Initialize()
    {
        ColorUtilities.addColor(new ColorVariable("_BulletTracersHitColor", "Оружие - пули трассеры (Hit)", new Color32(byte.MaxValue, 0, 0, byte.MaxValue), true));
        ColorUtilities.addColor(new ColorVariable("_BulletTracersColor", "Оружие - пули трассеры", new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue), true));
        ColorUtilities.addColor(new ColorVariable("_WeaponInfoColor", "Оружие - Информация", new Color32(0, byte.MaxValue, 0, byte.MaxValue), true));
        ColorUtilities.addColor(new ColorVariable("_WeaponInfoBorder", "Оружие - Информация (Граница)", new Color32(0, 0, 0, byte.MaxValue), true));
        ColorUtilities.addColor(new ColorVariable("_ShowFOVAim", "Отрисовка FOV Aim", new Color32(0, byte.MaxValue, 0, byte.MaxValue), true));
        ColorUtilities.addColor(new ColorVariable("_ShowFOV", "Отрисовка FOV SilentAim", new Color32(byte.MaxValue, 0, 0, byte.MaxValue), true));
        ColorUtilities.addColor(new ColorVariable("_CoordInfoColor", "Координаты - Информация", new Color32(255, 255, 255, byte.MaxValue), true));
        ColorUtilities.addColor(new ColorVariable("_CoordInfoBorder", "Координаты - Информация (Граница)", new Color32(0, 0, 0, 0), false));

        HotkeyComponent.ActionDict.Add("_ToggleTriggerbot", delegate
        {
            TriggerbotOptions.Enabled = !TriggerbotOptions.Enabled;
        });
        HotkeyComponent.ActionDict.Add("_ToggleNoRecoil", delegate
        {
            WeaponOptions.NoRecoil = !WeaponOptions.NoRecoil;
        });
        HotkeyComponent.ActionDict.Add("_ToggleNoSpread", delegate
        {
            WeaponOptions.NoSpread = !WeaponOptions.NoSpread;
        });
        HotkeyComponent.ActionDict.Add("_ToggleNoSway", delegate
        {
            WeaponOptions.NoSway = !WeaponOptions.NoSway;
        });
    }
     
    public void Start()
    {
        base.StartCoroutine(WeaponComponent.UpdateWeapon());
    }
     
    public void OnGUI()
    {
        bool flag = WeaponComponent.MainCamera == null;
        if (flag)
        {
            WeaponComponent.MainCamera = Camera.main;
        }
        bool noSway = WeaponOptions.NoSway;
        if (noSway)
        {
            bool flag2 = OptimizationVariables.MainPlayer != null && OptimizationVariables.MainPlayer.animator != null;
            if (flag2)
            {
                OptimizationVariables.MainPlayer.animator.viewSway = Vector3.zero;
            }
        }
        bool flag3 = Event.current.type != EventType.Repaint;
        if (!flag3)
        {
            bool flag4 = !DrawUtilities.ShouldRun();
            if (!flag4)
            {
                bool tracers = WeaponOptions.Tracers;
                if (tracers)
                {
                    ESPComponent.GLMat.SetPass(0);
                    GL.PushMatrix();
                    GL.LoadProjectionMatrix(WeaponComponent.MainCamera.projectionMatrix);
                    GL.modelview = WeaponComponent.MainCamera.worldToCameraMatrix;
                    GL.Begin(1);
                    for (int i = WeaponComponent.Tracers.Count - 1; i > -1; i--)
                    {
                        TracerLine tracerLine = WeaponComponent.Tracers[i];
                        bool flag5 = DateTime.Now - tracerLine.CreationTime > TimeSpan.FromSeconds(5.0);
                        if (flag5)
                        {
                            WeaponComponent.Tracers.Remove(tracerLine);
                        }
                        else
                        {
                            GL.Color(tracerLine.Hit ? ColorUtilities.getColor("_BulletTracersHitColor") : ColorUtilities.getColor("_BulletTracersColor"));
                            GL.Vertex(tracerLine.StartPosition);
                            GL.Vertex(tracerLine.EndPosition);
                        }
                    }
                    GL.End();
                    GL.PopMatrix();
                }
                bool showWeaponInfo = WeaponOptions.ShowWeaponInfo;
                if (showWeaponInfo)
                {
                    bool flag6 = !(OptimizationVariables.MainPlayer.equipment.asset is ItemGunAsset);
                    if (!flag6)
                    {
                        GUI.depth = 0;
                        ItemGunAsset itemGunAsset = (ItemGunAsset)OptimizationVariables.MainPlayer.equipment.asset;
                        string content = string.Format("<size=15>{0}\nДальность: {1}\nУрон игрокам: {2}</size>", itemGunAsset.itemName, itemGunAsset.range, itemGunAsset.playerDamageMultiplier.damage);
                        DrawUtilities.DrawLabel(ESPComponent.ESPFont, LabelLocation.MiddleLeft, new Vector2(Screen.width - 20, Screen.height / 2), content, ColorUtilities.getColor("_WeaponInfoColor"), ColorUtilities.getColor("_WeaponInfoBorder"), 1, null, 12);
                    }
                }



                if (ESPOptions.ShowCoordinates)
                { 
                    float x = OptimizationVariables.MainPlayer.transform.position.x;
                    float y = OptimizationVariables.MainPlayer.transform.position.y;
                    float z = OptimizationVariables.MainPlayer.transform.position.z;
                    string content = string.Format("<size=10>Координаты(X,Y,Z): {0},{1},{2}</size>", System.Math.Round(x, 2).ToString(), System.Math.Round(y, 2).ToString(), System.Math.Round(z, 2).ToString());
                    DrawUtilities.DrawLabel(ESPComponent.ESPFont, LabelLocation.TopRight, new Vector2(Screen.width / Screen.width + 10, Screen.height / 38), content, ColorUtilities.getColor("_CoordInfoColor"), ColorUtilities.getColor("_CoordInfoBorder"), 1, null, 12);
                }
                float radius = RaycastOptions.SilentAimFOV * 7 + 20;
                float radiusAim = AimbotOptions.FOV * 7 + 20;
                if (RaycastOptions.ShowSilentAimUseFOV)
                {
                    DrawUtilities.DrawCircle(AssetVariables.Materials["ESP"], ColorUtilities.getColor("_ShowFOV"), new Vector2(Screen.width / 2, Screen.height / 2), radius);
                }
                if (RaycastOptions.ShowAimUseFOV)
                {
                    DrawUtilities.DrawCircle(AssetVariables.Materials["ESP"], ColorUtilities.getColor("_ShowFOVAim"), new Vector2(Screen.width / 2, Screen.height / 2), radiusAim);
                }
            }
        }
    }
     
    public static IEnumerator UpdateWeapon()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(0.1f);
            bool flag = !DrawUtilities.ShouldRun();
            if (!flag)
            {
                ItemGunAsset PAsset;
                bool flag2 = (PAsset = (OptimizationVariables.MainPlayer.equipment.asset as ItemGunAsset)) == null;
                if (!flag2)
                {
                    if (WeaponOptions.Zoom)
                    {
                        if (!PlayerCoroutines.IsSpying)
                        {
                            float num2 = 90f / WeaponOptions.ZoomValue;
                            WeaponComponent.ZoomInfo.SetValue(Player.player.equipment.useable, num2);
                            Player.player.look.scopeCamera.fieldOfView = num2;
                            if (((UseableGun)Player.player.equipment.useable).isAiming && Player.player.look.perspective == null)
                            {
                                WeaponComponent.fov.SetValue(Player.player.look, num2);
                            }
                        }
                        else
                        {
                            WeaponComponent.fov.SetValue(Player.player.look, OptionsSettings.view);
                            SDG.Unturned.MainCamera.instance.fieldOfView = OptionsSettings.view;
                        }
                    } 

                    bool flag3 = !WeaponComponent.AssetBackups.ContainsKey(PAsset.id);
                    if (flag3)
                    {
                        float[] Backups = new float[]
                        {
                                PAsset.recoilAim,
                                PAsset.recoilMax_x,
                                PAsset.recoilMax_y,
                                PAsset.recoilMin_x,
                                PAsset.recoilMin_y,
                                PAsset.spreadAim,
                                PAsset.spreadHip
                        };
                        Backups[6] = PAsset.spreadHip;
                        WeaponComponent.AssetBackups.Add(PAsset.id, Backups);
                        Backups = null;
                    }
                    bool flag4 = WeaponOptions.NoRecoil && !PlayerCoroutines.IsSpying;
                    if (flag4)
                    {
                        PAsset.recoilAim = 0f;
                        PAsset.recoilMax_x = 0f;
                        PAsset.recoilMax_y = 0f;
                        PAsset.recoilMin_x = 0f;
                        PAsset.recoilMin_y = 0f;
                    }
                    else
                    {
                        PAsset.recoilAim = WeaponComponent.AssetBackups[PAsset.id][0];
                        PAsset.recoilMax_x = WeaponComponent.AssetBackups[PAsset.id][1];
                        PAsset.recoilMax_y = WeaponComponent.AssetBackups[PAsset.id][2];
                        PAsset.recoilMin_x = WeaponComponent.AssetBackups[PAsset.id][3];
                        PAsset.recoilMin_y = WeaponComponent.AssetBackups[PAsset.id][4];
                    }
                    bool flag5 = WeaponOptions.NoSpread && !PlayerCoroutines.IsSpying;
                    if (flag5)
                    {
                        PAsset.spreadAim = 0f;
                        PAsset.spreadHip = 0f;
                        PlayerUI.updateCrosshair(0f);
                    }
                    else
                    {
                        PAsset.spreadAim = WeaponComponent.AssetBackups[PAsset.id][5];
                        PAsset.spreadHip = WeaponComponent.AssetBackups[PAsset.id][6];
                        WeaponComponent.UpdateCrosshair.Invoke(OptimizationVariables.MainPlayer.equipment.useable, null);
                    }
                    WeaponComponent.Reload();
                    PAsset = null;
                }
            }
        }
    }
     
    public static void Reload()
    {
        bool flag = !WeaponOptions.AutoReload || WeaponComponent.Ammo() > 0;
        if (!flag)
        {
            IEnumerable<InventorySearch> source = from i in OptimizationVariables.MainPlayer.inventory.search(EItemType.MAGAZINE, ((ItemGunAsset)OptimizationVariables.MainPlayer.equipment.asset).magazineCalibers)
                                                  where i.jar.item.amount > 0
                                                  select i;
            List<InventorySearch> list = source.ToList<InventorySearch>();
            bool flag2 = list.Count == 0;
            if (!flag2)
            {
                InventorySearch inventorySearch = (from i in list
                                                   orderby i.jar.item.amount descending
                                                   select i).First<InventorySearch>();
                OptimizationVariables.MainPlayer.channel.send("askAttachMagazine", ESteamCall.CLIENTS, ESteamPacket.UPDATE_UNRELIABLE_BUFFER, new object[]
                {
                        inventorySearch.page,
                        inventorySearch.jar.x,
                        inventorySearch.jar.y
                });
            }
        }
    }
     
    public static Dictionary<ushort, float[]> AssetBackups = new Dictionary<ushort, float[]>();
     
    public static List<TracerLine> Tracers = new List<TracerLine>();
     
    public static Camera MainCamera;
    public static FieldInfo ZoomInfo = typeof(UseableGun).GetField("zoom", BindingFlags.Instance | BindingFlags.NonPublic);
 
    public static FieldInfo AmmoInfo = typeof(UseableGun).GetField("ammo", BindingFlags.Instance | BindingFlags.NonPublic);

    
    public static MethodInfo UpdateCrosshair = typeof(UseableGun).GetMethod("updateCrosshair", BindingFlags.Instance | BindingFlags.NonPublic);

    public static FieldInfo attachments1 = typeof(UseableGun).GetField("firstAttachments", BindingFlags.Instance | BindingFlags.NonPublic);

    public static FieldInfo fov = typeof(PlayerLook).GetField("fov", BindingFlags.Instance | BindingFlags.NonPublic);
}

