using VoxaScript.Error;

namespace VoxaScript.Token;

public class Token
{
    public enum Type
    {
        Descriptive,
        DataType,
        Operator,
        Separator,
        SpecialIdentifier,
        Identifier,
        ScopeIdentifier,
        Literal,
        Eof,
        Unknown,
        Error
    }

    // Descriptive list of tokens
    public static readonly string[] DescriptiveList =
    {
        "if",
        "var",
        "switch",
        "case",
        "default",
        "while",
        "for",
        "fun",
        "function",
        "return",
        "scope"
    };

    // List of data types
    public static readonly string[] DataTypeList =
    {
        "int",
        "float",
        "string",
        "bool"
    };

    // List of operators
    public static readonly string[] OperatorList =
    {
        "+",
        "-",
        "*",
        "/",
        "%",
        "<<",
        ">>",
        "=",
        "==",
        "!=",
        ">",
        "<",
        ">=",
        "<=",
        "&&",
        "||",
        "!",
        "++",
        "--",
        "->"
    };

    // List of separators
    public static readonly string[] SeparatorList =
    {
        ";",
        ",",
        "(",
        ")",
        "{",
        "}",
        "[",
        "]"
    };

    // List of Special Identifiers
    public static readonly string[] SpecialIdentifierList =
    {
        "true",
        "false",
        "null"
    };
    // Description of the token types and what they are used for
    // Descriptive: Used to describe a statement or a block of code
    // DataType: Used to describe the type of data a variable is
    // Operator: Used to perform operations on data
    // Separator: Used to separate statements and blocks of code
    // SpecialIdentifier: Used to identify special values
    // Identifier: Used to identify variables and functions
    // ScopeIdentifier: Used to identify variables and functions in a specific scope
    // Literal: Used to identify literal values that are meant to be interpreted as data

    public static int MaxTokenLength;

    public Token(Type tokenType, string value, ErrorMessages.Error error = ErrorMessages.Error.None)
    {
        TokenType = tokenType;
        Value = value;
        Error = error;
    }

    public Type TokenType { get; set; }
    public string Value { get; set; }

    public int atLine { get; set; }
    public int atChar { get; set; }
    
    public ErrorMessages.Error Error { get; set; }
    public bool IsError => Error != ErrorMessages.Error.None;
    public Parser.Parser.Error AsParserError => new Parser.Parser.Error { Line = atLine, Char = atChar, Name = Error, CanRunInGlobalScope = true };
    
    public static void Init()
    {
        foreach (var tokenList in new[]
                     { DescriptiveList, DataTypeList, OperatorList, SeparatorList, SpecialIdentifierList })
        foreach (var token in tokenList)
            if (token.Length > MaxTokenLength)
                MaxTokenLength = token.Length;
    }

    public override string ToString()
    {
        //return $"Token: {TokenType}, Value: {Value}";
        return Value;
    }

    public static Token TestCharList(char[] nextChars)
    {
        // Test if the nextChars are the length of the longest token to be expected
        if (nextChars.Length < MaxTokenLength) return new Token(Type.Unknown, string.Join("", nextChars));

        var lenghtOfLongestToken = 0;

        // Test if the next chars are a descriptive token longer definitions are prioritized
        foreach (var tokenList in new[]
                     { DescriptiveList, OperatorList, SeparatorList, SpecialIdentifierList })
        {
            // sort the list by length of token, longest first
            var sortedTokenList = tokenList.OrderByDescending(x => x.Length).ToArray();

            foreach (var token in sortedTokenList)
            {
                if (token.Length > nextChars.Length) continue;

                var match = true;
                for (var i = 0; i < token.Length; i++)
                    if (token[i] != nextChars[i])
                    {
                        match = false;
                        break;
                    }

                if (match && token.Length > lenghtOfLongestToken)
                {
                    lenghtOfLongestToken = token.Length;

                    // dont use switch
                    if (tokenList == DescriptiveList) return new Token(Type.Descriptive, token);

                    if (tokenList == DataTypeList) return new Token(Type.DataType, token);

                    if (tokenList == OperatorList) return new Token(Type.Operator, token);

                    if (tokenList == SeparatorList) return new Token(Type.Separator, token);

                    if (tokenList == SpecialIdentifierList) return new Token(Type.SpecialIdentifier, token);
                }
            }
        }

        return new Token(Type.Unknown, string.Join("", nextChars));
    }
    
    public void AddLocationData(int line, int character)
    {
        atLine = line;
        atChar = character;
    }

    public bool IsInt()
    {
        return int.TryParse(Value, out _);
    }

    public int AsInt()
    {
        return int.Parse(Value);
    }

    public bool IsFloat()
    {
        return float.TryParse(Value, out _);
    }

    public float AsFloat()
    {
        return float.Parse(Value);
    }

    public bool IsIdentifier()
    {
        return TokenType == Type.Identifier;
    }

    public bool IsScopeIdentifier()
    {
        return TokenType == Type.ScopeIdentifier;
    }

    public bool IsBoolean()
    {
        return TokenType == Type.SpecialIdentifier && (Value == "true" || Value == "false");
    }

    public bool AsBoolean()
    {
        return Value == "true";
    }

    public bool IsString()
    {
        return TokenType == Type.Literal;
    }
}