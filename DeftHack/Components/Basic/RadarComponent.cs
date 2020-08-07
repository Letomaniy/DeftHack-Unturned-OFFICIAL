using SDG.Unturned;
using UnityEngine;


[Component]
public class RadarComponent : MonoBehaviour
{

    [OnSpy]
    public static void Disable()
    {
        RadarComponent.WasEnabled = RadarOptions.Enabled;
        RadarOptions.Enabled = false;
    }

    [OffSpy]
    public static void Enable()
    {
        RadarOptions.Enabled = RadarComponent.WasEnabled;
    }

    public void OnGUI()
    {
        bool flag = RadarOptions.Enabled && Provider.isConnected && !Provider.isLoading;
        if (flag)
        {
            RadarComponent.vew.width = (RadarComponent.vew.height = RadarOptions.RadarSize + 10f);
            GUI.color = new Color(1f, 1f, 1f, 0f);
            RadarComponent.veww = GUILayout.Window(345, RadarComponent.vew, new GUI.WindowFunction(RadarMenu), "Radar", new GUILayoutOption[0]);
            RadarComponent.vew.x = RadarComponent.veww.x;
            RadarComponent.vew.y = RadarComponent.veww.y;
            GUI.color = Color.white;
        }
    }

    public void RadarMenu(int windowID)
    {
        Drawing.DrawRect(new Rect(0f, 0f, RadarComponent.vew.width, 20f), new Color32(44, 44, 44, byte.MaxValue), null);
        Drawing.DrawRect(new Rect(0f, 20f, RadarComponent.vew.width, 5f), new Color32(34, 34, 34, byte.MaxValue), null);
        Drawing.DrawRect(new Rect(0f, 25f, RadarComponent.vew.width, RadarComponent.vew.height + 25f), new Color32(64, 64, 64, byte.MaxValue), null);
        GUILayout.Space(-19f);
        GUILayout.Label("Radar", new GUILayoutOption[0]);
        Vector2 vector;
        vector = new Vector2(RadarComponent.vew.width / 2f, (RadarComponent.vew.height + 25f) / 2f);
        RadarComponent.radarcenter = new Vector2(RadarComponent.vew.width / 2f, (RadarComponent.vew.height + 25f) / 2f);
        Vector2 vector2 = RadarComponent.GameToRadarPosition(Player.player.transform.position);
        bool trackPlayer = RadarOptions.TrackPlayer;
        if (trackPlayer)
        {
            RadarComponent.radarcenter.x = RadarComponent.radarcenter.x - vector2.x;
            RadarComponent.radarcenter.y = RadarComponent.radarcenter.y + vector2.y;
        }
        Drawing.DrawRect(new Rect(vector.x, 25f, 1f, RadarComponent.vew.height), Color.gray, null);
        Drawing.DrawRect(new Rect(0f, vector.y, RadarComponent.vew.width, 1f), Color.gray, null);
        DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector2.x, RadarComponent.radarcenter.y - vector2.y), Color.black, 4f);
        DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector2.x, RadarComponent.radarcenter.y - vector2.y), Color.white, 3f);
        bool showVehicles = RadarOptions.ShowVehicles;
        if (showVehicles)
        {
            foreach (InteractableVehicle interactableVehicle in VehicleManager.vehicles)
            {
                bool showVehiclesUnlocked = RadarOptions.ShowVehiclesUnlocked;
                if (showVehiclesUnlocked)
                {
                    bool flag = !interactableVehicle.isLocked;
                    if (flag)
                    {
                        Vector2 vector3 = RadarComponent.GameToRadarPosition(interactableVehicle.transform.position);
                        DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector3.x, RadarComponent.radarcenter.y - vector3.y), Color.black, 3f);
                        DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector3.x, RadarComponent.radarcenter.y - vector3.y), ColorUtilities.getColor("_Vehicles"), 2f);
                    }
                }
                else
                {
                    Vector2 vector4 = RadarComponent.GameToRadarPosition(interactableVehicle.transform.position);
                    DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector4.x, RadarComponent.radarcenter.y - vector4.y), Color.black, 3f);
                    DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector4.x, RadarComponent.radarcenter.y - vector4.y), ColorUtilities.getColor("_Vehicles"), 2f);
                }
            }
        }
        bool showPlayers = RadarOptions.ShowPlayers;
        if (showPlayers)
        {
            foreach (SteamPlayer steamPlayer in Provider.clients)
            {
                bool flag2 = steamPlayer.player != OptimizationVariables.MainPlayer;
                if (flag2)
                {
                    Vector2 vector5 = RadarComponent.GameToRadarPosition(steamPlayer.player.transform.position);
                    DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector5.x, RadarComponent.radarcenter.y - vector5.y), Color.black, 3f);
                    DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector5.x, RadarComponent.radarcenter.y - vector5.y), ColorUtilities.getColor("_Players"), 2f);
                }
            }
        }
        bool flag3 = MiscComponent.LastDeath != new Vector3(0f, 0f, 0f);
        if (flag3)
        {
            Vector2 vector6 = RadarComponent.GameToRadarPosition(MiscComponent.LastDeath);
            DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector6.x, RadarComponent.radarcenter.y - vector6.y), Color.black, 4f);
            DrawRadarDot(new Vector2(RadarComponent.radarcenter.x + vector6.x, RadarComponent.radarcenter.y - vector6.y), Color.grey, 3f);
        }
        GUI.DragWindow();
    }

    public void DrawRadarDot(Vector2 pos, Color color, float size = 2f)
    {
        Drawing.DrawRect(new Rect(pos.x - size, pos.y - size, size * 2f, size * 2f), color, null);
    }

    public static Vector2 GameToRadarPosition(Vector3 pos)
    {
        Vector2 result;
        result.x = pos.x / (Level.size / (RadarOptions.RadarZoom * RadarOptions.RadarSize));
        result.y = pos.z / (Level.size / (RadarOptions.RadarZoom * RadarOptions.RadarSize));
        return result;
    }

    public static Rect veww;

    public static Rect vew = new Rect(Screen.width - RadarOptions.RadarSize - 20f, 10f, RadarOptions.RadarSize + 10f, RadarOptions.RadarSize + 10f);

    public static Vector2 radarcenter;

    public static bool WasEnabled;
}

