using SDG.Unturned;



 
public static class FriendUtilities
{
    
    public static bool IsFriendly(Player player)
    {
        return (player.quests.isMemberOfSameGroupAs(OptimizationVariables.MainPlayer) && ESPOptions.UsePlayerGroup) || MiscOptions.Friends.Contains(player.channel.owner.playerID.steamID.m_SteamID);
    }

    
    public static void AddFriend(Player Friend)
    {
        ulong steamID = Friend.channel.owner.playerID.steamID.m_SteamID;
        bool flag = !MiscOptions.Friends.Contains(steamID);
        if (flag)
        {
            MiscOptions.Friends.Add(steamID);
        }
    }

 
    public static void RemoveFriend(Player Friend)
    {
        ulong steamID = Friend.channel.owner.playerID.steamID.m_SteamID;
        bool flag = MiscOptions.Friends.Contains(steamID);
        if (flag)
        {
            MiscOptions.Friends.Remove(steamID);
        }
    }
}

