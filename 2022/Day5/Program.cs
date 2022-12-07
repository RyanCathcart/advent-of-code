namespace Day5;

// Prompt link: https://adventofcode.com/2022/day/5
// Part 1 Solution: DHBJQJCCW
// Part 2 Solution: WJVRLSJJT
public class Program
{
    static void Main(string[] args)
    {
        string inputPath = Directory.GetCurrentDirectory() + @"\input.txt";

        Console.WriteLine("Part 1 Solution: " + Part1(inputPath));
        Console.WriteLine("Part 2 Solution: " + Part2(inputPath));
    }

    static string Part1(string inputPath)
    {
        Stack<char>[] stacks = new Stack<char>[9];

        string[] givenStacks = new string[9];

        using (StreamReader reader = new StreamReader(inputPath))
        {
            string? line;
            int ctr = 0;

            // fills array of strings with lines 1-9
            while (((line = reader.ReadLine()) != null) && ctr < 9) givenStacks[ctr++] = line;

            // populate indexes array to determine where each important char is located
            // should become {1, 5, 9, 13, 17, 21, 25, 29, 33}
            List<int> indices = new List<int>();

            for (int i = 0; i < givenStacks[8].Length; i++)
            {
                if (givenStacks[8][i] != ' ') indices.Add(i);
            }

            for (int i = 7; i >= 0; i--)
            {
                foreach (int index in indices)
                {
                    char selectedChar = givenStacks[i][index];

                    if (selectedChar != ' ')
                    {
                        int stackNumber = (int)(0.25 * (index - 1));

                        if (stacks[stackNumber] == null) stacks[stackNumber] = new Stack<char>();

                        stacks[stackNumber].Push(selectedChar);
                    }
                }
            }

            // move 'numberOfBoxesToMove' from 'stack1' to 'stack2'
            // 1. Split with space. Need a array var from the split of the string ✅
            // 2. Store numbers in var until line done. Need 3 vars for numberOfBoxesToMove, stack1 and stack2 ✅
            // 3. Remember to cast the numbers ✅
            // 4. For loop / while until looped through all required 'numberOfBoxesToMove'
            //      4.1 Pop from 'stack1' var
            //      4.2 Push to 'stack2'

            while ((line = reader.ReadLine()) != null)
            {
                string[] splitLine = line.Split(" ");

                int numberOfBoxesToMove = int.Parse(splitLine[1]);
                int stack1 = int.Parse(splitLine[3]) - 1; //to use stacks[stack1]
                int stack2 = int.Parse(splitLine[5]) - 1; // to use stacks[stack2]

                while (numberOfBoxesToMove > 0)
                {
                    char box = stacks[stack1].Pop();
                    stacks[stack2].Push(box);
                    numberOfBoxesToMove--;
                }
            }

            string result = "";

            foreach (Stack<char> s in stacks)
            {
                result += s.Peek();
            }

            return result;
        }
    }

    static string Part2(string inputPath)
    {
        Stack<char>[] stacks = new Stack<char>[9];

        string[] givenStacks = new string[9];

        using (StreamReader reader = new StreamReader(inputPath))
        {
            string? line;
            int ctr = 0;

            // fills array of strings with 1-9 lines
            while (((line = reader.ReadLine()) != null) && ctr < 9) givenStacks[ctr++] = line;

            // populate indexes array to determine where each important char is located
            // should become {1, 5, 9, 13, 17, 21, 25, 29, 33}
            List<int> indices = new List<int>();

            for (int i = 0; i < givenStacks[8].Length; i++)
            {
                if (givenStacks[8][i] != ' ') indices.Add(i);
            }


            for (int i = 7; i >= 0; i--)
            {
                foreach (int index in indices)
                {
                    char selectedChar = givenStacks[i][index];
                    if (selectedChar != ' ')
                    {
                        int stackNumber = (int)(0.25 * (index - 1));
                        if (stacks[stackNumber] == null)
                        {
                            stacks[stackNumber] = new Stack<char>();
                        }

                        stacks[stackNumber].Push(selectedChar);
                    }
                }
            }

            while ((line = reader.ReadLine()) != null)
            {
                string[] splitLine = line.Split(" ");

                int numberOfBoxesToMove = int.Parse(splitLine[1]);
                int stack1 = int.Parse(splitLine[3]) - 1; //to use stacks[stack1]
                int stack2 = int.Parse(splitLine[5]) - 1; // to use stacks[stack2]

                List<char> boxesBeingMoved = new List<char>();

                while (numberOfBoxesToMove > 0)
                {
                    char box = stacks[stack1].Pop();
                    boxesBeingMoved.Add(box);
                    numberOfBoxesToMove--;
                }

                boxesBeingMoved.Reverse();

                foreach (char box in boxesBeingMoved)
                {
                    stacks[stack2].Push(box);
                }
            }

            string result = "";

            foreach (Stack<char> s in stacks)
            {
                result += s.Peek();
            }

            return result;
        }
    }
}
