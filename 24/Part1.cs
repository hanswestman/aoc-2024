public enum Operator
{
    AND,
    OR,
    XOR,
}

public class Gate
{
    public string Left;
    public string Right;
    public Operator Operator;

    public int? Value = null;

    public Gate(string left, string right, string op)
    {
        Left = left;
        Right = right;
        switch (op)
        {
            case "AND":
                Operator = Operator.AND;
                break;
            case "OR":
                Operator = Operator.OR;
                break;
            default:
                Operator = Operator.XOR;
                break;
        }
    }
}

public class Part1
{
    Dictionary<string, int> Inputs = new Dictionary<string, int>();
    Dictionary<string, Gate> Gates = new Dictionary<string, Gate>();

    public Part1()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
        bool readingInputs = true;

        foreach (string line in File.ReadAllLines(path))
        {
            if (line.Length == 0)
            {
                readingInputs = false;
                continue;
            }

            if (readingInputs)
            {
                string[] parts = line.Split(": ");
                Inputs.Add(parts[0], int.Parse(parts[1]));
            }
            else
            {
                string[] parts = line.Split(" ");
                Gates.Add(parts[4], new Gate(parts[0], parts[2], parts[1]));
            }

        }
    }

    public long GetSolution()
    {
        List<int> results = new List<int>();

        foreach (string key in GetOutputKeys())
        {
            results.Add(GetOutputOfKey(key));
        }

        results.Reverse();

        Console.WriteLine(string.Join(" ", results));

        return Convert.ToInt64(string.Join("", results), 2);
    }

    private List<string> GetOutputKeys()
    {
        return Gates.Keys.Where(key => key.StartsWith("z")).Order().ToList();
    }

    private int GetOutputOfKey(string key)
    {
        //Console.WriteLine($"Getting {key}");
        if (Inputs.ContainsKey(key))
        {
            //Console.WriteLine($"{key} Got from inputs.");
            return Inputs[key];
        }
        else
        {
            Gate gate = Gates[key];

            if (gate.Value != null)
            {
                Console.WriteLine($"{key} Got from cached value.");
                return (int)gate.Value;
            }

            //Console.WriteLine($"{key} Getting left {gate.Left} and right {gate.Right}.");
            int left = GetOutputOfKey(gate.Left);
            int right = GetOutputOfKey(gate.Right);

            switch (gate.Operator)
            {
                case Operator.AND:
                    return left & right;
                case Operator.OR:
                    return left | right;
                default:
                    return left ^ right;
            }
        }
    }
}