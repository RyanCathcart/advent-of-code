namespace Day2;

// Prompt link: https://adventofcode.com/2022/day/2
// Part 1 Solution: 11449
// Part 2 Solution: 13187
public class Program
{
    static void Main(string[] args)
    {
        string inputPath = Directory.GetCurrentDirectory() + @"\input.txt";

        Console.WriteLine("Part 1 Solution: " + Part1(inputPath));
        Console.WriteLine("Part 2 Solution: " + Part2(inputPath));
    }

    static int Part1(string inputPath)
    {
        int totalScore = 0;

        using (StreamReader reader = new StreamReader(inputPath))
        {
            string? line;

            while ((line = reader.ReadLine()) != null)
            {
                //      Char ASCII values:
                //               OPP         YOU
                // Rock:        A (65)      X (88)
                // Paper:       B (66)      Y (89)
                // Scissors:    C (67)      Z (90)
                char opponentShape = line[0];
                char yourShape = line[2];

                int roundScore = yourShape - 87;
                int roundResult = yourShape - opponentShape;

                // Winning condition:
                if (roundResult == 21 || roundResult == 24) roundScore += 6;
                // Drawing condition: 
                else if (roundResult == 23) roundScore += 3;
                // Losing condition adds 0

                totalScore += roundScore;
            }
        }

        return totalScore;
    }

    static int Part2(string inputPath)
    {
        int totalScore = 0;

        using (StreamReader reader = new StreamReader(inputPath))
        {
            string? line;

            while ((line = reader.ReadLine()) != null)
            {
                //              Key:
                //             OPP  
                // Rock:        A       Lose:   X
                // Paper:       B       Draw:   Y
                // Scissors:    C       Win:    Z
                char opponentShape = line[0];
                char roundResult = line[2];

                int roundScore = (3 * roundResult) - 264;

                // Drawing condition:
                if (roundResult == 'Y') roundScore += opponentShape - 64;

                // Losing condition:
                if (roundResult == 'X')
                {
                    if (opponentShape == 'A') roundScore += 3;
                    if (opponentShape == 'B') roundScore += 1;
                    if (opponentShape == 'C') roundScore += 2;
                }

                // Winning condition:
                if (roundResult == 'Z')
                {
                    if (opponentShape == 'A') roundScore += 2;
                    if (opponentShape == 'B') roundScore += 3;
                    if (opponentShape == 'C') roundScore += 1;
                }

                totalScore += roundScore;
            }
        }

        return totalScore;
    }
}