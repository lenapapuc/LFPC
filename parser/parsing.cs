using System.Text;

namespace parser;
using static PrecedenceMatrixCreation;
using static ReadGrammar;

public class Parsing
{
    public void ParsingMethod()
    {
        var input = "abcdeabcccd$";
        Stack<string> stack = new Stack<string>();
        stack.Push("$");
        stack.Push(input[0].ToString());
        var newinput = input.Remove(0,1);
        Console.WriteLine(newinput);

        while (  stack.Peek() !="S" )
        {
            string SPrelation = matrix[stack.Peek()][newinput[0].ToString()];
            if (SPrelation is "<" or "=")
            {
                stack.Push(newinput[0].ToString());
                newinput = newinput.Remove(0,1);
            }
            else if (SPrelation == ">")
            {

                StringBuilder handle = new StringBuilder();
                handle.Append(stack.Peek());
                stack.Pop();
                
                while (matrix[stack.Peek()][handle.ToString()[^1].ToString()] is not "<" or ">")
                {
                    handle.Append(stack.Peek());
                    stack.Pop();
                    
                }

                string ne= String.Empty;
                if (handle.ToString().Length > 1)
                {
                    var charst = handle.ToString().ToCharArray();
                    var c = charst.Reverse();
                    ne = new string(c.ToArray());
                }
                else ne = handle.ToString();
                var myKEY = Transitions.FirstOrDefault(x => x.Value.Contains(ne)).Key;
                StringBuilder neweststring = new StringBuilder();
                neweststring.Append(myKEY);
                neweststring.Append(newinput);
                newinput = neweststring.ToString();
               
            }

          /*  Console.Write("This is the stack: ");
            foreach(string str in stack)
            {
                // Using ToString() method
                Console.Write(str.ToString());
            }
                Console.WriteLine();*/
            Console.WriteLine( newinput);
        } 
    }
}