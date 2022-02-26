using System;
using System.IO;
using System.Text;
using SettingsConfig;
using SettingsConfig.Serialization;

namespace DeserializerExample
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Expected a file.");
                
                return 1;
            }

            var path = File.Exists(args[0]) ? args[0] : Path.Join(Directory.GetCurrentDirectory(), args[0]);

            if (!File.Exists(path))
            {
                Console.WriteLine("The given file does not exist.");
                
                return 1;
            }

            var document = SettingsDocument.FromStream(File.OpenRead(path));
            var myClass = SettingsDeserializer.Deserialize<MyClass>(document);
            // For Serialization, use SettingSerializer.Serialize

            var builder = new StringBuilder();

            builder.AppendLine("Deserialize to 'MyClass'");
            builder.AppendLine("Deserialization Result:");
            builder.AppendLine($"SomeSnakeCaseNamedValue: {myClass.SomeSnakeCaseNamedValue}");
            builder.AppendLine($"SomeNumber: {myClass.SomeNumber}");
            builder.AppendLine($"SomeString: {myClass.SomeString}");
            builder.AppendLine();
            builder.AppendLine("Ignored Values:");
            builder.AppendLine($"IgnoredBoolean: {myClass.IgnoredBoolean}");
            
            Console.Write(builder.ToString());

            return 0;
        }
    }
    
    public class MyClass
    {
        public int SomeNumber { get; set; }
        public string SomeString { get; set; }
        
        [SettingsDeserializer.SerializationName("some_snake_case_named_value")] 
        // For Serialization, use SettingSerializer.SerializationName
        public string SomeSnakeCaseNamedValue { get; set; }
        
        [SettingsDeserializer.Ignore]
        // For Serialization, use SettingSerializer.Ignore
        public bool IgnoredBoolean { get; set; }
    }
}