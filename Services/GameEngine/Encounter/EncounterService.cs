using App.Models.Pokemon.Encounter;

static class EncounterService
{
    public static int Encounter(
        EncounterGroup group,
        IEnumerable<IEncounterModifier>? modifiers = null
    )
    {
        EncounterGroup result = group;

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
}

interface IEncounterModifier
{
    EncounterGroup Apply(EncounterGroup group);
}
