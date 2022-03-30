using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YASF.Settings
{
    public class SettingTree : SettingValue
    {
        private readonly List<Setting> b_settings;

        public IEnumerable<Setting> Settings => b_settings;
        
        public IEnumerable<Setting> this[string key] => Settings.Where(s => s.Key == key);
        
        public SettingTree(IEnumerable<Setting> settings)
        {
            b_settings = new List<Setting>();
            b_settings.AddRange(settings);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append('[');

            foreach (var setting in Settings)
            {
                builder.AppendLine(setting.ToString());
            }

            builder.Append(']');
            
            return builder.ToString();
        }
    }
}