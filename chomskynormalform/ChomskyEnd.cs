using System.Transactions;
using static chomskynormalform.ReadGrammar;
namespace chomskynormalform;

public class ChomskyEnd
{
    public void ChomskyTransform()
    {


        char[] letters = "ABDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        //dictionary to see which new productions fit with each new key
        Dictionary<string, char> added = new Dictionary<string, char>();

        //trimming the letters so there isn't a letter that is already a key
        for (int j = 0; j < letters.Length; j++)
        {
            foreach (var trans in Transitions.Keys)
            {
                if(trans == letters[j]) 
                {letters = letters.Where(f => f!=letters[j]).ToArray();}
            }
            
        }

        //giving the terminal symbols their own replacement
        for (int i = 0; i < terminal.Length; i++)
        {
            if (!added.ContainsKey(terminal[i]))
            {
                List<string> list1 = new List<string>();
                list1.Add(terminal[i]);
                Transitions.Add(letters[i], list1);
                added.Add(terminal[i], letters[i]);
                letters = letters.Where(f => f != letters[i]).ToArray();
            }
        }

        foreach (var transition in Transitions)
        {
            List<string> temporary = new List<string>();
            List<string> toDelete = new List<string>();
            string newvar = String.Empty;
            //if the length of a string is more than 1, all the terminals have to be replaced with their just introduced 
            //variables
            foreach (var var in transition.Value)
            {
                if (var.Length > 1 )
                {
                    foreach (var letter in var)
                    {
                        if (terminal.Contains(letter.ToString()))
                        {
                           toDelete.Add(var);
                           newvar = var.Replace(letter, added[letter.ToString()]);
                           temporary.Add(newvar);
                           toDelete.Add(var);
                        }
                    }
                }
                
            }

            foreach (var element in temporary)
            {
                Transitions[transition.Key].Add(element);
            }

            foreach (var delete in toDelete)
            {
                Transitions[transition.Key].Remove(delete);
            }
        }

        Dictionary<char, List<string>> TransitionsThreeorMore(Dictionary<char, List<string>> str)
        {
            foreach (var list in str)
            {
                List<string> temporary = new List<string>();
                List<string> toDelete = new List<string>();
                string newString = String.Empty;
                foreach (var str1 in list.Value)
                {
                    if (str1.Length > 2)
                    {
                        if (str1.Length % 2 == 0)
                        {
                            for (int i = 0; i < str1.Length; i += 2)
                            {
                                if (!added.ContainsKey(str1.Substring(i, 2)))
                                {
                                    added.Add(str1.Substring(i, 2), letters[i]);
                                    letters = letters.Where(f => f != letters[i]).ToArray();
                                    newString = str1.Remove(i, 2);
                                    newString = newString.Insert(i, added[str1.Substring(i, 2)].ToString());
                                    
                                }
                            }
                        }

                        else
                        {
                            
                            for (int i = 0; i < str1.Length-1; i += 2)
                            {
                                if (!added.ContainsKey(str1.Substring(i, 2)))
                                {
                                    added.Add(str1.Substring(i, 2), letters[i]);
                                    letters = letters.Where(f => f != letters[i]).ToArray();
                                    newString = str1.Remove(i, 2);
                                    newString = newString.Insert(i, added[str1.Substring(i, 2)].ToString());
                                    
                                }
                            }

                            

                        }
                        
                        toDelete.Add(str1);
                        temporary.Add(newString);
                       
                    }
                    
                }

                foreach (var val in temporary)
                {
                   str[list.Key].Add(val);
                }
                foreach (var val in toDelete)
                {
                    str[list.Key].Remove(val);
                }
            }

            foreach (var pair in str)
            {
                foreach (var val in pair.Value)
                {
                    if (val.Length >= 3)
                    {
                        TransitionsThreeorMore(str);
                        break;
                    }
                }
            }

            foreach (var member in added)
            {
                if (!str.ContainsKey(member.Value))
                {
                    List<string> temp = new List<string>();
                    temp.Add(member.Key);
                    str.Add(member.Value,temp);
                }
            }
            return str;
            
        }

        Transitions = TransitionsThreeorMore(Transitions);

        Console.WriteLine("This is the final version");
        foreach (var VARIABLE in Transitions.Keys)
        {
            List<string> list = new List<string>();
            list = Transitions[VARIABLE];
            for (int B = 0; B < list.Count; B++)
            {
                Console.WriteLine(VARIABLE + " " + list[B]);
            }
        }
    }

    public void DeleteSame()
    {
        foreach (var couple in Transitions)
        {
            List<string> distinct = couple.Value.Distinct().ToList();
           
           
            Transitions[couple.Key] = distinct;

        }
    }
    
}