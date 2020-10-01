using System;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;

namespace myGameOfLife
{
    internal class Program
    {
        private static int rowHeight = Console.WindowHeight - 1;
        private static int columnWidth = Console.WindowWidth;       
        private static Random random = new Random();
        private static string buffer = "";
        private static bool[,] currentGeneration = new bool[rowHeight, columnWidth];
        private static bool[,] newGeneration = new bool[rowHeight, columnWidth];

        private static void Main(string[] args)
        {
            Initialize();
            while (true)
            {
                Console.SetCursorPosition(0, 0);
                RunGame();
                Thread.Sleep(2000);
            }
        }

        private static void Initialize()
        {
            currentGeneration[10, 6] = true;
            currentGeneration[11, 7] = true;
            currentGeneration[12, 5] = true;
            currentGeneration[12, 6] = true;
            currentGeneration[12, 7] = true;

            //grid = newGeneration;
            Display();
        }

        private static void RunGame()
        {
            for (int rowIndex = 0; rowIndex < rowHeight; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < columnWidth; columnIndex++)
                {
                    int countNumberOfLives = 0;
                    
                    //
                    for (int neighbourRowIndex = rowIndex - 1 ; neighbourRowIndex < rowIndex + 2; neighbourRowIndex++)
                    {
                        for (int neighbourColumnIndex = columnIndex - 2; neighbourColumnIndex < columnIndex + 2; neighbourColumnIndex++)
                        {
                            if (!((neighbourRowIndex < 0 || neighbourColumnIndex < 0) || (neighbourRowIndex >= rowHeight || neighbourColumnIndex >= columnWidth)))
                            {
                                if( !(neighbourRowIndex == rowIndex && neighbourColumnIndex == columnIndex))
                                {
                                    if (currentGeneration[neighbourRowIndex, neighbourColumnIndex])
                                    {
                                        countNumberOfLives++;
                                    }
                                }
                                
                            }
                        }
                    }

                    //Life
                    if (currentGeneration[rowIndex, columnIndex])
                    {
                        if (countNumberOfLives < 2 || countNumberOfLives > 3)
                        {
                            newGeneration[rowIndex, columnIndex] = false;
                        }
                        else
                        {
                            newGeneration[rowIndex, columnIndex] = true;
                        }
                    }
                    else
                    {
                        if (countNumberOfLives == 3)
                        {
                            newGeneration[rowIndex, columnIndex] = true;
                        }
                        else
                        {
                            newGeneration[rowIndex, columnIndex] = false;
                        }
                      
                    }
                }
            }

            currentGeneration = newGeneration.Clone() as bool[,];
            //currentGeneration = latestGeneration.to
            Display();
        }

        private static void Display()
        {
            buffer = "";
            for (int rowIndex = 0; rowIndex < rowHeight; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < columnWidth; columnIndex++)
                {
                    if (columnIndex == columnWidth - 1)
                    {
                        if (currentGeneration[rowIndex, columnIndex])
                        {
                            buffer += "*\n";
                        }
                        else
                        {
                            buffer += " \n";
                        }
                    }
                    else
                    {
                        if (currentGeneration[rowIndex, columnIndex])
                        {
                            buffer += "*";
                        }
                        else
                        {
                            buffer += " ";
                        }
                    }
                }
            }

            Console.Write(buffer);
        }
    }
}