using Jammo.ParserTools;

namespace SettingsConfig.Internal.Lexer
{
    public readonly struct LexerError
    {
        public string Message { get; }
        public SettingsToken Token { get; }
        public StringContext Context { get; }

        public LexerError(string message, SettingsToken token, StringContext context)
        {
            Message = message;
            Token = token;
            Context = context;
        }
    }
}