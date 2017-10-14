using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfInt
{
    class InfInt: IComparable<InfInt>
    {
        private List<int> values = new List<int>();
        private bool sign = true; // Positive, if not set negative(false).
        private bool isnull = false;

        public InfInt(string infinite)
        {
            int i = 0; // 
            //int length = 0;
            if (infinite[0] == '-') {
                sign = false;
                i++;
                //length--;
            }
            while (infinite[i] == '0' && infinite[i + 1] != '\0')  // Skip zeros in the beginning, if there are any.
                i++;

            //length += infinite.Length - i; // Length of infint, not counting sign and any zeros in the beginning. 

            while (i < infinite.Length) 
            {
                values.Add((int)Char.GetNumericValue(infinite[i++])); //Int32.Parse(infinite[i++]+"")); 
            }
            if (infinite[i - 1] == '0')
                isnull = true;

        }

        public int CompareTo(InfInt otherInfInt)
        {
            if (otherInfInt == null) return 1; 

            int i = 0;
            int length = this.values.Count;
            if (this.values.Count == otherInfInt.values.Count) // If their lengths are equal.
            {  
                while (this.values[i] == otherInfInt.values[i] && i < length-1)  // Skip integers that are equal, until last integer.
                    i++;
                return this.values[i].CompareTo(otherInfInt.values[i]); // Return comparison result of leftmost integers that are not equal.
            }
            else
            {
                return this.values.Count.CompareTo(otherInfInt.values.Count);
            }
            
        }
        
        public override string ToString()
        {
            if (sign) {
                return string.Join("", values);
            }
            else
            {
                return "-" + string.Join("", values);
            }
        }
        
        public string sub(InfInt otherInfInt) // Subtract.
        {
            List<char> newList = new List<char>;
            int remainder = 0;
            int index1 = this.values.Count -1; // 
            int index2 = otherInfInt.values.Count -1;
            if(this.sign == otherInfInt.sign)
            {
                while(index1 != 0 || index2 != 0)
                {
                    if((remainder = this.values[index1] - otherInfInt.values[index2]) < 0) 
                    {
                        // axal listshi + it sheushvi remainderi char ad gadaqceuli
                        // ukan yvela noli 9 ianit shecvale pirvel ricxvshi.
                    }
                    else if(remainder == 0)
                    {
                        // sheagde 0
                    }
                    else
                    {
                        //
                        // pirdapir axal listshi sheagde (10 - remainder)
                    }

                }

                // IF overflow, add 1 to in the beginning of list.
            }// Add else, if their signs are not equal.
        }




        // public void multiply(InfInt object)

        // public void add(InfInt object){

    }



}

