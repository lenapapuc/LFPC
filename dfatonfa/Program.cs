namespace Lab2;

public class Program
{

    static readonly string textFile = @"C:\Users\User\RiderProjects\Solution1\dfatonfa\input.txt";
    public static string[] Q;
    public static string[] E;
    public static string[] start;
    public static string[] final;
    public static Dictionary<string, List<Node>> NFA = new Dictionary<string, List<Node>>();
    public static Queue<string> Transitions = new Queue<string>();

    public static Dictionary<string, List<Node>> DFA = new Dictionary<string, List<Node>>();

     public static void Parse()
               {
                   using (StreamReader file = new StreamReader(textFile))
                   {
                       int counter = 0;
                       string ln;
       
                       while ((ln = file.ReadLine()) != null)
                       {
                           counter++;
                           switch (counter)
                           {
                               case 1:
                                   Q = ln.Split(' ');
                                   
                                   break;
                               case 2:
                                   E = ln.Split(' ');
                                   
                                   break;
                               case 3:
                                   final = ln.Split(" ");
                                   
                                   break;
                               case 4:
                                   start = ln.Split(" ");
                                   foreach (var s in start)
                                   {
                                       Transitions.Enqueue(s);
                                   }
                                   break;
                               default:
       
                                   var transformations = ln.Split('=');
                                   if (!NFA.ContainsKey(transformations[0].Split('-')[0]))
                                       NFA.Add(transformations[0].Split('-')[0], new List<Node>());
                                   NFA[transformations[0].Split("-")[0]].Add(new Node
                                   {
                                       NextState = transformations[1],
                                       Value = transformations[0].Split('-')[1]
                                   });


                                   break;

                           }
                          // NFA.ToList().ForEach(x=> Console.WriteLine(x));

                       }
                       file.Close();
                   }
               }
   

    public static void ConvertNFAtoDFA()
    {
        while (Transitions.Any())
        {
            if (!DFA.ContainsKey(Transitions.Peek()))
            {
                DFA.Add(Transitions.Peek(), new List<Node>());
            }

//Console.WriteLine(E.Length);
                for (int i = 0; i < E.Length; i++)
                {
                    var currentTransition = new List<Node>();
                    var trans = Transitions.Peek().Split('-');
                    if (trans.Count() > 1)
                    {
                        foreach (var tr in trans)
                        {
                            if (tr != "X")
                            {
                                foreach (var transition in NFA[tr].Where(x => x.Value == E[i]).ToList())
                                    currentTransition.Add(transition);
                            }
                        }
                    }
                

                if (NFA.ContainsKey(Transitions.Peek()))
                {

                    currentTransition = NFA[Transitions.Peek()].Where(x => x.Value == E[i]).ToList();
                    // foreach (var HE in currentTransition)
                    // {
                    //     Console.WriteLine(HE.NextState + " " + HE.Value);
                    // }
                    //
                }

                //-----new q0q1 reunion needs to be made
                if (currentTransition.Count() > 0)
                {
                    var newState = String.Concat(currentTransition.Select(x => x.NextState + "-"));
                    newState = newState.TrimEnd('-');
                    DFA[Transitions.Peek()].Add(new Node
                    {
                        NextState = newState,
                        Value = E[i]
                    });
                    if (!DFA.ContainsKey(newState))
                    {
                        Transitions.Enqueue(newState);
                        DFA.Add(newState, new List<Node>());
                    }
                }
                else
                {
                    if (!DFA.ContainsKey("X"))
                    {
                        Transitions.Enqueue("X");
                        DFA.Add("X", new List<Node>());
                    }

                    DFA[Transitions.Peek()].Add(new Node
                    {
                        NextState = "X",
                        Value = E[i]
                    });
                }
            }

            Transitions.Dequeue();
        }
    }
    
  
    
}
