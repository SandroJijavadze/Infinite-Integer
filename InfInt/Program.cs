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
                new InfInt("65198198191979711"),
                new InfInt("65198198191979711"),
                new InfInt("6519819819197971"),
                new InfInt("06519819819197971"),
                new InfInt("06519819819197972")

            };
            for(int i = 0; i < 4; i++)
            {
                Console.WriteLine(infIntList[i].CompareTo(infIntList[i+1]));
            }
            Console.ReadLine();
            // output : 0, 1,0,-1

        }
    }
}
