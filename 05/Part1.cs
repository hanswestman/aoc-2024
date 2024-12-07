enum Section
{
    Rules,
    Updates
}

public class Part1
{
    protected List<List<int>> updates;
    protected List<(int before, int after)> rules;

    public Part1()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "input.txt");
        Section section = Section.Rules;

        updates = new List<List<int>>();
        rules = new List<(int before, int after)>();

        foreach (string line in File.ReadAllLines(path))
        {
            if (line == "")
            {
                section = Section.Updates;
                continue;
            }

            if (section == Section.Rules)
            {
                int[] values = line.Split("|").Select(int.Parse).ToArray();

                rules.Add((values[0], values[1]));
            }
            else if (section == Section.Updates)
            {
                updates.Add(line.Split(",").Select(int.Parse).ToList());
            }
        }
    }

    public int GetSolution()
    {
        int sum = 0;

        foreach (var update in updates)
        {
            if (EvaluateUpdate(update))
            {
                sum += update[(update.Count() - 1) / 2];
            }
        }

        return sum;
    }

    protected bool EvaluateUpdate(List<int> update)
    {
        foreach (var rule in rules)
        {
            if (!EvaluateRuleForUpdate(rule, update))
            {
                return false;
            }
        }

        return true;
    }

    private bool EvaluateRuleForUpdate((int before, int after) rule, List<int> update)
    {
        if (!update.Contains(rule.before) || !update.Contains(rule.after))
        {
            return true;
        }

        int indexShouldBeBefore = update.IndexOf(rule.before);
        int indexShouldBeAfter = update.IndexOf(rule.after);

        return indexShouldBeBefore < indexShouldBeAfter;
    }
}