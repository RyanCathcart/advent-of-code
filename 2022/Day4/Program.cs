namespace Day4;

// Prompt link: https://adventofcode.com/2022/day/4
// Part 1 Solution: 560
// Part 2 Solution: 839
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
        int total = 0;

        using (StreamReader reader = new StreamReader(inputPath))
        {
            string? line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] sections = line.Split(',');
                List<int> firstSections = sections[0].Split("-").ToList().ConvertAll(int.Parse);
                List<int> secondSections = sections[1].Split("-").ToList().ConvertAll(int.Parse);
                int firstNum = firstSections[0], secondNum = firstSections[1];
                int thirdNum = secondSections[0], fourthNum = secondSections[1];
                
                if ((firstNum <= thirdNum && secondNum >= fourthNum) ||
                    (thirdNum <= firstNum && fourthNum >= secondNum))
                {
                    total++;
                }
            }
        }

        return total;
    }

    static int Part2(string inputPath)
    {
        int total = 0;

        using (StreamReader reader = new StreamReader(inputPath))
        {
            string? line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] sections = line.Split(',');
                List<int> firstSections = sections[0].Split("-").ToList().ConvertAll(int.Parse);
                List<int> secondSections = sections[1].Split("-").ToList().ConvertAll(int.Parse);
                int firstNum = firstSections[0], secondNum = firstSections[1];
                int thirdNum = secondSections[0], fourthNum = secondSections[1];

                if ((firstNum >= thirdNum && firstNum <= fourthNum) ||
                    (secondNum >= thirdNum && secondNum <= fourthNum) || 
                    (thirdNum >= firstNum && thirdNum <= secondNum) ||
                    (fourthNum >= firstNum && fourthNum <= secondNum))
                {
                    total++;
                }
            }
        }

        return total;
    }
}
