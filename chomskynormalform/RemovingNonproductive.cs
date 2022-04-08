using static chomskynormalform.ReadGrammar;
namespace chomskynormalform;

public class RemovingNonproductive
{
    public void RemovingAllNonproductive()
    {
        Dictionary<string, bool> productive = new Dictionary<string, bool>();
        HashSet<string> nonproductive = new HashSet<string>();

        foreach (var element in nonterminal)
        {
            productive.Add(element, false);
        }

        foreach (var element1 in terminal)
        {
            productive.Add(element1, true);
        }
       
        
        //checking the nonproductive values by going through the productions 3 times
        int i = 0;
        while (i <= nonterminal.Length)
        {
            foreach (var transition in Transitions)
            {
                foreach (var strang in transition.Value)
                {
                    int count = 0;
                    foreach (var charly in strang)
                    {
                        if (productive[charly.ToString()] == false) count++;
                    }

                    if (count == 0) productive[transition.Key.ToString()] = true;
                }
            }
            i++;
        }

        foreach (var memer in productive)
        {
            if (memer.Value == false) nonproductive.Add(memer.Key);
        }
        
        foreach (var VARIABLE in Transitions.Keys)
        {
            List<string> list1 = new List<string>();
            list1 = Transitions[VARIABLE];
            if (nonproductive.Contains(VARIABLE.ToString()))
            {
                Transitions.Remove(VARIABLE);
            }
            else
            {
                for (int B = 0; B < list1.Count; B++)
                {
                    foreach (var nonproduct in nonproductive)
                    {
                        if (list1[B].Contains(nonproduct))
                        {
                            list1.RemoveAt(B);
                            break;
                        } 
                    }
                    
                } 
            }
        }
        
       /* foreach (var VARIABLE in Transitions.Keys)
        {
            List<string> list = new List<string>();
            list = Transitions[VARIABLE];
            for (int B = 0; B < list.Count; B++)
            {
                Console.WriteLine(VARIABLE + " " + list[B]);
            }
        }*/

    }
}