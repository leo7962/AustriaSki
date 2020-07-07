using System;
using System.Collections;
using System.IO;

namespace AustriaSki
{
    class PrincipalSki
    {
        public static int maxColumns = 1000, maxRows = 1000;

        static int[,] sampleMapInput = new int[4, 4]{
        { 4, 8, 7, 3 },
        { 2, 5, 9, 3 },
        { 6, 3, 2, 5 },
        { 4, 4, 1, 6 }
        };

        public static int[,] input = new int[maxColumns, maxRows];

        public static ArrayList overAllstepOfPath = new ArrayList();

        public static void readLines()
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader reader = new StreamReader("C:/Users/Ingen/source/repos/AustriaSki/AustriaSki/map.txt"))
                {
                    // Read the stream to a string, and write the string to the console.                                       
                    int i = 0;
                    string text = null;

                    while ((text = reader.ReadLine()) != null)
                    {
                        string[] words = text.Split(" ");
                        int j = 0;
                        foreach (var word in words)
                        {
                            input[i, j] = int.Parse(word);

                            j++;
                        }
                        i++;
                    }
                    reader.Close();
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
            readLines();
            //Descomentar si quiere realizar la prueba
            //PathFinder pathFinder = new PathFinder(sampleMapInput, maxRows, maxColumns, 1500);

            PathFinder pathFinder = new PathFinder(input, maxColumns, maxRows, 1500);
            FindAllSteepest(pathFinder, maxColumns, maxRows);
        }

        public static void FindAllSteepest(PathFinder pathFinder, int columns, int rows)
        {
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    ArrayList stepOfPath = new ArrayList(pathFinder.GetStepOfPath(i, j));

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

        private static int calculateSteepDepth(ArrayList stepOfPath)
        {
            return (int)stepOfPath.ToArray().GetValue(0) - (int)stepOfPath.ToArray().GetValue(stepOfPath.Count - 1);
        }

        private static void setNewSteepestPath(ArrayList stepOfPath)
        {
            overAllstepOfPath = new ArrayList(stepOfPath);
        }
    }
}
