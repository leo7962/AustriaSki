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
            CrossPoint(new Point(fromX, fromY), maxValue, new ArrayList());
            return stepOfPath;
        }

        private void CrossPoint(Point point, int value, ArrayList path)
        {
            int currentValue = input[point.srcX, point.srcY];
            int srcX = point.srcX;
            int srcY = point.srcY;

            if (path == null)
            {
                path = new ArrayList();
            }

            if (!SomeonePossible(srcX, srcY, currentValue))
            {
                path.Add(input[srcX, srcY]);
                if (stepOfPath == null || stepOfPath.Count == 0 || stepOfPath.Count <= path.Count)
                {
                    if (stepOfPath.Count == path.Count)
                    {
                        int overAllStepDepth = CalculateSteepDepth(stepOfPath);
                        int currentStepDept = CalculateSteepDepth(path);
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

                Console.WriteLine("the path is " + PrintList(path));
                path.Remove(path.Count - 1);
                return;
            }

            path.Add(input[srcX, srcY]);
            value = input[srcX, srcY];

            //Move up
            int prevX = srcX - 1;
            if (NextPossible(prevX, srcY, value) && (srcX != (numberOfColumns - 1)))
            {
                CrossPoint(new Point(prevX, srcY), value, path);
            }

            //move right
            int nextY = srcY + 1;
            if (NextPossible(srcX, nextY, value))
            {
                CrossPoint(new Point(srcX, nextY), value, path);
            }

            //move left
            int prevY = srcY - 1;
            if (NextPossible(srcX, prevY, value))
            {
                CrossPoint(new Point(srcX, prevY), value, path);
            }

            //move down
            int nextX = srcX + 1;
            if (NextPossible(nextX, srcY, value))
            {
                CrossPoint(new Point(nextX, srcY), value, path);
            }

            path.Remove(path.Count - 1);
        }

        private bool NextPossible(int nextX, int nextY, int value)
        {
            if ((nextX <= numberOfColumns - 1 && nextX >= 0) && (nextY <= numberOfRows - 1 && nextY >= 0))
            {
                return value > input[nextX, nextY];
            }

            return false;
        }

        private bool SomeonePossible(int x, int y, int value)
        {
            bool leftPossible = NextPossible(x, y - 1, value);
            bool rightPossible = NextPossible(x, y - 1, value);
            bool upPossible = NextPossible(x - 1, y, value);
            bool downPossible = NextPossible(x + 1, y, value);

            return leftPossible || rightPossible || upPossible || downPossible;
        }

        private static int CalculateSteepDepth(ArrayList stepOfPath)
        {
            return (int)stepOfPath.ToArray().GetValue(0) - (int)stepOfPath.ToArray().GetValue(stepOfPath.Count - 1);
        }


        private string PrintList(ArrayList list)
        {
            string r = "";
            foreach (object item in list)
            {
                r += (((r) != "") ? "," : "") + item;
            }

            return "[" + r + "]";
        }
    }
}
