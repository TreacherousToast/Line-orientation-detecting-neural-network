﻿using System;
using System.IO;

namespace Line_direction_detecting_neural_network
{
    class Program
    {
        static void Main(string[] args)
        {
            ProgramRun p = new ProgramRun();
            p.setup();
        }
    }
    class ProgramRun
    {
        int[,,] straightLineFilters = new int[3, 3, 100];
        int[,,] diagonalLineFilters = new int[3, 3, 100];
        int numOfImages = 10;
        int straightLineScore = 0;
        int diagonalLineScore = 0;
        string finalDirection = "";

        public void setup()
        {
            for (int i = 0; i < 100; i++)
            {
                straightLineFilters[1, 1, i] = 2;
                straightLineFilters[0, 1, i] = 2;
                straightLineFilters[2, 1, i] = 2;
                straightLineFilters[0, 0, i] = 2;
                straightLineFilters[1, 0, i] = 2;
                straightLineFilters[2, 0, i] = 2;
                straightLineFilters[0, 2, i] = 2;
                straightLineFilters[1, 2, i] = 2;
                straightLineFilters[2, 2, i] = 2;
            }
            for (int i = 0; i < 100; i++)
            {
                diagonalLineFilters[1, 1, i] = 2;
                diagonalLineFilters[0, 1, i] = 2;
                diagonalLineFilters[2, 1, i] = 2;
                diagonalLineFilters[0, 0, i] = 2;
                diagonalLineFilters[1, 0, i] = 2;
                diagonalLineFilters[2, 0, i] = 2;
                diagonalLineFilters[0, 2, i] = 2;
                diagonalLineFilters[1, 2, i] = 2;
                diagonalLineFilters[2, 2, i] = 2;
            }
            
            trainStraightLineFilters();
            trainDiagonalLineFilters();
            for (int i = 0; i < 10; i++)
            {
                checkImage(i+".txt");
                printToScreen();
            }
        }
        public void printToScreen()
        {
            Console.WriteLine(finalDirection);
        }
        public void trainStraightLineFilters()
        {
            int amountOfArrayWrittenTo = 0;
            for (int i = 0; i < (numOfImages / 2); i++)
            {
                string[] rawImage = File.ReadAllLines(i + ".txt");
                int[,] unpackedImage = new int[6, 6];
                for (int a = 0; a < 6; a++)
                {
                    string currLine = rawImage[a];
                    for (int j = 0; j < 6; j++)
                    {
                        unpackedImage[j, a] = Convert.ToInt32(currLine[j]);
                    }
                }
                for (int a = 1; a < 5; a++)
                {
                    for (int b = 1; b < 5; b++)
                    {
                        straightLineFilters[1,1, amountOfArrayWrittenTo] = unpackedImage[b, a];
                        straightLineFilters[1,2, amountOfArrayWrittenTo] = unpackedImage[b, a + 1];
                        straightLineFilters[1,0, amountOfArrayWrittenTo] = unpackedImage[b, a - 1];
                        straightLineFilters[2,1, amountOfArrayWrittenTo] = unpackedImage[b + 1, a];
                        straightLineFilters[0,1, amountOfArrayWrittenTo] = unpackedImage[b - 1, a];
                        straightLineFilters[2,2, amountOfArrayWrittenTo] = unpackedImage[b + 1, a + 1];
                        straightLineFilters[0,0, amountOfArrayWrittenTo] = unpackedImage[b - 1, a - 1];
                        straightLineFilters[2,0, amountOfArrayWrittenTo] = unpackedImage[b + 1, a - 1];
                        straightLineFilters[0,2, amountOfArrayWrittenTo] = unpackedImage[b - 1, a + 1];
                        amountOfArrayWrittenTo++;
                    }
                }
            }
        }
        public void trainDiagonalLineFilters()
        {
            int amountOfArrayWrittenTo = 0;
            for (int i = (numOfImages/2); i < numOfImages; i++)
            {
                string[] rawImage = File.ReadAllLines(i + ".txt");
                int[,] unpackedImage = new int[6, 6];
                for (int a = 0; a < 6; a++)
                {
                    string currLine = rawImage[a];
                    for (int j = 0; j < 6; j++)
                    {
                        unpackedImage[j, a] = Convert.ToInt32(currLine[j]);
                    }
                }
                for (int a = 1; a < 5; a++)
                {
                    for (int b = 1; b < 5; b++)
                    {
                        diagonalLineFilters[1,1, amountOfArrayWrittenTo] = unpackedImage[b, a];
                        diagonalLineFilters[1,2, amountOfArrayWrittenTo] = unpackedImage[b, a + 1];
                        diagonalLineFilters[1,0, amountOfArrayWrittenTo] = unpackedImage[b, a - 1];
                        diagonalLineFilters[2,1, amountOfArrayWrittenTo] = unpackedImage[b + 1, a];
                        diagonalLineFilters[0,1, amountOfArrayWrittenTo] = unpackedImage[b - 1, a];
                        diagonalLineFilters[2,2, amountOfArrayWrittenTo] = unpackedImage[b + 1, a + 1];
                        diagonalLineFilters[0,0, amountOfArrayWrittenTo] = unpackedImage[b - 1, a - 1];
                        diagonalLineFilters[2,0, amountOfArrayWrittenTo] = unpackedImage[b + 1, a - 1];
                        diagonalLineFilters[0,2, amountOfArrayWrittenTo] = unpackedImage[b - 1, a + 1];
                        amountOfArrayWrittenTo++;
                    }
                }
            }
        }
        
