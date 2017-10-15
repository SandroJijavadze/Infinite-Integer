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
            
            InfInt someInfInt = new InfInt("65198198191979711");
            List<InfInt> infIntList = new List<InfInt> {
                /*new InfInt("9999999999"),
                new InfInt("-1111111111"),
                */
                
                
                // Cases when first one  is more positive.   4 in total
                new InfInt("651981982319197971"), // Both Positive, # 1
                new InfInt("999999999999123"),

                new InfInt("-2319197971"), // Both negative, #2
                new InfInt("-999999999999123"),

                new InfInt("651123981982319197971"), // Only second one negative. First has larger modulus #3
                new InfInt("-99923999999999123"),

                new InfInt("651981982311239197971"), // Only second one negative. Second one has larger modulus
                new InfInt("-9999999123123099999123"),


                //Case when they are equal
                 new InfInt("651981982311239197971"),// #4
                 new InfInt("651981982311239197971"),

                // Cases when second one is more positive
                new InfInt("-651981982319197971"),  // Both are negative #5
                new InfInt("-99123999999123"),

                new InfInt("-999999999999123"), // Only first is negative, second large in modulus # 6
                new InfInt("9999999999999232123"),

                new InfInt("-999912391499999999123"), // Only first is negative, first large in modulus #7
                new InfInt("999999999999123"),

                new InfInt("999912391499999999123"), //Both positive. No matter which is larger in modulus. #8
                new InfInt("999999999999123")

                
            };
            for (int i = 0; i <17  ; i += 2) 
            {
                Console.Write($"#{i/2}:  ");
                Console.WriteLine(infIntList[i].Add(infIntList[i + 1]));
                

            }
            
            Console.WriteLine($"{1234.ToString()}");
            Console.ReadLine();

        }
    }
}
