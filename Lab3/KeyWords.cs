using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public static class KeyWords
    {
        public const string Digits = "1234567890";
        public const string Letters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static readonly Dictionary<string, string> Keywords = new() { 
            { "Function", "FUNCTION" }, 
            { "if", "IF" }, 
            { "elseif", "ELSEIF" }, 
            { "else", "ELSE" },
            { "VAR", "VAR"},
            {"END", "END"},
            {"DO", "DO"},
            {"return", "RETURN"}
        };

        public static readonly Dictionary<string, string> PredefinedFunctions = new()
        {
            { "Write", "CONSOLEWRITE" },
            { "Main", "MAIN" },
        };
    }
}