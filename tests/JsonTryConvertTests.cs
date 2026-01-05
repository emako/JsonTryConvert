using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;

namespace TryJsonConvert.Tests;

[TestFixture]
public class JsonTryConvertTests
{
    public class Dummy
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    [Test]
    public void TrySerializeAndDeserialize_ObjectType_Success()
    {
        var obj = new Dummy { Id = 1, Name = "abc" };
        Assert.IsTrue(JsonTryConvert.TrySerialize(obj, typeof(Dummy), out var json));
        Assert.IsNotNull(json);
        Assert.IsTrue(JsonTryConvert.TryDeserialize(json!, typeof(Dummy), out var result));
        Assert.IsInstanceOf<Dummy>(result);
        Assert.AreEqual(1, ((Dummy)result!).Id);
        Assert.AreEqual("abc", ((Dummy)result!).Name);
    }

    [Test]
    public void TrySerializeAndDeserialize_Generic_Success()
    {
        var obj = new Dummy { Id = 2, Name = "xyz" };
        Assert.IsTrue(JsonTryConvert.TrySerialize(obj, out string? json));
        Assert.IsNotNull(json);
        Assert.IsTrue(JsonTryConvert.TryDeserialize(json!, out Dummy? result));
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result!.Id);
        Assert.AreEqual("xyz", result.Name);
    }

    [Test]
    public void TrySerialize_InvalidObjectType_Fails()
    {
        Assert.IsFalse(JsonTryConvert.TrySerialize(new object(), typeof(List<int>), out var json));
        Assert.IsNull(json);
    }

    [Test]
    public void TryDeserialize_InvalidJson_Fails()
    {
        Assert.IsFalse(JsonTryConvert.TryDeserialize("not a json", typeof(Dummy), out var value));
        Assert.IsNull(value);
    }

    [Test]
    public void TrySerializeAndDeserialize_WithFormattingAndConverters()
    {
        var obj = new Dummy { Id = 3, Name = "test" };
        Assert.IsTrue(JsonTryConvert.TrySerialize(obj, Formatting.Indented, out var json));
        Assert.IsNotNull(json);
        Assert.IsTrue(json!.Contains("\n"));
        Assert.IsTrue(JsonTryConvert.TryDeserialize(json, out Dummy? result));
        Assert.IsNotNull(result);
        Assert.AreEqual(3, result!.Id);
        Assert.AreEqual("test", result.Name);
    }

    [Test]
    public void TrySerializeAndDeserialize_NullValue()
    {
        Dummy? obj = null;
        Assert.IsTrue(JsonTryConvert.TrySerialize(obj, out var json));
        Assert.AreEqual("null", json);
        Assert.IsTrue(JsonTryConvert.TryDeserialize(json!, out Dummy? result));
        Assert.IsNull(result);
    }
}
