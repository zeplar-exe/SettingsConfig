using Jammo.ParserTools;

namespace YASF.Parser
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