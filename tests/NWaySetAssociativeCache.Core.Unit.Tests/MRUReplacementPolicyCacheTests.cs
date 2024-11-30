namespace NWaySetAssociativeCache.Core.Unit.Tests;

[TestFixture]
public class MRUReplacementPolicyCacheTests
{
    private NWaySetAssociativeCache<int, string> _sut;
    
    [SetUp]
    public void Setup()
    {
        _sut = new NWaySetAssociativeCache<int, string>(1, 3, new MRUReplacementPolicy<int>());
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
        _sut.Put(4, "d");
        
        Assert.Throws<KeyNotFoundException>(() => _sut.Get(3));
        Assert.That(_sut.Get(4), Is.EqualTo("d"));
        Assert.That(_sut.Get(2), Is.EqualTo("b"));
        Assert.That(_sut.Get(1), Is.EqualTo("a"));

        _sut.Put(5, "e");
        Assert.Throws<KeyNotFoundException>(() => _sut.Get(1));
        Assert.That(_sut.Get(5), Is.EqualTo("e"));
    }
}