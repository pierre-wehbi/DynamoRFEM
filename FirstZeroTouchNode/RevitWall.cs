using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

//Adding a Reference to the Revit API
using Autodesk.Revit.DB;

//Utilizing the RevitServices.dll library (which is included with Dynamo’s install) for Revit interop and instantiation of this object
using RevitServices.Persistence;
using RevitServices.Transactions;

//The wrapper method is available in Dynamo’s RevitNodes.dll library
using Revit.Elements;

//Adding a new alias to our using directives and qualify the intended Wall class
using Wall = Autodesk.Revit.DB.Wall;

namespace FirstZeroTouchNode
{
    public class RevitWall
    {
        private RevitWall() { }

        //Qualifing the target class "Revit.Elements.Element" to avoid any ambiguous references with the Element class in the Revit API
        public static Revit.Elements.Element Create(double lenghtInFt, int levelId)
        {
            //Calling the current document
            Document doc = DocumentManager.Instance.CurrentDBDocument;

            //Creating two new XYZ objects called ptStart and ptEnd
            XYZ ptStart = new XYZ(); //no inputs creates an XYZ at 0,0,0
            XYZ ptEnd = new XYZ(lenghtInFt, 0.0, 0.0);

            //Using the Revit API Line class and its CreateBound() constructor
            Line lnLocationCurve = Line.CreateBound(ptStart, ptEnd);

            //Creating a new variable of this type called levelElementId and instantiate it using the new keyword
            //with the ElementId constructor which takes an int as an input
            ElementId levelElementId = new ElementId(levelId);

            //Using Dynamo’s RevitServices library's static TransactionManager class and use its EnsureInTransaction() method to start the transaction.
            TransactionManager.Instance.EnsureInTransaction(doc);

            //Adding the constructor to your method and assign it to a new variable named newWall adhering to C# syntax grammar.
            Wall newWall = Wall.Create(doc, lnLocationCurve, levelElementId, false);

            //The transaction must be closed using the TransactionTaskDone() method.
            TransactionManager.Instance.TransactionTaskDone();

            //The method we call to ‘wrap’ Revit elements into the Dynamo wrapper class is
            //ToDSType().It takes a bool input, where true is used if the element exists in Revit, or
            //false if the element is being instantiated by our code.
            //ToDSType() also has another purpose; it creates a binding(known as ‘Element binding’)
            //between the element created in our code, and its representation in Revit.It essentially
            //creates a dependency between the node and the Revit element(s) it outputs, to
            //prevent duplicates – similar to a synchronisation between the two applications. To
            //illustrate this behaviour; if our method inputs change(eg.length or level changes), the
            //change modifies the same Wall instance.Without element binding, we would get a
            //new wall every time the node’s inputs change, or if the Dynamo file is closed/
            //reopened and run! It is therefore essential to call ToDSType() on any Revit Element
            //instantiated in your code to prevent unwanted duplicates from appearing.
            return newWall.ToDSType(false);
        }
    }
}
