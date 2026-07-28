using System.ComponentModel.DataAnnotations;

namespace App.Models.Pokemon.Types;
public sealed class PokemonType
{
    [Required(ErrorMessage = "Type name must be present")]
    public required string Name { get; set; }
}

public sealed class PokemonTypeSlot
{
    public int Slot { get; set; }
 
    public required PokemonType Type { get; set; }
}

 