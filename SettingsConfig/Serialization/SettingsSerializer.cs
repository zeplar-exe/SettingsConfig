using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SettingsConfig.Settings;

namespace SettingsConfig.Serialization;

public class SettingsSerializer
{
    private uint Depth { get; set; }
    private uint MaxDepth { get; }
    private BindingFlags Flags { get; }
    
    public SettingsSerializer(uint maxDepth, BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
    {
        MaxDepth = maxDepth;
        Flags = flags;
    }
    
    public SettingsDocument Serialize<TType>(TType o)
    {
        var document = SettingsDocument.CreateEmpty();

        foreach (var property in o.GetType().GetProperties(Flags))
        {
            if (property.DeclaringType == null)
                continue;
            
            if (property.GetCustomAttribute<Ignore>() != null)
                continue;
            
            var key = property.Name;
            var nameAttribute = property.GetCustomAttribute<SerializationName>();

            if (nameAttribute != null)
                key = nameAttribute.Name;

            var value = property.GetValue(o);

            document.AddSetting(CreateSetting(key, value));
        }

        return document;
    }

    public SettingTree SerializeTree<TType>(TType o)
    {
        return SerializeTreeInternal(o);
    }

    private SettingTree SerializeTreeInternal<TType>(TType o)
    {
        if (Depth++ > MaxDepth)
            return new SettingTree(Array.Empty<Setting>());
        
        var settings = new List<Setting>();

        foreach (var property in o.GetType().GetProperties(Flags))
        {
            if (property.DeclaringType == null)
                continue;
            
            if (property.GetCustomAttribute<Ignore>() != null)
                continue;

            var key = property.Name;
            var nameAttribute = property.GetCustomAttribute<SerializationName>();

            if (nameAttribute != null)
                key = nameAttribute.Name;

            var value = property.GetValue(o);
            
            settings.Add(CreateSetting(key, value));
        }

        return new SettingTree(settings);
    }

    private Setting CreateSetting(string key, object value)
    {
        switch (value)
        {
            case int iValue:
                return Setting.Create(key, iValue);
            case float fValue:
                return Setting.Create(key, fValue);
            case double dValue:
                return Setting.Create(key, dValue);
            case string sValue:
                return Setting.Create(key, sValue);
            case bool bValue:
                return Setting.Create(key, bValue);
            case IDictionary<string, SettingValue> dictionary:
                return new Setting(key, new SettingTree(
                    dictionary.Select(p => new Setting(p.Key, p.Value))));
            default:
                return new Setting(key, SerializeTree(value));
        }
    }
    
    [AttributeUsage(
        AttributeTargets.Property | 
        AttributeTargets.Class | 
        AttributeTargets.Struct |
        AttributeTargets.Enum)]
    public class Ignore : Attribute
    {
            
    }

    [AttributeUsage(
        AttributeTargets.Property | 
        AttributeTargets.Class | 
        AttributeTargets.Struct |
        AttributeTargets.Enum)]
    public class SerializationName : Attribute
    {
        public string Name { get; }
            
        public SerializationName(string name)
        {
            Name = name;
        }
    }
}