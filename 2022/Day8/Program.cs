namespace Day8;

// Prompt link: https://adventofcode.com/2022/day/8
// Part 1 Solution: 1845
// Part 2 Solution: 230112
public class Program
{
    static void Main()
    {
        string inputPath = Directory.GetCurrentDirectory() + @"\input.txt";
        const int SIZE = 99;
        int[,] trees = new int[SIZE, SIZE];

        using (var reader = new StreamReader(inputPath))
        {
            string? line;
            int x = 0;
            //populate trees 2-D array
            while ((line = reader.ReadLine()) != null)
            {
                for (int y = 0; y < line.Length; y++)
                {
                    trees[x, y] = line[y] - 48;
                }

                x++;
            }
        }

        Console.WriteLine("Part 1 Solution: " + Part1(trees));
        Console.WriteLine("Part 2 Solution: " + Part2(trees));
    }

    static int Part1(int[,] trees)
    {
        int size = trees.GetLength(0);
        int count = (2 * size) + (2 * (size - 2));

        for (int row = 1; row < size - 1; row++)
        {
            for (int col = 1; col < size - 1; col++)
            {
                if (IsVisible(trees, row, col)) count++;
            }
        }

        return count;
    }

    public static bool IsVisible(int[,] trees, int row, int col)
    {
        int size = trees.GetLength(0);
        int currentTree = trees[row, col];

        // Peek up
        for (int tempRow = row - 1; tempRow >= 0; tempRow--)
        {
            if (trees[tempRow, col] >= currentTree) break;
            if (tempRow == 0) return true;
        }

        // Peek right
        for (int tempCol = col + 1; tempCol <= size - 1; tempCol++)
        {
            if (trees[row, tempCol] >= currentTree) break;
            if (tempCol == size - 1) return true;
        }

        // Peek down
        for (int tempRow = row + 1; tempRow <= size - 1; tempRow++)
        {
            if (trees[tempRow, col] >= currentTree) break;
            if (tempRow == size - 1) return true;
        }

        // Peek left
        for (int tempCol = col - 1; tempCol >= 0; tempCol--)
        {
            if (trees[row, tempCol] >= currentTree) break;
            if (tempCol == 0) return true;
        }

        return false;
    }

    static int Part2(int[,] trees)
    {
        int size = trees.GetLength(0);
        int highestScenicScore = 0;

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                int currentScenicScore = GetScenicScore(trees, row, col);
                if (currentScenicScore > highestScenicScore) highestScenicScore = currentScenicScore;
            }
        }

        return highestScenicScore;
    }

    public static int GetScenicScore(int[,] trees, int row, int col)
    {
        int size = trees.GetLength(0);
        int currentTree = trees[row, col];

        int[] directionalScores = new int[4];

        for (int tempRow = row - 1; tempRow >= 0; tempRow--)
        {
            directionalScores[0]++;

            if (trees[tempRow, col] >= currentTree) break;
        }

        for (int tempCol = col + 1; tempCol <= size - 1; tempCol++)
        {
            directionalScores[1]++;

            if (trees[row, tempCol] >= currentTree) break;
        }

        for (int tempRow = row + 1; tempRow <= size - 1; tempRow++)
        {
            directionalScores[2]++;

            if (trees[tempRow, col] >= currentTree) break;
        }

        for (int tempCol = col - 1; tempCol >= 0; tempCol--)
        {
            directionalScores[3]++;

            if (trees[row, tempCol] >= currentTree) break;
        }

        int scenicScore = directionalScores[0];

        for (int i = 1; i < directionalScores.Length; i++)
        {
            scenicScore *= directionalScores[i];
        }

        return scenicScore;
    }
}