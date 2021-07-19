using SDG.NetPak;
using SDG.NetTransport;
using SDG.Unturned;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using UnityEngine;




public static class PlayerCoroutines
{
    [Obsolete]
    public static IEnumerator TakeScreenshot()
    {
        Player plr = OptimizationVariables.MainPlayer;
        SteamChannel channel = plr.channel;
        switch (MiscOptions.AntiSpyMethod)
        {
            case 0:
                {
                    bool flag = Time.realtimeSinceStartup - PlayerCoroutines.LastSpy < 0.5f || PlayerCoroutines.IsSpying;
                    if (flag)
                    {
                        yield break;
                    }
                    PlayerCoroutines.IsSpying = true;
                    PlayerCoroutines.LastSpy = Time.realtimeSinceStartup;
                    bool flag2 = !MiscOptions.PanicMode;
                    if (flag2)
                    {
                        PlayerCoroutines.DisableAllVisuals();
                    }
                    yield return new WaitForFixedUpdate();
                    yield return new WaitForEndOfFrame();
                    Texture2D screenshotRaw = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false)
                    {
                        name = "Screenshot_Raw",
                        hideFlags = HideFlags.HideAndDontSave
                    };
                    screenshotRaw.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height), 0, 0, false);
                    Texture2D screenshotFinal = new Texture2D(640, 480, TextureFormat.RGB24, false)
                    {
                        name = "Screenshot_Final",
                        hideFlags = HideFlags.HideAndDontSave
                    };
                    Color[] pixels = screenshotRaw.GetPixels();
                    Color[] array = new Color[screenshotFinal.width * screenshotFinal.height];
                    float num = screenshotRaw.width / (float)screenshotFinal.width;
                    float num2 = screenshotRaw.height / (float)screenshotFinal.height;
                    for (int i = 0; i < screenshotFinal.height; i++)
                    {
                        int num3 = (int)(i * num2) * screenshotRaw.width;
                        int num4 = i * screenshotFinal.width;
                        for (int j = 0; j < screenshotFinal.width; j++)
                        {
                            int num5 = (int)(j * num);
                            array[num4 + j] = pixels[num3 + num5];
                        }
                    }
                    screenshotFinal.SetPixels(array);
                    byte[] data = screenshotFinal.EncodeToJPG(33);
                    if (data.Length < 30000)
                    {
                        if (Provider.isServer)
                        {
                            _HandleScreenshotData(data, channel);
                        }
                        else
                        {
                            ServerInstanceMethod SendScreenshotRelay = ServerInstanceMethod.Get(typeof(Player), "ReceiveScreenshotRelay");
                            SendScreenshotRelay.Invoke(Player.player.GetNetId(), ENetReliability.Reliable, delegate (NetPakWriter writer)
                            {
                                ushort num6 = (ushort)data.Length;
                                writer.WriteUInt16(num6);
                                writer.WriteBytes(data, num6);
                            });
                        }
                    }
                    yield return new WaitForFixedUpdate();
                    yield return new WaitForEndOfFrame();
                    PlayerCoroutines.IsSpying = false;
                    bool flag4 = !MiscOptions.PanicMode;
                    if (flag4)
                    {
                        PlayerCoroutines.EnableAllVisuals();
                    }

