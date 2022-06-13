namespace parser;
using static parser.ReadGrammar;

public class FirstLast
{
   public static Dictionary<string, HashSet<char>> First = new Dictionary<string, HashSet<char>>();
   public static Dictionary<string, HashSet<char>> Last = new Dictionary<string, HashSet<char>>();

   public void FirstLastMethod()
   {
      HashSet<char> GetLetterWhole(string letterwhole, string firstorlast)
      {
         HashSet<char> lt = new HashSet<char>();
         HashSet<char> ft = new HashSet<char>();
         Dictionary<string, bool> studied = new Dictionary<string, bool>();
         foreach (var VARIABLE in nonterminal)
         {
            studied.Add(VARIABLE, false);
         }

         //method
         void GetLetter(string letter)
         {
            
            foreach (var VARIABLE in Transitions[char.Parse(letter)])
            {
               switch (firstorlast)
               {
                  case "first":
                     ft.Add(VARIABLE[0]); 
                     break;
                  case "last":
                     lt.Add(VARIABLE[^1]);
                     break;
               }
               
               studied[letter] = true;
            }

            foreach (var VARIABLE in Transitions[char.Parse(letter)])
            {
               switch (firstorlast)
               {
                  case "first":
                     if (nonterminal.Contains<string>(VARIABLE[0].ToString()) && studied[VARIABLE[0].ToString()] == false)
                     {
                        GetLetter(VARIABLE[0].ToString());
                     }
                     break;
                  case "last":
                     if (nonterminal.Contains<string>(VARIABLE[^1].ToString()) && studied[VARIABLE[^1].ToString()] == false)
                     {
                        GetLetter(VARIABLE[^1].ToString());
                     }
                     break;
               }
               
            }

         }
         GetLetter(letterwhole);
         HashSet<char> result = new HashSet<char>();
         switch (firstorlast)
         {
            case "first":
               result = ft;
            break;
            case "last":
               result = lt;
            break;
         }

         return result;
      }
      
      foreach (var letter in nonterminal)
      {
         HashSet<char> news = GetLetterWhole(letter, "first");
         First[letter] = news;
         HashSet<char> morenews = GetLetterWhole(letter, "last");
         Last[letter] = morenews;
      }
      Console.WriteLine("First");
      foreach (var var in First)
      {
         Console.Write(var.Key + " " + "=" + " ");
         foreach (var VARIABLE in var.Value)
         {
            Console.Write(VARIABLE + " ");
         }

         Console.WriteLine();
      }
      
      Console.WriteLine("Last");
      foreach (var var in Last)
      {
         Console.Write(var.Key + " " + "=" + " ");
         foreach (var VARIABLE in var.Value)
         {
            Console.Write(VARIABLE + " ");
         }

         Console.WriteLine();
      }
   }
}