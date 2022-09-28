using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MathLogicAndAlghTheory
{
    public class Node : ICloneable, IEquatable<Node>
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
        public string getString
        {
            get
            {
                return ToString();
            }
        }
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
                return "(" + this.FirstOperand.getStringFormula() + this.Operation + this.SecondOperand.getStringFormula() + ")";
        }
        public void removeImplication()
        {
            if (this.isVariable)
                return;
            else if (this.isOperationWithOneOperand)
                this.FirstOperand.removeImplication();
            else if (this.isOperation)
            {
                if (this.Operation == '>')
                {
                    Operation = '|';
                    this.FirstOperand.addMinus();
                }
                this.FirstOperand.removeImplication();
                this.SecondOperand.removeImplication();
            }
        }
        private void downAllMinusesRecursy()
        {
            Console.WriteLine(this.getStringFormula());
            Console.WriteLine("*************************************");
            if (this.isVariable)
            {
                return;
            }
            else if (this.isOperation)
            {
                this.FirstOperand.downAllMinusesRecursy();
                this.SecondOperand.downAllMinusesRecursy();
            }
            else if (this.isOperationWithOneOperand)
            {
                if (this.FirstOperand.isOperationWithOneOperand)
                {
                    Node nextnext = this.FirstOperand.FirstOperand;
                    this.FirstOperand = nextnext.FirstOperand;
                    this.VariableName = nextnext.VariableName;
                    this.SecondOperand = nextnext.SecondOperand;
                    this.Operation = nextnext.Operation;
                    this.downAllMinusesRecursy();
                }
                else if (this.FirstOperand.isOperation)
                {
                    Node next = this.FirstOperand;
                    this.FirstOperand = next.FirstOperand;
                    this.FirstOperand.addMinus();
                    this.SecondOperand = next.SecondOperand;
                    this.SecondOperand.addMinus();
                    this.Operation = next.Operation.Value.getReversedOperation();
                    this.FirstOperand.downAllMinusesRecursy();
                    this.FirstOperand.downAllMinusesRecursy();
                }
            }
        }
        private void addMinus()
        {
            var thisCurrent = (Node)this.Clone();
            this.SecondOperand = null;
            this.VariableName = null;
            this.Operation = '-';
            this.FirstOperand = thisCurrent;

        }
        public void downAllMinuses()
        {
            this.removeImplication();
            this.downAllMinusesRecursy();
        }

        public object Clone()
        {
            return JsonSerializer.Deserialize<Node>(JsonSerializer.Serialize(this));
        }
        public override string ToString()
        {
            return getStringFormula();
        }

        public bool Equals(Node? other)
        {
            Table leftTable = new Table(this);
            Table rightTable = new Table(other);
            return leftTable == rightTable;
        }
    }

}
