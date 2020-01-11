using System;

namespace Exercise08
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //OR PROBLEM
            Perceptron perceptron = new Perceptron(2, 0.1f);
            Point[] trainpoints = new Point[900];
            Point[] testpoints = new Point[100];
            int good = 0;
            for (int i = 0; i < trainpoints.Length; i++)
            {
                trainpoints[i] = new Point(50);
                perceptron.LearnPerceptron(trainpoints[i].cooridnates, trainpoints[i].Label);
                if (i % trainpoints.Length / 10 == 0)
                    Console.WriteLine($"Weight 1:{perceptron.weights[0]}   Weight 2:{perceptron.weights[1]}");
            }

            for (int i = 0; i < testpoints.Length; i++)
            {
                testpoints[i] = new Point(75);
                int result = perceptron.TryGuess(testpoints[i].cooridnates);
                if (result == testpoints[i].Label)
                {
                    good++;
                }
                //Console.Write(good + " ");
                //if ((i + 1) % 20 == 0)
                //{
                //    //   Console.ReadKey();
                //    Console.WriteLine();
                //}
            }
            Console.Write("Perceptron worked for: " + (double)good / (double)testpoints.Length * 100 + " procent of test cases");
            Console.ReadKey();
            Console.WriteLine("\n\n\n\n");

            //XOR PROBLEM
            for (int j = 0; j < 100; j++)
            {
                Perceptron perceptronxor = new Perceptron(2, 0.1f);
                Pointxor[] trainpointsxor = new Pointxor[90000];
                Pointxor[] testpointsxor = new Pointxor[100];
                good = 0;
                for (int i = 0; i < trainpoints.Length; i++)
                {
                    trainpointsxor[i] = new Pointxor();
                    perceptronxor.LearnPerceptron(trainpointsxor[i].cooridnates, trainpointsxor[i].Label);
                    //     if (i % trainpointsxor.Length / 10 == 0)
                    //     Console.WriteLine($"Weight 1:{perceptronxor.weights[0]}   Weight 2:{perceptronxor.weights[1]}");
                }

                for (int i = 0; i < testpointsxor.Length; i++)
                {
                    testpointsxor[i] = new Pointxor();
                    int result = perceptronxor.TryGuess(testpointsxor[i].cooridnates);
                    //   Console.WriteLine($"{i}Pointxor  x:{testpointsxor[i].cooridnates[0]}  y: {testpointsxor[i].cooridnates[1]}    label={testpointsxor[i].Label}   result: {result}");
                    if (result == testpointsxor[i].Label)
                    {
                        good++;
                    }
                }
                Console.Write("Perceptron XOR worked for: " + (double)good / (double)testpointsxor.Length * 100 + " procent of test cases\n");
            }

            Console.ReadKey();
        }
    }
}