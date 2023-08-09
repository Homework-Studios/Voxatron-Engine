namespace VoxaScript.Parser;

public class Parser
{
    private Token.Token[] _tokens;

    private bool SkipSemicolon;

    public Parser(Token.Token[] tokens)
    {
        _tokens = tokens;
    }

    private Token.Token CurrentToken
    {
        get
        {
            if (_tokens.Length > 0) return _tokens[0];

            return new Token.Token(Token.Token.Type.Eof, "");
        }
    }

    public void Error(string message)
    {
        throw new Exception(message);
    }

    public Token.Token Eat(string expected = "")
    {
        var token = CurrentToken;

        if (expected == "")
        {
            _tokens = _tokens[1..];
            return token;
        }

        if (_tokens[0].Value == expected)
            _tokens = _tokens[1..];
        else
            Error($"Expected {expected} but got {_tokens[0].Value}");

        return token;
    }

    public Token.Token Peek(int offset = 1)
    {
        if (_tokens.Length > offset) return _tokens[offset];

        return new Token.Token(Token.Token.Type.Eof, "");
    }

    public IAst? Structures()
    {
        if (CurrentToken.Value == "var")
        {
            Eat("var");
            var name = Eat();

            // If Empty Declaration
            if (CurrentToken.Value == ";")
                return new VariableDeclaration
                {
                    Name = "VariableDeclaration",
                    VariableName = name,
                    Value = null
                };

            Eat("=");
            var value = BooleanExpression();
            return new VariableDeclaration
            {
                Name = "VariableDeclaration",
                VariableName = name,
                Value = value
            };
        }

        if (CurrentToken.Value == "if")
        {
            Eat("if");
            var condition = BooleanExpression();
            Eat("{");

            var body = new List<IAst?>();

            while (CurrentToken.Value != "}")
            {
                body.Add(BooleanExpression());
                Eat(";");
            }

            Eat("}");
            SkipSemicolon = true;

            return new If
            {
                Name = "If",
                Condition = condition,
                Body = new Block
                {
                    Name = "Block",
                    Statements = body.ToArray()
                }
            };
        }

        if (CurrentToken.Value == "function")
        {
            Eat("function");
            var name = Eat();
            Eat("(");

            var arguments = new List<Token.Token>();

            while (CurrentToken.Value != ")")
            {
                arguments.Add(Eat());

                if (CurrentToken.Value != ")") Eat(",");
            }

            Eat(")");

            Eat("{");

            var body = new List<IAst?>();

            while (CurrentToken.Value != "}")
            {
                body.Add(BooleanExpression());
                if (!SkipSemicolon)
                    Eat(";");
                else
                    SkipSemicolon = false;
            }

            Eat("}");

            SkipSemicolon = true;

            return new FunctionDeclaration
            {
                Name = "FunctionDeclaration",
                FunctionName = name,
                Arguments = arguments.ToArray(),
                Body = new Block
                {
                    Name = "Block",
                    Statements = body.ToArray()
                }
            };
        }

        if (CurrentToken.Value == "return")
        {
            Eat("return");
            var expr = BooleanExpression();
            return new Return
            {
                Name = "Return",
                Expr = expr
            };
        }

        if (CurrentToken.Value == "scope")
        {
            Eat("scope");
            var name = Eat();

            Eat("{");

            var body = new List<IAst?>();

            while (CurrentToken.Value != "}")
            {
                body.Add(BooleanExpression());
                if (!SkipSemicolon)
                    Eat(";");
                else
                    SkipSemicolon = false;
            }

            Eat("}");

            SkipSemicolon = true;

            return new Scope
            {
                Name = "Scope",
                ScopeName = name,
                Body = new Block
                {
                    Name = "Block",
                    Statements = body.ToArray()
                }
            };
        }

        if (CurrentToken.IsIdentifier() || CurrentToken.IsScopeIdentifier())
        {
            // decide if it's a function call or a variable
            if (Peek().Value == "(")
            {
                var name = Eat();
                Eat("(");

                var arguments = new List<IAst?>();

                while (CurrentToken.Value != ")")
                {
                    arguments.Add(BooleanExpression());

                    if (CurrentToken.Value != ")") Eat(",");
                }

                Eat(")");

                return new FunctionCall
                {
                    Name = "FunctionCall",
                    FunctionName = name,
                    Arguments = arguments.ToArray()
                };
            }

            // check if it's a variable assignment
            if (Peek().Value == "=")
            {
                var name = Eat();
                Eat("=");
                var value = BooleanExpression();

                if (value != null)
                    return new VariableAssignment
                    {
                        Name = "VariableAssignment",
                        VariableName = name,
                        Value = value
                    };

                Error("Variable assignment value is cannot be null");
            }

            var variable = Eat();
            return new Variable
            {
                Name = "Variable",
                Token = variable
            };
        }

        return null;
    }

