namespace NWaySetAssociativeCache.Core.Unit.Tests;

[TestFixture]
public class TTLReplacementPolicyCacheTests
{
    private NWaySetAssociativeCache<int, string> _sut;

    [SetUp]
    public void SetUp()
    {
        _sut = new NWaySetAssociativeCache<int, string>(1, 3, () => new TTLReplacementPolicy<int>(new TimeSpan(0, 0, 5)));
    }
    
    [Test]
    public void Test_GetValue_RemovedByTTLPolicy()
    {
        _sut.Put(1, "a");
        _sut.Put(2, "b");
        _sut.Put(3, "c");
        
        Assert.Throws<Exception>(() => _sut.Put(4, "d"));
        Thread.Sleep(new TimeSpan(0, 0, 5));
        _sut.Get(2);
        _sut.Get(3);
        _sut.Put(4, "d");
        Assert.Throws<KeyNotFoundException>(() => _sut.Get(1));
        Assert.That(_sut.Get(4), Is.EqualTo("d"));
    }
}