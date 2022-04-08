using static chomskynormalform.ReadGrammar;
namespace chomskynormalform;

public class RemovingUnreachable
{
    public void RemovingAllUnreachable()
    {
        Dictionary<string, bool> reachable = new Dictionary<string, bool>();
        HashSet<string> unreachable = new HashSet<string>();
        foreach (var element in nonterminal)
        {
            if (element == "S") reachable.Add(element, true);
            else reachable.Add(element, false);
        }

        foreach (var transition in Transitions)
        {
            foreach (var stringalitious in transition.Value)
            {
                foreach (var letter in nonterminal)
                {
                    if (stringalitious.Contains(letter))
                    {
                        reachable[letter] = true;
                    }
                }
            }

            if (reachable[transition.Key.ToString()] == false)
            {
                Transitions.Remove(transition.Key);
                unreachable.Add(transition.Key.ToString());
                nonterminal = nonterminal.Where( index => char.Parse(index)!= transition.Key).ToArray();
                reachable.Remove(transition.Key.ToString());
            }
          
        }

        foreach (var list1 in Transitions.Values)
        {
            for (int i = 0; i < list1.Count; i++)
            {
                foreach (var elem in unreachable)
                {
                    if (list1[i].Contains(elem)) list1.RemoveAt(i);
                }
            }
        }

       //recalling the function for all the times there are unreachable values
        while (reachable.Values.Contains(false))
        {
            RemovingUnreachable removing = new RemovingUnreachable();
            removing.RemovingAllUnreachable();
            
        }
        
        /*foreach (var VARIABLE in Transitions.Keys)
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