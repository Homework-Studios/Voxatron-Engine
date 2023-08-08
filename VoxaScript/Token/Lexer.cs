using System.Text.RegularExpressions;

namespace VoxaScript.Token;

public class Lexer
{

    private static char[] _source = Array.Empty<char>();

    public static bool CanSkip()
    {
        // if the token is a whitespace or new line skip

        if (_source[0] == ' ') return true;

        if (_source[0] == '\t') return true;
        
        if (_source[0] == '\r') return true;

        if (_source[0] == '\n') return true;

        return false;
    }
    
    public static bool CanSkip(char c)
    {
        // if the token is a whitespace or new line skip

        if (c == ' ') return true;

        if (c == '\t') return true;
        
        if (c == '\r') return true;

        if (c == '\n') return true;

        return false;
    }
    
    public static Token[] Lex(string script)
    {
        // Set the source
        _source = script.ToCharArray();
        
        // init the token class
        Token.Init();
        
        List<Token> processed = new List<Token>();
        bool inQuotes = false;
        
        foreach (char c in _source)
        {
            if (!HasSource()) break;

            if (c == '"') inQuotes = !inQuotes;
            
            if (CanSkip() && !inQuotes)
            {
                Consume();
                continue;
            }

            Token found = Token.TestCharList(PrepTokenCheck());

            if (found.TokenType != Token.Type.Unknown)
            {
                processed.Add(found);

                Consume(found.Value.Length);
                continue;
            }

            if (IsAlphaOrDot())
            {
                string value = "";
                
                while (IsAlphaOrDot())
                {
                    value += Consume();
                }
                
                bool isScopeIdentifier = value.Contains(".");

                processed.Add(new Token(isScopeIdentifier ? Token.Type.ScopeIdentifier : Token.Type.Identifier, value));
                continue;
            }
            
            if (IsNumericOrDot())
            {
                string value = "";
                
                while (IsNumericOrDot())
                {
                    value += Consume();
                }
                
                processed.Add(new Token(Token.Type.Literal, value));
                continue;
            }

            if (_source[0] == '"')
            {
                string value = "";

                Consume();
                
                while (true)
                {
                    if (!HasSource()) break;
                    
                    char next = Consume();
                    
                    if (next == '"') break;
                    
                    value += next;
                }
                
                processed.Add(new Token(Token.Type.Literal, value));
                continue;
            }
            
            Consume();
        }
        
        processed.Add(new Token(Token.Type.Eof, ""));

        return processed.ToArray();
    }

    public static bool HasSource()
    {
        return _source.Length > 0;
    }

    public static bool IsAlphaOrDot()
    {
        return Regex.IsMatch(_source[0].ToString(), "[a-zA-Z.]");
    }

    public static bool IsNumericOrDot()
    {
        if (!HasSource()) return false;
        return Regex.IsMatch(_source[0].ToString(), "[0-9.]");
    }
    
    public static char Consume(int lenght = 1)
    {
        char c = _source[0];
        
        for (int i = 0; i < lenght; i++)
        {
            if (!HasSource()) break;
            
            _source = _source[1..];
        }
        
        return c;
    }

    public static char[] PrepTokenCheck()
    {
        char[] prep = new char[Token.MaxTokenLength];
        for (int i = 0; i < Token.MaxTokenLength; i++)
        {
            if(i >= _source.Length) break;
            
            if (CanSkip(_source[i])) break;
            
            prep[i] = _source[i];
        }
        
        return prep;
    }
}