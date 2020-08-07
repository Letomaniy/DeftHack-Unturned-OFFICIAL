 
public static class AssetManager
{
  
    public static void Init()
    {
        SosiHui.BinaryOperationBinder.HookObject.GetComponent<CoroutineComponent>().StartCoroutine(LoaderCoroutines.LoadAssets());
    }
}

