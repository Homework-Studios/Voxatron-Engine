using System.Security.Cryptography;
using VoxaScript;
using VoxaScript.Enviroment;
using VoxaScript.Parser;

var enviroment = new Enviroment();

if (args.Length > 0 && args[0] == "--debug") { }

// Green font color
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("VoxaScript 0.1.0");


while (true)
{
    try
    {
        // readline for command
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(">> ");
        string? consoleInput = Console.ReadLine();
        string[]? command = consoleInput?.Split(" ");

        switch (command?[0])
        {
            case "exit":
                Console.ForegroundColor = ConsoleColor.White;
                Environment.Exit(0);
                break;
            case "reload":
                enviroment = new Enviroment();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Reloaded enviroment.");
                break;
            case "run":
                string goUpDirs = "../../../Scripts/";
                string path = goUpDirs + command[1];
                string source = File.ReadAllText(path);

                Console.ForegroundColor = ConsoleColor.White;
                enviroment.Load(source);
                break;
            default:
                // merge command into string
                Console.ForegroundColor = ConsoleColor.Yellow;
                if (consoleInput != null) enviroment.Load(consoleInput);
                break;
        }
    }
    catch (Exception e)
    {
        Console.WriteLine("Error: " + e.Message + "\n" + e.StackTrace);
    }
}