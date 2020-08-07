using System.Collections.Generic;
using UnityEngine;


 
public class DropDown
{
   
    public DropDown()
    {
        IsEnabled = false;
        ListIndex = 0;
        ScrollView = Vector2.zero;
    }
     
    public static DropDown Get(string identifier)
    {
        bool flag = DropDown.DropDownManager.TryGetValue(identifier, out DropDown dropDown);
        DropDown result;
        if (flag)
        {
            result = dropDown;
        }
        else
        {
            dropDown = new DropDown();
            DropDown.DropDownManager.Add(identifier, dropDown);
            result = dropDown;
        }
        return result;
    }
     
    public static Dictionary<string, DropDown> DropDownManager = new Dictionary<string, DropDown>();
     
    public bool IsEnabled;
     
    public int ListIndex;
     
    public Vector2 ScrollView;
}

