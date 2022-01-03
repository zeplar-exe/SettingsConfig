using System;
using System.Collections.Generic;
using Jammo.ParserTools;
using SettingsConfig.Internal.Lexer;

namespace SettingsConfig.Parser.Nodes
{
    public abstract class SettingsNode
    {
        public StringContext Context;

        protected SettingsNode(StringContext context)
        {
            Context = context;
        }

        protected static void ThrowIfInvalidToken(SettingsToken token, SettingsTokenId expected)
        {
            if (!token.Is(expected))
                throw new ArgumentException($"Expected {expected}, got '{token.Id}'");
        }

        public abstract string ToFormattedString();
    }
}