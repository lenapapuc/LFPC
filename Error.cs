namespace Lab3
{
    public class GetPlace
    {
        public int Line { get; private set; } = 1;
        public int Column { get; set; } = -1;
        public int Position { get; set; }
        public void AddLine()
        {
            Line++;
            Column = -1;
        }
    }
    public class TheError
    {
        public string Value { get; set; }

        public TheError(string value)
        {
            Value = value;
        }
        public static TheError UnexpectedSymbol(GetPlace place, char symbol)
        {
            return new TheError($"Unexpected symbol: {symbol} at line {place.Line}, column {place.Column}");
        }
        
       
        public override string ToString()
        {
            return $"Error: {Value}";
        }
    }
}