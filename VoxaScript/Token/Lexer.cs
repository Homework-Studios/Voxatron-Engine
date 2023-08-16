using System.Text.RegularExpressions;

namespace VoxaScript.Token;

public class Lexer
{
    public static int currentLine = 0;
    public static int currentCharInLine = 0;

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

        // filters out comments
        _source = Regex.Replace(new string(_source), @"/\*.*\*/", "", RegexOptions.Singleline).ToCharArray();
        _source = Regex.Replace(new string(_source), @"\/\/.*", "").ToCharArray();
        

        // init the token class
        Token.Init();

        var processed = new List<Token>();
        var inQuotes = false;

        foreach (var c in _source)
        {
            if (!HasSource()) break;
            
            if (c == '\n')
            {
                currentLine++;
                currentCharInLine = 0;
            }
            else
            {
                currentCharInLine++;
            }

            if (c == '"') inQuotes = !inQuotes;

            if (CanSkip() && !inQuotes)
            {
                Consume();
                continue;
            }

            var found = Token.TestCharList(PrepTokenCheck());

            if (found.TokenType != Token.Type.Unknown)
            {
                found.AddLocationData(currentLine, currentCharInLine);
                processed.Add(found);

                Consume(found.Value.Length);
                continue;
            }

            if (IsAlphaOrDot())
            {
                var value = "";

                while (HasSource() && IsAlphaOrDot())
                {
                    value += Consume();
                }
                

                var isScopeIdentifier = value.Contains(".");

                var token = new Token(isScopeIdentifier ? Token.Type.ScopeIdentifier : Token.Type.Identifier, value);
                token.AddLocationData(currentLine, currentCharInLine);
                processed.Add(token);
                continue;
            }

            if (IsNumericOrDot())
            {
                var value = "";

                while (HasSource() && IsNumericOrDot())
                {
                    value += Consume();
                }

                var token = new Token(Token.Type.Literal, value);
                token.AddLocationData(currentLine, currentCharInLine);
                processed.Add(token);
                continue;
            }

            if (_source[0] == '"')
            {
                var value = "";

                Consume();
                
                while (true)
                {
                    if (!HasSource()) break;

                    var next = Consume();
                    
                    if (next == '"') break;
                    
                    value += next;
                }

                var token = new Token(Token.Type.Literal, value);
                token.AddLocationData(currentLine, currentCharInLine);
                processed.Add(token);
                continue;
            }


            Consume();
        }

        var eof = new Token(Token.Type.Eof, "");
        eof.AddLocationData(currentLine, currentCharInLine);
        processed.Add(eof);

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
        var c = _source[0];

        for (var i = 0; i < lenght; i++)
        {
            if (!HasSource()) break;

            _source = _source[1..];
        }

        return c;
    }

    public static char[] PrepTokenCheck()
    {
        var prep = new char[Token.MaxTokenLength];
        for (var i = 0; i < Token.MaxTokenLength; i++)
        {
            if (i >= _source.Length) break;

            if (CanSkip(_source[i])) break;

            prep[i] = _source[i];
        }

        return prep;
    }
}