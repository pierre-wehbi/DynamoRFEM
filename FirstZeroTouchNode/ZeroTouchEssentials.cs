using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Interfaces;
using Autodesk.DesignScript.Geometry;


namespace ZeroTouchEssentials
{
    public class ZeroTouchEssentials
    {
        /// <summary>
        /// An example showing how to return multiple values from a Zero-Touch imported node
        /// The names in the attribute should match the keys in the returned dictionary.
        /// </summary>
        /// <param name="a">First number.</param>
        /// <param name="b">Second number.</param>
        /// <returns name="add">Number created by adding two inputs together.</returns>
        /// <returns name="mult">Number created by multiplying two inputs together.</returns>
        /// <search>example,multi</search>
        [MultiReturn(new[] { "add", "mult" })]
        public static Dictionary<string, object> ReturnMultiExample(double a, double b)
        {
            return new Dictionary<string, object>
            {
                { "add", (a + b + b) },
                { "mult", (a * b) }
            };
        }


        /// <summary>
        /// This method demonstrates how to use a native geometry object from Dynamo
        /// in a custom method
        /// </summary>
        /// <param name="curve">Input Curve. This can be of any type deriving from Curve, such as NurbsCurve, Arc, Circle, etc</param>
        /// <returns>The twice the length of the Curve </returns>
        /// <search>example,curve</search>
        public static double DoubleLength(Curve curve)
        {
            return curve.Length * 2.0;
        }


        private double _a;
        private double _b;

        // Make the constructor internal
        internal ZeroTouchEssentials(double a, double b)
        {
            _a = a;
            _b = b;
        }

        // The static method that Dynamo will convert into a Create node
        public static ZeroTouchEssentials ByTwoDoubles(double a, double b)
        {
            return new ZeroTouchEssentials(a, b);
        }

        /// <summary>
        /// Example property returning the value _a inside the object
        /// </summary>
        public double A
        {
            get { return _a; }
        }

        /// <summary>
        /// Example property returning the value _b inside the object
        /// </summary>
        public double B
        {
            get { return _b; }
        }

    }
}