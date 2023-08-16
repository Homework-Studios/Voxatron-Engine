namespace VoxaScript.Error;

public class ErrorMessages
{
    public enum Error
    {
        None,
        UnexpectedToken,
        UnknownToken,
        NullVariableAssignment,
        UnexpectedEndOfFile
    }

    public string source;
    
    public string GetMessage(Error error)
    {
        switch (error)
        {
            case Error.None:
                return "No error";
            case Error.UnexpectedToken:
                return "Unexpected token";
            case Error.UnknownToken:
                return "Unknown token";
            case Error.NullVariableAssignment:
                return "A variable assignment must have a value";
            case Error.UnexpectedEndOfFile:
                return "Unexpected end of file";
            default:
                return "Unknown error";
        }
    }

    public string ErrorVisualizer(int charLocation, int lineLocation)
    {
        // Error visualizer:
        // if 1 == 1 
        //          ^
        // This is the error visualizer example
        // It should show the line and the char where the error is
        
        var stringToReturn = "";
        
        var lines = source.Split('\n');
        
        stringToReturn += lines[lineLocation - 1] + "\n";
        
        for (var i = 0; i < charLocation; i++)
        {
            stringToReturn += " ";
        }
        
        stringToReturn += "^";

        return stringToReturn;
    }
    
    public void PrintError(Parser.Parser.Error error)
    {
        // Error message:
        // [Line, Char]: Error message
        var currentColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[{error.Line}, {error.Char}]: {GetMessage(error.Name)}");
        Console.WriteLine(ErrorVisualizer(error.Char, error.Line));
        Console.ForegroundColor = currentColor;
    }

    public static ErrorMessages? Instance;
    
    public ErrorMessages(string source)
    {
        Instance = this;
        this.source = source;
    }
}