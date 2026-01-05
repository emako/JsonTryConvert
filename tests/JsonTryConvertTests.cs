using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;

namespace TryJsonConvert.Tests;

/// <summary>
/// Unit tests for the JsonTryConvert class, covering serialization and deserialization scenarios.
/// </summary>
[TestFixture]
public class JsonTryConvertTests
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
    /// Tests serialization and deserialization using explicit object type.
    /// </summary>
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

    /// <summary>
    /// Tests generic serialization and deserialization.
    /// </summary>
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

    /// <summary>
    /// Tests serialization with an invalid object type.
    /// </summary>
    [Test]
    public void TrySerialize_InvalidObjectType_Success()
    {
        Assert.IsTrue(JsonTryConvert.TrySerialize(new object(), typeof(List<int>), out var json));
        Assert.IsNotNull(json);
    }

    /// <summary>
    /// Tests deserialization with invalid JSON input.
    /// </summary>
    [Test]
    public void TryDeserialize_InvalidJson_Fails()
    {
        Assert.IsFalse(JsonTryConvert.TryDeserialize("not a json", typeof(Dummy), out var value));
        Assert.IsNull(value);
    }

    /// <summary>
    /// Tests serialization and deserialization with formatting and converters.
    /// </summary>
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

    /// <summary>
    /// Tests serialization and deserialization of a null value.
    /// </summary>
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
