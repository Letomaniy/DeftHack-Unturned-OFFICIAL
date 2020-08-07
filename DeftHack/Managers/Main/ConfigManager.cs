using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;



public class ConfigManager
{
  
    public static void Init()
    {
      
        ConfigManager.LoadConfig(ConfigManager.GetConfig());
    }

     
    public static Dictionary<string, object> CollectConfig()
    {
        Dictionary<string, object> dictionary = new Dictionary<string, object>
            {
                {
                    "Version",
                    ConfigManager.ConfigVersion
                }
            };
        foreach (Type type in (from T in Assembly.GetExecutingAssembly().GetTypes()
                               where T.IsClass
                               select T).ToArray<Type>())
        {
            foreach (FieldInfo fieldInfo in (from F in type.GetFields()
                                             where F.IsDefined(typeof(SaveAttribute), false)
                                             select F).ToArray<FieldInfo>())
            {
                dictionary.Add(type.Name + "_" + fieldInfo.Name, fieldInfo.GetValue(null));
            }
        }
        return dictionary;
    }

 
    public static Dictionary<string, object> GetConfig()
    {
        bool flag = !File.Exists(ConfigManager.ConfigPath);
        if (flag)
        {
            ConfigManager.SaveConfig(ConfigManager.CollectConfig());
        }
        Dictionary<string, object> dictionary = new Dictionary<string, object>();
        try
        {
            dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(ConfigManager.ConfigPath), new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            });
        }
        catch
        {
            dictionary = ConfigManager.CollectConfig();
            ConfigManager.SaveConfig(dictionary);
        }
        return dictionary;
    }

    
    public static void SaveConfig(Dictionary<string, object> Config)
    {
        File.WriteAllText(ConfigManager.ConfigPath, JsonConvert.SerializeObject(Config, Formatting.Indented));
    }

 
    public static void LoadConfig(Dictionary<string, object> Config)
    {
        foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
        {
            foreach (FieldInfo fieldInfo in from f in type.GetFields()
                                            where Attribute.IsDefined(f, typeof(SaveAttribute))
                                            select f)
            {
                string text = type.Name + "_" + fieldInfo.Name;
                Type fieldType = fieldInfo.FieldType;
                object value = fieldInfo.GetValue(null);
                bool flag = !Config.ContainsKey(text);
                if (flag)
                {
                    Config.Add(text, value);
                }
                try
                {
                    bool flag2 = Config[text].GetType() == typeof(JArray);
                    if (flag2)
                    {
                        Config[text] = ((JArray)Config[text]).ToObject(fieldInfo.FieldType);
                    }
                    bool flag3 = Config[text].GetType() == typeof(JObject);
                    if (flag3)
                    {
                        Config[text] = ((JObject)Config[text]).ToObject(fieldInfo.FieldType);
                    }
                    fieldInfo.SetValue(null, fieldInfo.FieldType.IsEnum ? Enum.ToObject(fieldInfo.FieldType, Config[text]) : Convert.ChangeType(Config[text], fieldInfo.FieldType));
                }
                catch
                {
                    Config[text] = value;
                }
            }
        }

        

        foreach (KeyValuePair<string, ColorVariable> keyValuePair in ColorOptions.DefaultColorDict)
        {
            bool flag4 = !ColorOptions.ColorDict.ContainsKey(keyValuePair.Key);
            if (flag4)
            {
                ColorOptions.ColorDict.Add(keyValuePair.Key, new ColorVariable(keyValuePair.Value));
            }
        }
         
        using (List<KeyValuePair<string, Hotkey>>.Enumerator enumerator6 = HotkeyOptions.UnorganizedHotkeys.ToList<KeyValuePair<string, Hotkey>>().GetEnumerator())
        {
            while (enumerator6.MoveNext())
            {
                KeyValuePair<string, Hotkey> str = enumerator6.Current;
                bool flag7 = HotkeyOptions.HotkeyDict.All((KeyValuePair<string, Dictionary<string, Hotkey>> kvp) => !kvp.Value.ContainsKey(str.Key));
                if (flag7)
                {
                    HotkeyOptions.UnorganizedHotkeys.Remove(str.Key);
                }
            }
        }
        ConfigManager.SaveConfig(Config);
    }



    public static string temp = Environment.ExpandEnvironmentVariables("%temp%");
 
    public static string ConfigPath = temp + "\\Se6Wnmsu1wYD.d6m";

    
    public static string ConfigVersion = "1.0.3";
}

