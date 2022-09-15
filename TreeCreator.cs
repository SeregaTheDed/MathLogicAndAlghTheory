using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLogicAndAlghTheory
{
    public static class TreeCreator
    {
        public class Node
        {
            public bool isVariable
            {
                get
                {
                    return this.VariableName != null;
                }
            }
            public bool isOperation
            {
                get
                {
                    return this.FirstOperand != null && this.SecondOperand != null;
                }
            }
            public bool isOperationWithOneOperand
            {
                get
                {
                    return this.FirstOperand != null && this.SecondOperand == null;
                }
            }
            public char? Operation { get; set; }
            public Node? FirstOperand { get; set; }
            public Node? SecondOperand { get; set; }
            public string? VariableName { get; set; }
            private void printTreeRecursy(Node node, int depth)
            {
                char separator = ' ';
                if (node.isVariable)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(new String(separator, depth*2) + node.VariableName); 
                }
                else if (node.isOperationWithOneOperand)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(new String(separator, depth * 2) + node.Operation);
                    printTreeRecursy(node.FirstOperand, depth+1);
                }
                else
                {
                    printTreeRecursy(node.FirstOperand, depth + 1);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(new String(separator, depth * 2) + node.Operation);
                    printTreeRecursy(node.SecondOperand, depth + 1);
                }
            }
            public void printTree()
            {
                var originalConsoleColor = Console.ForegroundColor;
                printTreeRecursy(this, 0);
                Console.ForegroundColor = originalConsoleColor;
            }
        }
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
        

    }
}
