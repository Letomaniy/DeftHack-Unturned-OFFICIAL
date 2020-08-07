using SDG.Unturned;
using System.Threading;




 
public static class SpammerThread
{
     
    [Thread]
    public static void Spammer()
    {
        for (; ; )
        {
            Thread.Sleep(MiscOptions.SpammerDelay);
            if (MiscOptions.SpammerEnabled)
            {
                ChatManager.sendChat(EChatMode.GLOBAL, MiscOptions.SpamText);
            }
        }
    }
}
