using MatchmakingLab.Matching;

var players = new[] { new Player("A", 1000, "SA", 30), new Player("B", 1020, "SA", 40), new Player("C", 1010, "EU", 20) };
var matches = Matchmaker.FindMatches(players, 50, 100);
if (matches.Count != 1 || matches[0].First.Region != matches[0].Second.Region) throw new Exception("Region or MMR pairing failed.");
var ratings = Matchmaker.UpdateElo(1000, 1000, 1);
if (ratings.First <= 1000 || ratings.Second >= 1000) throw new Exception("Elo update failed.");
Console.WriteLine("Matchmaking checks passed.");
