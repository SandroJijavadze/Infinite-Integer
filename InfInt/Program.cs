using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfInt
{
    class Program
    {
        static void Main(string[] args)
        {
        string operation;
        InfInt operand1;
        InfInt operand2;    
        // Read the file and display it line by line.  
        
        System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\a\infint.txt");  
        for(int i = 0; i < 14; i++) 
        { 
            operand1  = new InfInt(file.ReadLine());
            operand2  = new InfInt(file.ReadLine());
            operation = file.ReadLine();
            Console.Write(operand1.ToString() + " " + operation + " " + operand2.ToString() + " = ");
            if(operation == "+")
                    Console.WriteLine(operand1.Add(operand2));
            else if(operation == "-")
                    Console.WriteLine(operand1.Subtract(operand2));
            else if(operation == "*")
                    Console.WriteLine(operand1.Multiply(operand2));

        } 

        file.Close();
        Console.ReadLine();
        }
    }
}

