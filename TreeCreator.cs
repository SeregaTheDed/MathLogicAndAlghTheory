using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLogicAndAlghTheory
{
    public static class TreeCreator
    {
        
        private static int findCloseBracketIndex(string[] parts, int indexOfOpenBracket)
        {
            int depht = 1;
            for (int i = indexOfOpenBracket+1; i < parts.Length; i++)
            {
                if (parts[i] == "(")
                    depht++;
                if (parts[i] == ")")
                    depht--;
                if (depht == 0)
                    return i;
            }
            throw new ArgumentException("Input string has non balanced bracket");
        }
        
        
        private static int findIndexTwoOperandOperationInRange(string[] parts, int startIndex, int endIndex)
        {
            int depht = 0;
            for (int i = startIndex + 1; i < parts.Length; i++)
            {
                if (parts[i] == "(")
                    depht++;
                if (parts[i] == ")")
                    depht--;
                if (depht == 0 && parts[i].isTwoOperandOperation())
                    return i;
            }
            return -1;
        }
        private static int findNextVariableIndex(string[] parts, int startIndex)
        {
            for (int i = startIndex; i < parts.Length; i++)
            {
                if (parts[i].isVariable())
                    return i;
            }
            return -1;
        }
        private static Node createVariableNode(string variableName)
        {
            return new Node
            {
                VariableName = variableName
            };
        }
        private static Node createNode(string[] parts, int startIndex, int endIndex)
        {
            if (startIndex == endIndex)
            {
                if (parts[startIndex].isVariable())
                    return createVariableNode(parts[startIndex]);
                else
                    throw new ArgumentException("Bad parsing");
            }
            else if (startIndex < endIndex)
            {
                if (parts[startIndex].isBracket())
                {
                    int operationIndex = findIndexTwoOperandOperationInRange(parts, startIndex, endIndex);
                    int closeBracketIndex = findCloseBracketIndex(parts, startIndex);
                    if (closeBracketIndex + 1 < parts.Length && parts[closeBracketIndex + 1].isVariable())
                        throw new ArgumentException("Bad parsing");
                    Node leftOperand = createNode(parts, startIndex+1, operationIndex-1);
                    Node rightOperand = createNode(parts, operationIndex + 1, closeBracketIndex-1);
                    return new Node
                    {
                        FirstOperand = leftOperand,
                        SecondOperand = rightOperand,
                        Operation = parts[operationIndex][0],
                    };
                }
                else if (parts[startIndex].isOneOperandOperation())
                {
                    return new Node
                    {
                        FirstOperand = createNode(parts, startIndex+1, findNextVariableIndex(parts, startIndex)),
                        Operation = parts[startIndex][0],
                    };
                }
                else
                {
                    throw new ArgumentException("Bad parsing");
                }
            }
            else
            {
                throw new ArgumentException("Bad parsing");
            }
        }
        public static Node createTree(string[] parts)
        {
            var partsOfInputString = parts.ToArray();
            Node root = createNode
                (
                partsOfInputString,
                0, 
                partsOfInputString.Length-1
                );

            return root;
        }
        public static Node createTree(string formula)
        {
            return createTree(Splitter.Split(formula));
        }
        private static string getVariableOfNegativeVariable(KeyValuePair<string, int> kvp, bool reverse = false)
        {
            if (reverse)
            {
                if (kvp.Value == 0)
                    return kvp.Key;
                else
                    return "-" + kvp.Key;
            }
            else
            {
                if (kvp.Value == 0)
                    return "-" + kvp.Key;
                else
                    return kvp.Key;
            }
           
        }
        private static string getPrimeConjunctiveFromTableLine(SortedDictionary<string, int> variables)
        {
            string formula = "";
            foreach (var kvp in variables)
            {
                if (formula == "")
                    formula = getVariableOfNegativeVariable(kvp);
                else
                    formula = $"({formula}&{getVariableOfNegativeVariable(kvp)})";
            }
            return formula;
        }
        public static Node createPerfectDNF(Node root)
        {
            string formula = "";
            foreach (var kvp in new Table(root).iterateTable())
            {
                if (kvp.Value == 1)
                {
                    if (formula == "")
                        formula = getPrimeConjunctiveFromTableLine(kvp.Key);
                    else
                        formula = $"({formula}|{getPrimeConjunctiveFromTableLine(kvp.Key)})";
                }
            }
            return createTree(formula);
        }

        private static string getPrimeDisjunctionFromTableLine(SortedDictionary<string, int> variables)
        {
            string formula = "";
            foreach (var kvp in variables)
            {
                if (formula == "")
                    formula = getVariableOfNegativeVariable(kvp, true);
                else
                    formula = $"({formula}|{getVariableOfNegativeVariable(kvp, true)})";
            }
            return formula;
        }
        public static Node createPerfectCNF(Node root)
        {
            string formula = "";
            foreach (var kvp in new Table(root).iterateTable())
            {
                if (kvp.Value == 0)
                {
                    if (formula == "")
                        formula = getPrimeDisjunctionFromTableLine(kvp.Key);
                    else
                        formula = $"({formula}&{getPrimeDisjunctionFromTableLine(kvp.Key)})";
                }
            }
            return createTree(formula);
        }

    }
}
