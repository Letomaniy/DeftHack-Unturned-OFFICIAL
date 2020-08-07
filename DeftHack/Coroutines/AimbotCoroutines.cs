using SDG.Unturned;
using System.Collections;
using System.Reflection;
using UnityEngine;


 
public static class AimbotCoroutines
{ 
    public static float Pitch
    {
        get => OptimizationVariables.MainPlayer.look.pitch;
        set => AimbotCoroutines.PitchInfo.SetValue(OptimizationVariables.MainPlayer.look, value);
    } 
    public static float Yaw
    {
        get => OptimizationVariables.MainPlayer.look.yaw;
        set => AimbotCoroutines.YawInfo.SetValue(OptimizationVariables.MainPlayer.look, value);
    } 
    [Initializer]
    public static void Init()
    {
        AimbotCoroutines.PitchInfo = typeof(PlayerLook).GetField("_pitch", BindingFlags.Instance | BindingFlags.NonPublic);
        AimbotCoroutines.YawInfo = typeof(PlayerLook).GetField("_yaw", BindingFlags.Instance | BindingFlags.NonPublic);
    }
     
    public static IEnumerator SetLockedObject()
    {
        for (; ; )
        {
            bool flag = !DrawUtilities.ShouldRun() || !AimbotOptions.Enabled;
            if (flag)
            {
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                Player p = null;
                Vector3 aimPos = OptimizationVariables.MainPlayer.look.aim.position;
                Vector3 aimForward = OptimizationVariables.MainPlayer.look.aim.forward;
                SteamPlayer[] players = Provider.clients.ToArray();
                int num;
                for (int i = 0; i < players.Length; i = num + 1)
                {
                    TargetMode targetMode = AimbotOptions.TargetMode;
                    SteamPlayer cPlayer = players[i];
                    bool flag2 = cPlayer == null || cPlayer.player == OptimizationVariables.MainPlayer || cPlayer.player.life == null || cPlayer.player.life.isDead || FriendUtilities.IsFriendly(cPlayer.player);
                    if (!flag2)
                    {
                        if (targetMode != TargetMode.Distance)
                        {
                            if (targetMode == TargetMode.FOV)
                            {
                                bool flag3 = VectorUtilities.GetAngleDelta(aimPos, aimForward, players[i].player.transform.position) < AimbotOptions.FOV;
                                if (flag3)
                                {
                                    bool flag4 = p == null;
                                    if (flag4)
                                    {
                                        p = players[i].player;
                                    }
                                    else
                                    {
                                        bool flag5 = VectorUtilities.GetAngleDelta(aimPos, aimForward, players[i].player.transform.position) < VectorUtilities.GetAngleDelta(aimPos, aimForward, p.transform.position);
                                        if (flag5)
                                        {
                                            p = players[i].player;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {

                            bool flag7 = AimbotOptions.Distance > VectorUtilities.GetDistance(players[i].player.transform.position);
                            if (flag7)
                            {
                                p = players[i].player;
                            }

                        }
                        cPlayer = null;
                    }
                    num = i;
                }
                if (!AimbotCoroutines.IsAiming)
                {
                    AimbotCoroutines.LockedObject = ((p != null) ? p.gameObject : null);
                }
                yield return new WaitForEndOfFrame();
                p = null;
                aimPos = default(Vector3);
                aimForward = default(Vector3);
                players = null;
            }
        }
    }
     
    public static IEnumerator AimToObject()
    {
        for (; ; )
        {
            bool flag = !DrawUtilities.ShouldRun() || !AimbotOptions.Enabled;
            if (flag)
            {
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                bool flag2 = AimbotCoroutines.LockedObject != null && AimbotCoroutines.LockedObject.transform != null && ESPComponent.MainCamera != null;
                if (flag2)
                {
                    bool flag3 = HotkeyUtilities.IsHotkeyHeld("_AimbotKey") || !AimbotOptions.OnKey;
                    if (flag3)
                    {
                        AimbotCoroutines.IsAiming = true;
                        bool smooth = AimbotOptions.Smooth;
                        if (smooth)
                        {
                            AimbotCoroutines.SmoothAim(AimbotCoroutines.LockedObject);
                        }
                        else
                        {
                            AimbotCoroutines.Aim(AimbotCoroutines.LockedObject);
                        }
                    }
                    else
                    {
                        AimbotCoroutines.IsAiming = false;
                    }
                }
                else
                {
                    AimbotCoroutines.IsAiming = false;
                }
                yield return new WaitForEndOfFrame();
            }
        }
    }
     
    public static void Aim(GameObject obj)
    {
        Camera mainCam = OptimizationVariables.MainCam;
        Vector3 aimPosition = AimbotCoroutines.GetAimPosition(obj.transform, "Skull");
        bool flag = aimPosition == AimbotCoroutines.PiVector;
        if (!flag)
        {
            OptimizationVariables.MainPlayer.transform.LookAt(aimPosition);
            OptimizationVariables.MainPlayer.transform.eulerAngles = new Vector3(0f, OptimizationVariables.MainPlayer.transform.rotation.eulerAngles.y, 0f);
            mainCam.transform.LookAt(aimPosition);
            float num = mainCam.transform.localRotation.eulerAngles.x;
            bool flag2 = num <= 90f && num <= 270f;
            if (flag2)
            {
                num = mainCam.transform.localRotation.eulerAngles.x + 90f;
            }
            else
            {
                bool flag3 = num >= 270f && num <= 360f;
                if (flag3)
                {
                    num = mainCam.transform.localRotation.eulerAngles.x - 270f;
                }
            }
            AimbotCoroutines.Pitch = num;
            AimbotCoroutines.Yaw = OptimizationVariables.MainPlayer.transform.rotation.eulerAngles.y;
        }
    }
     
    public static void SmoothAim(GameObject obj)
    {
        Camera mainCam = OptimizationVariables.MainCam;
        Vector3 aimPosition = AimbotCoroutines.GetAimPosition(obj.transform, "Skull");
        bool flag = aimPosition == AimbotCoroutines.PiVector;
        if (!flag)
        {
            OptimizationVariables.MainPlayer.transform.rotation = Quaternion.Slerp(OptimizationVariables.MainPlayer.transform.rotation, Quaternion.LookRotation(aimPosition - OptimizationVariables.MainPlayer.transform.position), Time.deltaTime * AimbotOptions.AimSpeed);
            OptimizationVariables.MainPlayer.transform.eulerAngles = new Vector3(0f, OptimizationVariables.MainPlayer.transform.rotation.eulerAngles.y, 0f);
            mainCam.transform.localRotation = Quaternion.Slerp(mainCam.transform.localRotation, Quaternion.LookRotation(aimPosition - mainCam.transform.position), Time.deltaTime * AimbotOptions.AimSpeed);
            float num = mainCam.transform.localRotation.eulerAngles.x;
            bool flag2 = num <= 90f && num <= 270f;
            if (flag2)
            {
                num = mainCam.transform.localRotation.eulerAngles.x + 90f;
            }
            else
            {
                bool flag3 = num >= 270f && num <= 360f;
                if (flag3)
                {
                    num = mainCam.transform.localRotation.eulerAngles.x - 270f;
                }
            }
            AimbotCoroutines.Pitch = num;
            AimbotCoroutines.Yaw = OptimizationVariables.MainPlayer.transform.rotation.eulerAngles.y;
        }
    }
     
    public static Vector2 CalcAngle(GameObject obj)
    {
        Vector3 vector = ESPComponent.MainCamera.WorldToScreenPoint(AimbotCoroutines.GetAimPosition(obj.transform, "Skull"));
        return Vector2.zero;
    }
     
    public static void AimMouseTo(float x, float y)
    {
        AimbotCoroutines.Yaw = x;
        AimbotCoroutines.Pitch = y;
    }
     
    public static Vector3 GetAimPosition(Transform parent, string name)
    {
        Transform[] componentsInChildren = parent.GetComponentsInChildren<Transform>();
        bool flag = componentsInChildren == null;
        Vector3 piVector;
        if (flag)
        {
            piVector = AimbotCoroutines.PiVector;
        }
        else
        {
            foreach (Transform transform in componentsInChildren)
            {
                bool flag2 = transform.name.Trim() == name;
                if (flag2)
                {
                    return transform.position + new Vector3(0f, 0.4f, 0f);
                }
            }
            piVector = AimbotCoroutines.PiVector;
        }
        return piVector;
    }
     
    public static Vector3 PiVector = new Vector3(0f, 3.14159274f, 0f);
     
    public static GameObject LockedObject;
     
    public static bool IsAiming = false;
     
    public static FieldInfo PitchInfo;
     
    public static FieldInfo YawInfo;
}

