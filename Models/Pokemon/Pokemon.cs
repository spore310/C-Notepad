using System.ComponentModel.DataAnnotations;
using App.Models.Pokemon.Stats;
using App.Models.Pokemon.Types;

namespace App.Models.Pokemon;

public sealed class Pokemon
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Order { get; set; }
    public required PokemonType[] Types { get; set; }

    public string ImageUrl
    {
        get => $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{Id}.png";
    }
    public required PokemonStat[] Stats { get; set; }
}

public sealed class PokemonResponse
{
    public List<Pokemon> Pokemon { get; set; } = [];
}
