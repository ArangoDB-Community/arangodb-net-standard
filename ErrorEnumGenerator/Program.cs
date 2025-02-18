﻿using System.Text;
using System.Text.RegularExpressions;

if (args.Length == 0)
{
    Console.WriteLine("Please provide a file path as an argument.");
    Environment.Exit(1);
}

string filePath = args[0];

if (!File.Exists(filePath))
{
    Console.WriteLine($"File not found: {filePath}");
    Environment.Exit(1);
}

string fileContent = File.ReadAllText(filePath);

if (fileContent.Length == 0)
{
    Console.WriteLine("File is empty.");
    Environment.Exit(1);
}

var enumBuilder = new StringBuilder();

enumBuilder.AppendLine("// This file is generated by a tool.");
enumBuilder.AppendLine();

enumBuilder.AppendLine("namespace ArangoDBNetStandard");
enumBuilder.AppendLine("{");
enumBuilder.AppendLine("    /// <summary>");
enumBuilder.AppendLine("    /// ArangoDB error codes and their meanings.");
enumBuilder.AppendLine("    /// </summary>");
enumBuilder.AppendLine("    public enum ArangoDBErrors");
enumBuilder.AppendLine("    {");

var regex = new Regex(@"^(\d+) - (\w+)\s*(.*)$", RegexOptions.Multiline);

var matches = regex.Matches(fileContent);

foreach (Match match in matches)
{
    if (!match.Success)
    {
        Console.WriteLine($"Invalid format: {match.Value}");
        continue;
    }

    string enumValue = match.Groups[1].Value.Trim();
    string enumName = match.Groups[2].Value.Trim();
    string enumSummary = match.Groups[3].Value.Trim();

    enumBuilder.AppendLine("        /// <summary>");
    enumBuilder.AppendLine($"        /// {enumSummary}");
    enumBuilder.AppendLine("        /// </summary>");
    enumBuilder.AppendLine($"        {enumName} = {enumValue},");
}

enumBuilder.AppendLine("    }");
enumBuilder.AppendLine("}");

File.WriteAllText("ArangoDBErrors.cs", enumBuilder.ToString());