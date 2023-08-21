using Microsoft.VisualBasic.CompilerServices;
using VoxaScript.Enviroment;

var enviroment = new Enviroment();

if (args.Length > 0 && args[0] == "--debug")
{
}

var comments = new[] {"exit", "new", "run"};

// Green font color
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("VoxaScript 0.1.0");

while (true)
    try
    {
        // readline for command
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write(">> ");
        Console.ForegroundColor = ConsoleColor.White;
        var readKey = Console.ReadKey(true);
        var consoleLine = "";
        while (readKey.Key != ConsoleKey.Enter)
        {
            if (consoleLine.Length == 0 && readKey.Key == ConsoleKey.Backspace)
            {
                readKey = Console.ReadKey(true);
                continue;
            }

            switch (readKey.Key)
            {
                case ConsoleKey.Backspace:
                    consoleLine = consoleLine.Remove(consoleLine.Length - 1);
                    Console.Write("\b \b");
                    break;
                case ConsoleKey.Tab:
                    //TODO: improve this
                    var c = consoleLine.Split(" ");
                    if (c.Length == 1)
                    {
                        var suggestions = comments.Where(x => x.StartsWith(c[0])).ToList();
                        if (suggestions.Count == 1)
                        {
                            var effectiveSuggestion = suggestions[0].Substring(c[0].Length);
                            consoleLine += effectiveSuggestion;
                            Console.Write(effectiveSuggestion);
                        }
                    }
                    else
                    {
                        if(c[0] == "run")
                        {
                            var goUpDirs = "../../../Scripts/";
                            var path = goUpDirs + c[1];
                            var suggestions = Directory.GetFiles(goUpDirs).Where(x => x.StartsWith(path)).ToList();
                            if (suggestions.Count == 1)
                            {
                                var effectiveSuggestion = suggestions[0].Substring(path.Length);
                                consoleLine += effectiveSuggestion;
                                Console.Write(effectiveSuggestion);
                            }
                        }
                    }
                    break;
                default:
                    consoleLine += readKey.KeyChar;
                    Console.Write(readKey.KeyChar);
                    break;
            }
            
            readKey = Console.ReadKey(true);
        }

        var command = consoleLine.Split(" ");
        Console.WriteLine();

        switch (command?[0])
        {
            case "exit":
                Console.ForegroundColor = ConsoleColor.White;
                Environment.Exit(0);
                break;
            case "new":
                enviroment = new Enviroment();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Reloaded new enviroment.");
                break;
            case "run":
                var goUpDirs = "../../../Scripts/";
                var path = goUpDirs + command[1];
                var source = File.ReadAllText(path);

                Console.ForegroundColor = ConsoleColor.White;
                enviroment.Load(source);
                break;
            default:
                // merge command into string
                Console.ForegroundColor = ConsoleColor.Yellow;
                if (readKey != null) enviroment.Load(consoleLine);
                break;
        }
    }
    catch (Exception e)
    {
        Console.WriteLine("Error: " + e.Message + "\n" + e.StackTrace);
    }