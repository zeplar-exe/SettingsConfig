using System;

namespace YASF.Settings.Exceptions
{
    public class SettingsException : Exception
    {
        public SettingsException(string message) : base(message) { }
    }
}