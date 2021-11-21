using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SettingsConfig.Parser;
using SettingsConfig.Settings;

namespace SettingsConfig
{
    public class SettingsDocument
    {
        private List<Setting> SettingsList { get; } = new();

        public IEnumerable<Setting> Settings => SettingsList.AsReadOnly();

        public Setting this[string key] => SettingsList.FirstOrDefault(s => s.Key == key);

        public SettingsDocument(IEnumerable<Setting> settings)
        {
            SettingsList.AddRange(settings);
        }

        public static SettingsDocument FromText(string text) => new SettingsParser(text).Parse();
        public static SettingsDocument FromStream(Stream stream) => new SettingsParser(stream).Parse();
        
        public void AddSetting(Setting setting) => SettingsList.Add(setting);
        public bool RemoveSetting(Setting setting) => SettingsList.Remove(setting);

        public override string ToString()
        {
            var builder = new StringBuilder();

            foreach (var setting in SettingsList)
                builder.Append(setting);

            return builder.ToString();
        }
    }
}