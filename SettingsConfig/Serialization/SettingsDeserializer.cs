using System;
using System.Collections.Generic;
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

            DeserializeTo(document, o);

            return o;
        }
        
        public static void DeserializeTo<TType>(SettingsDocument document, TType o)
        {
            var properties = CacheTypeProperties(o.GetType());
            
            foreach (var setting in document.Settings)
            {
                if (!properties.TryGetValue(setting.Key, out var property))
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
        }
        
        public static TType DeserializeTree<TType>(SettingTree tree)
        {
            return (TType)DeserializeTree(tree, typeof(TType));
        }

        private static object DeserializeTree(SettingTree tree, Type type)
        {
            var o = Activator.CreateInstance(type);

            DeserializeTreeTo(tree, o);

            return o;
        }

        public static void DeserializeTreeTo<TType>(SettingTree tree, TType o)
        {
            var properties = CacheTypeProperties(o.GetType());
            
            foreach (var setting in tree.Settings)
            {
                if (!properties.TryGetValue(setting.Key.ToLower(), out var property))
                    return;

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
                    var deserialized = DeserializeTree(settingTree, property.PropertyType);

                    if (property.PropertyType == deserialized.GetType())
                    {
                        property.SetValue(o, deserialized);
                    }
                }
            }
        }

        private static Dictionary<string, PropertyInfo> CacheTypeProperties(Type type)
        {
            var properties = new Dictionary<string, PropertyInfo>();

            foreach (var p in type.GetProperties(
                         BindingFlags.Public |
                         BindingFlags.NonPublic |
                         BindingFlags.Instance))
            {
                if (!p.CanWrite)
                    continue;
                
                if (p.GetCustomAttribute<Ignore>() != null)
                    continue;

                var propertyName = p.Name;
                var nameAttribute = p.GetCustomAttribute<SerializationName>();

                if (nameAttribute != null)
                    propertyName = nameAttribute.Name;

                properties[propertyName.ToLower()] = p;
            }

            return properties;
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
}