using System.Globalization;
using System.Text;

namespace parser;
using static parser.ReadGrammar;
using static FirstLast;

public class PrecedenceMatrix
{
    public static Dictionary<string, List<string>> rule1 = new Dictionary<string, List<string>>();
    public static Dictionary<string, List<string>> rule2 = new Dictionary<string, List<string>>();
    public static Dictionary<List<string>, string> rule3 = new Dictionary<List<string>, string>();
    public static Dictionary<List<string>, List<string>> rule4 = new Dictionary<List<string>, List<string>>();
    public void PrecedenceMethod()
    {
        static Dictionary<string, List<string>> rules1()
        {
            
            foreach (var strList in Transitions.Values)
            {
                foreach (var str in strList)
                    {
                        if (str.Length > 1)
                        {
                            for (int i = 1; i < str.Length; i++)
                            {
                                if (!rule1.ContainsKey(str[i-1].ToString())) 
                                rule1.Add(str[i-1].ToString(), new List<string>());
                                rule1[str[i-1].ToString()].Add(str[i].ToString());
                            }
                        }
                    }
                
            }
            return rule1;
        }
        
        

        Dictionary<string, List<string>> rules2()
        {
            foreach (var rules in Transitions)
            {
                foreach (var str in rules.Value)
                {
                    if (str.Length > 1)
                    {
                        for (int i = 1; i < str.Length; i++)
                        {
                            if (!rule2.ContainsKey(str[i-1].ToString())) 
                                rule2.Add(str[i-1].ToString(), new List<string>());

                            if (terminal.Contains(str[i - 1].ToString()) && nonterminal.Contains(str[i].ToString()))
                            {
                                foreach (var element in First[str[i].ToString()])
                                {
                                    rule2[str[i-1].ToString()].Add(element.ToString());
                                }
                               
                            }

                            if (rule2[str[i - 1].ToString()].Count == 0) rule2.Remove(str[i - 1].ToString());
                        }
                        
                    }
                }
            }
            
            return rule2;
        }
        

        Dictionary<List<string>, string> rules3()
        {
            var builder = new StringBuilder();
            foreach (var rule in Transitions)
            {
                foreach (var str in rule.Value)
                {
                    if (str.Length > 1)
                    {
                        for (int i = 1; i < str.Length; i++)
                        {
                            if (nonterminal.Contains(str[i - 1].ToString()) && terminal.Contains(str[i].ToString()))
                            {
                                var key = new List<string>();
                                foreach (var element in Last[str[i-1].ToString()])
                                {
                                    key.Add(element.ToString());
                                    
                                }

                                if (!rule3.ContainsKey(key))
                                {
                                    rule3.Add(key, str[i].ToString());
                                    builder.Append(str[i]);
                                }
                                else
                                {
                                    builder.Append(str[i]);
                                    var alpha = builder.ToString();
                                    rule3[key] = alpha;
                                }
                            }
                            
                        }
                    }
                }
            }
            return rule3;
        }

        Dictionary<List<string>, List<string>> rules4()
        {
            foreach (var rule in Transitions)
            {
                foreach (var str in rule.Value)
                {
                    if (str.Length > 1)
                    {
                        for (int i = 1; i < str.Length; i++)
                        {
                            if (nonterminal.Contains(str[i - 1].ToString()) && nonterminal.Contains(str[i].ToString()))
                            {
                                var key = new List<string>();
                                var value = new List<string>();
                                foreach (var element in Last[str[i-1].ToString()])
                                {
                                    key.Add(element.ToString());
                                }

                                foreach (var member in First[str[i].ToString()])
                                {
                                    value.Add(member.ToString());
                                }
                                
                                if(!rule4.ContainsKey(key)) rule4.Add(key,value);
                                else
                                {
                                    var newlist = new List<string>();
                                    newlist = rule4[key].Concat(value).ToList();
                                    rule4[key] = newlist;
                                }
                            }
                            
                        }
                    }
                }
            }
            return rule4;
        }

        Console.WriteLine("Rule 1:");
        Dictionary<string, List<string>> newrule1 = rules1();
       
        foreach (var rule in newrule1)
        {
            Console.Write(rule.Key + " " + " = ");
            foreach (var member in rule.Value)
            {
                Console.Write(member + " ");
            }
            Console.WriteLine();
        }
        
        Console.WriteLine("Rule 2:");
        Dictionary<string, List<string>> newrule2 = rules2();
        foreach (var rule in newrule2)
        {
            Console.Write(rule.Key + " " + " < ");
            foreach (var member in rule.Value)
            {
                Console.Write(member + " ");
            }
            Console.WriteLine();
        }

        Console.WriteLine("Rule 3: ");
        Dictionary< List<string>, string> newrule3 = rules3();
        foreach (var rule in newrule3)
        {
          
            foreach (var member in rule.Key)
            {
                Console.Write(member + " ");
            }

            foreach (var members in rule.Value)
            {
                Console.Write(" > " + members + " " ); 
            }
            
            Console.WriteLine();
        }
        
       
        Dictionary<List<string>, List<string>> newrule4 = rules4();
        if (newrule4.Keys.Count != 0)
        {
            Console.WriteLine(" Rule 4: ");
            foreach (var rule in newrule4)
            {

                foreach (var member in rule.Key)
                {
                    Console.Write(member + " ");
                }

                Console.Write(" > ");
                foreach (var members in rule.Value)
                {
                    Console.Write(members + " ");
                }

                Console.WriteLine();
            }
        }

        
    }
    
}