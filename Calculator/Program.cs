﻿using System;
using CalculatorLibrary;

/* Not a big thing, more of a cleanliness thing.
Dotnet 3.5 (C# 4 I think), introduced the `var` keyword. What this basically does is it allows the compiler
to manage the typing. It also helps to keep the code inline

eg

var item1 = ""; -- will evaluate to a stirng
var item2 = false; -- will evaluate to a bool
var item3 = new List<string>(); -- will evaluate to a list of string

is much easier to read than

string item1 = "";
bool items2 = false;
List<string> item3 = new List<string>();

it can also be applied to objects.

eg
var calculator = new Calculator();

You will still have access to all intellisense
*/


namespace CalculatorProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;

            //Title
            Console.WriteLine("Console Calculator\r");
            Console.WriteLine("------------------\n");


            Calculator calculator = new Calculator();

            while (!endApp)
            {
                string numInput1 = "";
                string numInput2 = "";
                double result = 0;

                //User input for values
                Console.Write("Type a number and press Enter: ");
                numInput1 = Console.ReadLine();

                double cleanNum1 = 0;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput1 = Console.ReadLine();
                }

                Console.Write("Type another number and press Enter: ");
                numInput2 = Console.ReadLine();

                double cleanNum2 = 0;
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Console.Write("This is not valid input. Please enter an integer value: ");
                    numInput2 = Console.ReadLine();
                }

                //User input for operation
                Console.WriteLine("Choose an option from the list below:");
                Console.WriteLine("\t1. Add");
                Console.WriteLine("\t2. Subtract");
                Console.WriteLine("\t3. Multiply");
                Console.WriteLine("\t4. Divide");
                Console.Write("What option do you select? ");

                string op = Console.ReadLine();

                try
                {
                    result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else Console.WriteLine("Your result: {0:0.##}\n", result);
                }
                catch (Exception e)
                {
                    Console.WriteLine("An exception occurred whilst trying to do the math.\n - Details: " + e.Message);
                }

                Console.WriteLine("--------------------------------\n");

                Console.Write("Press 'n' and Enter to close the app, or just press Enter to continue using the calculator\n");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n");
            }

            calculator.Finish();
            return;
        }
    }

}

