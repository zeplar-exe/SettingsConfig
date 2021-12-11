using System;
using System.Linq;
using System.Reflection;
using SettingsConfig.Settings;

namespace SettingsConfig.Serialization
{
    public static class SettingsDeserializer
    {
        public static TType Deserialize<TType>(SettingsDocument document) where TType : new()
        {
            var o = new TType();

            foreach (var setting in document.Settings)
            {
                var property = typeof(TType).GetProperty(setting.Key, 
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Instance |
                    BindingFlags.IgnoreCase);
                
                if (property?.CanWrite != true)
                    continue;
                
                if (property.GetCustomAttribute<Ignore>() != null)
                    continue;

                if (setting.Value is TextSetting textSetting)
                {
                    if (property.PropertyType == typeof(string))
                    {
                        property.SetValue(o, textSetting.Value);
                    }
                    else if (property.PropertyType == typeof(Enum))
                    {
                        var name = Enum.GetNames(property.PropertyType).FirstOrDefault(n => n == textSetting.Value);
                        
                        if (name == null)
                            continue;
                        
                        property.SetValue(o, Enum.Parse(property.PropertyType, name));
                    }
                }
                else if (setting.Value is NumericSetting numericSetting)
                {
                    if (property.PropertyType == typeof(int))
                    {
                        property.SetValue(o, (int)numericSetting.Value);
                    }
                    else if (property.PropertyType == typeof(float))
                    {
                        property.SetValue(o, (float)numericSetting.Value);
                    }
                    else if (property.PropertyType == typeof(double))
                    {
                        property.SetValue(o, numericSetting.Value);
                    }
                }
                else if (setting.Value is BooleanSetting booleanSetting)
                {
                    if (property.PropertyType == typeof(bool))
                        property.SetValue(o, booleanSetting.Value);
                }
                else if (setting.Value is SettingTree tree)
                {
                    var t = property.PropertyType;
                    var deserialized = DeserializeTree(tree, t);

                    if (property.PropertyType == deserialized.GetType())
                    {
                        property.SetValue(o, deserialized);
                    }
                }
            }

            return o;
        }

        public static TType DeserializeTree<TType>(SettingTree tree)
        {
            return (TType)DeserializeTree(tree, typeof(TType));
        }

        private static object DeserializeTree(SettingTree tree, Type type)
        {
            var o = Activator.CreateInstance(type);
            
            foreach (var setting in tree.Settings)
            {
                var property = type.GetProperty(setting.Key, 
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.Instance |
                    BindingFlags.IgnoreCase);
                
                if (property?.CanWrite != true)
                    continue;
                
                if (property.GetCustomAttribute<Ignore>() != null)
                    continue;
                
                if (setting.Value is TextSetting textSetting)
                {
                    if (property.PropertyType == typeof(string))
                    {
                        property.SetValue(o, textSetting.Value);
                    }
                    else if (property.PropertyType == typeof(Enum))
                    {
                        var name = Enum.GetNames(property.PropertyType).FirstOrDefault(n => n == textSetting.Value);
                        
                        if (name == null)
                            continue;
                        
                        property.SetValue(o, Enum.Parse(property.PropertyType, name));
                    }
                }
                else if (setting.Value is NumericSetting numericSetting)
                {
                    if (property.PropertyType == typeof(int))
                    {
                        property.SetValue(o, (int)numericSetting.Value);
                    }
                    else if (property.PropertyType == typeof(float))
                    {
                        property.SetValue(o, (float)numericSetting.Value);
                    }
                    else if (property.PropertyType == typeof(double))
                    {
                        property.SetValue(o, numericSetting.Value);
                    }
                }
                else if (setting.Value is BooleanSetting booleanSetting)
                {
                    if (property.PropertyType == typeof(bool))
                        property.SetValue(o, booleanSetting.Value);
                }
                else if (setting.Value is SettingTree settingTree)
                {
                    var t = property.PropertyType;
                    var deserialized = DeserializeTree(settingTree, t);

                    if (property.PropertyType == deserialized.GetType())
                    {
                        property.SetValue(o, deserialized);
                    }
                }
            }

            return o;
        }
        
        [AttributeUsage(
            AttributeTargets.Property | 
            AttributeTargets.Class | 
            AttributeTargets.Struct | 
            AttributeTargets.Interface)]
        public class Ignore : Attribute
        {
            
        }
    }
}