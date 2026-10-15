# matchmaking-lab

Forma partidas por MMR, região e ping e aplica atualização Elo.

## Desktop

```sh
dotnet run --project MatchmakingLab.Desktop/MatchmakingLab.Desktop.csproj
```

## Browser

Requer o workload wasm-tools do .NET 10.

```sh
dotnet run --project MatchmakingLab.Browser/MatchmakingLab.Browser.csproj
```

## Exemplo

Busque partidas com a faixa MMR padrão e registre o resultado do primeiro par.
