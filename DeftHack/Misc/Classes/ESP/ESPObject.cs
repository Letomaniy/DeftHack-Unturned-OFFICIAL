
using UnityEngine;

 
public class ESPObject
{ 
    public ESPObject(ESPTarget t, object o, GameObject go)
    {
        Target = t;
        Object = o;
        GObject = go;
    }
     
    public ESPTarget Target;
     
    public object Object;
     
    public GameObject GObject;
}

