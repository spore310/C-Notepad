using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using App.Models.Pokemon.Stats;
using App.Models.Pokemon.Types;

namespace App.Models.Pokemon;

public class Pokemon
{
    [Key]
    public required int Id { get; set; }
    public required string Name { get; set; } = string.Empty;

    [JsonPropertyName("order")]
    public required int SortOrder { get; set; }
    public required PokemonType[] Types { get; set; }

    public required string ImageUrl
    {
        get =>
            field
            ?? $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{Id}.png";
        init;
    } = $"/assets/images/missingno.png";
    public required PokemonStat[] Stats { get; set; }
}
