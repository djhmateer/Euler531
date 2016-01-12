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
            DateTime start = DateTime.Now;
            long sumOfSolution = 0;
            List<TotientPair> listOfTotients = new List<TotientPair>();
            for (long i = 1000000; i < 1005000; i++)
            {
                listOfTotients.Add(new TotientPair(i));
            }
            for (int n = 0; n < 4999; n++)
            {
                for (int m = n+1; m < 5000; m++)
                {
                    if (listOfTotients[n].Totient % listOfTotients[n].Number == listOfTotients[m].Totient % listOfTotients[m].Number)
                    {
                        sumOfSolution = sumOfSolution + ((listOfTotients[n].Totient % listOfTotients[n].Number));
                        Console.WriteLine(listOfTotients[n].Totient % listOfTotients[n].Number + " n=" + listOfTotients[n].Number+ " m=" + listOfTotients[n].Number);
                    }
                }
            }
            DateTime end = DateTime.Now;
            Console.WriteLine(sumOfSolution + "!!!" + (end - start).TotalMinutes);
        }

        

        
    }
    class Factor
    {
        public long Number { get; set; }

        public long Occurance { get; set; }

        public Factor(long number, long occurance)
        {
            Number = number;
            Occurance = occurance;
        }       
    }
    class TotientPair
    {
        public long Number { get; set; }

        public long Totient { get; set; }

        public TotientPair(long number)
        {
            Number = number;
            Totient = totientOf(number);
        }

        public static long totientOf(long n)
        {
            List<Factor> factors = primeFactors(n);
            List<float> phiSegments = new List<float>();
            long totient = 1;
            foreach (Factor factor in factors)
            {
                //count how many of those factors are in the list

                //record how many and then remove the duplicates. raise factor to power of the count
                long pk = (long)Math.Pow(factor.Number, factor.Occurance);
                float pBracket = 1 - (1 / (float)factor.Number);
                phiSegments.Add(pk * pBracket);
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

                if (number == 0)
                {
                    goto start;
                }
                int factorsOfTwo = 0;
                while (number % 2 == 0)
                {
                    factorsOfTwo++;
                    number = number / 2;
                }
                if (factorsOfTwo != 0) factors.Add(new Factor(2, factorsOfTwo));

                for (long j = 3; j <= number; j += 2)
                {
                    int factorOccurance = 0;
                    while (number % j == 0)
                    {
                        factorOccurance++;
                        number = number / j;
                    }
                    if (factorOccurance != 0) factors.Add(new Factor(j, factorOccurance));
                }
                long result = 1;
                foreach (var factor in factors)
                {
                    result = result * (long)Math.Pow(factor.Number, factor.Occurance);
                }

                if (result == origNumber)
                {
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

}
