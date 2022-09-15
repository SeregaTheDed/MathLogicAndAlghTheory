using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLogicAndAlghTheory
{
    public static class Constants
    {
        public static readonly char[] Sigma1 = (
            "abcdefghijklmnopqrstuvwxyz" 
            +
            "abcdefghijklmnopqrstuvwxyz".ToUpper()
            +
            "0123456789"
            ).ToCharArray();

        public static readonly char[] Sigma2 = "|&>".ToCharArray();

        //Минус имеет 1 операнд и немного другие функции => вынес в отдельную переменную
        public static readonly char[] Sigma2WithOneOperand = "-".ToCharArray(); 
        
        public static readonly char[] Sigma3 = "()".ToCharArray();

    }
}
