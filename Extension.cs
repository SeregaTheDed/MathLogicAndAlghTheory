using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLogicAndAlghTheory
{
    public static class StringExtension
    {
        public static bool isOperation(this string line)
        {
            if (line.Length > 1)
                return false;
            return 
                Constants.Sigma2.Contains(line[0]) 
                || 
                Constants.Sigma2WithOneOperand.Contains(line[0]);
        }
        public static bool isOneOperandOperation(this string line)
        {
            if (line.Length > 1)
                return false;
            return Constants.Sigma2WithOneOperand.Contains(line[0]);
        }
        public static bool isTwoOperandOperation(this string line)
        {
            if (line.Length > 1)
                return false;
            return Constants.Sigma2.Contains(line[0]);
        }
        public static bool isVariable(this string line)
        {
            foreach (var symbol in line)
            {
                if (!Constants.Sigma1.Contains(symbol))
                    return false;
            }
            return true;
        }
        public static bool isBracket(this string line)
        {
            if (line.Length > 1)
                return false;
            return Constants.Sigma3.Contains(line[0]);
        }
        public static char getReversedOperation(this char operation)
        {
            if (operation == '|')
                return '&';
            else
                return '|';
        }
    }
}
