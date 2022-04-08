using System.Collections.Generic;
using System.Collections.Specialized;

namespace chomskynormalform;
using static chomskynormalform.ReadGrammar;

public class RemovingEmpty
{
    //this function takes the grammar and removes the empty productions
    public void RemovingAllEmpty()

    {
        //finding the transitions where the value is empty and the key/s corresponding
        HashSet<char> key = new HashSet<char>();
        foreach(var pair in Transitions.Where(x => x.Value.Contains("empty")))
        {
            key.Add(pair.Key);
          
        }
        
        //Removing the empty transition from the dictionary
        foreach (var pair1 in Transitions)
        {
            for(var j = 0; j < pair1.Value.Count; j++)
            {
                if (pair1.Value[j] == "empty")
                {
                    pair1.Value.RemoveAt(j);
                }
            }
        }
        
        //trying for each string in each list to check if the string contains elements that are in the array, to replace that
        //element with an string.empty

        foreach (var keyvar in Transitions)
        {
            List<string> temporary = new List<string>();
            foreach (var strings in keyvar.Value)
            {
               
                    char[] stringstochar = strings.ToCharArray();
                    //if there are common elements between the empty keys and the chars in the given string, we get those 
                    //elements and for each of them we calculate how many times they appear in the string
                    //if it's once, we just replace that element with an empty string, if it's more times, we do the same thing and then
                    //for each time it appears, we remove it
                    if (key.Overlaps(stringstochar))
                    {
                        var duplicates = key.Intersect(stringstochar);
                        foreach (var element in duplicates)
                        {
                           
                            int freq = strings.Count(f => f == element);
                            //Console.WriteLine(strings + " " + element + " " + freq);
                            
                                var newstring = strings.Replace(element.ToString(), String.Empty);
                                if (newstring == String.Empty) newstring = "empty";
                                temporary.Add(newstring);
                            

                            if(freq > 1)
                            {
                                for (int i = 0; i < strings.Length; i++)
                                {
                                    if (strings[i] == element)
                                    {
                                        var newstringfull = strings.Remove(i, 1);
                                        temporary.Add(newstringfull);
                                        
                                    }
                                }
                                
                            }

                            
                        }
                    }

                    
            }
            //Adding all the strings in the temporary list to the main Dictionary
             foreach(var value in temporary) Transitions[keyvar.Key].Add(value);
           
        }
        
       /* Console.WriteLine("These are the productions after the removal of empty strings:");
        foreach (var VARIABLE in Transitions.Keys)
        {
            List<string> list = new List<string>();
            list = Transitions[VARIABLE];
            
            for (int B = 0; B < list.Count; B++)
            {
                Console.WriteLine(VARIABLE +" " + list[B]);
            }
        }
*/
        //calling recursevly this function for the case where we end up with more empty strings
        foreach (var keyr in Transitions.Keys)
        {
            if (Transitions[keyr].Contains("empty"))
            {
                RemovingEmpty removal = new RemovingEmpty();
                removal.RemovingAllEmpty();
            }
        }

  
       
          
        
    }
    
    
}