    public IAst? Factor()
    {
        if (CurrentToken.IsInt())
            return new Int
            {
                Name = "Int",
                Token = Eat()
            };

        if (CurrentToken.IsFloat())
            return new Float
            {
                Name = "Float",
                Token = Eat()
            };

        if (CurrentToken.IsBoolean())
            return new Boolean
            {
                Name = "Boolean",
                Token = Eat()
            };

        if (CurrentToken.IsString())
            return new String
            {
                Name = "String",
                Token = Eat()
            };

        if (CurrentToken.Value == "(")
        {
            Eat("(");
            var node = BooleanExpression();
            Eat(")");
            return node;
        }

        return Structures();
    }

    public IAst? Unary()
    {
        // UnaryOp: +1, -1
        while (CurrentToken.Value == "+" || CurrentToken.Value == "-")
        {
            var token = CurrentToken;

            if (token.Value == "+")
                Eat("+");
            else if (token.Value == "-") Eat("-");

            return new UnaryOp
            {
                Name = "UnaryOp",
                Op = token,
                Expr = Factor()
            };
        }

        return Factor();
    }

    public IAst? Term()
    {
        var node = Unary();

        while (CurrentToken.Value == "*" || CurrentToken.Value == "/")
        {
            var token = CurrentToken;

            if (token.Value == "*")
                Eat("*");
            else if (token.Value == "/") Eat("/");

            node = new BinaryOp
            {
                Name = "BinaryOp",
                Left = node,
                Op = token,
                Right = Unary()
            };
        }

        return node;
    }

    public IAst? Expression()
    {
        var node = Term();

        while (CurrentToken.Value == "+" || CurrentToken.Value == "-")
        {
            var token = CurrentToken;

            if (token.Value == "+")
                Eat("+");
            else if (token.Value == "-") Eat("-");

            node = new BinaryOp
            {
                Name = "BinaryOp",
                Left = node,
                Op = token,
                Right = Term()
            };
        }

        return node;
    }

    public IAst? BooleanExpression()
    {
        var node = Expression();

        string[] booleanOperators = { "==", "!=", ">=", "<=", ">", "<", "&&", "||" };

        while (booleanOperators.Contains(CurrentToken.Value))
        {
            var token = CurrentToken;

            Eat(token.Value);

            node = new BinaryOp
            {
                Name = "BinaryOp",
                Left = node,
                Op = token,
                Right = Expression()
            };
        }

        return node;
    }

    public Block? Parse()
    {
        var statements = new List<IAst>();

        while (CurrentToken.TokenType != Token.Token.Type.Eof)
        {
            var node = BooleanExpression();

            if (node != null) statements.Add(node);

            if (SkipSemicolon)
            {
                SkipSemicolon = false;
                continue;
            }

            Eat(";");
        }

        return new Block
        {
            Name = "Block",
            Statements = statements.ToArray()
        };
    }

    public interface IAst
    {
        public bool Valid()
        {
            return true;
        }
    }

    public struct Block : IAst
    {
        public string Name;
        public IAst?[] Statements;

        public bool Valid()
        {
            return Statements.Length > 0;
        }
    }

    public struct Int : IAst
    {
        public string Name;
        public Token.Token Token;
    }

    public struct Float : IAst
    {
        public string Name;
        public Token.Token Token;
    }

    public struct Boolean : IAst
    {
        public string Name;
        public Token.Token Token;
    }

    public struct String : IAst
    {
        public string Name;
        public Token.Token Token;
    }

    public struct Variable : IAst
    {
        public string Name;
        public Token.Token Token;
    }

    public struct BinaryOp : IAst
    {
        public string Name;
        public IAst? Left;
        public Token.Token Op;
        public IAst? Right;

        public bool Valid()
        {
            return Left != null && Right != null;
        }
    }

    public struct VariableDeclaration : IAst
    {
        public string Name;
        public Token.Token VariableName;
        public IAst? Value;

        public bool Valid()
        {
            return VariableName != null && Value != null;
        }
    }

    public struct VariableAssignment : IAst
    {
        public string Name;
        public Token.Token VariableName;
        public Token.Token Op;
        public IAst Value;

        public bool Valid()
        {
            return VariableName != null && Value != null;
        }
    }

    public struct UnaryOp : IAst
    {
        public string Name;
        public Token.Token Op;
        public IAst? Expr;

        public bool Valid()
        {
            return Expr != null;
        }
    }

    public struct If : IAst
    {
        public string Name;
        public IAst? Condition;
        public Block? Body;

        public bool Valid()
        {
            return Condition != null && Body != null;
        }
    }

    public struct FunctionDeclaration : IAst
    {
        public string Name;
        public Token.Token FunctionName;
        public Token.Token[] Arguments;
        public Block? Body;

        public bool Valid()
        {
            return FunctionName != null && Body != null;
        }
    }

    public struct FunctionCall : IAst
    {
        public string Name;
        public Token.Token FunctionName;
        public IAst?[] Arguments;

        public bool Valid()
        {
            return FunctionName != null;
        }
    }

    public struct Return : IAst
    {
        public string Name;
        public IAst? Expr;

        public bool Valid()
        {
            return Expr != null;
        }
    }

    public struct Scope : IAst
    {
        public string Name;
        public Token.Token ScopeName;
        public Block? Body;

        public bool Valid()
        {
            return Body != null;
        }
    }
}