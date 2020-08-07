using SDG.Unturned;
using System;
using System.Collections;
using System.IO;
using UnityEngine;



 
public static class PlayerCoroutines
{ 
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
                    Color[] oldColors = screenshotRaw.GetPixels();
                    Color[] newColors = new Color[screenshotFinal.width * screenshotFinal.height];
                    float widthRatio = screenshotRaw.width / (float)screenshotFinal.width;
                    float heightRatio = screenshotRaw.height / (float)screenshotFinal.height;
                    int num10;
                    for (int i = 0; i < screenshotFinal.height; i = num10 + 1)
                    {
                        int num = (int)(i * heightRatio) * screenshotRaw.width;
                        int num2 = i * screenshotFinal.width;
                        for (int j = 0; j < screenshotFinal.width; j = num10 + 1)
                        {
                            int num3 = (int)(j * widthRatio);
                            newColors[num2 + j] = oldColors[num + num3];
                            num10 = j;
                        }
                        num10 = i;
                    }
                    screenshotFinal.SetPixels(newColors);
                    byte[] data = ImageConversion.EncodeToJPG(screenshotFinal, 33);
                    bool flag3 = data.Length < 30000;
                    if (flag3)
                    {
                        channel.longBinaryData = true;
                        channel.openWrite();
                        channel.write(data);
                        channel.closeWrite("tellScreenshotRelay", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_CHUNK_BUFFER);
                        channel.longBinaryData = false;
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
                    Color[] oldColors2 = texRaw.GetPixels();
                    Color[] newColors2 = new Color[screenshotFinal2.width * screenshotFinal2.height];
                    float widthRatio2 = texRaw.width / (float)screenshotFinal2.width;
                    float heightRatio2 = texRaw.height / (float)screenshotFinal2.height;
                    int num10;
                    for (int k = 0; k < screenshotFinal2.height; k = num10 + 1)
                    {
                        int num4 = (int)(k * heightRatio2) * texRaw.width;
                        int num5 = k * screenshotFinal2.width;
                        for (int l = 0; l < screenshotFinal2.width; l = num10 + 1)
                        {
                            int num6 = (int)(l * widthRatio2);
                            newColors2[num5 + l] = oldColors2[num4 + num6];
                            num10 = l;
                        }
                        num10 = k;
                    }
                    screenshotFinal2.SetPixels(newColors2);
                    byte[] data2 = ImageConversion.EncodeToJPG(screenshotFinal2, 33);
                    bool flag5 = data2.Length < 30000;
                    if (flag5)
                    {
                        channel.longBinaryData = true;
                        channel.openWrite();
                        channel.write(data2);
                        channel.closeWrite("tellScreenshotRelay", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_CHUNK_BUFFER);
                        channel.longBinaryData = false;
                    }
                    break;
                }
            case 3:
                {
                    yield return new WaitForFixedUpdate();
                    yield return new WaitForEndOfFrame();
                    Texture2D screenshotRaw2 = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false)
                    {
                        name = "Screenshot_Raw",
                        hideFlags = HideFlags.HideAndDontSave
                    };
                    screenshotRaw2.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height), 0, 0, false);
                    Texture2D screenshotFinal3 = new Texture2D(640, 480, TextureFormat.RGB24, false)
                    {
                        name = "Screenshot_Final",
                        hideFlags = HideFlags.HideAndDontSave
                    };
                    Color[] oldColors3 = screenshotRaw2.GetPixels();
                    Color[] newColors3 = new Color[screenshotFinal3.width * screenshotFinal3.height];
                    float widthRatio3 = screenshotRaw2.width / (float)screenshotFinal3.width;
                    float heightRatio3 = screenshotRaw2.height / (float)screenshotFinal3.height;
                    int num10;
                    for (int m = 0; m < screenshotFinal3.height; m = num10 + 1)
                    {
                        int num7 = (int)(m * heightRatio3) * screenshotRaw2.width;
                        int num8 = m * screenshotFinal3.width;
                        for (int n = 0; n < screenshotFinal3.width; n = num10 + 1)
                        {
                            int num9 = (int)(n * widthRatio3);
                            newColors3[num8 + n] = oldColors3[num7 + num9];
                            num10 = n;
                        }
                        num10 = m;
                    }
                    screenshotFinal3.SetPixels(newColors3);
                    byte[] data3 = ImageConversion.EncodeToJPG(screenshotFinal3, 33);
                    bool flag6 = data3.Length < 30000;
                    if (flag6)
                    {
                        channel.longBinaryData = true;
                        channel.openWrite();
                        channel.write(data3);
                        channel.closeWrite("tellScreenshotRelay", ESteamCall.SERVER, ESteamPacket.UPDATE_RELIABLE_CHUNK_BUFFER);
                        channel.longBinaryData = false;
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

