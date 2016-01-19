using System;
using System.Collections.Generic;
using System.Numerics;
using Xunit;

namespace p_531
{
    public class Program
    {
        //--------------------https://projecteuler.net/problem=531----------------------------
        public static void Main()
        {
            var listOfTotients = new List<TotientPair>();
            var start = DateTime.Now;
            BigInteger sumOfSolution = 0;

            // Creating 5000 TotientPairs
            // TotientPair in the number i, followed by that number passed through TotietOf, and PrimeFactors
            for (long i = 1000000; i < 1005000; i++)
            {
                listOfTotients.Add(new TotientPair(i));
            }

            for (int n = 0; n < 4999; n++)
            {
                for (int m = n + 1; m < 5000; m++)
                {
                    long gFuncResult = GFunc(listOfTotients[n], listOfTotients[m]);
                    if (gFuncResult != 0)
                    {
                        sumOfSolution = sumOfSolution + gFuncResult;
                    }
                }
            }
            var end = DateTime.Now;
            Console.WriteLine(sumOfSolution + "!!!    Total Milliseconds:" + (end - start).TotalMilliseconds);
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
            ntmt = ntmt % n.Number;
            if (dmN != 1)
            {
                if (m.Number % dmN != 0) { return 0; }
                dmNdivisor = m.Number / dmN;
            }
            return (m.Totient + dmNdivisor * ntmt) % (dmNdivisor * n.Number);
        }

        [Fact]
        public void Givenexample_GfuncShouldReturn10()
        {
            var twoFour = new TotientPair(1);
            twoFour.Number = 4;
            twoFour.Totient = 2;
            var fourSix = new TotientPair(1);
            fourSix.Number = 6;
            fourSix.Totient = 4;
            long result = GFunc(twoFour, fourSix);
            Assert.Equal(10, result);
        }

        [Fact]
        public void Given456547347_TotientShouldReturn301961520()
        {
            long result = TotientPair.TotientOf(456547347);
            Assert.Equal(301961520, result);
        }

        [Fact]
        public void Given306765572_TotientShouldReturn153382784()
        {
            long result = TotientPair.TotientOf(30676556672);
            Assert.Equal(15334681600, result);
        }

        [Fact]
        public void GivenPrime3_TotientShouldReturn2()
        {
            long result = TotientPair.TotientOf(3);
            Assert.Equal(2, result);
        }

        [Fact]
        public void Given6453_PrimeFactorsShouldReturn3and239()
        {
            var expected = new List<Factor> {new Factor(3, 3), new Factor(239, 1)};
            var result = TotientPair.PrimeFactors(6453);
            Assert.Equal(expected[0].Number, result[0].Number);
            Assert.Equal(expected[0].Occurance, result[0].Occurance);
            Assert.Equal(expected[1].Number, result[1].Number);
            Assert.Equal(expected[1].Occurance, result[1].Occurance);
        }

        [Fact]
        public void Given1_PrimeFactorsShouldReturn1()
        {
            var expected = new List<Factor> {new Factor(1, 1)};
            var result = TotientPair.PrimeFactors(1);
            Assert.Equal(expected[0].Number, result[0].Number);
            Assert.Equal(expected[0].Occurance, result[0].Occurance);
        }

        [Fact]
        public void Given3_PrimeFactorsShouldReturn1and3()
        {
            var expected = new List<Factor> {new Factor(3, 1)};
            //expected.Add(new Factor {Number = 3, Occurance = 1});
            var result = TotientPair.PrimeFactors(3);
            Assert.Equal(expected[0].Number, result[0].Number);
            Assert.Equal(expected[0].Occurance, result[0].Occurance);
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
            Totient = TotientOf(number);
        }

        public static long TotientOf(long n)
        {
            List<Factor> factors = PrimeFactors(n);
            List<float> phiSegments = new List<float>();
            long totient = 1;
            foreach (Factor factor in factors)
            {
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

        public static List<Factor> PrimeFactors(long number)
        {
            long origNumber = number;
            while (number != 0)
            {
                var factors = new List<Factor>();

                int factorsOfTwo = 0;
                while (number % 2 == 0)
                {
                    factorsOfTwo++;
                    number = number / 2;
                }
                if (factorsOfTwo != 0)
                {
                    factors.Add(new Factor(2, factorsOfTwo));
                }

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
                if (origNumber == 1)
                {
                    factors.Add(new Factor(1, 1));
                }
                if (result == origNumber)
                {
                    return factors;
                }
                Console.WriteLine("I HAVE FAILED to factorise");
                Console.WriteLine();
            }
            return null;
        }
    }
}