using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;

namespace JsonTryConvert.Tests;

/// <summary>
/// Unit tests for type overload scenarios in JsonTryConvert.
/// </summary>
[TestFixture]
public class JsonTryConvertTypeOverloadTests
{
    /// <summary>
    /// Dummy class for testing serialization and deserialization.
    /// </summary>
    public class Dummy
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string? Name { get; set; }
    }

    /// <summary>
    /// Tests serialization and deserialization with a null type parameter.
    /// </summary>
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

    /// <summary>
    /// Tests serialization with a mismatched type parameter.
    /// </summary>
    [Test]
    public void TrySerializeAndDeserialize_TypeMismatch_Success()
    {
        var obj = new Dummy { Id = 11, Name = "mismatch" };
        Assert.IsTrue(JsonTryConvert.TrySerialize(obj, typeof(List<int>), out var json));
        Assert.IsNotNull(json);
    }

    /// <summary>
    /// Tests deserialization with a null type parameter.
    /// </summary>
    [Test]
    public void TryDeserialize_TypeNull_Success()
    {
        var json = "{\"Id\":1,\"Name\":\"abc\"}";
        Assert.IsTrue(JsonTryConvert.TryDeserialize(json, null, out var value));
        Assert.IsNotNull(value);
    }
}
