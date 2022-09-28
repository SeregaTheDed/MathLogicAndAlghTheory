using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MathLogicAndAlghTheory
{
    public class Table
    {
        private int[] _table;
        private Node _root;
        private SortedDictionary<string, int> _variables;
        public Table(Node root)
        {
            _root = root;
            _variables = getVariables();
            _table = getTable();
        }
        public Table(Node root, Node additional_root)
        {
            _root = root;
            _variables = getVariables();
            foreach (var item in new Table(additional_root).getVariables())
            {
                if (!_variables.Contains(item))
                    _variables.Add(item.Key, item.Value);
            }
            _table = getTable();
        }
        private SortedDictionary<string, int> getVariables()
        {
            SortedDictionary<string, int> variables = new SortedDictionary<string, int>();
            getVariablesRecursy(variables, _root);
            return variables;
        }
        private void getVariablesRecursy(SortedDictionary<string, int> variables, Node node)
        {
            if (node.isVariable)
            {
                variables[node.VariableName] = 0;
            }
            else if (node.isOperationWithOneOperand)
            {
                getVariablesRecursy(variables, node.FirstOperand);
            }
            else if (node.isOperation)
            {
                getVariablesRecursy(variables, node.FirstOperand);
                getVariablesRecursy(variables, node.SecondOperand);
            }
            else
            {
                throw new ArgumentException();
            }
        }
        private int[] getTable() 
        {
            int rowCount = 1 << _variables.Count;
            var table = new int[rowCount];
            for (int i = 0; i < rowCount; i++)
            {
                string binaryNumber = getBinaryNumber(_variables.Count, i);
                int j = 0;
                foreach (var variableName in _variables.Keys.ToArray())
                {
                    _variables[variableName] = binaryNumber[j] - '0';
                    j++;
                }
                var currentValue = TreeCreator.createTree(Splitter.SplitWithReplace(_root.getStringFormula(), _variables)).getResult();
                table[i] = currentValue;
            }

            return table;
        }

        private string getBinaryNumber(int length, int number)
        {
            return Convert.ToString(number, 2).PadLeft(length, '0'); 
        }
        public static bool operator==(Table left, Table right)
        {
            if (!left._variables.Keys.SequenceEqual(right._variables.Keys))
                return false;
            return left._table.SequenceEqual(right._table);
        }
        public static bool operator!=(Table left, Table right)
        {
            return !(left == right);
        }

    }
}
