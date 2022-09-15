namespace MathLogicAndAlghTheory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string inputLine = "((-x1|x2)>x3)".ToLower();
            string inputLine = "(-(p>(q&p))>(p|r))";
            //string inputLine = "----p";
            if (Validator.Validate(inputLine))
            {
                Console.WriteLine("String is a formula. Tree:");
                var tree = TreeCreator.createTree(Splitter.Split(inputLine));
                tree.printTree();

            }
            else
            {
                Console.WriteLine("String is not a formula");
            }
        }
    }
}