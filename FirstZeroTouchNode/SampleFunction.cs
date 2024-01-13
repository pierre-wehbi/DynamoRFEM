using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstZeroTouchNode
{
    public class SampleFunction
    {
        //The empty private constructor.
        //This will be not imported into Dynamo.
        private SampleFunction() { }

        //The public multiplication method. 
        //This will be imported into Dynamo.
        public static double MultiplyByTwo(double inputNumber)
        {
            return inputNumber * 2.0;
        }
    }
}
