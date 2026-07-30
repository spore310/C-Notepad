using System.Text.Json.Serialization;

namespace App.Models.Pokemon.Stats;

public sealed class BaseStatEntry
{
    public required string Name { get; set; }
}

public sealed class PokemonStat
{
    [JsonPropertyName("base_stat")]
    public int BaseStat { get; set; }
    public required BaseStatEntry Stat { get; set; }
}
