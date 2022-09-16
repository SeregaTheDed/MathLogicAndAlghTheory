using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLogicAndAlghTheory
{
    public static class Splitter
    {
        private static int getSigmaNumber(char symbol)
        {
            char[][] sets = 
            { 
                Constants.Sigma1,
                Constants.Sigma2,
                Constants.Sigma2WithOneOperand,
                Constants.Sigma3,
            };
            for (int i = 0; i < sets.Length; i++)
            {
                for (int j = 0; j < sets[i].Length; j++)
                {
                    if (sets[i][j] == symbol)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        private static string getStringWithAdditionalSymbols(int lastSigmaNumber, int currentSigmaNumber,string symbol)
        {
            if (currentSigmaNumber == getSigmaNumber('('))
                return " " + symbol;
            if (currentSigmaNumber != lastSigmaNumber || lastSigmaNumber == getSigmaNumber('-') && currentSigmaNumber == getSigmaNumber('-'))
                return " " + symbol;
            return symbol;
        }
        public static string[] Split(string input)
        {
            StringBuilder inputWithSpaces = new();
            int lastSigmaNumber = getSigmaNumber(input[0]);
            if (lastSigmaNumber == -1)
                throw new ArgumentException("The input string has an unregistered character");
            foreach (var symbol in input)
            {
                int currentSigmaNumber = getSigmaNumber(symbol);
                if (currentSigmaNumber == -1)
                    throw new ArgumentException("The input string has an unregistered character");
                string modificatedChar = getStringWithAdditionalSymbols(
                    lastSigmaNumber,
                    currentSigmaNumber,
                    symbol.ToString()
                    );
                inputWithSpaces.Append(modificatedChar);
                
                lastSigmaNumber = currentSigmaNumber;
            }
            return inputWithSpaces.ToString().Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
        }
        public static string[] SplitWithReplace(string input, Dictionary<string, int> keyValuePairs)
        {
            string[] splittedString = Split(input);
            foreach (var keyValuePair in keyValuePairs)
            {
                string variableName = keyValuePair.Key;
                int variableValue = keyValuePair.Value;
                if (variableValue > 1 || variableValue < 0)
                    throw new ArgumentException($"{variableValue} not included in (0,1) set");
                for (int i = 0; i < splittedString.Length; i++)
                {
                    if (splittedString[i] == variableName)
                        splittedString[i] = "" + variableValue;

                }
            }
            return splittedString;
        }
    }
}
