using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DMSpike
{
    public class Program
    {
        //http://projecteuler.chat/viewtopic.php?t=4014
        public static int GFunction(int a, int n, int b, int m)
        {
            var solutions = new List<int>();
            for (int i = 1; i <= 10; i++)
            {
                // 2 % 4 = 2 ie 2 is a solution
                // 10 % 4 = 2 ie 10 is a solution 
                if (i%n == a) solutions.Add(i);
            }

            int result2 = 0;
            foreach (var solution in solutions.OrderByDescending(x => x))
            {
                if (solution % m == b) result2 = solution;
            }
            return result2;
        }

        public class NAndM
        {
            public int N { get; set; }
            public int M { get; set; }
        }

        public static List<NAndM> DoLoopOfNAndM()
        {
            // 5 <= n < m < 10
            // n = 5   m = 6
            // 12million combinations of n and m
            int start = 1000000;  // 5 is a good test
            int end = 1005000; // 10 is a good test
            var list = new List<NAndM>();
            for (int n = start; n < end; n++)
            {
                for (int m = n+1; m < end; m++)
                {
                    list.Add(new NAndM {N = n, M = m});
                }
            }
            return list;
        }

        public static int FFunction(int n, int m)
        {
            // TODO: implement
            return 0;
        }

        // Part 2 - FFunction

        // Part 3
        [Fact]
        public void LoopTest()
        {
            var result = DoLoopOfNAndM();
            Assert.Equal(12497500, result.Count);
        }

        // Answer is to run the loop, run FFunction, and sum

        // Part 1
        [Fact]
        public void GivenFirstTestCase_ShouldReturn10()
        {
            int result = GFunction(2, 4, 4, 6);
            Assert.Equal(10, result);
        }

        [Fact]
        public void GivenNoSolution_ShouldReturn0()
        {
            int result = GFunction(3, 4, 4, 6);
            Assert.Equal(0, result);
        }
    }
}