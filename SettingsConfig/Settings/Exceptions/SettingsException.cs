using System;

namespace SettingsConfig.Settings.Exceptions
{
    public class SettingsException : Exception
    {
        public SettingsException(string message) : base(message) { }
    }
}