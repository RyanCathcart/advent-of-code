namespace Day6;

// Prompt link: https://adventofcode.com/2022/day/6
// Part 1 Solution: 1300
// Part 2 Solution: 
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
        using (StreamReader reader = new StreamReader(inputPath))
        {
            char? c;

            int count = 0;

            Queue<char?> pastFourCharacters = new Queue<char?>();

            while (!reader.EndOfStream)
            {
                c = (char)reader.Read();

                pastFourCharacters.Enqueue(c);
                count++;

                if (pastFourCharacters.Count == 4)
                {
                    HashSet<char?> temp = new HashSet<char?>();
                    bool anyDupes = false;
                    
                    foreach (char? letter in pastFourCharacters)
                    {
                        if (!temp.Add(letter))
                        {
                            anyDupes = true;
                            break;
                        }
                    }

                    if (!anyDupes) return count;

                    pastFourCharacters.Dequeue();
                }
            }

            return count;
        }
    }

    static int Part2(string inputPath)
    {
        using (StreamReader reader = new StreamReader(inputPath))
        {
            char? c;

            int count = 0;

            Queue<char?> pastFourCharacters = new Queue<char?>();

            while (!reader.EndOfStream)
            {
                c = (char)reader.Read();

                pastFourCharacters.Enqueue(c);
                count++;

                if (pastFourCharacters.Count == 14)
                {
                    HashSet<char?> temp = new HashSet<char?>();
                    bool anyDupes = false;

                    foreach (char? letter in pastFourCharacters)
                    {
                        if (!temp.Add(letter))
                        {
                            anyDupes = true;
                            break;
                        }
                    }

                    if (!anyDupes) return count;

                    pastFourCharacters.Dequeue();
                }
            }

            return count;
        }
    }
}