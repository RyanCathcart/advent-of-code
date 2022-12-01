namespace Day1;

// Prompt link: https://adventofcode.com/2022/day/1
// Part 1 Solution: 69206
// Part 2 Solution: 197400
public class Program
{
    static void Main(string[] _args)
    {
        string inputPath = Directory.GetCurrentDirectory() + @"\input.txt";
        List<int> calorieTotals = new List<int>();   

        using (StreamReader reader = new StreamReader(inputPath))
        {
            string? line;
            int currentCalorieCount = 0;

            while ((line = reader.ReadLine()) != null)
            {
                try
                {
                    int calorieValue = int.Parse(line);

                    currentCalorieCount += calorieValue;
                }
                catch (FormatException)
                {
                    calorieTotals.Add(currentCalorieCount);

                    currentCalorieCount = 0;
                }
            }
        }

        calorieTotals = calorieTotals.OrderDescending().ToList();

        Console.WriteLine("Part 1 Solution: " + calorieTotals[0]);
        Console.WriteLine("Part 2 Solution: " + (calorieTotals[0] + calorieTotals[1] + calorieTotals[2]));
    }
}