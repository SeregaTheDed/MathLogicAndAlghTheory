using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLogicAndAlghTheory
{
    public static class Validator
    {
        private static bool checkBracketBalance(string line)
        {
            Stack<char> stack = new Stack<char>();
            foreach (var symbol in line)
            {
                if (symbol == '(')
                    stack.Push(symbol);
                else if (symbol == ')')
                {
                    if (stack.Count == 0)
                        return false;
                    if (stack.Pop() != '(')
                        return false;
                }
            }
            return stack.Count == 0;
        }
        public static bool Validate(string input)
        {
            if (!checkBracketBalance(input))
            {
                return false;
            }
            try
            {
                TreeCreator.createTree(Splitter.Split(input));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
