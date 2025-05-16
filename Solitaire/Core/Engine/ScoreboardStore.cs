using System.Text.Json;
using Solitaire.Core.Models;

namespace Solitaire.Core.Engine;

public static class ScoreboardStore
{
    private const string ScoreboardFileName = "scoreboard.json";
    public static List<GameResult> Scoreboard = Load();
    
    public static void AppendNewResult(GameResult result)
    {
        Scoreboard.Add(result);
        
        string json = JsonSerializer.Serialize(Scoreboard, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(ScoreboardFileName, json);
    }

    private static List<GameResult> Load()
    {
        if (!File.Exists(ScoreboardFileName)) return [];
        
        string json = File.ReadAllText(ScoreboardFileName);
        List<GameResult>? results = JsonSerializer.Deserialize<List<GameResult>>(json);

        return results ?? [];
    }
}