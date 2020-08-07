using SDG.Unturned;
using System;
using UnityEngine;


 
[SpyComponent]
[Component]
public class MenuComponent : MonoBehaviour
{
   

    [Initializer]
    public static void Initialize()
    {

        ColorUtilities.addColor(new ColorVariable("_OutlineBorderBlack", "Меню - черный контур", new Color32(0, 0, 0, byte.MaxValue), true));
        ColorUtilities.addColor(new ColorVariable("_OutlineBorderLightGray", "Меню - контур 1", new Color32(65, 65, 65, byte.MaxValue), true));
        ColorUtilities.addColor(new ColorVariable("_OutlineBorderDarkGray", "Меню -  контур 2", new Color32(65, 65, 65, byte.MaxValue), true));
        ColorUtilities.addColor(new ColorVariable("_FillLightBlack", "Меню - Фон", new Color32(65, 65, 65, byte.MaxValue), true));
        ColorUtilities.addColor(new ColorVariable("_Accent1", "Меню - Акцент 1", new Color32(0, 0, 0, byte.MaxValue), true));
        ColorUtilities.addColor(new ColorVariable("_Accent2", "Меню - Акцент 2", new Color32(255, 95, 0, byte.MaxValue), true));
    }

   
    public void Start()
    {
        MenuTabs.AddTabs();
    }

 
    public void Update()
    {
        HotkeyUtilities.Initialize();
        if (!HotkeyOptions.UnorganizedHotkeys.ContainsKey("_Menu"))
        {
            HotkeyUtilities.AddHotkey("Прочее", "Активация меню", "_MenuComponent", new KeyCode[]
            {
                    KeyCode.F1
            });
        }
        if ((HotkeyOptions.UnorganizedHotkeys["_MenuComponent"].Keys.Length == 0 && Input.GetKeyDown(MenuComponent.MenuKey)) || HotkeyUtilities.IsHotkeyDown("_MenuComponent"))
        {
            MenuComponent.IsInMenu = !MenuComponent.IsInMenu;
            if (MenuComponent.IsInMenu)
            {
                SectionTab.CurrentSectionTab = null;
            }
        }

    }

 
    public void OnGUI()
    {
        Prefab.CheckStyles();
        if (MenuComponent.IsInMenu)
        {
            bool flag2 = _cursorTexture == null;
            if (flag2)
            {
                _cursorTexture = (Resources.Load("UI/Cursor") as Texture);
            }
            GUI.depth = -1;
            MenuComponent.MenuRect = GUI.Window(0, MenuComponent.MenuRect, new GUI.WindowFunction(DoMenu), "DeftHack");
            GUI.depth = -2;
            _cursor.x = Input.mousePosition.x;
            _cursor.y = Screen.height - Input.mousePosition.y;
            GUI.DrawTexture(_cursor, _cursorTexture);
            Cursor.lockState = 0;
            bool flag3 = PlayerUI.window != null;
            if (flag3)
            {
                PlayerUI.window.showCursor = true;
            }
            MenuComponent.SetGUIColors();
        }

    }

 
    public static void DoMenu(int id)
    {
         
            if (SectionTab.CurrentSectionTab == null)
            {
                DoBorder();
                DoTabs();
                DrawTabs();
                DoConfigButtons();
            }
            else
            {
                DoSectionTab();
            }
            GUI.DragWindow(new Rect(0f, 0f, MenuComponent.MenuRect.width, 25f));

      
    }
 
    public static void DoBorder()
    {
        Rect rect = new Rect(0f, 0f, MenuComponent.MenuRect.width, MenuComponent.MenuRect.height);
        Rect rect2 = MenuUtilities.Inline(rect, 1);
        Rect rect3 = MenuUtilities.Inline(rect2, 1);
        Rect rect4 = MenuUtilities.Inline(rect3, 3);
        Rect position = MenuUtilities.Inline(rect4, 1);
        Rect position2 = new Rect(position.x + 2f, position.y + 2f, position.width - 4f, 2f);
        Rect position3 = new Rect(position.x + 2f, position.y + 4f, position.width - 4f, 2f);
        Drawing.DrawRect(rect, MenuComponent._OutlineBorderBlack, null);
        Drawing.DrawRect(rect2, MenuComponent._OutlineBorderLightGray, null);
        Drawing.DrawRect(rect3, MenuComponent._OutlineBorderDarkGray, null);
        Drawing.DrawRect(rect4, MenuComponent._OutlineBorderLightGray, null);
        Drawing.DrawRect(position, MenuComponent._FillLightBlack, null);
        Drawing.DrawRect(position2, MenuComponent._Accent1, null);
        Drawing.DrawRect(position3, MenuComponent._Accent2, null);
    }
 
