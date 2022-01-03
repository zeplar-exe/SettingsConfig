using System;
using SettingsConfig.Internal.Lexer;

namespace SettingsConfig.Parser.Nodes;

public class BooleanLiteralNode : LiteralNode
{
    private readonly bool b_value;
    public bool Value => b_value;

    public BooleanLiteralNode(SettingsToken token) : base(token)
    {
        if (!token.Is(SettingsTokenId.BooleanTrue) && !token.Is(SettingsTokenId.BooleanFalse))
            throw new ArgumentException($"Expected a boolean token, got '{token.Id}'");

        bool.TryParse(token.ToString(), out b_value);
    }

    public override string ToFormattedString()
    {
        return Value.ToString();
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}