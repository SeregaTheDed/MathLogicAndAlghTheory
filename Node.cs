using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLogicAndAlghTheory
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
                Console.WriteLine(new String(separator, depth * 2) + node.VariableName);
            }
            else if (node.isOperationWithOneOperand)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(new String(separator, depth * 2) + node.Operation);
                printTreeRecursy(node.FirstOperand, depth + 1);
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
        public string getStringFormula()
        {
            if (this.isVariable)
                return this.VariableName;
            else if (this.isOperationWithOneOperand)
                return this.Operation + this.FirstOperand.getStringFormula();
            else
                return "("+ this.FirstOperand.getStringFormula() + this.Operation + this.SecondOperand.getStringFormula() +")";
        }
        public void removeImplication()
        {
            if (this.isVariable)
                return;
            else if (this.isOperationWithOneOperand)
                this.FirstOperand.removeImplication();
            else if (this.isOperation)
            {
                Operation = '|';
                this.FirstOperand = new Node
                {
                    Operation = '-',
                    FirstOperand = this.FirstOperand,
                };
                this.FirstOperand.removeImplication();
                this.SecondOperand.removeImplication();
            }
        }
    }

}
