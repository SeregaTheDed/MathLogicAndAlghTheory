using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MathLogicAndAlghTheory.TreeCreator;

namespace MathLogicAndAlghTheory
{
    public static class Operation
    {
        public static int getResult(this Node node)
        {
            if (node.isOperation)
                return int.Parse(node.VariableName);
            char operation = node.Operation.Value;
            switch (operation)
            {
                case '-':
                    return doMinus(node);
                case '>':
                    return doImplication(node);
                case '&':
                    return doMultiplication(node);
                case '|':
                    return doAdditional(node);
                default:
                    throw new ArgumentException("Not valid operation");
            }
        }
        public static int doMultiplication(Node node)
        {
            return getResult(node.FirstOperand) * getResult(node.SecondOperand);
        }
        public static int doAdditional(Node node)
        {
            if (getResult(node.FirstOperand) == 1 || getResult(node.SecondOperand) == 1)
                return 1;
            else
                return 0;
        }
        public static int doMinus(Node node)
        {
            if (getResult(node.FirstOperand) == 1)
                return 0;
            else
                return 1;
        }
        public static int doImplication(Node node)
        {
            if (getResult(node.FirstOperand) == 0 && getResult(node.SecondOperand) == 1)
                return 0;
            else
                return 1;
        }
    }
}
