using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace InfInt
{
    class InfInt : IComparable<InfInt>
    {
        private List<int> values = new List<int>();
        private bool sign = true; // Positive, if not set negative(false).
        private bool isnull = false;
        public InfInt(string infinite)
        {
            int i = 0; // 
            if (infinite[0] == '-')
            {
                sign = false;
                i++;
            }
            while (infinite[i] == '0' && infinite[i + 1] != '\0')  // Skip zeros in the beginning, if there are any.
                i++;

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

            if (this.sign != otherInfInt.sign)
            {
                if (this.sign == true) // If one is positive and another negative, return accordingly.
                    return 1;
                else
                    return -1;
            }
            else if (this.values.Count == otherInfInt.values.Count)
            {// If their lengths are equal.
                while (this.values[i] == otherInfInt.values[i] && i < length - 1)  // Skip integers that are equal, until last integer.
                    i++;
                if (this.sign == true)
                    return this.values[i].CompareTo(otherInfInt.values[i]); // Return comparison result of leftmost integers that are not equal.
                else
                    return -((this.values[i]).CompareTo(otherInfInt.values[i]));
            }
            else
            { // If their count are not equal, return the higher count if it is positive, return -(minus) that if negative.
                if (this.sign == true) // if they are positive
                    return this.values.Count.CompareTo(otherInfInt.values.Count); // return CompareTo of their count.
                else
                    return -(this.values.Count.CompareTo(otherInfInt.values.Count));
            }
        }

        public override string ToString()
        {
            if (sign)
                return string.Join("", values);
            else
                return "-" + string.Join("", values);
        }

        private string MemberwiseAdd(InfInt other, bool otherFirstOperand = false)
        {
            string result = "";
            InfInt firstOperand = null;
            InfInt secondOperand = null;
            if (otherFirstOperand)
            {
                firstOperand = other;
                secondOperand = this;
            }
            else
            {
                firstOperand = this;
                secondOperand = other;
            }

            int thisOne = firstOperand.values.Count() - 1;
            int otherOne = secondOperand.values.Count() - 1;
            int carry = 0;
            int addRes = 0;
            while (thisOne != -1)
            {  // While all integers are not added.
                if (otherOne >= 0)  // If not last integer.
                    addRes = firstOperand.values[thisOne] + secondOperand.values[otherOne] + carry;
                else // If last integer.
                    addRes = firstOperand.values[thisOne] + carry;
                carry = 0;
                if (addRes >= 10) // IF we have carry, increment it, and decrement the actual integer by 10.
                {
                    addRes -= 10;
                    carry += 1;
                }
                result = addRes.ToString() + result;
                thisOne--;
                otherOne--;
            }
            if (thisOne == -1 && carry > 0)
                result = carry.ToString() + result;
            if (otherFirstOperand == true)
                result = "-" + result;
            return result;
        }

        private string MemberwiseSubtract(InfInt other, bool otherFirstOperand = false)
        {
            string result = "";
            InfInt firstOperand = null;
            InfInt secondOperand = null;
            if (otherFirstOperand)
            {
                firstOperand = other;
                secondOperand = this;
            }
            else
            {
                firstOperand = this;
                secondOperand = other;
            }
            int thisOne = firstOperand.values.Count() - 1;
            int otherOne = secondOperand.values.Count() - 1;
            int remainder = 0;
            int subRes = 0;
            while(thisOne >= 0) // While Last Integer Not Reached 
            {
                if (otherOne >= 0) // If second operand has still got , result = first - second - remainder
                    subRes = firstOperand.values[thisOne] - secondOperand.values[otherOne] - remainder;
                else               // If no members are left, result = first - remainder
                    subRes = firstOperand.values[thisOne] - remainder;
                remainder = 0; //Alreadey used it.
                if (subRes < 0)  // If result is less than 0
                {
                    subRes += 10; // Increment it by 10
                    remainder++; // Increase remainder
                }

                result = subRes.ToString() + result; // Add result
                thisOne--;                          // Decrement indices.
                otherOne--;                 
            }
            if (otherFirstOperand == true)  //Assumes that first operand is larger in modulus.
                result = "-" + result;
            return result;
        }

        private int ModulusCompareTo(InfInt other)// True if this one is larger in modulus, false otherwise.
        {
            bool thisOne = this.sign;
            bool otherOne = other.sign;
            int result;
            // Save their signs.

            //Make them positive.
            this.sign = true;
            other.sign = true;
            result = this.CompareTo(other);

            //Restore signs
            this.sign = thisOne;
            other.sign = otherOne;

            return result; // Return result of compareto.
        }

        public string Add(InfInt other)
        {
            string result = "";
            if (this.CompareTo(other) >= 0)// If first one is larger.
            { 
                if (this.sign == true && other.sign == true) // both are positive and first one is larger
                {        
                    result = this.MemberwiseAdd(other);
                }
                else if (this.sign == false && other.sign == false)// Both are negative, result is negative. other - this, memberwise.
                {
                    result = this.MemberwiseAdd(other, true);
                }
                else // first one is  positive, second one is negative. larger in modulus - smaller in modulus(memberwise), sign of larger one in modulus.
                {
                    if (this.ModulusCompareTo(other) == 0)//If modulus are equal
                        result = "0";
                    else if (this.ModulusCompareTo(other) > 0) // If this one has bigger modulus.
                        result = this.MemberwiseSubtract(other);
                    else   // The modulus of negative one is larger
                        result = this.MemberwiseSubtract(other, true);
                }
            }
            else if (this.CompareTo(other) < 0) // Second one is larger
            {
                if (other.sign == false)                     // both are negative, result = other + this. negative sign
                    result = other.MemberwiseAdd(this, true);
                else if (this.sign == false && other.sign == true)//2.this negative and other positive. result =  larger in modulus - smaller in modulus, sign of larger in modulus.
                    result = other.MemberwiseSubtract(this, (this.ModulusCompareTo(other) >= 0));
                else                    // Both are positive. result other + this, positive sgn.
                    result = other.MemberwiseAdd(this);
            }
            return result;

        }

       
        // public void multiply(InfInt object)
    }

}





