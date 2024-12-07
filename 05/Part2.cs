public class Part2 : Part1
{
    new public int GetSolution()
    {
        int sum = 0;

        foreach (var update in updates)
        {
            if (!EvaluateUpdate(update))
            {
                List<int> fixedUpdate = FixUpdate(update);

                sum += fixedUpdate[(fixedUpdate.Count() - 1) / 2];
            }
        }

        return sum;
    }

    private List<int> FixUpdate(List<int> update)
    {
        List<(int before, int after)> eligibleRules = GetEligibleRules(update);
        List<int> fixedUpdate = new List<int>();

        while (eligibleRules.Count() > 1)
        {
            int page = GetRulePageWithNoAfter(eligibleRules, update.Except(fixedUpdate).ToList());

            fixedUpdate.Add(page);

            List<(int before, int after)> eligibleRulesCopy = eligibleRules.ToList();
            eligibleRulesCopy.RemoveAll(rule => rule.before == page || rule.after == page);
            eligibleRules = eligibleRulesCopy;
        }

        fixedUpdate.Add(eligibleRules[0].before);
        fixedUpdate.Add(eligibleRules[0].after);

        return fixedUpdate;
    }

    private List<(int before, int after)> GetEligibleRules(List<int> update)
    {
        return rules.Where(rule =>
        {
            return update.Contains(rule.before) && update.Contains(rule.after);
        }).ToList();
    }

    private int GetRulePageWithNoAfter(List<(int before, int after)> rulesPartition, List<int> update)
    {
        foreach (int page in update)
        {
            if (!rulesPartition.Exists(rule => rule.after == page))
            {
                return page;
            }
        }

        return 0;
    }
}