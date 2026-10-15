# matchmaking-lab

Build player matches using MMR range, region, and ping limits. See the selected pairs and update their ratings with an Elo result.

## Run

Requires the .NET 10 SDK. Browser builds also need the WebAssembly workload: `dotnet workload install wasm-tools`.

### Desktop

~~~sh
dotnet run --project MatchmakingLab.Desktop/MatchmakingLab.Desktop.csproj
~~~

### Browser

~~~sh
dotnet run --project MatchmakingLab.Browser/MatchmakingLab.Browser.csproj
~~~

## Example

Run matchmaking with the default MMR range, then record the result of a pair.
