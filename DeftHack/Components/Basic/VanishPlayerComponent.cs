using SDG.Unturned;
using UnityEngine;



[Component]
public class VanishPlayerComponent : MonoBehaviour
{

    [OnSpy]
    public static void Disable()
    {
        VanishPlayerComponent.WasEnabled = ESPOptions.ShowVanishPlayers;
        ESPOptions.ShowVanishPlayers = false;
    }


    [OffSpy]
    public static void Enable()
    {
        ESPOptions.ShowVanishPlayers = VanishPlayerComponent.WasEnabled;
    }

    public void OnGUI()
    {
        bool showVanishPlayers = ESPOptions.ShowVanishPlayers;
        if (showVanishPlayers)
        {
            GUI.color = new Color(1f, 1f, 1f, 0f);
            VanishPlayerComponent.vew = GUILayout.Window(350, VanishPlayerComponent.vew, new GUI.WindowFunction(PlayersMenu), "Игроки в вашине", new GUILayoutOption[0]);
            GUI.color = Color.white;
        }
    }

    public void PlayersMenu(int windowID)
    {
        Drawing.DrawRect(new Rect(0f, 0f, VanishPlayerComponent.vew.width, 20f), new Color32(44, 44, 44, byte.MaxValue), null);
        Drawing.DrawRect(new Rect(0f, 20f, VanishPlayerComponent.vew.width, 5f), new Color32(34, 34, 34, byte.MaxValue), null);
        Drawing.DrawRect(new Rect(0f, 25f, VanishPlayerComponent.vew.width, VanishPlayerComponent.vew.height + 25f), new Color32(64, 64, 64, byte.MaxValue), null);
        GUILayout.Space(-19f);
        GUILayout.Label("Vanish Players", new GUILayoutOption[0]);
        foreach (SteamPlayer steamPlayer in Provider.clients)
        {
            bool flag = Vector3.Distance(steamPlayer.player.transform.position, Vector3.zero) < 10f;
            if (flag)
            {
                GUILayout.Label(steamPlayer.playerID.characterName, new GUILayoutOption[0]);
            }
        }
        GUI.DragWindow();
    }

    public static bool WasEnabled;

    public static Rect vew = new Rect(1075f, 10f, 200f, 300f);
}

