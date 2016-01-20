using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.Remoting.Messaging;
using Xunit;

namespace DMSpike
{
    public class Program
    {
        //http://projecteuler.chat/viewtopic.php?t=4014
        public static int GFunction(int a, int n, int b, int m)
        {
            // wrong
            // var result = a%n;

            // correct
            // var result %4 = 2;
            // 10 % 4 = 2
            // how to change around formula to get 10 as the result?
            // try brute force
            var solutions = new List<int>();
            for (int i = 1; i <= 10; i++)
            {
                // 2 % 4 = 2 !! ie 2 is a solution for result1
                // 10 % 4 = 2 ie 10 is a solution 
                if (i%n == a) solutions.Add(i);
            }

            int result2 = 0;
            foreach (var solution in solutions)
            {
                if (solution % m == b) result2 = solution;
            }
            return result2;
        }

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