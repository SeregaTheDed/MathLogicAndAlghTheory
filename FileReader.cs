using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLogicAndAlghTheory
{
    public static class FileReader
    {
        public static string[] getFormuls()
        {
            using (StreamReader sr = new StreamReader("proof.txt"))
            {
                return sr.ReadToEnd().Split(new char[] {'\n','\r'}, StringSplitOptions.RemoveEmptyEntries);
            }
        }
    }
}
