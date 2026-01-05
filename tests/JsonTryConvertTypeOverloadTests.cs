using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;

namespace TryJsonConvert.Tests;

[TestFixture]
public class JsonTryConvertTypeOverloadTests
{
    public class Dummy
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    [Test]
    public void TrySerializeAndDeserialize_TypeNull_Success()
    {
        var obj = new Dummy { Id = 10, Name = "type-null" };
        Assert.IsTrue(JsonTryConvert.TrySerialize(obj, null, out var json));
        Assert.IsNotNull(json);
        Assert.IsTrue(JsonTryConvert.TryDeserialize(json!, typeof(Dummy), out var result));
        Assert.IsInstanceOf<Dummy>(result);
        Assert.AreEqual(10, ((Dummy)result!).Id);
        Assert.AreEqual("type-null", ((Dummy)result!).Name);
    }

    [Test]
    public void TrySerializeAndDeserialize_TypeMismatch_Fails()
    {
        var obj = new Dummy { Id = 11, Name = "mismatch" };
        Assert.IsFalse(JsonTryConvert.TrySerialize(obj, typeof(List<int>), out var json));
        Assert.IsNull(json);
    }

    [Test]
    public void TryDeserialize_TypeNull_Fails()
    {
        var json = "{\"Id\":1,\"Name\":\"abc\"}";
        Assert.IsFalse(JsonTryConvert.TryDeserialize(json, null, out var value));
        Assert.IsNull(value);
    }
}
