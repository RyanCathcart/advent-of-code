namespace Day9;

// Prompt link: https://adventofcode.com/2022/day/9
// Part 1 Solution: 5619
// Part 2 Solution: 2376
public class Program
{
    static void Main()
    {
        string inputPath = Directory.GetCurrentDirectory() + @"\input.txt";

        Console.WriteLine("Part 1 Solution: " + Part1(inputPath));
        Console.WriteLine("Part 2 Solution: " + Part2(inputPath));
    }

    static int Part1(string inputPath)
    {
        int[] headPosition = new int[] { 0, 0 }, tailPosition = new int[] { 0, 0 };
        var tailCoordsHistory = new List<int[]> { tailPosition };

        using (var reader = new StreamReader(inputPath))
        {
            string? line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] splitLine = line.Split(' ');
                char direction = splitLine[0][0];
                int distance = int.Parse(splitLine[1]);

                for (int step = distance; step > 0; step--)
                {
                    switch (direction)
                    {
                        case 'U':
                            headPosition[1]++;
                            break;
                        case 'D':
                            headPosition[1]--;
                            break;
                        case 'L':
                            headPosition[0]--;
                            break;
                        case 'R':
                            headPosition[0]++;
                            break;
                    }

                    tailPosition = UpdateTail(headPosition, tailPosition);

                    tailCoordsHistory.Add((int[])tailPosition.Clone());
                }
            }
        }

        var uniqueCoords = new List<int[]>();

        foreach (int[] tCoord in tailCoordsHistory)
        {
            bool coordIsUnique = true;

            foreach (int[] uCoord in uniqueCoords)
            {
                if (tCoord[0] == uCoord[0] && tCoord[1] == uCoord[1]) coordIsUnique = false;
            }

            if (coordIsUnique) uniqueCoords.Add(tCoord);
        }

        return uniqueCoords.Count;
    }

    static int Part2(string inputPath)
    {
        var knots = new int[10][];

        for (int i = 0; i < knots.Length; i++)
        {
            knots[i] = new int[] { 0, 0 };
        }

        var tailCoordsHistory = new List<int[]> { knots[9] };

        using (var reader = new StreamReader(inputPath))
        {
            string? line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] splitLine = line.Split(' ');
                char direction = splitLine[0][0];
                int distance = int.Parse(splitLine[1]);

                for (int step = distance; step > 0; step--)
                {
                    switch (direction)
                    {
                        case 'U':
                            knots[0][1]++;
                            break;
                        case 'D':
                            knots[0][1]--;
                            break;
                        case 'L':
                            knots[0][0]--;
                            break;
                        case 'R':
                            knots[0][0]++;
                            break;
                    }

                    for (int i = 1; i < knots.Length; i++)
                    {
                        knots[i] = UpdateTail(knots[i - 1], knots[i]);
                    }

                    tailCoordsHistory.Add((int[])knots[9].Clone());
                }
            }
        }

        var uniqueCoords = new List<int[]>();

        foreach (int[] tCoord in tailCoordsHistory)
        {
            bool coordIsUnique = true;

            foreach (int[] uCoord in uniqueCoords)
            {
                if (tCoord[0] == uCoord[0] && tCoord[1] == uCoord[1]) coordIsUnique = false;
            }

            if (coordIsUnique) uniqueCoords.Add(tCoord);
        }

        return uniqueCoords.Count;
    }

    static int[] UpdateTail(int[] headPosition, int[] tailPosition)
    {
        if (IsTouching(headPosition, tailPosition)) return tailPosition;

        // If tail is not touching, but in the same column as head
        if (tailPosition[0] == headPosition[0])
        {
            if (tailPosition[1] > headPosition[1]) tailPosition[1]--;
            else tailPosition[1]++;
        }

        // If tail is not touching, but in the same row as head
        if (tailPosition[1] == headPosition[1])
        {
            if (tailPosition[0] > headPosition[0]) tailPosition[0]--;
            else tailPosition[0]++;
        }

        // If tail is 2 diagonals away from head (needed for part 2 to work)
        if (tailPosition[0] - headPosition[0] >=  2 && tailPosition[1] - headPosition[1] >=  2) return new int[] { headPosition[0] + 1, headPosition[1] + 1 };

        if (tailPosition[0] - headPosition[0] >=  2 && tailPosition[1] - headPosition[1] <= -2) return new int[] { headPosition[0] + 1, headPosition[1] - 1 };

        if (tailPosition[0] - headPosition[0] <= -2 && tailPosition[1] - headPosition[1] <= -2) return new int[] { headPosition[0] - 1, headPosition[1] - 1 };

        if (tailPosition[0] - headPosition[0] <= -2 && tailPosition[1] - headPosition[1] >=  2) return new int[] { headPosition[0] - 1, headPosition[1] + 1 };
        

        // If tail is a 'knight's move' away from head
        if (tailPosition[0] - headPosition[0] >=  2) return new int[] { headPosition[0] + 1, headPosition[1] };

        if (tailPosition[0] - headPosition[0] <= -2) return new int[] { headPosition[0] - 1, headPosition[1] };

        if (tailPosition[1] - headPosition[1] >=  2) return new int[] { headPosition[0], headPosition[1] + 1 };

        if (tailPosition[1] - headPosition[1] <= -2) return new int[] { headPosition[0], headPosition[1] - 1 };   

        return tailPosition;
    }

    // Returns true if knotA and knotB are adjacent, diagonally adjacent, or overlapping
    static bool IsTouching(int[] knotA, int[] knotB)
    {
        return !(Math.Abs(knotA[0] - knotB[0]) > 1 || Math.Abs(knotA[1] - knotB[1]) > 1);
    }
}