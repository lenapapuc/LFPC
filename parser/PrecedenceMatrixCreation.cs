namespace parser;
using static ReadGrammar;
using static PrecedenceMatrix;
public class PrecedenceMatrixCreation
{
   public static Dictionary<string, Dictionary<string, string>> matrix = new Dictionary<string, Dictionary<string, string>>();
   public void Creation()
   {
      
      foreach (var element in nonterminal)
      {
         matrix.Add(element, new Dictionary<string, string>());
      }

      foreach (var elemn in terminal)
      {
         matrix.Add(elemn, new Dictionary<string, string>());
      }

      matrix.Add("$", new Dictionary<string, string>());

      foreach (var value in matrix.Values)
      {
         foreach (var eme in nonterminal)
         {
            value.Add(eme, string.Empty);
         }

         foreach (var eme in terminal)
         {
            value.Add(eme, string.Empty);
         }

         value.Add("$", String.Empty);
      }

      //filling the matrix with rule 1
      foreach (var key in rule1.Keys)
      {
         foreach (var alsokey in matrix.Keys)
         {
            if (key == alsokey)
            {
               foreach (var value in rule1[key])
               {
                  foreach (var k in matrix[alsokey].Keys)
                  {
                     foreach (var individualvalue in value)
                     {
                        if (individualvalue.ToString() == k)
                           matrix[alsokey][k] = "=";
                     }

                  }
               }
            }
         }
      }

      //filling in the matrix with rule 2
      foreach (var key in rule2.Keys)
      {
         foreach (var alsokey in matrix.Keys)
         {
            if (key == alsokey)
            {
               foreach (var value in matrix[alsokey].Keys)
               {
                  foreach (var element in rule2[key])
                  {
                     if (value == element) matrix[alsokey][value] = "<";
                  }
               }
            }
         }
      }

      //placing the signs for the dollar sign rows
      foreach (var key in matrix.Keys)
      {
         if (key == "$")
         {
            foreach (var alsokey in matrix[key].Keys)
            {
               if (alsokey != "$")
                  matrix[key][alsokey] = "<";
            }
         }
      }

      //placing the signs for dollar sign columns
      foreach (var key in matrix.Keys)
      {
         if (key != "$")
         {
            foreach (var alsokey in matrix[key].Keys)
            {
               if (alsokey == "$")
                  matrix[key][alsokey] = ">";
            }
         }
      }

      //filling in matrix with rule 3
      foreach (var list in rule3.Keys)
      {
         foreach (var str in list)
         {
            foreach (var key in matrix.Keys)
            {
               if (str == key)
               {
                  foreach (var st in matrix[key].Keys)
                  {
                     if (st == rule3[list]) matrix[key][st] = ">";
                  }
               }
            }
         }
      }

      //filling in for rule 4
      foreach (var list in rule4.Keys)
      {
         foreach (var str in list)
         {
            foreach (var key in matrix.Keys)
            {
               if (key == str)
               {
                  foreach (var st in matrix[key].Keys)
                  {
                     foreach (var st1 in rule4[list])
                     {
                        if (st == st1) matrix[key][st1] = ">";
                     }
                  }
               }
            }
         }
      }

     // Console.WriteLine("RES");
     // int count = 0;
      /*
      foreach (var pair in matrix)
      {
        
         foreach (var alsopair in pair.Value)
         {
            Console.Write(pair.Key + " ");
            Console.Write(alsopair.Key + " ");
            Console.WriteLine(alsopair.Value);
            if (alsopair.Value == ">") count++;
         }
      
      }
      Console.WriteLine(count);
      
      */
      
      //display the matrix
      Console.Write("   ");
      foreach (var KEY in matrix.Keys)
      {
         Console.Write(" " + KEY +"|");
      }
      Console.WriteLine();
   

      foreach (var key in matrix.Keys)
      {
         Console.Write(key+ "| ");
         foreach (var val in matrix[key].Keys)
         {
            if(matrix[key][val] == string.Empty) Console.Write("  |");
            else Console.Write(matrix[key][val] + " |");
         }
         Console.WriteLine();
      }
   }
}