using System;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        //Remove this and replace with later comment
        JsonWriter writer;
        public Calculator()
        {
            /*
                lets look at changing this structure as currently we are keeping this file locked in memory during the whole sequence.
                If this was used by multiple people we would have issues
                Lets create the file as we are in the constructor (However, this will overwrite each time https://docs.microsoft.com/en-us/dotnet/api/system.io.file.createtext?view=net-5.0, how about we look for something which Appends text ;) )
                Then that's it

                We can create a a new class somewhere (in this file is fine), in the namespace scope. NOT INSIDE OF THE CALCULATOR CLASS SCOPE!!! and call it "Operation"

                This will have 4 properties
                double number1;
                double number2;
                double result;
                int mathOperation;

                Make sure these are all Pascal cased and have the appropriate get and set;

                Then during the Do operation, lets create a new class instance, assign the values to the correct properties :)


                Come down to my comment on the Finished method
            */
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }

        enum a
        {
            add = 1,
            b = 2
        }

        public double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN;
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");


            /* 
                Very good use of a switch statement
                This saves runtime as it allows for memory jumps instead of a basic if.

                The only problem here is we are delving in what is a "magic string". It also can be a bit hard to read
                In order for us to understand exactly what we are doing we need to look further into the code and see that
                "Ahh yes, we have a plus or a minus or a multiple"

                We can fix this by using an enum statement

                Create a new property of Enum MathOptions (or similar on this page)
                EG
                enum MathOptions
                {
                    Add = 1,
                    Minus,
                    Multiply,
                    Divide
                }

                It will just assume 2,3,4. We do not need to assign them values (we can if you wish)

                Then parse the operand to an integer, then to an enum (google how to do that) then compare the enum values in the switch

                eg 
                var parseEnum = ...

                switch(parseEnum)
                {
                    case MathOptions.Add:
                        ...
                    break;

                    case MathOptions.Subtract:
                        ...
                    break
                }
            */

            switch (op)
            {
                case "1.":
                    result = num1 + num2;
                    writer.WriteValue("Add");
                    break;
                case "2.":
                    result = num1 - num2;
                    writer.WriteValue("Subtract");
                    break;
                case "3.":
                    result = num1 * num2;
                    writer.WriteValue("Multiply");
                    break;
                case "4.":
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        writer.WriteValue("Divide");
                    }
                    break;
                default:
                    break;

            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();

            return result;
        }

        public void Finish()
        {
            /*
                Lets update this to accept our newly created object as the first parameter. Lets also call this after we do the math result. (so before the return on line 129)
                when finish is called, what we want to do is (hint) Serialize (end hint) the object passed in using a Newtonsoft.Json method.

                We then want to Append that text to the file we created at the start

                This will clear up a lot of the unneeded `writer...` calls we currently have :)
            */

            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }
}
