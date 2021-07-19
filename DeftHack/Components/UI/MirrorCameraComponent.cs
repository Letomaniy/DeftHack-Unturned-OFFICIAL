using UnityEngine; 

[Component]
public class MirrorCameraComponent : MonoBehaviour
{

    [OnSpy]
    public static void Disable()
    {
        MirrorCameraComponent.WasEnabled = MirrorCameraOptions.Enabled;
        MirrorCameraOptions.Enabled = false;
        UnityEngine.Object.Destroy(MirrorCameraComponent.cam_obj);
    }

    [OffSpy]
    public static void Enable()
    {
        MirrorCameraOptions.Enabled = MirrorCameraComponent.WasEnabled;
    }

    public void Update()
    {
        bool flag = !MirrorCameraComponent.cam_obj || !MirrorCameraComponent.subCam;
        if (!flag)
        {
            bool enabled = MirrorCameraOptions.Enabled;
            if (enabled)
            {
                MirrorCameraComponent.subCam.enabled = true;
            }
            else
            {
                MirrorCameraComponent.subCam.enabled = false;
            }
        }
    }

    public void OnGUI()
    {
        bool enabled = MirrorCameraOptions.Enabled;
        if (enabled)
        {
            GUI.color = new Color(1f, 1f, 1f, 0f);
            MirrorCameraComponent.viewport = GUILayout.Window(99, MirrorCameraComponent.viewport, new GUI.WindowFunction(DoMenu), "Mirror Camera", new GUILayoutOption[0]);
            GUI.color = Color.white;
        }
    }

    public void DoMenu(int windowID)
    {
        bool flag = MirrorCameraComponent.cam_obj == null || MirrorCameraComponent.subCam == null;
        if (flag)
        {
            MirrorCameraComponent.cam_obj = new GameObject();
            bool flag2 = MirrorCameraComponent.subCam != null;
            if (flag2)
            {
                UnityEngine.Object.Destroy(MirrorCameraComponent.subCam);
            } 
            MirrorCameraComponent.subCam = MirrorCameraComponent.cam_obj.AddComponent<Camera>();
            MirrorCameraComponent.subCam.CopyFrom(OptimizationVariables.MainCam); 
            //MirrorCameraComponent.cam_obj.AddComponent<>();
            MirrorCameraComponent.cam_obj.transform.position = OptimizationVariables.MainCam.gameObject.transform.position;
            MirrorCameraComponent.cam_obj.transform.rotation = OptimizationVariables.MainCam.gameObject.transform.rotation;
            MirrorCameraComponent.cam_obj.transform.Rotate(0f, 180f, 0f);
            MirrorCameraComponent.subCam.transform.SetParent(OptimizationVariables.MainCam.transform, true);
            MirrorCameraComponent.subCam.enabled = true;
            MirrorCameraComponent.subCam.rect = new Rect(0.6f, 0.6f, 0.4f, 0.4f);
            MirrorCameraComponent.subCam.depth = 99f;
            UnityEngine.Object.DontDestroyOnLoad(MirrorCameraComponent.cam_obj);
        }
        float num = MirrorCameraComponent.viewport.x / Screen.width;
        float num2 = (MirrorCameraComponent.viewport.y + 25f) / Screen.height;
        float num3 = MirrorCameraComponent.viewport.width / Screen.width;
        float num4 = MirrorCameraComponent.viewport.height / Screen.height;
        num2 = 1f - num2;
        num2 -= num4;
        MirrorCameraComponent.subCam.rect = new Rect(num, num2, num3, num4);
        Drawing.DrawRect(new Rect(0f, 0f, MirrorCameraComponent.viewport.width, 20f), new Color32(44, 44, 44, byte.MaxValue), null);
        Drawing.DrawRect(new Rect(0f, 20f, MirrorCameraComponent.viewport.width, 5f), new Color32(34, 34, 34, byte.MaxValue), null);
        GUILayout.Space(-19f);
        GUILayout.Label("Mirror Camera", new GUILayoutOption[0]);
        GUI.DragWindow();
    }
    public static void FixCam()
    {
        bool flag = MirrorCameraComponent.cam_obj != null && MirrorCameraComponent.subCam != null;
        if (flag)
        {
            MirrorCameraComponent.cam_obj.transform.position = OptimizationVariables.MainCam.gameObject.transform.position;
            MirrorCameraComponent.cam_obj.transform.rotation = OptimizationVariables.MainCam.gameObject.transform.rotation;
            MirrorCameraComponent.cam_obj.transform.Rotate(0f, 180f, 0f);
            MirrorCameraComponent.subCam.transform.SetParent(OptimizationVariables.MainCam.transform, true);
            MirrorCameraComponent.subCam.depth = 99f;
            MirrorCameraComponent.subCam.enabled = true;
        }
    }

    public static Rect viewport = new Rect(1075f, 10f, Screen.width / 4, Screen.height / 4);

    public static GameObject cam_obj;

    public static Camera subCam;

    public static bool WasEnabled;
}

