using SDG.Unturned;
using Steamworks;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

 
public class OV_PlayerInput
{
    public static List<PlayerInputPacket> ClientsidePackets
    {
        get
        {
            if (!DrawUtilities.ShouldRun() || !OV_PlayerInput.Run)
            {
                return null;
            }
            return (List<PlayerInputPacket>)ClientsidePacketsField.GetValue(OptimizationVariables.MainPlayer.input);
        }
    }
     
    public static void OV_askAck(PlayerInput instance, CSteamID steamId, int ack)
    {
        bool flag = steamId != Provider.server;
        if (!flag)
        {
            for (int i = OV_PlayerInput.Packets.Count - 1; i >= 0; i--)
            {
                bool flag2 = OV_PlayerInput.Packets[i].sequence <= ack;
                if (flag2)
                {
                    OV_PlayerInput.Packets.RemoveAt(i);
                }
            }
        }
    }
    public static FieldInfo ClientsidePacketsField = typeof(PlayerInput).GetField("clientsidePackets", BindingFlags.Instance | BindingFlags.NonPublic);
 
    public static void OV_FixedUpdate()
    {
        Player mainPlayer = OptimizationVariables.MainPlayer;

        bool punchSilentAim = MiscOptions.PunchSilentAim;
        if (punchSilentAim)
        {
            OV_DamageTool.OVType = OverrideType.PlayerHit;
        }
        RaycastInfo raycastInfo = DamageTool.raycast(new Ray(mainPlayer.look.aim.position, mainPlayer.look.aim.forward), 6f, RayMasks.DAMAGE_SERVER);
        OverrideUtilities.CallOriginal(null, new object[0]);
        List<PlayerInputPacket> clientsidePackets = ClientsidePackets;
        LastPacket = ((clientsidePackets != null) ? clientsidePackets.Last<PlayerInputPacket>() : null);
    }

 
    [Override(typeof(PlayerInput), "InitializePlayer", BindingFlags.Instance | BindingFlags.NonPublic, 0)]
    public static void OV_InitializePlayer(PlayerInput instance)
    {
        bool flag = instance.player != Player.player;
        if (flag)
        {
            OverrideUtilities.CallOriginal(instance, new object[0]);
        }
        else
        {
            OptimizationVariables.MainPlayer = Player.player;
            OV_PlayerInput.Rate = 4;
            OV_PlayerInput.Count = 0;
            OV_PlayerInput.Buffer = 0;
            OV_PlayerInput.Packets.Clear();
            OV_PlayerInput.LastPacket = null;
            OV_PlayerInput.SequenceDiff = 0;
            OV_PlayerInput.ClientSequence = 0;
            OverrideUtilities.CallOriginal(instance, new object[0]);
        }
    }
     
    public static PlayerInputPacket LastPacket;
     
    public static float Yaw;
     
    public static float Pitch;
     
    public static int Count;
     
    public static int Buffer;
 
    public static int Choked;
 
    public static uint Clock = 1u;
     
    public static int Rate;
     
    public static int ClientSequence = 1;
     
    public static int SequenceDiff;
     
    public static List<PlayerInputPacket> Packets = new List<PlayerInputPacket>();
     
    public static Queue<PlayerInputPacket> WaitingPackets = new Queue<PlayerInputPacket>();
     
    public static float LastReal;
     
    public static bool Run;
     
    public static FieldInfo SimField = typeof(PlayerInput).GetField("_simulation", BindingFlags.Instance | BindingFlags.NonPublic);

 
    public static Vector3 lastSentPositon = Vector3.zero;
}

