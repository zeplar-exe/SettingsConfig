using System.Globalization;
using SettingsConfig.Internal.Lexer;

namespace SettingsConfig.Parser.Nodes;

public class NumericLiteralNode : LiteralNode
{
    private readonly double b_value;
    public double Value => b_value;
    
    public NumericLiteralNode(SettingsToken token) : base(token)
    {
        ThrowIfInvalidToken(token, SettingsTokenId.NumericLiteral);

        double.TryParse(token.ToString(), out b_value);
    }

    public override string ToFormattedString()
    {
        return Value.ToString(CultureInfo.CurrentCulture);
    }

    public override string ToString()
    {
        return Value.ToString(CultureInfo.CurrentCulture);
    }
}