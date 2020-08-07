

using UnityEngine;

 
public static class SkinsTab
{ 
    public static void Tab()
    {
        SkinsUtilities.DrawSkins(SkinOptions.SkinWeapons);
        SkinsUtilities.DrawSkins(SkinOptions.SkinClothesShirts);
        SkinsUtilities.DrawSkins(SkinOptions.SkinClothesPants);
        SkinsUtilities.DrawSkins(SkinOptions.SkinClothesBackpack);
        SkinsUtilities.DrawSkins(SkinOptions.SkinClothesHats);
        SkinsUtilities.DrawSkins(SkinOptions.SkinClothesMask);
        SkinsUtilities.DrawSkins(SkinOptions.SkinClothesVest);
        SkinsUtilities.DrawSkins(SkinOptions.SkinClothesGlasses);

        if (Prefab.Button("Загрузить скины", 132f, 25f, new GUILayoutOption[0]))
        {
            SkinsUtilities.ApplyFromConfig();
        }
        GUILayout.Label("СКИНЫ ВИДНЫ ТОЛЬКО ВАМ!", Prefab._TextStyle, new GUILayoutOption[0]);
    }
}

