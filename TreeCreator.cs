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
        private static Node createVariable(string variableName)
        {
            return new Node
            {
                VariableName = variableName,
            };
        }
        private static Node createOneOperandNode(string[] parts, int startIndex, int endIndex, char operation)
        {
            if (startIndex == endIndex)
            {
                Node node = createVariable(parts[startIndex]);
                node.Operation = operation;
                return node;
            }
            else
            {
                return new Node
                {
                    Operation = operation,
                    FirstOperand = createNode(parts, startIndex, endIndex),
                };
            }
            
        }
        private static int findIndexOperationInRange(string[] parts, int startIndex, int endIndex)
        {
            int depht = 1;
            for (int i = startIndex + 1; i < parts.Length; i++)
            {
                if (parts[i] == "(")
                    depht++;
                if (parts[i] == ")")
                    depht--;
                if (depht == 0 && parts[i].isOperation())
                    return i;
            }
            return -1;
        }
        private static Node createNode(string[] parts, int startIndex, int endIndex)
        {
            char Operation = ' ';
            Node? leftNode = new Node();
            Node? rightNode = null;
            int twoOperandOperationIndex = findIndexOperationInRange(parts, startIndex, endIndex);
            if (twoOperandOperationIndex == -1)
            {
                if (parts[startIndex].isVariable())
                {
                    leftNode = createVariable(parts[startIndex]);
                }
                else if (parts[startIndex].isOneOperandOperation())
                {
                    if (parts[startIndex + 1].isBracket())
                    {
                        leftNode = createOneOperandNode(
                            parts,
                            startIndex + 1,
                            findCloseBracketIndex(parts, startIndex + 1),
                            parts[startIndex][0]
                            );
                    }
                    else
                    {
                        leftNode = createOneOperandNode(
                            parts,
                            startIndex + 1,
                            startIndex + 1,
                            parts[startIndex][0]
                            );
                    }

                }
            }
            else
            {
                int secondOperandIndex = twoOperandOperationIndex + 1;

            }
            Node Current = new Node
            {
                FirstOperand = leftNode,
                SecondOperand = rightNode,
                Operation = Operation
            };
            return Current;
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
