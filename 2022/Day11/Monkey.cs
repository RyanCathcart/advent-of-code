using System.Numerics;

namespace Day11;

public class Monkey
{
	public int Number { get; set; }
	public List<BigInteger> Items { get; set; } = new List<BigInteger>();
	public string[] Operation { get; set; } = new string[2];
	public int[] Test { get; set; } = new int[3];
	public int ItemInspectionCount { get; set; } = 0;

	public override string ToString()
	{
		string result = $"Monkey {Number}:" +
						$"\n  Items: ";

		foreach (var item in Items)
		{
			result += (item + ", ");
		}

		result += $"\n  Operation: new = old {Operation[0]} {Operation[1]}" +
				  $"\n  Test: divisible by {Test[0]}" +
			      $"\n    If true: throw to monkey {Test[1]}" +
			      $"\n    If false: throw to monkey {Test[2]}" +
				  $"\n  Item Inspection Count: {ItemInspectionCount}";

        return result;
    }
}
