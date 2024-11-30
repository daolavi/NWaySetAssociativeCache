namespace NWaySetAssociativeCache.Core;

public interface IReplacementPolicy<TKey> where TKey : notnull
{
    void RecordAccess(TKey key);
    TKey SelectVictim();
    void Add(TKey key);
    void Remove(TKey key);
}