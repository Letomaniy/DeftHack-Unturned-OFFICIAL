using SDG.Unturned;
using UnityEngine;

 
public class InfoTab
{ 
    public static void Tab()
    {
        Prefab.MenuArea(new Rect(0f, 0f, 466f, 436f), "ИНФО", delegate
        {
            if (Provider.isConnected)
            {
                GUILayout.Label("Ваш SteamID64: ", Prefab._TextStyle, new GUILayoutOption[0]);
                GUILayout.Space(2f);
                GUILayout.TextField(string.Format("{0}", Provider.user), Prefab._TextStyle, new GUILayoutOption[0]);
                GUILayout.Space(8f);
                GUILayout.Label("Данные о сервере: ", Prefab._TextStyle, new GUILayoutOption[0]);
                GUILayout.Space(2f);
                GUILayout.TextField(string.Format("{0}:{1}", Parser.getIPFromUInt32(Provider.currentServerInfo.ip), Provider.currentServerInfo.port), Prefab._TextStyle, new GUILayoutOption[0]);
                GUILayout.Space(4f);
            }
            GUILayout.Label("Пожелания и жалобы по читу сюда:", Prefab._TextStyle, new GUILayoutOption[0]);
            GUILayout.TextField("http://vk.me/beyondcheat", Prefab._TextStyle, new GUILayoutOption[0]);
            GUILayout.Space(2f);
            if (Prefab.Button("Сайт", 200f, 25f, new GUILayoutOption[0]))
            {
                Application.OpenURL("http://vk.com/beyondcheat");
            }
        });
    }
}

