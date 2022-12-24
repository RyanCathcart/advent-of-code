using System.Numerics;

namespace Day11;

// Prompt link: https://adventofcode.com/2022/day/11
// Part 1 Solution: 58322
// Part 2 Solution: 13937702909 
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
        const int NUM_ROUNDS = 20;
        string[] lines = File.ReadAllLines(inputPath);
        var monkeys = InitializeMonkeys(lines);

        for (int round = 0; round < NUM_ROUNDS; round++)
        {
            foreach (var monkey in monkeys)
            {
                foreach (var item in monkey.Items.ToList())
                {
                    // Monkey inspects the item
                    // The item's worry level is changed, based on the monkey's operation
                    var worryLevel = item;

                    if (monkey.Operation[0] == "+")
                    {
                        if (monkey.Operation[1] == "old") worryLevel += worryLevel;
                        else worryLevel += int.Parse(monkey.Operation[1]);
                    }
                    else // if operation is *
                    {
                        if (monkey.Operation[1] == "old") worryLevel *= worryLevel;
                        else worryLevel *= int.Parse(monkey.Operation[1]);
                    }

                    monkey.ItemInspectionCount++;

                    // Monkey gets bored with the item, worry level is divided by 3
                    worryLevel /= 3;

                    // Monkey throws the item to another monkey, based on the test case
                    if (worryLevel % monkey.Test[0] == 0)
                    {
                        monkeys.Find(m => m.Number == monkey.Test[1])!.Items.Add(worryLevel);  
                    }
                    else
                    {
                        monkeys.Find(m => m.Number == monkey.Test[2])!.Items.Add(worryLevel);
                    }

                    monkey.Items.Remove(item);
                }
            }
        }

        monkeys = monkeys.OrderByDescending(m => m.ItemInspectionCount).ToList();

        int mostActiveMonkey = monkeys[0].ItemInspectionCount;
        int secondMostActiveMonkey = monkeys[1].ItemInspectionCount;

        int monkeyBusiness = mostActiveMonkey * secondMostActiveMonkey;

        return monkeyBusiness;
    }

    static long Part2(string inputPath)
    {
        const int NUM_ROUNDS = 10_000;
        string[] lines = File.ReadAllLines(inputPath);
        var monkeys = InitializeMonkeys(lines);

        var product = 1;

        foreach (var monkey in monkeys)
        {
            product *= monkey.Test[0];
        }

        for (int round = 0; round < NUM_ROUNDS; round++)
        {
            foreach (var monkey in monkeys)
            {
                foreach (var item in monkey.Items.ToList())
                {
                    // Monkey inspects the item
                    // The item's worry level is changed, based on the monkey's operation
                    var worryLevel = item;                    

                    if (monkey.Operation[0] == "+")
                    {
                        if (monkey.Operation[1] == "old") worryLevel += worryLevel;
                        else worryLevel += BigInteger.Parse(monkey.Operation[1]);
                    }
                    else
                    {
                        if (monkey.Operation[1] == "old") worryLevel *= worryLevel;
                        else worryLevel *= BigInteger.Parse(monkey.Operation[1]);
                    }

                    monkey.ItemInspectionCount++;

                    worryLevel %= product;

                    // Monkey throws the item to another monkey, based on the test case
                    if (worryLevel % monkey.Test[0] == 0)
                    {
                        monkeys.Find(m => m.Number == monkey.Test[1])!.Items.Add(worryLevel);
                    }
                    else
                    {
                        monkeys.Find(m => m.Number == monkey.Test[2])!.Items.Add(worryLevel);
                    }

                    monkey.Items.Remove(item);
                }
            }
        }

        monkeys = monkeys.OrderByDescending(m => m.ItemInspectionCount).ToList();

        int mostActiveMonkey = monkeys[0].ItemInspectionCount;
        int secondMostActiveMonkey = monkeys[1].ItemInspectionCount;

        long monkeyBusiness = (long)mostActiveMonkey * secondMostActiveMonkey;

        return monkeyBusiness;
    }

    static List<Monkey> InitializeMonkeys(string[] lines)
    {
        var monkeys = new List<Monkey>();

        for (int i = 0; i < lines.Length; i += 7)
        {
            var number = int.Parse(lines[i][7..].Replace(":", ""));

            var items = lines[i + 1][18..]
                            .Replace(",", "")
                            .Split(" ")
                            .ToList()
                            .ConvertAll(BigInteger.Parse);

            var operation = lines[i + 2][23..].Split(" ");

            var test = new int[]
            {
                int.Parse(lines[i + 3][21..]),
                int.Parse(lines[i + 4][29..]),
                int.Parse(lines[i + 5][30..])
            };

            monkeys.Add(new Monkey
            {
                Number = number,
                Items = items,
                Operation = operation,
                Test = test
            });
        }

        return monkeys;
    }
}