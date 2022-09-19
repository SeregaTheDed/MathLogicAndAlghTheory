using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLogicAndAlghTheory
{
    public static class FileReader
    {
        public static string[] getFormulsFromProof()
        {
            using (StreamReader sr = new StreamReader("proof.txt"))
            {
                return sr.ReadToEnd().Split(new char[] {'\n','\r'}, StringSplitOptions.RemoveEmptyEntries);
            }
        }
        public static string[] getFormulsFromEquals()
        {
            using (StreamReader sr = new StreamReader("equals.txt"))
            {
                return sr.ReadToEnd().Split(new char[] {'\n','\r'}, StringSplitOptions.RemoveEmptyEntries);
            }
        }
        public static string getFormulaFromEquals()
        {
            using (StreamReader sr = new StreamReader("dnfknf.txt"))
            {
                return (sr.ReadToEnd() +"\n\r").Split(new char[] {'\n','\r'}, StringSplitOptions.RemoveEmptyEntries)[0];
            }
        }
    }
}
