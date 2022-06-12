namespace parser;
public class ReadGrammar
{
    public static int counter = 0;
    public static Dictionary<char, List<string>> Transitions = new Dictionary<char, List<string>>();
    public static string[] nonterminal; public static string[] terminal;
    
    public void ReadGrammarAll()
    { 
     
        
        foreach (string line in System.IO.File.ReadLines(@"C:\Users\User\RiderProjects\Solution1\parser\parser.txt"))
        {
            string[] separatingstrings = {"{", "}", ", ", " = "};
            switch (counter)
            {
                case (0):
                {
                    nonterminal = line.Split(separatingstrings, System.StringSplitOptions.RemoveEmptyEntries);
                    nonterminal = nonterminal.Where(w => w != "VN").ToArray();
                    break;
                }

                case(1):
                {
                    terminal = line.Split(separatingstrings, StringSplitOptions.RemoveEmptyEntries);
                    terminal =  terminal.Where(w => w != "VT").ToArray();
                    break;
                }
                default:
                {
                    string[] transformations = line.Split(" -> ");
                    char keys = Char.Parse(transformations[0]);
                    if (!Transitions.ContainsKey(keys))
                    {
                        Transitions.Add(keys, new List<string>());
                    }
                    Transitions[keys].Add(transformations[1]);

                    break;
                }
            }
            
            counter++;  
        }
    }
}