        public void checkImage(string fileName)
        {
            string[] rawImage = File.ReadAllLines(fileName);
            int[,] unpackedImage = new int[6, 6];
            for (int a = 0; a < 6; a++)
                {
                    string currLine = rawImage[a];
                    for (int j = 0; j < 6; j++)
                    {
                        unpackedImage[j, a] = Convert.ToInt32(currLine[j]);
                    }
                }
            for (int i = 1; i < 5; i++)
            {
                for (int j = 1; j < 5; j++)
                {
                    for (int a = 0; a < 100; a++)
                    {
                        if (unpackedImage[j, i] == straightLineFilters[1, 1, a] && unpackedImage[j, i + 1] == straightLineFilters[1, 2, a] && unpackedImage[j, i - 1] == straightLineFilters[1, 0, a] && unpackedImage[j + 1, i] == straightLineFilters[2, 1, a] && unpackedImage[j - 1, i] == straightLineFilters[0, 1, a] && unpackedImage[j + 1, i + 1] == straightLineFilters[2, 2, a] && unpackedImage[j - 1, i - 1] == straightLineFilters[0, 0, a] && unpackedImage[j + 1, i - 1] == straightLineFilters[2, 0, a] && unpackedImage[j - 1, i + 1] == straightLineFilters[0, 2, a])
                        {
                            straightLineScore++;
                        }
                        if (unpackedImage[j, i] == diagonalLineFilters[1, 1, a] && unpackedImage[j, i + 1] == diagonalLineFilters[1, 2, a] && unpackedImage[j, i - 1] == diagonalLineFilters[1, 0, a] && unpackedImage[j + 1, i] == diagonalLineFilters[2, 1, a] && unpackedImage[j - 1, i] == diagonalLineFilters[0, 1, a] && unpackedImage[j + 1, i + 1] == diagonalLineFilters[2, 2, a] && unpackedImage[j - 1, i - 1] == diagonalLineFilters[0, 0, a] && unpackedImage[j + 1, i - 1] == diagonalLineFilters[2, 0, a] && unpackedImage[j - 1, i + 1] == diagonalLineFilters[0, 2, a])
                        {
                            diagonalLineScore++;
                        }
                    }
                }
            }
            if (straightLineScore > diagonalLineScore)
            {
                finalDirection = "Straight";
            }
            else if (straightLineScore < diagonalLineScore)
            {
                finalDirection = "Diagonal";
            }
            straightLineScore = 0;
            diagonalLineScore = 0;
        }
    }
}
