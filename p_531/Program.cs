
using System;
using System.Collections.Generic;
using Xunit;


namespace p_531
{
    public class Program
    {
        //public static List<TotientPair> listOfTotients
        //{
        //    get; set;
        //}

        public static void Main(string[] args)
        {
            List<TotientPair> listOfTotients = new List<TotientPair>();
            DateTime start = DateTime.Now;
            long sumOfSolution = 0;           
            for (long i = 1000000; i < 1005000; i++)
            {
                listOfTotients.Add(new TotientPair(i));
            }
            for (int n = 0; n < 4999; n++)
            {
                //Console.WriteLine(n);
                for (int m = n + 1; m < 5000; m++)
                {
                    long gFuncResult = GFunc(listOfTotients[n], listOfTotients[m]);
                    if (gFuncResult != 0)
                    {
                        sumOfSolution = sumOfSolution + gFuncResult;
                       // Console.WriteLine(gFuncResult + " n=" + listOfTotients[n].Number + " m=" + listOfTotients[n].Number);
                    }
                }
            }
            DateTime end = DateTime.Now;
            Console.WriteLine(sumOfSolution + "!!!" + (end - start).TotalMinutes);
        }

        public static long GFunc(TotientPair n, TotientPair m)
        {
            long dmNdivisor = m.Number;
            long dmN = m.Number % n.Number;
            long ntmt = n.Totient - m.Totient;
            
            while (ntmt < 0)
            {
                ntmt = ntmt + n.Number;
            }
            if(dmN != 1)
            {
                if (!(m.Number % dmN == 0)) return 0;
                else {
                    dmNdivisor = m.Number / dmN;
                }
            }
            //Console.WriteLine("  m.N=" + m.Number + "   m.T=" + m.Totient + "  dmNdiv =" + dmNdivisor + "   ntmt=" + ntmt + "  n.N=" + n.Number + "  n.T=" + n.Totient + "Gfunc Returns: " + (m.Totient + dmNdivisor * ntmt) % (dmNdivisor * n.Number));
            return (m.Totient + dmNdivisor * ntmt) % (dmNdivisor * n.Number);
            

        }

        [Fact]
        public void Givenexample_GfuncShouldReturn10()
        {
            TotientPair TwoFour = new TotientPair(1);
            TwoFour.Number = 4;
            TwoFour.Totient = 2;
            TotientPair FourSix = new TotientPair(1);
            FourSix.Number = 6;
            FourSix.Totient = 4;
            long result = GFunc(TwoFour,FourSix);
            Assert.Equal(10, result);
        }


        [Fact]
        public void Given456547347_TotientShouldReturn301961520()
        {
            long result = TotientPair.totientOf(456547347);
            Assert.Equal(301961520, result);
        }

        [Fact]
        public void Given306765572_TotientShouldReturn153382784()
        {
            long result = TotientPair.totientOf(30676556672);
            Assert.Equal(15334681600, result);
        }

        [Fact]
        public void Given6453_PrimeFactorsShouldReturn3and239()
        {
            List<Factor> expected = new List<Factor>();
            expected.Add(new Factor(3, 3));
            expected.Add(new Factor(239, 1));
            List<Factor> result = TotientPair.primeFactors(6453);
            Assert.Equal(expected[0].Number, result[0].Number);
            Assert.Equal(expected[0].Occurance, result[0].Occurance);
            Assert.Equal(expected[1].Number, result[1].Number);
            Assert.Equal(expected[1].Occurance, result[1].Occurance);
        }

       

    }
    public class Factor
    {
        public long Number { get; set; }

        public long Occurance { get; set; }

        public Factor(long number, long occurance)
        {
            Number = number;
            Occurance = occurance;
        }
    }
   public class TotientPair
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