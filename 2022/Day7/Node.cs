namespace Day7;

public class Node
{
    public string Name { get; set; }
    public int Size { get; set; } = 0;
    public Node? Parent { get; set; } = null;
    public List<Node> Children { get; set; } = new List<Node>();
    public static List<Node> Nodes { get; set; } = new List<Node>();

    public Node(string name)
    {
        Name = name;
    }

    public void UpdateSize()
    {
        // If there are no children, do nothing
        if (!Children.Any()) return;

        // If there are children, add their sizes to the current node's size
        foreach (Node child in Children)
        {
            child.UpdateSize();
            Size += child.Size;
        }
    }

    public static int FindSumOfNodesUnderLimit(int limit, Node currentNode)
    {                                                                           
        int total = 0;

        // If there are any children, check them first
        if (currentNode.Children.Any())
        {
            foreach (Node child in currentNode.Children)
            {
                total += FindSumOfNodesUnderLimit(limit, child);
            }
        }
        
        // If node size is under limit, add it to the total
        if (currentNode.Size <= limit) total += currentNode.Size;        

        return total;
    }

    public static Node? FindSmallestNodeOverLimit(int limit, Node currentNode)
    {
        List<Node> nodes = ToList(currentNode);

        Node? smallest = null;

        foreach (Node node in nodes)
        {
            if (smallest == null && node.Size >= limit) smallest = node;
            if (smallest != null && node.Size >= limit && node.Size < smallest.Size) smallest = node;
        }

        return smallest;
    }

    public static List<Node> ToList(Node currentNode)
    {
        if (currentNode == null) return new List<Node>();

        // If there are any children, check them first
        if (currentNode.Children.Any())
        {
            foreach (Node child in currentNode.Children)
            {
                ToList(child);
            }
        }

        Nodes.Add(currentNode);

        return Nodes;
    }
}
