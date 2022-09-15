using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLogicAndAlghTheory
{
    public sealed class Validator
    {
        private readonly bool _debug;
        private readonly string _debugSeparator = "------------------------------";
        public Validator(bool withDebugPrint = false)
        {
            _debug = withDebugPrint;
        }
        private bool checkString(string line)
        {
            if (line.isBracket())
            {
                return true;
            }
            else if (line.isOperation())
            {
                return true;
            }
            else if (line.isVariable())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool checkPartsOnSigmaNumber(string[] partsOfInputString)
        {
            if (_debug)
            {
                Console.WriteLine("Check parts on sigma number");
            }
            foreach (var part in partsOfInputString)
            {
                if (!checkString(part))
                {
                    if (_debug)
                        Console.WriteLine($"Bad string: {part}");
                    return false;
                }
                else if (_debug)
                {
                    Console.WriteLine($"{part} - OK");
                }

            }
            if (_debug)
            {
                Console.WriteLine(_debugSeparator);
            }
            return true;
        }
        private bool checkBracketBalance(string line)
        {
            Stack<char> stack = new Stack<char>();
            if (_debug)
                Console.WriteLine("Check bracket balance:");
            foreach (var symbol in line)
            {
                if (_debug)
                    Console.Write(symbol);
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
            Console.WriteLine();
            if (_debug)
                Console.WriteLine(_debugSeparator);
            return stack.Count == 0;
        }
        private bool checkOperations(string[] parts)
        {
            if (_debug)
                Console.WriteLine("Check parts on twice operations");
            for (int i = 0; i < parts.Length-1; i++)
            {
                if (parts[i].isOperation() && parts[i + 1].isOperation())
                {
                    if (_debug)
                        Console.WriteLine(parts[i] + parts[i+1] + " - bad string");
                    return false;
                }
                else
                {
                    Console.WriteLine(parts[i] + parts[i + 1] + " - OK");
                }
            }
            Console.WriteLine(_debugSeparator);
            return true;
        }
        public bool Validate(string input)
        {
            if (_debug)
            {
                Console.WriteLine("Original string:");
                Console.WriteLine(input);
                Console.WriteLine(_debugSeparator);
            }
            string[] partsOfInputString = Splitter.Split(input);
            if (_debug)
            {
                Console.WriteLine("Splitted string:");
                Console.WriteLine(String.Join(" ", partsOfInputString));
                Console.WriteLine(_debugSeparator);
            }
            
            if (!checkPartsOnSigmaNumber(partsOfInputString))
                return false;
            
            if (!checkBracketBalance(String.Join("", partsOfInputString)))
            { 
                if (_debug)
                    Console.WriteLine(" - Bad balance");
                return false; 
            }
            
            if (!checkOperations(partsOfInputString))
                return false;


            return true;
        }
    }
}
