using System.Text.Json;
using System.Xml;

namespace MathLogicAndAlghTheory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] menu =
            {
                "1. Check formulas for correctness(proof.txt)",
                "2. Check formulas for equivalence(equals.txt)",
                "3. Find normal forms for formula(dnfknf.txt)",
                "4. Exit", 
            };
            while (true)
            {
                Console.Clear();
                Console.WriteLine(String.Join("\n", menu));
                Console.Write("Enter a menu item(1,2,3,4):");
                string punkt = Console.ReadLine();
                //string punkt = "3";
                switch (punkt)
                {
                    case "1":
                        Task.Task1();
                        break;
                    case "2":
                        Task.Task2();
                        break;
                    case "3":
                        Task.Task3();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Uncorrect input! Input 1, 2, 3 or 4");
                        break;
                }
                Console.WriteLine("Press any key...");
                Console.ReadKey();
            }




            Console.ReadKey();
        }
    }
}