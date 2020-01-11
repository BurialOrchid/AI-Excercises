using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Exercise08
{
    internal class Point
    {
        public float[] cooridnates;
        public int Label { get; set; }

        public Point(int size)
        {
            cooridnates = new float[2];
            cooridnates[0] = new Random().Next(-size, size);
            cooridnates[1] = new Random().Next(-size, size);

            if (cooridnates[0] > cooridnates[1]) Label = 1;
            else Label = 0;
        }
    }

    internal class Pointxor
    {
        public float[] cooridnates;
        public int Label { get; set; }

        public Pointxor()
        {
            cooridnates = new float[2];
            cooridnates[0] = new Random().Next(0, 2);

            if (cooridnates[0] == 0)
            {
                cooridnates[0] = -1;
            }

            cooridnates[1] = new Random().Next(0, 2);
            if (cooridnates[1] == 0)
            {
                cooridnates[1] = -1;
            }

            if (cooridnates[0] != cooridnates[1]) Label = 1;
            else Label = 0;
        }
    }

    internal class Perceptron
    {
        public readonly float[] weights;
        public readonly float learningRate;

        public Perceptron(int inputsNumber, float learningRate)
        {
            weights = new float[inputsNumber];
            this.learningRate = learningRate;

            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] = new Random().Next(-1, 1);
            }
        }

        public int ActivationFunction(float[] inputs)
        {
            float sum = 0;

            for (int i = 0; i < inputs.Length; i++)
            {
                sum += inputs[i] * weights[i] + 1;
            }

            if (sum >= 0)
                return 1;
            else return 0;
        }

        public int TryGuess(float[] inputs)
        {
            return ActivationFunction(inputs);
        }

        public void LearnPerceptron(float[] inputs, int label)
        {
            int guess = TryGuess(inputs);
            int error = label - guess;

            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] += error * inputs[i] * learningRate;
            }
        }
    }
}