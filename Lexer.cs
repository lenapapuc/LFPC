using Lab3;

namespace lexer
{
    public class Lexer
    {
        public string Code { get; set; }
        public GetPlace PositionTracker { get; set; }
        public List<Token> Tokens { get; set; }
        public List<TheError> Errors { get; set; }

        public Lexer(string code)
        {
            Code = code;
            PositionTracker = new GetPlace();
            Errors = new List<TheError>();
            Tokens = new List<Token>();
        }

        public void Scan()
        {
            for(PositionTracker.Position = 0; PositionTracker.Position < Code.Length; PositionTracker.Position++, PositionTracker.Column++)
            {
                int i = PositionTracker.Position;

                if (Code[i] == ' ' || Code[i] == '\t' || Code[i] == '\r')
                {
                    continue;
                }
                else if (Code[i] == '\n')
                {
                    PositionTracker.AddLine();
                }
                else if (Code[i].IsDigit())
                {
                    var newToken = GetToken.GenerateNumber(Code, PositionTracker, Errors);
                    if (newToken is not null)
                    {
                        Tokens.Add(newToken);
                    }
                }
                else if (Code[i].IsIdentifierStart())
                {
                    var newToken = GetToken.GenerateIdentifier(Code, PositionTracker, Errors);
                    if (newToken is not null)
                    {
                        Tokens.Add(newToken);
                    }
                }
                else if (Code[i] == '"')
                { 
                    var newTokens = GetToken.GenerateString(Code, PositionTracker, Errors);
                    if (newTokens is not null)
                    {
                        Tokens.AddRange(newTokens);
                    }
                }
                else if (Code[i] == '(')
                {
                    Tokens.Add(new Token(TokenType.LeftParantheses, "("));
                }
                else if (Code[i] == ')')
                {
                    Tokens.Add(new Token(TokenType.RightParantheses, ")"));
                }
                else if (Code[i] == ',')
                {
                    Tokens.Add(new Token(TokenType.Comma, ","));
                }
                else if (Code[i] == '-')
                {
                    Tokens.Add(new Token(TokenType.Minus, "-"));
                }
                else if (Code[i] == '+')
                {
                    Tokens.Add(new Token(TokenType.Plus, "+"));
                }
                else if (Code[i] == '*')
                {
                    Tokens.Add(new Token(TokenType.Multiplication, "*"));
                }
                else if (Code[i] == ':')
                {
                    Tokens.Add(new Token(TokenType.Division, ":"));
                }
               
                else if (Code[i].IsComparisonOperator() || Code[i] == '=')
                {
                    if(i < Code.Length - 1 && Code[i + 1] == '=')
                    {
                        Tokens.Add(GetToken.GenerateComparisonOperator(Code, PositionTracker, true));
                    }
                    else 
                    {
                        Tokens.Add(GetToken.GenerateComparisonOperator(Code, PositionTracker, false));
                    }
                }
                else if (Code[i].IsBoolOperatorStart())
                {
                   
                        Tokens.Add(GetToken.GenerateBoolOperator(Code, PositionTracker));
                }
                else
                {
                    Errors.Add(TheError.UnexpectedSymbol(PositionTracker, Code[i]));
                }
            }
        }

        public void PrintResult()
        {
            if(Errors.Count == 0)
            {
                foreach (var token in Tokens)
                {
                    Console.WriteLine(token);
                }
            }
            else
            {
                foreach (var error in Errors)
                {
                    Console.WriteLine(error);
                }
            }
        }
    }
}