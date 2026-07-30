using System.Text.Json.Serialization;

namespace App.Models.Pokemon.Stats;

public sealed class BaseStatEntry
{
    public required string Name { get; set; }
}

public class PokemonMetaStat
{
    [JsonPropertyName("base_stat")]
    public int BaseStat { get; set; }
    public required BaseStatEntry Stat { get; set; }
}

public class PokemonStat()
{
    public required int Value { get; set; }
}
