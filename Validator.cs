using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLogicAndAlghTheory
{
    public static class Validator
    {
        public static bool Validate(string input)
        {
            try
            {
                TreeCreator.createTree(Splitter.Split(input));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
