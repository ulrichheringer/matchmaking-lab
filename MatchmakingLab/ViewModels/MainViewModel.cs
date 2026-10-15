using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using MatchmakingLab.Matching;

namespace MatchmakingLab.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly List<Player> _players = [
        new("Ada", 1320, "SA", 34), new("Beto", 1290, "SA", 48), new("Cris", 1345, "SA", 61),
        new("Dani", 1510, "EU", 28), new("Eli", 1525, "EU", 39), new("Fran", 1490, "EU", 52)];
    private IReadOnlyList<Match> _matches = [];
    [ObservableProperty] private int _maxDifference = 80;
    [ObservableProperty] private int _maxPing = 100;
    [ObservableProperty] private string _winnerChoice = "A vence";
    [ObservableProperty] private string _results = "Procure partidas para visualizar os pares.";
    [ObservableProperty] private string _ratingResult = "";
    public string[] WinnerChoices { get; } = ["A vence", "B vence"];

    [RelayCommand]
    private void FindMatches()
    {
        _matches = Matchmaker.FindMatches(_players, Math.Clamp(MaxDifference, 0, 1000), Math.Clamp(MaxPing, 1, 500));
        Results = _matches.Count == 0 ? "Nenhum par atende aos critérios." : string.Join(Environment.NewLine,
            _matches.Select(m => $"{m.First.Name} ({m.First.Mmr}) × {m.Second.Name} ({m.Second.Mmr}) · {m.First.Region} · Δ{Math.Abs(m.First.Mmr - m.Second.Mmr)} MMR"));
        RatingResult = "";
    }

    [RelayCommand]
    private void ApplyFirstMatchResult()
    {
        if (_matches.Count == 0) { RatingResult = "Encontre partidas primeiro."; return; }
        var match = _matches[0];
        var score = WinnerChoice == "A vence" ? 1d : 0d;
        var updated = Matchmaker.UpdateElo(match.First.Mmr, match.Second.Mmr, score);
        RatingResult = $"Após o resultado: {match.First.Name} {updated.First} MMR · {match.Second.Name} {updated.Second} MMR.";
    }
}
