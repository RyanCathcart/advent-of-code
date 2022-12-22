namespace Day10;

// Prompt link: https://adventofcode.com/2022/day/10
// Part 1 Solution: 14860
// Part 2 Solution: RGZEHURK
public class Program
{
    public static int CycleNum { get; set; } = 1;
    public static int X { get; set; }
    public static List<int> SignalStrengths { get; set; } = new List<int>();
    public static List<string> CRTRows { get; set; } = new List<string>();

    static void Main()
    {
        string inputPath = Directory.GetCurrentDirectory() + @"\input.txt";

        Console.WriteLine("Part 1 Solution: " + Part1(inputPath));
        Console.WriteLine("Part 2 Solution:\n" + Part2(inputPath));
    }

    static int Part1(string inputPath)
    {
        X = 1;

        using (var reader = new StreamReader(inputPath))
        {
            string? line;

            while ((line = reader.ReadLine()) != null)
            {
                var instruction = line.Split(' ');
                var command = instruction[0];

                if (command == "noop")
                {
                    Part1Cycle(); // End of 'noop' instruction
                    continue;
                }
                
                var v = int.Parse(instruction[1]);

                Part1Cycle(); // End of first cycle of 'addx' instruction
                Part1Cycle(); // End of second cycle of 'addx' instruction

                X += v;
            }
        }

        int sumOfSignalStrengths = 0;

        foreach (var signalStrength in SignalStrengths) sumOfSignalStrengths += signalStrength;

        return sumOfSignalStrengths;
    }

    static void Part1Cycle()
    {
        if ((CycleNum + 20) % 40 == 0) // Executes on the 20th, 60th, 100th, 140th, 180th, etc. cycles
        {
            int signalStrength = CycleNum * X;

            SignalStrengths.Add(signalStrength);
        }

        CycleNum++;
    }

    static string Part2(string inputPath)
    {
        X = 1;
        string row = "";

        using (var reader = new StreamReader(inputPath))
        {
            string? line;

            while ((line = reader.ReadLine()) != null)
            {
                var instruction = line.Split(' ');
                var command = instruction[0];

                if (command == "noop")
                {
                    row = Part2Cycle(row);// End of 'noop' instruction
                    continue;
                }

                var v = int.Parse(instruction[1]);

                row = Part2Cycle(row); // End of first cycle of 'addx' instruction
                row = Part2Cycle(row); // End of second cycle of 'addx' instruction

                X += v;
            }
        }

        string result = "";

        foreach (var crtRow in CRTRows) result += crtRow;

        return result;
    }

    static string Part2Cycle(string row)
    {
        // Checks if any part the sprite (3px wide) is currently where the next pixel is being drawn
        if (Math.Abs(row.Length - X) <= 1) row += "#";
        else row += ".";

        CycleNum++;

        if (CycleNum % 40 == 1) // Adds row to CRTRows List, and resets the row to empty
        {
            row += "\n";
            CRTRows.Add(row);
            return "";
        }

        return row;
    }
}