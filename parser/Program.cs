namespace parser;
using static parser.ReadGrammar;
public class Program
{
    static void Main(string[] args)
    {
        ReadGrammar readGrammar = new ReadGrammar();
        readGrammar.ReadGrammarAll();
        FirstLast firstLast = new FirstLast();
        firstLast.FirstLastMethod();
        PrecedenceMatrix precedenceMatrix = new PrecedenceMatrix();
        precedenceMatrix.PrecedenceMethod();
        PrecedenceMatrixCreation precedenceMatrixCreation = new PrecedenceMatrixCreation();
        precedenceMatrixCreation.Creation();
        Parsing parsing = new Parsing();
        parsing.ParsingMethod();
        /*foreach (var VARIABLE in Transitions.Keys)
        {
            List<string> list = new List<string>();
            list = Transitions[VARIABLE];
             
            for (int B = 0; B < list.Count; B++)
            {
                Console.WriteLine(VARIABLE +" " + list[B]);
            }
        }*/
    }
}