                    break;
                }
            case 1:
                {
                    System.Random r = new System.Random();
                    string[] files = Directory.GetFiles(MiscOptions.AntiSpyPath);
                    byte[] dataRaw = File.ReadAllBytes(files[r.Next(files.Length)]);
                    Texture2D texRaw = new Texture2D(2, 2);
                    ImageConversion.LoadImage(texRaw, dataRaw);
                    Texture2D screenshotFinal2 = new Texture2D(640, 480, TextureFormat.RGB24, false)
                    {
                        name = "Screenshot_Final",
                        hideFlags = HideFlags.HideAndDontSave
                    };
                    Color[] pixels = texRaw.GetPixels();
                    Color[] array = new Color[screenshotFinal2.width * screenshotFinal2.height];
                    float num = texRaw.width / (float)screenshotFinal2.width;
                    float num2 = texRaw.height / (float)screenshotFinal2.height;
                    for (int i = 0; i < screenshotFinal2.height; i++)
                    {
                        int num3 = (int)(i * num2) * texRaw.width;
                        int num4 = i * screenshotFinal2.width;
                        for (int j = 0; j < screenshotFinal2.width; j++)
                        {
                            int num5 = (int)(j * num);
                            array[num4 + j] = pixels[num3 + num5];
                        }
                    }
                    screenshotFinal2.SetPixels(array);
                    byte[] data = screenshotFinal2.EncodeToJPG(33);
                    if (data.Length < 30000)
                    {
                        if (Provider.isServer)
                        {
                            _HandleScreenshotData(data, channel);
                        }
                        else
                        {
                            ServerInstanceMethod SendScreenshotRelay = ServerInstanceMethod.Get(typeof(Player), "ReceiveScreenshotRelay");
                            SendScreenshotRelay.Invoke(Player.player.GetNetId(), ENetReliability.Reliable, delegate (NetPakWriter writer)
                            {
                                ushort num6 = (ushort)data.Length;
                                writer.WriteUInt16(num6);
                                writer.WriteBytes(data, num6);
                            });
                        }
                    }
                    break;
                }
            case 3:
                {
                    yield return new WaitForFixedUpdate();
                    yield return new WaitForEndOfFrame();
                    Texture2D screenshotRaw = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false)
                    {
                        name = "Screenshot_Raw",
                        hideFlags = HideFlags.HideAndDontSave
                    };
                    screenshotRaw.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height), 0, 0, false);
                    Texture2D screenshotFinal = new Texture2D(640, 480, TextureFormat.RGB24, false)
                    {
                        name = "Screenshot_Final",
                        hideFlags = HideFlags.HideAndDontSave
                    };
                    Color[] pixels = screenshotRaw.GetPixels();
                    Color[] array = new Color[screenshotFinal.width * screenshotFinal.height];
                    float num = screenshotRaw.width / (float)screenshotFinal.width;
                    float num2 = screenshotRaw.height / (float)screenshotFinal.height;
                    for (int i = 0; i < screenshotFinal.height; i++)
                    {
                        int num3 = (int)(i * num2) * screenshotRaw.width;
                        int num4 = i * screenshotFinal.width;
                        for (int j = 0; j < screenshotFinal.width; j++)
                        {
                            int num5 = (int)(j * num);
                            array[num4 + j] = pixels[num3 + num5];
                        }
                    }
                    screenshotFinal.SetPixels(array);
                    byte[] data = screenshotFinal.EncodeToJPG(33);
                    if (data.Length < 30000)
                    {
                        if (Provider.isServer)
                        {
                            _HandleScreenshotData(data, channel);
                        }
                        else
                        {
                            ServerInstanceMethod SendScreenshotRelay = ServerInstanceMethod.Get(typeof(Player), "ReceiveScreenshotRelay");
                            SendScreenshotRelay.Invoke(Player.player.GetNetId(), ENetReliability.Reliable, delegate (NetPakWriter writer)
                            {
                                ushort num6 = (ushort)data.Length;
                                writer.WriteUInt16(num6);
                                writer.WriteBytes(data, num6);
                            });
                        }
                    }
                    yield return new WaitForFixedUpdate();
                    yield return new WaitForEndOfFrame();
                    break;
                }
        }
        bool alertOnSpy = MiscOptions.AlertOnSpy;
        if (alertOnSpy)
        {
            OptimizationVariables.MainPlayer.StartCoroutine(PlayerCoroutines.ScreenShotMessageCoroutine());
        }
        yield break;
    } 
    public static void _HandleScreenshotData(byte[] data, SteamChannel channel)
    { 
            if (Dedicator.isDedicated)
            { 
                ReadWrite.writeBytes(string.Concat(new string[]
                {
                    ReadWrite.PATH,
                    ServerSavedata.directory,
                    "/",
                    Provider.serverID,
                    "/Spy.jpg"
                }), false, false, data); 
                ReadWrite.writeBytes(string.Concat(new object[]
                {
                    ReadWrite.PATH,
                    ServerSavedata.directory,
                    "/",
                    Provider.serverID,
                    "/Spy/",
                    channel.owner.playerID.steamID.m_SteamID,
                    ".jpg"
                }), false, false, data); 
                if (Player.player .onPlayerSpyReady != null)
                {
                Player.player.onPlayerSpyReady(channel.owner.playerID.steamID, data);
                     
                } 
                Queue<PlayerSpyReady> screenshotsCallbacks = new Queue<PlayerSpyReady>();
                PlayerSpyReady playerSpyReady = screenshotsCallbacks.Dequeue();
                if (playerSpyReady != null)
                {
                    playerSpyReady(channel.owner.playerID.steamID, data);
                    return;
                } 
            }
            else
            {
                ReadWrite.writeBytes("/Spy.jpg", false, true, data);
                if (Player.onSpyReady != null)
                {
                    Player.onSpyReady(channel.owner.playerID.steamID, data);
                }
                Debug.Log("0x4");
            } 
    }


    public static IEnumerator ScreenShotMessageCoroutine()
    {
        float started = Time.realtimeSinceStartup;
        bool flag2;
        do
        {
            yield return new WaitForEndOfFrame();
            bool flag = !PlayerCoroutines.IsSpying;
            if (flag)
            {
                PlayerUI.hint(null, EPlayerMessage.INTERACT, "Тебя проверяют на /spy", Color.red, new object[0]);

            }
            flag2 = (Time.realtimeSinceStartup - started > 3f);
        }
        while (!flag2);
        yield break;
    }
    public static void DisableAllVisuals()
    {

        SpyManager.InvokePre();
        bool flag = DrawUtilities.ShouldRun();
        if (flag)
        {
            ItemGunAsset itemGunAsset;
            bool flag2 = (itemGunAsset = (OptimizationVariables.MainPlayer.equipment.asset as ItemGunAsset)) != null;
            if (flag2)
            {
                UseableGun useableGun = OptimizationVariables.MainPlayer.equipment.useable as UseableGun;
                PlayerUI.updateCrosshair(useableGun.isAiming ? WeaponComponent.AssetBackups[itemGunAsset.id][5] : WeaponComponent.AssetBackups[itemGunAsset.id][6]);
            }
        }
        if (LevelLighting.seaLevel == 0f)
        {
            LevelLighting.seaLevel = MiscOptions.Altitude;
        }

        SpyManager.DestroyComponents();

    }
    public static void EnableAllVisuals()
    {
        SpyManager.AddComponents();
        SpyManager.InvokePost();
    }

    public static float LastSpy;

    public static bool IsSpying;

    public static Player SpecPlayer;
}

