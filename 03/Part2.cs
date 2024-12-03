using System.Text.RegularExpressions;

public class Part2
{
    public int GetSolution()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");

        string memory = File.ReadAllText(path);
        string pattern = @"mul\((\d+),(\d+)\)|do\(\)|don't\(\)";
        Regex regex = new Regex(pattern);
        Match match = regex.Match(memory);

        int sum = 0;
        bool disabled = false;

        while (match.Success)
        {
            if (match.Groups[0].ToString() == "don't()")
            {
                disabled = true;
            }

            if (match.Groups[0].ToString() == "do()")
            {
                disabled = false;
            }

            if (!disabled && match.Groups[0].ToString().StartsWith("mul("))
            {
                int first = int.Parse(match.Groups[1].ToString());
                int second = int.Parse(match.Groups[2].ToString());

                sum += first * second;
            }

            match = match.NextMatch();
        }

        return sum;
    }


}