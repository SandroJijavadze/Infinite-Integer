using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
/// Sandro Jijavadze
/// 819819553   
/// CompE361
/// InfInt class.
namespace InfInt
{
    class InfInt : IComparable<InfInt>
    {
        /// Members, values contains digits.
        private List<int> values = new List<int>();
        /// Positive, if not set negative(false).
        private bool sign = true; 

       
        /// Constructor for InfInt Class.
        public InfInt(string infinite)
        {

            int i = 0; // 
            if (infinite[0] == '-') // Sign.
            {
                sign = false;
                i++;
            }
            // Parse characters into "Values" list. 
            if(infinite.Length == 0 && sign == true){
                values.Add((int)Char.GetNumericValue(infinite[0]));
            }else{
                while (infinite[i] == '0' && i < infinite.Length -1 && infinite[i + 1] != '\0')  // Skip zeros in the beginning, if there are any.
                    i++;
                while (i < infinite.Length)
                    values.Add((int)Char.GetNumericValue(infinite[i++])); //Int32.Parse(infinite[i++]+"")); 
            }

        }
        
        /// CompareTo, used in add, subtract, times.
        public int CompareTo(InfInt otherInfInt)
        {
            if (otherInfInt == null) return 1; // If other one is not a valid object reference.

            int i = 0;
            int length = this.values.Count;

            if (this.sign != otherInfInt.sign) // If they have different signs.
            {
                if (this.sign == true) // If one is positive and another negative, return accordingly.
                    return 1;
                else
                    return -1;
            }
            else if (this.values.Count == otherInfInt.values.Count)// If amount of values are the same.
            {
                while (this.values[i] == otherInfInt.values[i] && i < length - 1)// Skip integers that are equal, until last integer.
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


        //This was by far the easiest member.
        public override string ToString()
        {
            if (sign)
                return string.Join("", values);
            else
                return "-" + string.Join("", values);
        }

        /// MemberwiseAdd member.
        ///
        /// Used in Add and Subtract, assumes that the one that calls it is larger,
        /// Else, set otherFirstOperand to true.
        /// So that it returns correct sign.
        private string MemberwiseAdd(InfInt other, bool otherFirstOperand = false)
        {
            string result = "";
            InfInt firstOperand = null;
            InfInt secondOperand = null;
            // If bool in parameter was set true, flip the operands to get correct sign.
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
                    addRes = firstOperand.values[thisOne] + secondOperand.values[otherOne] + carry; // Add elements + carry.
                else // If last integer.
                    addRes = firstOperand.values[thisOne] + carry; // Only elements of larger InfInt + carry.
                carry = 0;
                if (addRes >= 10) // IF we have carry, increment it, and decrement the actual integer by 10.
                {
                    addRes -= 10;
                    carry += 1;
                }
                // Stringify result. Decrement variables used in loop.
                result = addRes.ToString() + result;
                thisOne--;
                otherOne--;
            }
            

            /// If after ending the loop, carry was not added to
            /// the result, this condition makes sure that it is added.
            if (thisOne == -1 && carry > 0)
                result = carry.ToString() + result;

            // Condition for checking and setting sign.
            if (otherFirstOperand == true)
                result = "-" + result;
            return result;
        }

        
        /// MemberwiseSubtract member.
        ///
        /// Used in Add and Subtract, assumes that the one that calls it is larger,
        /// Else, set otherFirstOperand to true.
        /// So that it returns correct sign.
        private string MemberwiseSubtract(InfInt other, bool otherFirstOperand = false)
        {
            string result = "";
            InfInt firstOperand = null;
            InfInt secondOperand = null;
            // If bool in parameter was set true, flip the operands to get correct sign.
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

            //  Getting Indexes
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
                if (this.sign == true && other.sign == true) // both are positive and first one is larger  
                    result = this.MemberwiseAdd(other);
                else if (this.sign == false && other.sign == false)// Both are negative, result is negative. other - this, memberwise.
                    result = this.MemberwiseAdd(other, true);
                else // first one is  positive, second one is negative. larger in modulus - smaller in modulus(memberwise), sign of larger one in modulus.
                    if (this.ModulusCompareTo(other) == 0)//If modulus are equal
                        result = "0";
                    else if (this.ModulusCompareTo(other) > 0) // If this one has bigger modulus.
                        result = this.MemberwiseSubtract(other);
                    else   // The modulus of negative one is larger
                        result = this.MemberwiseSubtract(other, true);
            else if (this.CompareTo(other) < 0) // Second one is larger
                if (other.sign == false)                     // both are negative, result = other + this. negative sign
                    result = other.MemberwiseAdd(this, true);
                else if (this.sign == false && other.sign == true)//2.this negative and other positive. result =  larger in modulus - smaller in modulus, sign of larger in modulus.
                    result = other.MemberwiseSubtract(this, (this.ModulusCompareTo(other) >= 0));
                else                    // Both are positive. result other + this, positive sgn.
                    result = other.MemberwiseAdd(this);
            return result;
        }
       
        /// Subtract member.
        /// Uses MemberwiseSubtract and MemberwiseAdd according to operands.
        public string Subtract(InfInt other){
            string result = "";
            
            if (this.CompareTo(other) >= 0){// If first one is larger or they are equal.
                if (this.sign == true && other.sign == true){ // both are positive and first one is larger
                    if(this.CompareTo(other) == 0)
                        result = "0";
                    else
                        result = this.MemberwiseSubtract(other);
                }
                else if (this.sign == false && other.sign == false)
                {// Both are negative, result is negative. other - this, memberwise.
                    result = other.MemberwiseSubtract(this);
                }
                else
                { // first one is  positive, second one is negative. larger in modulus - smaller in modulus(memberwise), sign of larger one in modulus.
                    if (this.ModulusCompareTo(other) == 0)
                    {//If modulus are equal
                        result = "0";
                    }
                    else if (this.ModulusCompareTo(other) > 0)// If this one has bigger modulus.
                    { 
                        result = this.MemberwiseAdd(other);
                    }
                    else // The modulus of negative one is larger
                    {
                        result = other.MemberwiseAdd(this);
                    }
                }
            }
             else if (this.CompareTo(other) < 0)
            {// Second one is larger
                if (other.sign == false)
                { // both are negative, result = other + this. negative sign
                    result = other.MemberwiseSubtract(this, true);
                }else if (this.sign == false && other.sign == true)
                {//2.this negative and other positive. result =  larger in modulus - smaller in modulus, sign of larger in modulus.
                    if(this.ModulusCompareTo(other)< 0){
                        result = this.MemberwiseAdd(other, true);
                    }else{ 
                        result = other.MemberwiseAdd(this, true);
                    }
                }else{ // Both are positive.  // Second one is larger. 
                    result = this.MemberwiseSubtract(other, true);
                }
             }  
            return result;
        }

        public string Multiply(InfInt other){
            string result ="";

            /// We check which element is smaller, so that loop will be done less times.
            InfInt Smaller, Larger; 
            if (this.ModulusCompareTo(other) > 0){
                Smaller = other;
                Larger = this; 
            }else{
                Smaller = this;
                Larger = other;
            }            
            //Setting indexes.
            int indexSmaller = Smaller.values.Count()-1;
            int indexLarger = Larger.values.Count()-1;
            InfInt total = new InfInt("0");
            InfInt temporary = new InfInt("0");
            
            /// This one is fun.
            /// The main multiply function was not working for every case for some reason.
            /// I implemented simpler version for uploading asssignment using while
            /// loop , e.g. 35*15 => loop x 15 { total += 35}
            /// It is only 6 lines of code, and it works, but,
            /// Some calculations take few seconds, 
            ///  I was not sure if it was frozen or if it was still calculating.
            /*
            while(counter.ModulusCompareTo(Smaller) < 0 || counter.ModulusCompareTo(Smaller) >0 ){
                total = new InfInt(total.Add(Larger));

                if (counter.ModulusCompareTo(Smaller) < 0)
                    counter = new InfInt(counter.Add(new InfInt("1")));
                else if(counter.ModulusCompareTo(Smaller) >0)
                    counter = new InfInt(counter.Add(new InfInt("-1")));
            }*/

            for(int i = 0; i < Smaller.values.Count; i++){ // For every digit in InfInt
                temporary = new InfInt("0"); // Reset temporary.
                for(int j = 0; j < Smaller.values[indexSmaller-i]; j++) // add Smaller.values[indexSmaller] times to itself.
                    temporary = new InfInt(temporary.Add(Larger)); // temporary += Larger
                for(int j = 0; j < i; j++) // Add i amount of zeroes.
                    temporary.values.Add(0);  // temporary *= 10 to the power of i
                total = new InfInt(total.Add(temporary)); // Total += temporary.
            }
   
            result = total.ToString(); // Convert result to string.
            if(this.sign != other.sign)//Add sign if negative.
                result = "-"+result;
            return result;// Return
        }
    }

}





