using System;
using System.Collections;

namespace AustriaSki
{
    public class PathFinder
    {
        private int[,] input;
        private int numberOfColumns;
        private int numberOfRows;
        private int maxValue;
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
            int currentValue = input[point.srcX, point.srcY];
            int srcX = point.srcX;
            int srcY = point.srcY;

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

                Console.WriteLine("the path is " + printList(path));
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

        private bool nextPossible(int nextX, int nextY, int value)
        {
            if ((nextX <= numberOfColumns - 1 && nextX >= 0) && (nextY <= numberOfRows - 1 && nextY >= 0))
            {
                return value > input[nextX, nextY];
            }

            return false;
        }

        private bool someonePossible(int x, int y, int value)
        {
            bool leftPossible = nextPossible(x, y - 1, value);
            bool rightPossible = nextPossible(x, y - 1, value);
            bool upPossible = nextPossible(x - 1, y, value);
            bool downPossible = nextPossible(x + 1, y, value);

            return leftPossible || rightPossible || upPossible || downPossible;
        }

        private static int calculateSteepDepth(ArrayList stepOfPath)
        {
            return (int)stepOfPath.ToArray().GetValue(0) - (int)stepOfPath.ToArray().GetValue(stepOfPath.Count - 1);
        }


        private string printList(ArrayList list)
        {
            string r = "";
            foreach (var item in list)
            {
                r += (((r) != "") ? "," : "") + item;
            }            
            return "[" + r + "]";
        }
    }
}
