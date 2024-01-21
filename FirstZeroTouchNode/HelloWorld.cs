using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstZeroTouchNode
{
    public class HelloWorld
    {
        //HelloWorld field.................
        //adding a field to our class to store the input message value. We can
        //then access the value stored in the field whenever required using the ‘.’ (dot) operator.
        //(the type of this field must match, so precede the name with string to define its type)
        private string _message;
        
        //HelloWorld constructor................
        public HelloWorld(string message)
        {
            //assigning the message that’s input into the constructor to the _message field.
            _message = message;
        }
        
        //HelloWorld property...................
        //creating a public property so the field value is accessible at every level
        //Properties use the get and set keywords and adhere to the following syntax
        public string Message
        { 
            //access to the field
            get { return _message; }
            //restrict modification to the field
            //set { _message = value; }
        }
        
        //HelloWorld method.............................
        //add a new public method to our HelloWorld class which can be used to query if a character or word
        //appears in the message of a HelloWorld instance, returning true if the query is found, or false if it’s not. 
        public bool Contains(string subString)
        {
            return Message.Contains(subString);
        }
    }
}
