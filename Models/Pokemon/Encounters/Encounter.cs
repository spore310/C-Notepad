namespace App.Models.Pokemon.Encounter;

readonly record struct GuaranteedItemDrop(int ItemId);

readonly record struct WeightedItemDrop(int ItemId, int Weight);

readonly record struct LevelCap(int MinLevel, int MaxLevel);

sealed class Encounter(int pokedexId, int weight, IEnumerable<WeightedItemDrop> itemDrops)
{
    public int PokedexId { get; } = pokedexId;
    public int Weight { get; } = weight;
    public IReadOnlyList<WeightedItemDrop> ItemDrops { get; } = [.. itemDrops];
}

sealed class EncounterTable
{
    public IReadOnlyList<Encounter> Encounters { get; }

    public int TotalWeight { get; }

    public EncounterTable(IEnumerable<Encounter> encounters)
    {
        Encounter[] orderedEncounters = [.. encounters.OrderBy(encounter => encounter.Weight)];

        Encounters = orderedEncounters;

        TotalWeight = orderedEncounters.Sum(encounter => encounter.Weight);
    }
}

sealed class Stage
{
    public EncounterTable EncounterTable { get; }

    public IReadOnlyList<WeightedItemDrop> WeightedItemDrops { get; }
    public IReadOnlyList<GuaranteedItemDrop> GuaranteedItemDrops { get; }

    public Stage(
        IEnumerable<Encounter> encounters,
        IEnumerable<GuaranteedItemDrop> guaranteedItemDrops,
        IEnumerable<WeightedItemDrop> weightedItemDrops
    )
    {
        GuaranteedItemDrops = [.. guaranteedItemDrops];

        WeightedItemDrops = [.. weightedItemDrops];

        EncounterTable = new(encounters);
    }
}

sealed class Zone(string id, Dictionary<string, Stage> stages)
{
    public string Id { get; } = id;
    public IReadOnlyDictionary<string, Stage> Stages { get; } =
        new Dictionary<string, Stage>(stages, StringComparer.OrdinalIgnoreCase);
}
