namespace MatchmakingLab.Matching;

public sealed record Player(string Name, int Mmr, string Region, int PingMs, string Queue = "ranked");
public sealed record Match(Player First, Player Second);

public static class Matchmaker
{
    public static IReadOnlyList<Match> FindMatches(IEnumerable<Player> players, int maxMmrDifference, int maxPingMs)
    {
        var waiting = players.OrderBy(p => p.Mmr).ThenBy(p => p.Name, StringComparer.Ordinal).ToList();
        var matches = new List<Match>();
        while (waiting.Count > 1)
        {
            var first = waiting[0];
            var candidateIndex = Enumerable.Range(1, waiting.Count - 1)
                .Where(i => waiting[i].Queue == first.Queue && waiting[i].Region == first.Region
                    && Math.Abs(waiting[i].Mmr - first.Mmr) <= maxMmrDifference
                    && first.PingMs <= maxPingMs && waiting[i].PingMs <= maxPingMs)
                .OrderBy(i => Math.Abs(waiting[i].Mmr - first.Mmr)).FirstOrDefault(-1);
            waiting.RemoveAt(0);
            if (candidateIndex < 0) continue;
            var second = waiting[candidateIndex - 1];
            waiting.RemoveAt(candidateIndex - 1);
            matches.Add(new(first, second));
        }
        return matches;
    }

    public static (int First, int Second) UpdateElo(int first, int second, double firstScore, int k = 32)
    {
        var expected = 1d / (1d + Math.Pow(10d, (second - first) / 400d));
        var newFirst = (int)Math.Round(first + k * (firstScore - expected));
        var newSecond = (int)Math.Round(second + k * ((1 - firstScore) - (1 - expected)));
        return (newFirst, newSecond);
    }
}
