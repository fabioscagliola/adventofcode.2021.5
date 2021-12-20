using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace com.fabioscagliola.AdventOfCode202105
{
    class Program
    {
        /// <summary>
        /// Represent a line of hydrothermal vents 
        /// </summary>
        class Line
        {
            public int X1 { get; protected set; }
            public int Y1 { get; protected set; }
            public int X2 { get; protected set; }
            public int Y2 { get; protected set; }
            public int MaxX => X1 > X2 ? X1 : X2;
            public int MaxY => Y1 > Y2 ? Y1 : Y2;

            public Line(int x1, int y1, int x2, int y2)
            {
                X1 = x1;
                Y1 = y1;
                X2 = x2;
                Y2 = y2;
            }

        }

        static void Main()
        {
            List<Line> lineList = new List<Line>();

            Regex regex = new Regex(@"(?:\d+)");

            MatchCollection matchCollection = regex.Matches(File.ReadAllText("Input1.txt"));

            for (int i = 0; i < matchCollection.Count; i += 4)
            {
                int x1 = int.Parse(matchCollection[i].Value);
                int y1 = int.Parse(matchCollection[i + 1].Value);
                int x2 = int.Parse(matchCollection[i + 2].Value);
                int y2 = int.Parse(matchCollection[i + 3].Value);
                Line line = new Line(x1, y1, x2, y2);
                lineList.Add(line);
            }

            int[,] matrix = new int[lineList.Max(line => line.MaxX) + 1, lineList.Max(line => line.MaxY) + 1];

            // PART 1 

            foreach (Line line in lineList.FindAll(line => line.X1 == line.X2))
            {
                if (line.Y1 > line.Y2)
                {
                    for (int y = line.Y1; y > line.Y2 - 1; y--)
                    {
                        matrix[line.X1, y]++;
                    }
                }
                else
                {
                    for (int y = line.Y1; y < line.Y2 + 1; y++)
                    {
                        matrix[line.X1, y]++;
                    }
                }
            }

            foreach (Line line in lineList.FindAll(line => line.Y1 == line.Y2))
            {
                if (line.X1 > line.X2)
                {
                    for (int x = line.X1; x > line.X2 - 1; x--)
                    {
                        matrix[x, line.Y1]++;
                    }
                }
                else
                {
                    for (int x = line.X1; x < line.X2 + 1; x++)
                    {
                        matrix[x, line.Y1]++;
                    }
                }
            }

            int pointsExcludingDiagonalLines = 0;

            for (int x = 0; x < matrix.GetLength(0); x++)
                for (int y = 0; y < matrix.GetLength(1); y++)
                    if (matrix[x, y] > 1)
                        pointsExcludingDiagonalLines++;

            Console.WriteLine($"Excluding diagonal lines, the number of points where at least two lines overlap is {pointsExcludingDiagonalLines}");

            // PART 2 

            foreach (Line line in lineList.FindAll(line => line.X1 != line.X2 && line.Y1 != line.Y2))
            {
                if (line.X1 > line.X2)
                {
                    if (line.Y1 > line.Y2)
                    {
                        for (int x = line.X1, y = line.Y1; x > line.X2 - 1 && y > line.Y2 - 1; x--, y--)
                        {
                            matrix[x, y]++;
                        }
                    }
                    else
                    {
                        for (int x = line.X1, y = line.Y1; x > line.X2 - 1 && y < line.Y2 + 1; x--, y++)
                        {
                            matrix[x, y]++;
                        }
                    }
                }
                else
                {
                    if (line.Y1 > line.Y2)
                    {
                        for (int x = line.X1, y = line.Y1; x < line.X2 + 1 && y > line.Y2 - 1; x++, y--)
                        {
                            matrix[x, y]++;
                        }
                    }
                    else
                    {
                        for (int x = line.X1, y = line.Y1; x < line.X2 + 1 && y < line.Y2 + 1; x++, y++)
                        {
                            matrix[x, y]++;
                        }
                    }
                }
            }

            int pointsIncludingDiagonalLines = 0;

            for (int x = 0; x < matrix.GetLength(0); x++)
                for (int y = 0; y < matrix.GetLength(1); y++)
                    if (matrix[x, y] > 1)
                        pointsIncludingDiagonalLines++;

            Console.WriteLine($"Including diagonal lines, the number of points where at least two lines overlap is {pointsIncludingDiagonalLines}");

        }

    }
}

