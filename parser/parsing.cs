using System.Text;

namespace parser;
using static PrecedenceMatrixCreation;
using static ReadGrammar;

public class Parsing
{
    public void ParsingMethod()
    {
        var input = "$abcdeabcccd";
        Stack<string> stack = new Stack<string>();
        stack.Push("$");
        stack.Push(input[^1].ToString());
        var newinput = input.Remove(input.Length-1,1);
        Console.WriteLine(newinput);
        var stackstring = string.Empty;

        while (  stackstring !="S$" || newinput != "$" )
        {
           
            string SPrelation = matrix[newinput[^1].ToString()][stack.Peek()];
            if (SPrelation is ">" or "=")
            {
                stack.Push(newinput[^1].ToString());
                newinput = newinput.Remove(newinput.Length-1,1);
            }
            else if (SPrelation == "<")
            {

                StringBuilder handle = new StringBuilder();
                handle.Append(stack.Peek());
                stack.Pop();
                //Console.WriteLine(handle.ToString());

                while (matrix[handle.ToString()[^1].ToString()][stack.Peek()] is not "<" && matrix[handle.ToString()[^1].ToString()][stack.Peek()] is not ">" )
                {
                    handle.Append(stack.Peek());
                    stack.Pop();
                  // newinput = newinput.Remove(newinput.Length - 1, 1);

                } //Console.WriteLine(handle.ToString());*/
                string ne= String.Empty;
               if (handle.ToString().Length > 1)
                {
                    var charst = handle.ToString().ToCharArray();
                    var c = charst.Reverse();
                    ne = new string(c.ToArray());
                }
                
                 ne = handle.ToString();
               //Console.WriteLine(ne);
                var myKEY = Transitions.FirstOrDefault(x => x.Value.Contains(ne)).Key;
              //  Console.WriteLine(myKEY);
                StringBuilder neweststring = new StringBuilder();
                
                neweststring.Append(newinput);
                neweststring.Append(myKEY);
                newinput = neweststring.ToString();

            }
            else 
            {
               Console.WriteLine("String not accepted!"); 
               break;
            }
            
            if (stackstring == "S$" && newinput == "$")
            {
                Console.WriteLine("String accepted!");
            }
          
            /*Console.Write("This is the stack: ");
            foreach(string str in stack)
            {
                // Using ToString() method
                Console.Write(str.ToString());
            }
         Console.WriteLine();*/
             stackstring = string.Join("", stack);
             Console.WriteLine(newinput);
             //Console.WriteLine("THIS IS STACK STRING " + stackstring);
             if(stackstring == "S$" && newinput == "$") Console.WriteLine("String accepted");
        } 
    }
}