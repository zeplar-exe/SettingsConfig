using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YASF.Parser;
using YASF.Parser.Nodes;
using YASF.Settings;

namespace YASF
{
    public class SettingsDocument
    {
        private readonly List<ParserError> b_errors;
        private List<Setting> b_settings;

        public IEnumerable<Setting> Settings => b_settings;
        public IEnumerable<ParserError> Errors => b_errors;

        public Setting this[string key] => b_settings.FirstOrDefault(s => s.Key == key);
        
        public static SettingsDocument CreateEmpty()
        {
            return new SettingsDocument(Array.Empty<SettingsNode>(), Array.Empty<ParserError>());
        }

        public static SettingsDocument FromText(string text) => FromParser(new SettingsParser(text));
        public static SettingsDocument FromStream(Stream stream) => FromParser(new SettingsParser(stream));

        public static SettingsDocument FromParser(SettingsParser parser)
        {
            var document = new SettingsDocument(parser.ParseSyntaxTree(), parser.Errors);

            return document;
        }

        private SettingsDocument(IEnumerable<SettingsNode> nodes, IEnumerable<ParserError> errors)
        {
            b_errors = new List<ParserError>();
            b_errors.AddRange(errors);
            
            b_settings = new List<Setting>();
            b_settings.AddRange(nodes.Select(ParseSetting).Where(n => n != null));
        }

        private Setting ParseSetting(SettingsNode node)
        {
            switch (node)
            {
                case SettingAssignmentExpression assignmentExpression:
                    switch (assignmentExpression.ValueNode)
                    {
                        case StringLiteralNode stringLiteral:
                            return new Setting(
                                assignmentExpression.Name.ToString(), 
                                new TextSetting(stringLiteral.Value));
                        case NumericLiteralNode numericLiteral:
                            return new Setting(
                                assignmentExpression.Name.ToString(), 
                                new NumericSetting(numericLiteral.Value));
                        case BooleanLiteralNode booleanLiteral:
                            return new Setting(
                                assignmentExpression.Name.ToString(), 
                                new BooleanSetting(booleanLiteral.Value));
                        case SettingTreeNode treeNode:
                            return new Setting(
                                assignmentExpression.Name.ToString(),
                                new SettingTree(treeNode.Assignments.Select(ParseSetting)));
                    }
                    break;
            }

            return null;
        }

        public void AddSetting(Setting setting) => b_settings.Add(setting);
        public bool RemoveSetting(Setting setting) => b_settings.Remove(setting);

        public override string ToString()
        {
            var builder = new StringBuilder();

            foreach (var setting in b_settings)
                builder.Append(setting);

            return builder.ToString();
        }
    }
}