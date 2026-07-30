using System.ComponentModel.DataAnnotations;

namespace App.Models.Pokemon.Types;

public sealed class PokemonType
{
    [Required(ErrorMessage = "Type name must be present")]
    public required string Name { get; set; }
}

public sealed class PokemonTypeSlot
{
    public required PokemonType Info { get; set; }
}
