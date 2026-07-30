using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using App.Models.Pokemon.Stats;
using App.Models.Pokemon.Types;

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

    public required PokemonMetaStat[] Stats { get; set; }
}

class PokemonWild : PokemonMeta
{
    public required int Level { get; init; }

    public PokemonWild((int MinLevel, int MaxLevel) levelOptions)
    {
        Level = Random.Shared.Next(levelOptions.MinLevel, levelOptions.MaxLevel + 1);
    }
}

class PokemonMetaList(List<PokemonMeta> pokemons)
{
    public required IReadOnlyList<PokemonMeta> Pokemon = pokemons;
}
