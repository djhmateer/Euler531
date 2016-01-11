using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace p_531
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(totientOf(102));


        }

        public static long totientOf(int n)
        {
            List<Factor> factors = primeFactors(n);
            List<long> phiSegments = new List<long>();
            long totient = 1;
            foreach (Factor factor in factors )
            {
                //count how many of those factors are in the list

                //record how many and then remove the duplicates. raise factor to power of the count
                long pk = (long)Math.Pow(factor.Number, factor.Occurance);             
                long pBracket = 1 - (ONE / factor.Number);
                phiSegments.Add(pk*pBracket);
            }
            foreach (long phiSegment in phiSegments)
            {
                totient = totient * phiSegment;
            }

            return totient;
        }

        public static List<Factor> primeFactors(long number)
        {
            long origNumber = number;
            while (true)
            {      
                start:                
                
                var factors = new List<Factor>();

                if(number==0)
                {
                    goto start;
                }
                int factorsOfTwo = 0;
                while (number % 2 == 0)
                {
                    factorsOfTwo++;
                    number = number / 2;                    
                }
                if(factorsOfTwo != 0) factors.Add(new Factor(2, factorsOfTwo));

                for (long j = 3; j <= number; j += 2)
                {
                    int factorOccurance = 0;
                    while (number % j == 0)
                    {
                        factorOccurance++;
                        number = number / j;                        
                    }
                    if(factorOccurance != 0) factors.Add(new Factor(j, factorOccurance));
                }
                long result = 1;
                foreach (var factor in factors)
                {
                    result = result* (long)Math.Pow(factor.Number,factor.Occurance);                   
                }
               
                if (result == origNumber) {                     
                    return factors;
                }
                else
                {
                    Console.WriteLine("I HAVE FAILED to factorise");
                    Console.WriteLine();
                }
            }
        }
    }
    class Factor
    {
        // Copy constructor.
       
        public Factor(long number, long occurance)
        {
            Number = number;
            Occurance = occurance;
        }

        public long Number { get; set; }

        public long Occurance { get; set; }

       
    }

}
