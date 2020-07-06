using System;
using System.Collections.Generic;
using System.IO;

namespace AustriaSki
{
    class PrincipalSki
    {
        //public static int maxColumns = 1000, maxRows = 1000;

        static int[,] sampleMapInput = new int[4, 4]{
        { 4, 8, 7, 3 },
        { 2, 5, 9, 3 },
        { 6, 3, 2, 5 },
        { 4, 4, 1, 6 }
        };

        //public static int[,] input = new int[maxColumns, maxRows];

        public static List<int> overAllstepOfPath = new List<int>();

        public static void readLines()
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader reader = new StreamReader("C:/Users/Ingen/source/repos/AustriaSki/AustriaSki/map.txt"))
                {
                    // Read the stream to a string, and write the string to the console.
                    String line = reader.ReadToEnd();
                    Console.WriteLine(line);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public static void Main(string[] args)
        {
            //readLines();
            PathFinder steepestPathFinder = new PathFinder(sampleMapInput, 4, 4, 1500);

            FindAllSteepest(steepestPathFinder);
        }

        public static void FindAllSteepest(PathFinder pathFinder)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    List<int> stepOfPath = new List<int>(pathFinder.GetStepOfPath(i, j));

                    int overAllSteepestPathSize = sampleMapInput.Length;
                    int steepestPathSize = stepOfPath.Count;

                    if ((overAllSteepestPathSize == 0 || overAllSteepestPathSize <= steepestPathSize) && steepestPathSize > 0)
                    {
                        if (overAllSteepestPathSize == steepestPathSize)
                        {
                            int overAllSteepDepth = calculateSteepDepth(overAllstepOfPath);
                            int currentSteepDepth = calculateSteepDepth(stepOfPath);

                            if (currentSteepDepth > overAllSteepDepth)
                            {
                                setNewSteepestPath(stepOfPath);
                            }
                        }
                        else
                        {
                            setNewSteepestPath(stepOfPath);
                        }
                    }
                }
            }
            Console.WriteLine("El camino más empinado es " + overAllstepOfPath.ToString() + " de longitud " + overAllstepOfPath.Count + " y profundidad " + calculateSteepDepth(overAllstepOfPath));
        }

        private static int calculateSteepDepth(List<int> stepOfPath)
        {
            return (int)stepOfPath.ToArray().GetValue(0) - (int)stepOfPath.ToArray().GetValue(stepOfPath.Count - 1);
        }

        private static void setNewSteepestPath(List<int> stepOfPath)
        {
            overAllstepOfPath = new List<int>(stepOfPath);
        }
    }
}
