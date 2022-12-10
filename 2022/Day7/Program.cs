namespace Day7;

// Prompt link: https://adventofcode.com/2022/day/7
// Part 1 Solution: 1743217
// Part 2 Solution: 8319096
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
        var currentNode = new Node(name: "/");

        using (var reader = new StreamReader(inputPath))
        {
            string? line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] splitLine = line.Split(" ");

                if (splitLine[0] == "$")
                {
                    if (splitLine[1] == "ls" || splitLine[2] == "/") continue;

                    // if split[2] is '..' go back a node - if it is not, then go into the node called
                    if (splitLine[2] == "..") currentNode = currentNode.Parent!;
                    else currentNode = currentNode.Children.Where(c => c.Name == splitLine[2]).First();
                }
                else if (splitLine[0] == "dir")
                {
                    // AT DIR - Len - 2
                    // Create a new node: new Node(name)
                    // Add new node to children of current Node
                    var nodeToAdd = new Node(name: splitLine[1])
                    {
                        Parent = currentNode,
                        Children = new List<Node>()
                    };
                    currentNode.Children.Add(nodeToAdd);
                }
                else
                {
                    // AT FILE - Len - 2
                    // Increase total file size of current Node
                    int size = int.Parse(splitLine[0]);
                    currentNode.Size += size;
                }
            }
        }

        currentNode.Parent!.UpdateSize();

        return Node.FindSumOfNodesUnderLimit(100_000, currentNode.Parent);
    }

    static int Part2(string inputPath)
    {
        const int totalDiskSpace = 70_000_000;
        const int requiredUpdateSpace = 30_000_000;

        var currentNode = new Node(name: "/");

        using (var reader = new StreamReader(inputPath))
        {
            string? line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] splitLine = line.Split(" ");

                if (splitLine[0] == "$")
                {
                    if (splitLine[1] == "ls" || splitLine[2] == "/") continue;

                    if (splitLine[2] == "..") currentNode = currentNode.Parent!;
                    else currentNode = currentNode.Children.Where(c => c.Name == splitLine[2]).First();
                }
                else if (splitLine[0] == "dir")
                {
                    var nodeToAdd = new Node(name: splitLine[1])
                    {
                        Parent = currentNode,
                        Children = new List<Node>()
                    };

                    currentNode.Children.Add(nodeToAdd);
                }
                else
                {
                    int size = int.Parse(splitLine[0]);
                    currentNode.Size += size;
                }
            }
        }

        currentNode = currentNode.Parent!;
        currentNode.UpdateSize();

        int unusedSpace = totalDiskSpace - currentNode.Size;
        int requiredFileSizeToDelete = requiredUpdateSpace - unusedSpace;

        var result = Node.FindSmallestNodeOverLimit(requiredFileSizeToDelete, currentNode)!.Size;
        Console.WriteLine(currentNode.Size);

        return result;
    }
}
