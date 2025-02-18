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