    public static void DoTabs()
    {


        GUILayout.BeginArea(new Rect(15f, 25f, 130f, 325f));
        GUILayout.BeginVertical(new GUILayoutOption[0]);
        for (int i = 0; i < MenuTabOption.tabs[_pIndex].Count; i++)
        {
            bool flag = Prefab.MenuTab(MenuTabOption.tabs[_pIndex][i].name, ref MenuTabOption.tabs[_pIndex][i].enabled, 29);
            if (flag)
            {
                MenuTabOption.CurrentTab = (MenuTabOption.tabs[_pIndex][i].enabled ? MenuTabOption.tabs[_pIndex][i] : null);
            }
            GUILayout.Space(-11f);
            bool flag2 = MenuTabOption.tabs[_pIndex][i] != MenuTabOption.CurrentTab;
            if (flag2)
            {
                MenuTabOption.tabs[_pIndex][i].enabled = false;
            }
        }
        GUILayout.Space(20f);
        GUILayout.EndVertical();
        bool flag3 = false;
        bool flag4 = Prefab.MenuTabAbsolute(new Vector2(0f, 292f), "пред", ref flag3, 19) && _pIndex > 0;
        if (flag4)
        {
            _pIndex--;
        }
        bool flag5 = Prefab.MenuTabAbsolute(new Vector2(76f, 292f), "след", ref flag3, 19) && _pIndex < MenuTabOption.tabs.Length - 1;
        if (flag5)
        {
            _pIndex++;
        }
        GUILayout.EndArea();
    }

 
    public static void DrawTabs()
    { 
            GUILayout.BeginArea(new Rect(160f, 25f, 466f, 436f));
            bool flag = MenuTabOption.CurrentTab != null;
            if (flag)
            {
                MenuTabOption.CurrentTab.tab();
            }
            GUILayout.EndArea();
      
       
    }
 
    public static void DoSectionTab()
    {
        bool flag = SectionTab.CurrentSectionTab != null;
        if (flag)
        {
            DoBorder();
            Prefab.MenuArea(new Rect(10f, 20f, MenuComponent.MenuRect.width - 20f, MenuComponent.MenuRect.height - 20f - 10f), SectionTab.CurrentSectionTab.name.ToUpper(), SectionTab.CurrentSectionTab.code);
            bool flag2 = false;
            bool flag3 = Prefab.MenuTabAbsolute(new Vector2(17f, 428f), "Назад", ref flag2, 19);
            if (flag3)
            {
                SectionTab.CurrentSectionTab = null;
            }

        }
    }
    public static string appdata = Environment.ExpandEnvironmentVariables("%appdata%");
 
    public static void DoConfigButtons()
    {
        Prefab.MenuArea(new Rect(18f, 370f, 125f, 91f), "КОНФИГ", delegate
        {

            GUILayout.Space(5f);
            bool flag = Prefab.Button("Сохранить", 90f, 25f, new GUILayoutOption[0]);
            if (flag)
            {
                ConfigManager.SaveConfig(ConfigManager.CollectConfig());
            }
            GUILayout.Space(5f);
            bool flag2 = Prefab.Button("Загрузить", 90f, 25f, new GUILayoutOption[0]);
            if (flag2)
            {
                ConfigManager.Init();
                MenuComponent.SetGUIColors(); 
            }
        });
    }

 
    public static void LogoTab()
    {
        Prefab.MenuArea(new Rect(0f, 0f, 466f, 436f), string.Format(""), delegate
        {
            
        });
    }

  
    public static void UpdateColors()
    {
        MenuComponent._OutlineBorderBlack = ColorUtilities.getColor("_OutlineBorderBlack");
        MenuComponent._OutlineBorderLightGray = ColorUtilities.getColor("_OutlineBorderLightGray");
        MenuComponent._OutlineBorderDarkGray = ColorUtilities.getColor("_OutlineBorderDarkGray");
        MenuComponent._FillLightBlack = ColorUtilities.getColor("_FillLightBlack");
        MenuComponent._Accent1 = ColorUtilities.getColor("_Accent1");
        MenuComponent._Accent2 = ColorUtilities.getColor("_Accent2");
    }

 
    public static void SetGUIColors()
    {
        MenuComponent.UpdateColors();
        Prefab.UpdateColors();
    }

 
    public static Font _TabFont;


 
    public static Font _TextFont;

  
    public static Texture2D _LogoTexLarge;

 
    public static bool IsInMenu;
 
    public static KeyCode MenuKey = KeyCode.F1;

  
    public static Rect MenuRect = new Rect(29f, 29f, 640f, 480f);

 
    public static Color32 _OutlineBorderBlack;

 
    public static Color32 _OutlineBorderLightGray;


 
    public static Color32 _OutlineBorderDarkGray;

 
    public static Color32 _FillLightBlack;
 
    public static Color32 _Accent1;

 
    public static Color32 _Accent2;

 
    public Rect _cursor = new Rect(0f, 0f, 20f, 20f);
 
    public Texture _cursorTexture;

 
    public static int _pIndex = 0;
}

