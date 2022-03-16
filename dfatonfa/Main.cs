using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab2
{
class Start
{
 
        static void Main(string[] args)
        {
            var program = new Program();
            Program.Parse();
            Program.ConvertNFAtoDFA();
            Console.WriteLine("This is the NFA: ");
            NFA.PrintNFA();
            Console.Write('\n');
            Console.WriteLine("This is the DFA" );
            DFA.PrindDFA();
            
            
        }
    }
}