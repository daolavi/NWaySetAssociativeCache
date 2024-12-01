namespace NWaySetAssociativeCache.Core;

public class TTLReplacementPolicy<TKey> : IReplacementPolicy<TKey> 
    where TKey : notnull
{
    private Dictionary<TKey, DateTime> _expirations;
    private TimeSpan _ttl;

    public TTLReplacementPolicy(TimeSpan ttl)
    {
        _ttl = ttl;
        _expirations = new Dictionary<TKey, DateTime>();
    }
    
    public void RecordAccess(TKey key)
    {
        _expirations[key] = DateTime.Now.Add(_ttl);
    }

    public TKey SelectVictim()
    {
        var key = _expirations.FirstOrDefault(pair => pair.Value <= DateTime.Now).Key;
        if (!EqualityComparer<TKey>.Default.Equals(key, default))
        {
            return key;
        }
        throw new Exception("No item expired");
    }

    public void Add(TKey key)
    {
        _expirations[key] = DateTime.Now.Add(_ttl);
    }

    public void Remove(TKey key)
    {
        _expirations.Remove(key);
    }
}