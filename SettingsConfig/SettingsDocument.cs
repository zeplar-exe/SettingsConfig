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
        public IEnumerable<ParserError> Errors { get; private set; }

        public Setting this[string key] => SettingsList.FirstOrDefault(s => s.Key == key);

        public SettingsDocument(IEnumerable<Setting> settings)
        {
            SettingsList.AddRange(settings);
        }

        public static SettingsDocument FromText(string text) => FromParser(new SettingsParser(text));
        public static SettingsDocument FromStream(Stream stream) => FromParser(new SettingsParser(stream));

        public static SettingsDocument FromParser(SettingsParser parser)
        {
            var document = parser.Parse();

            document.Errors = parser.Errors;

            return document;
        }
        
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