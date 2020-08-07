using System.Collections.Generic;



 
public class SkinOptionList
{
 
    public SkinOptionList(SkinType Type)
    {
        this.Type = Type;
    }
     
    public SkinType Type = SkinType.Weapons;
     
    public HashSet<Skin> Skins = new HashSet<Skin>();
}
