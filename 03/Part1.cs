using System.Text.RegularExpressions;

public class Part1
{
    public int GetSolution()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");

        string memory = File.ReadAllText(path);
        string pattern = @"mul\((\d+),(\d+)\)";
        Regex regex = new Regex(pattern);
        Match match = regex.Match(memory);

        int sum = 0;

        while (match.Success)
        {
            int first = int.Parse(match.Groups[1].ToString());
            int second = int.Parse(match.Groups[2].ToString());

            sum += first * second;

            match = match.NextMatch();
        }

        return sum;
    }


}