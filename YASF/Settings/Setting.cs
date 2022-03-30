using Jammo.ParserTools;

namespace YASF.Settings
{
    public class Setting
    {
        public string Key { get; }
        public SettingValue Value { get; }

        internal Setting(string key, SettingValue value)
        {
            Key = key;
            Value = value;
        }
        
        public static Setting Create(string key, string value)
        {
            return new Setting(key, new TextSetting(value));
        }
        
        public static Setting Create(string key, bool value)
        {
            return new Setting(key, new BooleanSetting(value));
        }
        
        public static Setting Create(string key, int value)
        {
            return new Setting(key, new NumericSetting(value));
        }
        
        public static Setting Create(string key, float value)
        {
            return new Setting(key, new NumericSetting(value));
        }
        
        public static Setting Create(string key, double value)
        {
            return new Setting(key, new NumericSetting(value));
        }
        
        public override string ToString()
        {
            return $"{Key}=\"{Value}\"";
        }
    }
}