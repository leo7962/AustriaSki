using System;
using System.Collections;
using System.IO;

namespace AustriaSki
{
    class PrincipalSki
    {
        public static int maxColumns = 1000, maxRows = 1000;

        static int[,] sampleMapInput = new int [4,4]{
        { 4, 8, 7, 3 },
        { 2, 5, 9, 3 },
        { 6, 3, 2, 5 },
        { 4, 4, 1, 6 }
        };

        public static int[,] input = new int[maxColumns, maxRows];

        public static ArrayList overAllSteepestPath = new ArrayList();

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

        public static void FindAllSteepest(PathFinder steepestPathFinder)
        {
            for (int i = 0; i < maxColumns; i++)
            {
                for (int j = 0; j < maxRows; j++)
                {
                    ArrayList steepPath = new ArrayList(steepestPathFinder.GetStepOfPath(i,j));

                    int overAllSteepestPathSize = overAllSteepestPath.Count;
                    int steepestPathSize = steepPath.Count;

                    if (((overAllSteepestPathSize == 0 || overAllSteepestPathSize <= steepestPathSize)) && steepestPathSize > 0)
                    {
                        if (overAllSteepestPathSize == steepestPathSize)
                        {
                            int overAllSteepDepth = calculateSteepDepth(overAllSteepestPath);
                            int currentSteepDepth = calculateSteepDepth(steepPath);

                            if (currentSteepDepth > overAllSteepDepth)
                            {
                                setNewSteepestPath(steepPath);
                            }                            
                        }
                        else
                        {
                            setNewSteepestPath(steepPath);
                        }
                    }
                }
            }
            Console.WriteLine("El camino más empinado es " + overAllSteepestPath.ToString() + " de longitud " + overAllSteepestPath.Count + " y profundidad " + calculateSteepDepth(overAllSteepestPath));
        }

        private static int calculateSteepDepth(ArrayList steepestPath)
        {
            return (int)steepestPath.ToArray().GetValue(0) - (int)steepestPath.ToArray().GetValue(steepestPath.Count - 1);
        }

        private static void setNewSteepestPath(ArrayList steepestPath)
        {
            overAllSteepestPath = new ArrayList(steepestPath);
        }
    }
}
