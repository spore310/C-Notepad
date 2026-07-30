using App.Models.Pokemon.Encounter;
using App.Models.Pokemon.Stats;
using Microsoft.AspNetCore.Routing.Template;

namespace App.Services.GameEngine;

static class EncounterService
{
    public static int Encounter(
        EncounterTable group,
        IEnumerable<IEncounterModifier>? modifiers = null
    )
    {
        var result = group;

        int CumulativeWeight = 0;

        if (modifiers is not null)
        {
            foreach (IEncounterModifier modifier in modifiers)
            {
                result = modifier.Apply(result);
            }
        }

        int Roll = Random.Shared.Next(result.TotalWeight);

        foreach (var encounter in result.Encounters)
        {
            CumulativeWeight += encounter.Weight;
            if (Roll < CumulativeWeight)
            {
                return encounter.PokedexId;
            }
        }
        throw new InvalidOperationException("Encounter failed");
    }

    public static (string, int) GenPokemonStat(int level, PokemonMetaStat stat)
    {
        string Name = stat.Stat.Name;
        int FinalValue = (int)Math.Round(new decimal(stat.BaseStat / 10)) * level;
        if (FinalValue >= 301)
        {
            FinalValue = 300;
        }
        else if (FinalValue < 0)
        {
            FinalValue = 0;
        }
        return new(Name, FinalValue);
    }
}

interface IEncounterModifier
{
    EncounterTable Apply(EncounterTable group);
}
