using VoxaScript.Token;

namespace VoxaScript.Parser;

public class AstPrinter
{
    public AstPrinter(string script)
    {
        Token.Token[] tokens = Lexer.Lex(script);
        
        Parser parser = new Parser(tokens);
        Parser.IAst? ast = parser.Parse();
        
        if (ast != null)
        {
            PrintAst(ast);
        }
    }
    
    public void PrintAst(Parser.IAst ast)
    {
        YamlDotNet.Serialization.Serializer serializer = new YamlDotNet.Serialization.Serializer();
        string yaml = serializer.Serialize(ast);
        
        Console.WriteLine(yaml);
    }
}