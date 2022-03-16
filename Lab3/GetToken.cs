using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lexer;

namespace Lab3
{
    public static class GetToken
    {
        public static Token GenerateNumber(string code, GetPlace place, List<TheError> errors)
        {
            string tokenValue = "";
            bool isFloat = false;
            for (; place.Position < code.Length; place.Position++)
            {
                int i = place.Position;
                
                if (code[i].IsWhiteSpace() || code[i].IsOperator() || code[i].IsParantheses())
                {
                    place.Position--;
                    break;
                }

                place.Column++;

                if(code[i] == '.' && isFloat)
                {
                    errors.Add(TheError.UnexpectedSymbol(place, code[i]));
                }
                else if(code[i] == '.')
                {
                    tokenValue += code[i];
                    isFloat = true;
                }
                else if (code[i].IsDigit())
                {
                    tokenValue += code[i];
                }
                else
                {
                    errors.Add(TheError.UnexpectedSymbol(place, code[i]));
                }
            }
            
            if(errors.Count > 0)
            {
                return null;
            }
            return isFloat ?
                new Token(TokenType.Float, tokenValue) :
                new Token(TokenType.Integer, tokenValue);
        }

        public static Token GenerateIdentifier(string code, GetPlace place, List<TheError> errors)
        {
            string tokenValue = "";

            for (; place.Position < code.Length; place.Position++)
            {
                int i = place.Position;

                if (code[i].IsWhiteSpace() || code[i].IsOperator() || code[i].IsParantheses() || code[i] == ',')
                {
                    place.Position--;
                    break;
                }

                place.Column++;

                if (code[i].IsIdentifierSymbol())
                {
                    tokenValue += code[i];
                }
                else
                {
                    errors.Add(TheError.UnexpectedSymbol(place, code[i]));
                }
            }

            if(errors.Count > 0)
            {
                return null;
            }
            if(tokenValue.IsKeyword())
            {
                return new Token($"{TokenType.Keyword}[{KeyWords.Keywords[tokenValue]}]", tokenValue);
            }
            if (tokenValue.IsPredefinedFunction())
            {
                return new Token($"{TokenType.PredefinedFunction}[{KeyWords.PredefinedFunctions[tokenValue]}]", tokenValue);
            }
            return new Token(TokenType.Name, tokenValue);
        }

        public static List<Token> GenerateString(string code, GetPlace place, List<TheError> errors)
        {
            List<Token> tokens = new List<Token>();
            tokens.Add(new Token(TokenType.StringStart, "\""));
            place.Position++;
            place.Column++;

            string tokenValue = "";

            for (; place.Position < code.Length; place.Position++, place.Column++)
            {
                int i = place.Position;

                if (code[i] == '"')
                {
                    place.Position++;
                    place.Column++;
                    break;
                }

                if (code[i].IsCharacter())
                {
                    tokenValue += code[i];
                }
               
            }
            
            if (errors.Count > 0)
            {
                return null;
            }
            if (!string.IsNullOrEmpty(tokenValue))
            {
                tokens.Add(new Token(TokenType.String, tokenValue));
            }
            tokens.Add(new Token(TokenType.StringEnd, "\""));

            return tokens;
        }

        public static Token GenerateComparisonOperator(string code, GetPlace place, bool withEquals)
        {
            string tokenValue = "";
            tokenValue += code[place.Position];

            if (withEquals)
            {
                place.Position++;
                place.Column++;
                tokenValue += code[place.Position];
                switch(tokenValue[0])
                {
                    case '>': return new Token(TokenType.GreaterOrEqual, tokenValue);
                    case '<': return new Token(TokenType.LessOrEqual, tokenValue);
                    case '=': return new Token(TokenType.Equals, tokenValue);
                    case '!': return new Token(TokenType.NotEquals, tokenValue);
                }
            }
            else
            {
                switch (tokenValue[0])
                {
                    case '>': return new Token(TokenType.Greater, tokenValue);
                    case '<': return new Token(TokenType.Less, tokenValue);
                    case '=': return new Token(TokenType.Assignment, tokenValue);
                    case '!': return new Token(TokenType.Not, tokenValue);
                }
            }
            return null;
        }

        public static Token GenerateBoolOperator(string code, GetPlace place)
        {
            string tokenValue = "";
            tokenValue += code[place.Position];

            place.Position++;
            place.Column++;
            tokenValue += code[place.Position];

            switch (tokenValue[0])
            {
                case '#': return new Token(TokenType.Or, tokenValue);
                case '&': return new Token(TokenType.And, tokenValue);
            }

            return null;
        }
    }
}