using SDG.Unturned;
using Steamworks;
using System;
using System.Linq;
using UnityEngine;

 
public static class PlayersTab
{ 
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

     
    public static void Tab()
    {
        GUILayout.BeginHorizontal(new GUILayoutOption[0]);
        GUILayout.Space(5f);
        PlayersTab.SearchString = Prefab.TextField(PlayersTab.SearchString, "Поиск: ", 466);
        GUILayout.EndHorizontal();
        Prefab.ScrollView(new Rect(0f, 30f, 466f, 215f), "Игроки", ref PlayersTab.PlayersScroll, delegate ()
        {
            for (int i = 0; i < Provider.clients.Count; i++)
            {
                Player player = Provider.clients[i].player;
                bool flag = player == OptimizationVariables.MainPlayer || player == null || (PlayersTab.SearchString != "" && player.name.IndexOf(PlayersTab.SearchString, StringComparison.OrdinalIgnoreCase) == -1);
                if (!flag)
                {
                    bool flag2 = FriendUtilities.IsFriendly(player);
                    bool flag3 = MiscOptions.SpectatedPlayer == player;
                    bool flag4 = false;
                    bool flag5 = player == PlayersTab.SelectedPlayer;
                    string text = flag4 ? "<color=#ff0000ff>" : (flag2 ? "<color=#00ff00ff>" : "");
                    bool flag6 = Prefab.Button(string.Concat(new string[]
                    {
                            flag5 ? "<b>" : "",
                            flag3 ? "<color=#0000ffff>[НАБЛЮДЕНИЕ]</color> " : "",
                            text,
                            player.name,
                            (flag2 || flag4) ? "</color>" : "",
                            flag5 ? "</b>" : ""
                    }), 400f, 25f, new GUILayoutOption[0]);
                    if (flag6)
                    {
                        PlayersTab.SelectedPlayer = player;
                    }
                    GUILayout.Space(2f);
                }
            }
        }, 20, new GUILayoutOption[0]);
        Prefab.MenuArea(new Rect(0f, 260f, 190f, 175f), "ОПЦИИ", delegate
        {
            bool flag = PlayersTab.SelectedPlayer == null;
            if (!flag)
            {
                CSteamID steamID = PlayersTab.SelectedPlayer.channel.owner.playerID.steamID;
                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                GUILayout.BeginVertical(new GUILayoutOption[0]);
                bool flag2 = FriendUtilities.IsFriendly(PlayersTab.SelectedPlayer);
                if (flag2)
                {
                    bool flag3 = Prefab.Button("Убрать из друзей", 150f, 25f, new GUILayoutOption[0]);
                    if (flag3)
                    {
                        FriendUtilities.RemoveFriend(PlayersTab.SelectedPlayer);
                    }
                }
                else
                {
                    bool flag4 = Prefab.Button("Добавить в друзья", 150f, 25f, new GUILayoutOption[0]);
                    if (flag4)
                    {
                        FriendUtilities.AddFriend(PlayersTab.SelectedPlayer);
                    }
                }
                bool flag11 = Prefab.Button("Наблюдаль", 150f, 25f, new GUILayoutOption[0]);
                if (flag11)
                {
                    MiscOptions.SpectatedPlayer = PlayersTab.SelectedPlayer;
                }
                bool flag12 = MiscOptions.SpectatedPlayer != null && MiscOptions.SpectatedPlayer == PlayersTab.SelectedPlayer;
                if (flag12)
                {
                    bool flag13 = Prefab.Button("Не наблюдать", 150f, 25f, new GUILayoutOption[0]);
                    if (flag13)
                    {
                        MiscOptions.SpectatedPlayer = null;
                    }
                }
                bool noMovementVerification = MiscOptions.NoMovementVerification;
                if (noMovementVerification)
                {
                    bool flag14 = Prefab.Button("Телепортироваться", 150f, 25f, new GUILayoutOption[0]);
                    if (flag14)
                    {
                        OptimizationVariables.MainPlayer.transform.position = PlayersTab.SelectedPlayer.transform.position;
                    }
                }
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
            }
        });
        Prefab.MenuArea(new Rect(196f, 260f, 270f, 175f), "Информация", delegate
        {
            bool flag = PlayersTab.SelectedPlayer == null;
            if (!flag)
            {
                int count = Provider.clients.Count((SteamPlayer c) => c.player != PlayersTab.SelectedPlayer && c.player.quests.isMemberOfSameGroupAs(PlayersTab.SelectedPlayer));
                int counnt = Convert.ToInt32(count) + 1;
                string finishcount = Convert.ToString(counnt);

                GUILayout.BeginHorizontal(new GUILayoutOption[0]);
                GUILayout.BeginVertical(new GUILayoutOption[0]);
                GUILayout.Label("SteamID:", new GUILayoutOption[0]);
                GUILayout.TextField(PlayersTab.SelectedPlayer.channel.owner.playerID.steamID.ToString(), Prefab._TextStyle, new GUILayoutOption[0]);
                GUILayout.Space(2f);
                GUILayout.TextField("Локация: " + LocationUtilities.GetClosestLocation(PlayersTab.SelectedPlayer.transform.position).name, Prefab._TextStyle, new GUILayoutOption[0]);
                GUILayout.TextField("Координаты X,Y,Z:\r\n" + SelectedPlayer.transform.position.ToString(), Prefab._TextStyle, new GUILayoutOption[0]);
                GUILayout.Label("Оружие: " + ((PlayersTab.SelectedPlayer.equipment.asset != null) ? PlayersTab.SelectedPlayer.equipment.asset.itemName : "Fists"), Prefab._TextStyle, new GUILayoutOption[0]);
                GUILayout.Label("Транспорт: " + ((PlayersTab.SelectedPlayer.movement.getVehicle() != null) ? PlayersTab.SelectedPlayer.movement.getVehicle().asset.name : "No Vehicle"), Prefab._TextStyle, new GUILayoutOption[0]);
                GUILayout.Label("Кол-во в группе: " + finishcount, Prefab._TextStyle, new GUILayoutOption[0]);
                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
            }
        });
    }
     
    public static Vector2 PlayersScroll; 
    public static Player SelectedPlayer; 
    public static string SearchString = "";
}

