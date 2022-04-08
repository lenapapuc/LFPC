namespace chomskynormalform;
using static RemovingEmpty;
using static ReadGrammar;

public class RemovingRenamings
{
    public void RemovingAllRenamings()
    {
        foreach (var transition in Transitions)
        {
            List<string> temporary = new List<string>();
            List<string> toDelete = new List<string>();
            foreach (var value in transition.Value)
            {
                if (nonterminal.Contains(value))
                {
                    toDelete.Add(value);
                    foreach (var stringy in Transitions[Char.Parse(value)])
                    {
                        temporary.Add(stringy);
                        

                    }

                }
            }

            foreach (var VARIABLE in temporary)
            {
                Transitions[transition.Key].Add(VARIABLE);
            }
            foreach (var VARIABLE in toDelete)
            {
                Transitions[transition.Key].Remove(VARIABLE);
            }

        }

        foreach (var transition in Transitions)
        {
            foreach (var st in transition.Value)
            {
                if (nonterminal.Contains(st)) RemovingAllRenamings();
                break;
            }
        }
        

     /*   Console.WriteLine("These are the productions after removing renamings:");

        foreach (var VARIABLE in Transitions.Keys)
        {
            List<string> list = new List<string>();
            list = Transitions[VARIABLE];
            for (int B = 0; B < list.Count; B++)
            {
                Console.WriteLine(VARIABLE + " " + list[B]);
            }
        }
*/

    }



}

