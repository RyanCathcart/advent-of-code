namespace Day3;

// Prompt link: https://adventofcode.com/2022/day/3
// Part 1 Solution: 7967
// Part 2 Solution: 2716
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
                int compartmentSize = line.Length / 2;
                string compartment1 = line[..compartmentSize];
                string compartment2 = line[compartmentSize..];

                foreach (char item in compartment1)
                {
                    if (compartment2.Contains(item))
                    {
                        total += GetItemPriority(item);
                        break;
                    }
                }
            }
        }

        return total;
    }

    static int Part2(string inputPath)
    {
        int total = 0;

        using (var reader = new StreamReader(inputPath))
        {
            string? line1;
            string? line2;
            string? line3;

            while (!reader.EndOfStream)
            {
                line1 = reader.ReadLine();
                line2 = reader.Peek() > 0 ? reader.ReadLine() : null;
                line3 = reader.Peek() > 0 ? reader.ReadLine() : null;

                foreach (char letter in line1!)
                {
                    if ((line3 != null || line2 != null) && line2!.Contains(letter) && line3!.Contains(letter))
                    {
                        total += GetItemPriority(letter);
                        break;
                    }
                }
            }
        }

        return total;
    }

    static int GetItemPriority(char item)
    {
        if (item > 96) return item - 96;

        return item - 38;
    }
}