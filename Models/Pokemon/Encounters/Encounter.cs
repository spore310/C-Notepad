namespace App.Models.Pokemon.Encounter;

readonly record struct Encounter(int PokedexId, int Weight);

sealed class EncounterGroup
{
    public IReadOnlyList<Encounter> Encounters { get; }

    public int TotalWeight { get; }

    public EncounterGroup(IEnumerable<Encounter> encounters)
    {
        Encounter[] orderedEncounters = [.. encounters.OrderBy(encounter => encounter.Weight)];

        Encounters = orderedEncounters;

        TotalWeight = orderedEncounters.Sum(encounter => encounter.Weight);
    }
}

sealed class Zone(string id, Dictionary<string, EncounterGroup> encounterGroups)
{
    public string Id { get; } = id;
    public IReadOnlyDictionary<string, EncounterGroup> EncounterGroups { get; } =
        new Dictionary<string, EncounterGroup>(encounterGroups, StringComparer.OrdinalIgnoreCase);
}
