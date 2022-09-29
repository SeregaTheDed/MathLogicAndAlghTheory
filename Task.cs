using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLogicAndAlghTheory
{
    public static class Task
    {
        public static void Task1()
        {
            foreach (var inputLine in FileReader.getFormulsFromProof())
            {
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Input string: " + inputLine);
                if (Validator.Validate(inputLine))
                {
                    Console.WriteLine("String is a formula. Tree:");
                    var tree = TreeCreator.createTree(Splitter.Split(inputLine));
                    tree.printTree();
                    /*Console.WriteLine("Tree without implication:");
                    tree.removeImplication();
                    tree.printTree();
                    Console.WriteLine("Formula without implication: ");
                    Console.WriteLine(tree.getStringFormula());*/

                }
                else
                {
                    Console.WriteLine("String is not a formula");
                }
                
            }
        }
        public static void Task2()
        {
            string[] formuls = FileReader.getFormulsFromEquals();
            if (!Validator.Validate(formuls[0]))
            {
                Console.WriteLine(formuls[0] + " is not a formula");
                return;
            }
            if (!Validator.Validate(formuls[1]))
            {
                Console.WriteLine(formuls[1] + " is not a formula");
                return;
            }
            var tree1 = TreeCreator.createTree(Splitter.Split(formuls[0]));
            var tree2 = TreeCreator.createTree(Splitter.Split(formuls[1]));
            Table table1 = new Table(tree1, tree2);
            Table table2 = new Table(tree2, tree1);
            Console.WriteLine(formuls[0]);
            Console.WriteLine(table1 == table2 ? "equal":"not equal");
            Console.WriteLine(formuls[1]);
        }
        public static void Task3()
        {
            string formula = FileReader.getFormulaFromEquals();
            if (!Validator.Validate(formula))
            {
                Console.WriteLine(formula + " is not a formula");
                return;
            }
            Node input = TreeCreator.createTree(formula);
            Console.WriteLine("!!!!!Perfect forms were constructed by truth tables!!!!!");
            Console.WriteLine("Input:");
            Console.WriteLine(input);
            Console.WriteLine("--------------------");
            Node dnf = TreeCreator.createDNF(input);
            Console.WriteLine("DNF:");
            Console.WriteLine(dnf);
            Console.WriteLine("DNF tree:");
            dnf.printTree();
            try
            {
                Node perfectDNF = TreeCreator.createPerfectDNF(input);
                Console.WriteLine("Perfect DNF:");
                Console.WriteLine(perfectDNF);
                Console.WriteLine("Perfect DNF tree:");
                perfectDNF.printTree();
            }
            catch (Exception)
            {
                Console.WriteLine("Perfect DNF not exists");
            }
            Console.WriteLine("--------------------");
            try
            {
                Node perfectCNF = TreeCreator.createPerfectCNF(input);
                Console.WriteLine("Perfect CNF:");
                Console.WriteLine(perfectCNF);
                Console.WriteLine("Perfect CNF tree:");
                perfectCNF.printTree();
            }
            catch (Exception)
            {
                Console.WriteLine("Perfect CNF not exists");
            }
            
        }

    }
}
