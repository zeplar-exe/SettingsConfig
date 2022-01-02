using Jammo.ParserTools;

namespace SettingsConfig.Parser
{
    public readonly struct ParserError
    {
        public string Message { get; }
        public StringContext Context { get; }

        public ParserError(string message, StringContext context)
        {
            Message = message;
            Context = context;
        }
    }
}