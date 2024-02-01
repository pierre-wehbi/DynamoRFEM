using Autodesk.DesignScript.Runtime;
using DesignScript.Builtin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstZeroTouchNode
{
    public class MultipleOutputs
    {
        private MultipleOutputs() { }

        /// <summary>
        /// An example showing how to return multiple values from a Zero-Touch imported node
        /// The names in the attribute should match the keys in the returned dictionary.
        /// </summary>
        /// <param name="ListFromDynamo">Provide the list here.</param>
        /// <returns name="evens">Evens in the passed list.</returns>
        /// <returns name="odds">Odds in the passed list.</returns>
        /// <search>evens,odds,multi</search>

        //Adding a MultiReturn attribute to the function
        [MultiReturn(new[] { "evens", "odds" })]
        public static Dictionary<string, object> SplitOddEven(List<int> ListFromDynamo)
        {
            var odds = new List<int>();
            var evens = new List<int>();

            //Checking integers in list if even or odd
            foreach (var i in ListFromDynamo)
            {
                if (i % 2 == 0)
                {
                    evens.Add(i);
                }
                else
                {
                    odds.Add(i);
                }
            }

            //Creating a new dictionary and return it
            return new Dictionary<string, object>
            {
                { "evens", evens },
                { "odds", odds }
            };
        }
    }
}
