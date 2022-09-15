namespace MathLogicAndAlghTheory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string inputLine = "((-x1|x2)>x3)".ToLower();
            //string inputLine = "(-(p>(q&p))>(p|r))";
            string inputLine = "-p";

            var tree = TreeCreator.createTree(Splitter.Split(inputLine));
            Validator validator = new Validator(true);
            validator.Validate(inputLine);

            Console.ReadKey();
        }
    }
}