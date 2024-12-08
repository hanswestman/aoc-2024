public class Part2 : Part1
{
    new public long GetSolution()
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
        char[] baseChars = { '0', '1', '2' };

        for (int i = 0; i < Math.Pow(3, numberOfOperations); i++)
        {
            string operators = IntToBaseString(i, baseChars).PadLeft(numberOfOperations, '0');

            long sum = equation.numbers[0];

            for (int operatorIndex = 0; operatorIndex < numberOfOperations; operatorIndex++)
            {
                int numberIndex = operatorIndex + 1;

                if (operators[operatorIndex] == '0')
                {
                    sum += equation.numbers[numberIndex];
                }
                else if (operators[operatorIndex] == '1')
                {
                    sum *= equation.numbers[numberIndex];
                }
                else
                {
                    sum = long.Parse($"{sum}{equation.numbers[numberIndex]}");
                }
            }

            if (sum == equation.result)
            {
                return true;
            }
        }

        return false;
    }

    private string IntToBaseString(int value, char[] baseChars)
    {
        string result = "";
        int targetBase = baseChars.Count();

        do
        {
            result = baseChars[value % targetBase] + result;
            value /= targetBase;
        } while (value > 0);

        return result;
    }
}