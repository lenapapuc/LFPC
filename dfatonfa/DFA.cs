namespace Lab2;

using static Lab2.Program;
using static Lab2.Node;

public class DFA
{
     public static void PrindDFA()
        {
            for (int i = 0; i < E.Length; i++)
            {
                Console.Write("\t\t");
                Console.Write(E[i]);
            }
            Console.WriteLine();
            foreach(var transition in Program.DFA)
            {
            
                    
               if (transition.Key.Contains("-"))
                {
                    var transitions = transition.Key.Split("-");
                    var printed = false;
                    foreach(var tr in transitions)
                    {
                        if (start.Contains(tr) && final.Contains(tr))
                        {
                            Console.Write("->*");
                            foreach (var startEndTransition in transitions)
                                Console.Write(startEndTransition);
                            printed = true;
                            break;
                        }
                        else if (start.Contains(tr))
                        {
                            Console.Write("->");
                            foreach (var startEndTransition in transitions)
                                Console.Write(startEndTransition);
                            printed = true;
                            break;
                        }
                        else if (final.Contains(tr))
                        {
                            Console.Write("*");
                            foreach (var startEndTransition in transitions)
                                Console.Write(startEndTransition);
                            printed = true;
                            break;
                        }

                    }
                    if(!printed)
                    {
                        
                        foreach (var startEndTransition in transitions)
                           
                                Console.Write(startEndTransition);
                        
                    }
                }
                else
                {
                   
                    if (start.Contains(transition.Key) && final.Contains(transition.Key))
                    {
                        Console.Write($"->*{transition.Key}");

                    }
                    if (start.Contains(transition.Key))
                    {
                        Console.Write($"->{transition.Key}");

                    }
                    else if (final.Contains(transition.Key))
                    {
                        Console.Write($"*{transition.Key}");
                    }
                    else
                    {
                        Console.Write($"{transition.Key}");
                    }
                }
                
                for (int j = 0; j < E.Length; j++)
                {
                    foreach (var tr in transition.Value)
                    {
                       
                            if (tr.Value == E[j])
                            {
                                Console.Write("\t\t");
                                Console.Write(tr.NextState);
                            }
                        
                    }
                }
                Console.WriteLine();
            }
        }
}