namespace App.Models.Pokemon.Encounter;

readonly record struct Encounter(int PokedexId, int Weight);

sealed class EncounterGroup(IEnumerable<Encounter> encounters)
{
    public IReadOnlyList<Encounter> Encounters { get; } =
    [.. encounters.OrderBy(encounter => encounter.Weight)];
}

sealed class Zone(string id, Dictionary<string, EncounterGroup> encounterGroups)
{
    public string Id { get; } = id;
    public IReadOnlyDictionary<string, EncounterGroup> EncounterGroups { get; } =
        new Dictionary<string, EncounterGroup>(encounterGroups, StringComparer.OrdinalIgnoreCase);
}
