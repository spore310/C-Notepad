using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using App.Models.Pokemon.Stats;
using App.Models.Pokemon.Types;
using App.Services.GameEngine;

namespace App.Models.Pokemon;

public enum PokemonConstants
{
    MAXNATIONALID = 1025,
}

public class PokemonMeta
{
    [Key]
    public required int Id { get; set; }
    public required string Name { get; set; } = string.Empty;

    [JsonPropertyName("order")]
    public required int SortOrder { get; set; }

    [JsonPropertyName("base_experience")]
    public required int BaseExp
    {
        get;
        init
        {
            if (!int.IsPositive(value))
                throw new ArgumentOutOfRangeException(
                    $"App.Models.Pokemon.PokemonMeta: base_experience {nameof(value)}:{value} must be a postive integer!"
                );
            field = value;
        }
    }

    public required PokemonTypeSlot[] Types { get; set; }

    public string ImageUrl =>
        $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{Id}.png";

    [JsonPropertyName("stats")]
    public required PokemonMetaStat[] BaseStats { get; init; }
}

class PokemonMetaList(List<PokemonMeta> pokemons)
{
    public required IReadOnlyList<PokemonMeta> Pokemon = pokemons;
}

class PokemonWild
{
    [Key]
    public required int Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public required int Level { get; set; }

    [JsonPropertyName("order")]
    public required int SortOrder { get; init; }
    public required string ImageUrl { get; init; }
    public required IReadOnlyList<PokemonType> Types { get; init; }
    protected Dictionary<string, PokemonStat> Stats { set; get; }

    public PokemonWild(int level, PokemonMeta pokemon)
    {
        Id = pokemon.Id;
        Name = pokemon.Name;
        SortOrder = pokemon.SortOrder;
        Level = level;
        ImageUrl = pokemon.ImageUrl;
        Types = [.. pokemon.Types.Select(type => type.Info)];
        Dictionary<string, PokemonStat> newStats = [];
        foreach (PokemonMetaStat stat in pokemon.BaseStats)
        {
            (string key, int value) = EncounterService.GenPokemonStat(level, stat);
            newStats.Add(key, new() { Value = value });
        }
        Stats = newStats;
    }
}
