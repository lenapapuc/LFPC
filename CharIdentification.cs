using Lab3;

namespace Lab3
{
    public static class CharIdentification
    {
        public static bool IsDigit(this char c)
        {
            return KeyWords.Digits.Contains(c);
        }
        public static bool IsLetter(this char c)
        {
            return KeyWords.Letters.Contains(c);
        }
        public static bool IsIdentifierStart(this char c)
        {
            return c.IsLetter() || c == '_';
        }
        public static bool IsIdentifierSymbol(this char c)
        {
            return c.IsIdentifierStart() || c.IsDigit();
        }
        public static bool IsCharacter(this char c)
        {
            return !c.IsWhiteSpace() || c == ' ' || c == '\t';
        }
        public static bool IsWhiteSpace(this char c)
        {
            return c == '\n' || c == ' ' || c == '\t' || c == '\r';
        }
        public static bool IsKeyword(this string s)
        {
            return KeyWords.Keywords.ContainsKey(s);
        }
        public static bool IsPredefinedFunction(this string s)
        {
            return KeyWords.PredefinedFunctions.ContainsKey(s);
        }
        public static bool IsMathOperator(this char c)
        {
            return c == '+' || c == '-' || c == '*' || c == ':';
        }
        public static bool IsComparisonOperator(this char c)
        {
            return c == '>' || c == '<' || c == '!';
        }
        public static bool IsAssignment(this char c)
        {
            return c == '=';
        }
        public static bool IsOperator(this char c)
        {
            return c.IsMathOperator() || c.IsAssignment() || c.IsComparisonOperator();
        }
        public static bool IsParantheses(this char c)
        {
            return c == '(' || c == ')';
        }
      
        public static bool IsBoolOperatorStart(this char c)
        {
            return c == '#' || c == '&';
        }
    }
}