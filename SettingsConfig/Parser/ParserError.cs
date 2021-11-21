using Jammo.ParserTools;

namespace SettingsConfig.Parser
{
    public readonly struct ParserError
    {
        public readonly string Message;
        public readonly StringContext Context;

        public ParserError(string message, StringContext context)
        {
            Message = message;
            Context = context;
        }
    }
}