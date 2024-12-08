global using Equation = (long result, long[] numbers);

public class Part1
{
    protected List<Equation> equations;

    public Part1()
    {
        equations = new List<Equation>();

        var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");

        foreach (string line in File.ReadAllLines(path))
        {
            var parts = line.Split(": ");

            equations.Add((
                long.Parse(parts[0]),
                parts[1].Split(" ").Select(long.Parse).ToArray()
            ));
        }
    }

    public long GetSolution()
    {
        long sum = 0;

        foreach (var equation in equations)
        {
            if (HasSolution(equation))
            {
                sum += equation.result;
            }
        }

        return sum;
    }

    private bool HasSolution(Equation equation)
    {
        int numberOfOperations = equation.numbers.Count() - 1;

        for (int i = 0; i < Math.Pow(2, numberOfOperations); i++)
        {
            string operators = Convert.ToString(i, 2).PadLeft(numberOfOperations, '0');

            long sum = equation.numbers[0];

            for (int operatorIndex = 0; operatorIndex < numberOfOperations; operatorIndex++)
            {
                int numberIndex = operatorIndex + 1;

                if (operators[operatorIndex] == '0')
                {
                    sum += equation.numbers[numberIndex];
                }
                else
                {
                    sum *= equation.numbers[numberIndex];
                }
            }

            if (sum == equation.result)
            {
                return true;
            }
        }

        return false;
    }
}