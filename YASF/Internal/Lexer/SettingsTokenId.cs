namespace YASF.Internal.Lexer
{
    public enum SettingsTokenId
    {
        Unknown = 0,
        
        Identifier,
        
        StringLiteral,
        NumericLiteral,
        
        Comment,
        
        BooleanTrue,
        BooleanFalse,
        
        IfKeyword,
        OrKeyword,
        AndKeyword,
        NorKeyword,
        NandKeyword,
        XorKeyword,
        XandKeyword,
        
        OpenParenthesis, 
        CloseParenthesis,
        OpenBracket,
        CloseBracket,
        OpenCurlyBracket,
        CloseCurlyBracket,
        Period,
        Equals,
        
        EndOfFile
    }
}