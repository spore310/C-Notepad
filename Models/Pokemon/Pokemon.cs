using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using App.Models.Pokemon.Stats;
using App.Models.Pokemon.Types;

namespace App.Models.Pokemon;

public enum PokemonConstants
{
    MAXNATIONALID = 1025,
}

public class Pokemon
{
    [Key]
    public required int Id { get; set; }
    public required string Name { get; set; } = string.Empty;

    [JsonPropertyName("order")]
    public required int SortOrder { get; set; }

    public required PokemonTypeSlot[] Types { get; set; }

    public string ImageUrl =>
        $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{Id}.png";

    public required PokemonStat[] Stats { get; set; }
}
