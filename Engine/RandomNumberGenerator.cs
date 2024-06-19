using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Engine
{
       
    public  class RandomNumberGenerator
    {
        public static int NumberBetween(int minimumValue, int maximumValue)
        {

            byte[] randomNumber = System.Security.Cryptography.RandomNumberGenerator.GetBytes(1);

            double asciiValueOfRandomCharacter = Convert.ToDouble(randomNumber[0]);
            //we are using math.max and substrating 0.00000000001, to ensure "multiplier" wil lalways be betwen 0.0 and 0.9999999999
            //otherwise it's posible to be 1 and that causes problems

            double multiplier =
                Math.Max(0, asciiValueOfRandomCharacter / 255d) - 0.00000000001d;

            //we need to add one to the range to allow rounding with Math.Floor

            int range = maximumValue - minimumValue + 1;
            double randomValueInRange = Math.Floor(multiplier*range);
            return (int)(minimumValue+randomValueInRange);
        }
    }

}
