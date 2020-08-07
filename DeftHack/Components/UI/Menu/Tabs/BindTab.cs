
using UnityEngine;


 
public class BindTab
{ 
    public static void Tab()
    {
        Prefab.MenuArea(new Rect(0f, 0f, 466f, 436f), "БИНДЫ", delegate
        {
            GUILayout.Label("Прописываете команды в поля \n для ввода, после идёте в раздел\n КНОПКИ и назначаете клавиши\n на каждую команду.", Prefab._TextStyle, new GUILayoutOption[0]);
            GUILayout.Label("ВНИМАНИЕ!!! Команды писать без '/'- слеша.", Prefab._TextStyle, new GUILayoutOption[0]);
            GUILayout.Space(5f);
            GUILayout.Label("___________________________", Prefab._TextStyle, new GUILayoutOption[0]);
            GUILayout.Label("Команда 1 ", Prefab._TextStyle, new GUILayoutOption[0]);
            BindOptions.Com1 = Prefab.TextField(BindOptions.Com1, "Команда: ", 150);
            GUILayout.Space(5f);
            GUILayout.Label("___________________________", Prefab._TextStyle, new GUILayoutOption[0]);
            GUILayout.Label("Команда 2 ", Prefab._TextStyle, new GUILayoutOption[0]);
            BindOptions.Com2 = Prefab.TextField(BindOptions.Com2, "Команда: ", 150);
            GUILayout.Space(5f);
            GUILayout.Label("___________________________", Prefab._TextStyle, new GUILayoutOption[0]);
            GUILayout.Label("Команда 3 ", Prefab._TextStyle, new GUILayoutOption[0]);
            BindOptions.Com3 = Prefab.TextField(BindOptions.Com3, "Команда: ", 150);
            GUILayout.Space(5f);
            GUILayout.Label("___________________________", Prefab._TextStyle, new GUILayoutOption[0]);
            GUILayout.Label("Команда 4 ", Prefab._TextStyle, new GUILayoutOption[0]);
            BindOptions.Com4 = Prefab.TextField(BindOptions.Com4, "Команда: ", 150);
            GUILayout.Space(5f);
            GUILayout.Label("___________________________", Prefab._TextStyle, new GUILayoutOption[0]);
            GUILayout.Label("Команда 5 ", Prefab._TextStyle, new GUILayoutOption[0]);
            BindOptions.Com5 = Prefab.TextField(BindOptions.Com5, "Команда: ", 150);
        });
    }

}

