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
            foreach (var inputLine in FileReader.getFormuls())
            {
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Input string: " + inputLine);
                if (Validator.Validate(inputLine))
                {
                    Console.WriteLine("String is a formula. Tree:");
                    var tree = TreeCreator.createTree(Splitter.Split(inputLine));
                    tree.printTree();
                    Console.WriteLine("Tree without implication:");
                    tree.removeImplication();
                    tree.printTree();
                    Console.WriteLine("Formula without implication: ");
                    Console.WriteLine(tree.getStringFormula());

                }
                else
                {
                    Console.WriteLine("String is not a formula");
                }
                
            }
        }
    }
}
