using System;
using System.Collections;

namespace AustriaSki
{
    public class PathFinder
    {
        private readonly int[,] input;
        private readonly int numberOfColumns;
        private readonly int numberOfRows;
        private readonly int maxValue;
        private ArrayList stepOfPath = new ArrayList();

        /// <summary>
        /// Se realiza la clase que contendrá los métodos para la busqueda de la ruta dentro de la matríz de cualquier tamaño
        /// el objetibo es primero encontrar el mayor valor y de esta forma realizar un método recursivo que se llame así mismo para las diferentes direcciones
        /// </summary>       

        public PathFinder(int[,] input, int numberOfColumns, int numberOfRows, int maxValue)
        {
            this.input = input;
            this.numberOfColumns = numberOfColumns;
            this.numberOfRows = numberOfRows;
            this.maxValue = maxValue;
        }

        public ArrayList GetStepOfPath(int fromX, int fromY)
        {
            stepOfPath = new ArrayList();
            crossPoint(new Point(fromX, fromY), maxValue, new ArrayList());
            return stepOfPath;
        }

        private void crossPoint(Point point, int value, ArrayList path)
        {
            int currentValue = input[point.pointX, point.pointY];
            int srcX = point.pointX;
            int srcY = point.pointY;

            if (path == null)
            {
                path = new ArrayList();
            }

            if (!someonePossible(srcX, srcY, currentValue))
            {
                path.Add(input[srcX, srcY]);
                if (stepOfPath == null || stepOfPath.Count == 0 || stepOfPath.Count <= path.Count)
                {
                    if (stepOfPath.Count == path.Count)
                    {
                        int overAllStepDepth = calculateSteepDepth(stepOfPath);
                        int currentStepDept = calculateSteepDepth(path);
                        if (currentStepDept > overAllStepDepth)
                        {
                            stepOfPath = new ArrayList(path);
                        }
                    }
                    else
                    {
                        stepOfPath = new ArrayList(path);
                    }
                }

                Console.WriteLine("the path is " + path.ToString());
                path.Remove(path.Count - 1);
                return;
            }

            path.Add(input[srcX, srcY]);
            value = input[srcX, srcY];

            //Move up
            int prevX = srcX - 1;
            if (nextPossible(prevX, srcY, value) && (srcX != (numberOfColumns - 1)))
            {
                crossPoint(new Point(prevX, srcY), value, path);
            }

            //move right
            int nextY = srcY + 1;
            if (nextPossible(srcX, nextY, value))
            {
                crossPoint(new Point(srcX, nextY), value, path);
            }

            //move left
            int prevY = srcY - 1;
            if (nextPossible(srcX, prevY, value))
            {
                crossPoint(new Point(srcX, prevY), value, path);
            }

            //move down
            int nextX = srcX + 1;
            if (nextPossible(nextX, srcY, value))
            {
                crossPoint(new Point(nextX, srcY), value, path);
            }

            path.Remove(path.Count - 1);
        }

        public bool nextPossible(int nextX, int nextY, int value)
        {
            if ((nextX <= numberOfColumns - 1 && nextX >= 0) && (nextY <= numberOfRows - 1 && nextY >= 0))
            {
                return value > input[nextX, nextY];
            }

            return false;
        }

        public bool someonePossible(int x, int y, int someValue)
        {
            bool leftPossible = nextPossible(x, y - 1, someValue);
            bool rightPossible = nextPossible(x, y - 1, someValue);
            bool upPossible = nextPossible(x - 1, y, someValue);
            bool downPossible = nextPossible(x + 1, y, someValue);

            return leftPossible || rightPossible || upPossible || downPossible;
        }

        private static int calculateSteepDepth(ArrayList steepestPath)
        {
            return (int)steepestPath.ToArray().GetValue(0) - (int)steepestPath.ToArray().GetValue(steepestPath.Count - 1);
        }
    }
}
