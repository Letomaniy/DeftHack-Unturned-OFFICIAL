using System;

 
public class SectionTab
{
   
    public SectionTab(string name, Action code)
    {
        this.name = name;
        this.code = code;
    }
     
    public static SectionTab CurrentSectionTab;
     
    public Action code;
     
    public string name;
}
