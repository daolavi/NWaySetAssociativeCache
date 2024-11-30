namespace NWaySetAssociativeCache.Core.Unit.Tests;

[TestFixture]
public class LRUReplacementPolicyCacheTests
{
    private NWaySetAssociativeCache<int, string> _sut;
    
    [SetUp]
    public void Setup()
    {
        _sut = new NWaySetAssociativeCache<int, string>(1, 2, new LRUReplacementPolicy<int>());
    }

    [Test]
    public void Test_PutAndGetValue()
    {
        _sut.Put(1, "a");
        var value = _sut.Get(1);
        Assert.That(value, Is.EqualTo("a"));
    }

    [Test]
    public void Test_GetValue_NotExist()
    {
        _sut.Put(1, "a");
        Assert.Throws<KeyNotFoundException>(() => _sut.Get(2));
    }

    [Test]
    public void Test_GetValue_RemovedByLRUPolicy()
    {
        _sut.Put(1, "a");
        _sut.Put(2, "b");
        _sut.Put(3, "c");
        
        Assert.Throws<KeyNotFoundException>(() => _sut.Get(1));
        Assert.That(_sut.Get(2), Is.EqualTo("b"));
        Assert.That(_sut.Get(3), Is.EqualTo("c"));
    }
}