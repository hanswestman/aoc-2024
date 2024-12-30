using System.Text.RegularExpressions;

public class Machine
{
    public Machine(decimal aX, decimal aY, decimal bX, decimal bY, decimal prizeX, decimal prizeY)
    {
        AX = aX;
        AY = aY;
        BX = bX;
        BY = bY;
        PrizeX = prizeX;
        PrizeY = prizeY;
    }

    public decimal AX;
    public decimal AY;
    public decimal BX;
    public decimal BY;
    public decimal PrizeX;
    public decimal PrizeY;

    public void Debug()
    {
        Console.WriteLine($"ax:{AX} ay:{AY} bx:{BX} by:{BY} prizeX:{PrizeX} prizeY:{PrizeY}");
    }

    public (decimal a, decimal b) FindButtonIntersect()
    {
        /*
        Button A: X+94, Y+34
        Button B: X+22, Y+67
        Prize: X=8400, Y=5400

        B = (5400 - 34A) / 67

        94A + 22B = 8400

        94A + 22(5400/67 - (34/67) * A) = 8400

        94A + 22*5400/67 - (22*34/67)*A = 8400

        94A - (22*34/67)*A = 8400 - 22*5400/67

        (94 - 22*34/67)A = 8400 - 22*5400/67

        A = (8400 - 22*5400/67) / (94 - 22*34/67)

        A = 80

        B = (5400 - 34A) / 67

        B = (5400 - 34 * 80) / 67

        B = 40
        */

        decimal a = RoundIfPrecisionError((PrizeX - BX * PrizeY / BY) / (AX - BX * AY / BY));
        decimal b = RoundIfPrecisionError((PrizeY - AY * a) / BY);

        return (a, b);
    }

    private decimal RoundIfPrecisionError(decimal value)
    {
        decimal roundedValue = Math.Round(value);
        decimal epsilon = 0.000001M;

        if (value >= roundedValue - epsilon && value <= roundedValue + epsilon)
        {
            return roundedValue;
        }

        return value;
    }
}

public class Part1
{
    public List<Machine> Machines = new List<Machine>();

    public Part1()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
        List<string> lines = File.ReadAllLines(path).ToList();

        string buttonPattern = @"X\+(\d+), Y\+(\d+)";
        string prizePattern = @"X=(\d+), Y=(\d+)";

        for (int i = 0; i < lines.Count(); i += 4)
        {
            Match aButtonMatch = Regex.Match(lines[i], buttonPattern);
            Match bButtonMatch = Regex.Match(lines[i + 1], buttonPattern);
            Match prizeMatch = Regex.Match(lines[i + 2], prizePattern);

            Machines.Add(
                new Machine(
                    decimal.Parse(aButtonMatch.Groups[1].Value),
                    decimal.Parse(aButtonMatch.Groups[2].Value),
                    decimal.Parse(bButtonMatch.Groups[1].Value),
                    decimal.Parse(bButtonMatch.Groups[2].Value),
                    decimal.Parse(prizeMatch.Groups[1].Value),
                    decimal.Parse(prizeMatch.Groups[2].Value)
                )
            );
        }
    }

    public long GetSolution()
    {
        int tokens = 0;

        foreach (Machine machine in Machines)
        {
            // machine.Debug();

            (decimal a, decimal b) = machine.FindButtonIntersect();

            if (decimal.IsInteger(a) && decimal.IsInteger(b))
            {
                // Console.WriteLine($"A: {a}, B: {b}");

                tokens += (int)a * 3 + (int)b;
            }
        }

        return tokens;
    }
}