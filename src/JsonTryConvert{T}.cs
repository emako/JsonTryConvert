using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;

namespace Newtonsoft.Json;

/// <summary>
/// Provides generic try-method extensions for Newtonsoft.Json to safely attempt serialization and deserialization for a specific type.
/// </summary>
public static partial class JsonTryConvert
{
    /// <summary>
    /// Attempts to serialize a value of type <typeparamref name="T"/> to a JSON string.
    /// </summary>
    /// <typeparam name="T">The type of the value to serialize.</typeparam>
    /// <param name="value">The value to serialize.</param>
    /// <param name="json">The resulting JSON string if successful; otherwise, null.</param>
    /// <returns>True if serialization succeeds; otherwise, false.</returns>
    public static bool TrySerialize<T>(T? value, out string? json)
    {
        return TrySerialize(value, typeof(T), default(JsonSerializerSettings), out json);
    }

    /// <summary>
    /// Attempts to serialize a value of type <typeparamref name="T"/> to a JSON string with the specified formatting.
    /// </summary>
    /// <typeparam name="T">The type of the value to serialize.</typeparam>
    /// <param name="value">The value to serialize.</param>
    /// <param name="formatting">The formatting options for the output JSON.</param>
    /// <param name="json">The resulting JSON string if successful; otherwise, null.</param>
    /// <returns>True if serialization succeeds; otherwise, false.</returns>
    public static bool TrySerialize<T>(T? value, Formatting formatting, out string? json)
    {
        return TrySerialize(value, typeof(T), new JsonSerializerSettings()
        {
            Formatting = formatting,
        }, out json);
    }

    /// <summary>
    /// Attempts to serialize a value of type <typeparamref name="T"/> to a JSON string with the specified formatting and converters.
    /// </summary>
    /// <typeparam name="T">The type of the value to serialize.</typeparam>
    /// <param name="value">The value to serialize.</param>
    /// <param name="formatting">The formatting options for the output JSON.</param>
    /// <param name="json">The resulting JSON string if successful; otherwise, null.</param>
    /// <param name="converters">Optional JSON converters to use during serialization.</param>
    /// <returns>True if serialization succeeds; otherwise, false.</returns>
    public static bool TrySerialize<T>(T? value, Formatting formatting, out string? json, params JsonConverter[] converters)
    {
        JsonSerializerSettings? settings = (converters != null && converters.Length > 0)
            ? new JsonSerializerSettings { Converters = converters, Formatting = formatting }
            : null;

        return TrySerialize(value, typeof(T), settings, out json);
    }

    /// <summary>
    /// Attempts to serialize a value of type <typeparamref name="T"/> to a JSON string with the specified converters.
    /// </summary>
    /// <typeparam name="T">The type of the value to serialize.</typeparam>
    /// <param name="value">The value to serialize.</param>
    /// <param name="json">The resulting JSON string if successful; otherwise, null.</param>
    /// <param name="converters">Optional JSON converters to use during serialization.</param>
    /// <returns>True if serialization succeeds; otherwise, false.</returns>
    public static bool TrySerialize<T>(T? value, out string? json, params JsonConverter[] converters)
    {
        JsonSerializerSettings? settings = (converters != null && converters.Length > 0)
            ? new JsonSerializerSettings { Converters = converters }
            : null;

        return TrySerialize(value, typeof(T), settings, out json);
    }

    /// <summary>
    /// Attempts to serialize a value of type <typeparamref name="T"/> to a JSON string with the specified serializer settings.
    /// </summary>
    /// <typeparam name="T">The type of the value to serialize.</typeparam>
    /// <param name="value">The value to serialize.</param>
    /// <param name="settings">Optional serializer settings.</param>
    /// <param name="json">The resulting JSON string if successful; otherwise, null.</param>
    /// <returns>True if serialization succeeds; otherwise, false.</returns>
    public static bool TrySerialize<T>(T? value, JsonSerializerSettings? settings, out string? json)
    {
        return TrySerialize(value, typeof(T), settings, out json);
    }

    /// <summary>
    /// Attempts to deserialize a JSON string to a value of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type to deserialize to.</typeparam>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <param name="value">The resulting value if successful; otherwise, default.</param>
    /// <returns>True if deserialization succeeds; otherwise, false.</returns>
    public static bool TryDeserialize<T>(string json, out T? value)
    {
        return TryDeserialize(json, default(JsonSerializerSettings), out value);
    }

    /// <summary>
    /// Attempts to deserialize a JSON string to a value of type <typeparamref name="T"/> using the specified formatting.
    /// </summary>
    /// <typeparam name="T">The type to deserialize to.</typeparam>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <param name="formatting">The formatting options for the JSON parser.</param>
    /// <param name="value">The resulting value if successful; otherwise, default.</param>
    /// <returns>True if deserialization succeeds; otherwise, false.</returns>
    public static bool TryDeserialize<T>(string json, Formatting formatting, out T? value)
    {
        return TryDeserialize(json, new JsonSerializerSettings()
        {
            Formatting = formatting,
        }, out value);
    }

    /// <summary>
    /// Attempts to deserialize a JSON string to a value of type <typeparamref name="T"/> using the specified formatting and converters.
    /// </summary>
    /// <typeparam name="T">The type to deserialize to.</typeparam>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <param name="formatting">The formatting options for the JSON parser.</param>
    /// <param name="value">The resulting value if successful; otherwise, default.</param>
    /// <param name="converters">Optional JSON converters to use during deserialization.</param>
    /// <returns>True if deserialization succeeds; otherwise, false.</returns>
    public static bool TryDeserialize<T>(string json, Formatting formatting, out T? value, params JsonConverter[] converters)
    {
        JsonSerializerSettings? settings = (converters != null && converters.Length > 0)
            ? new JsonSerializerSettings { Converters = converters, Formatting = formatting }
            : null;

        return TryDeserialize(json, settings, out value);
    }

    /// <summary>
    /// Attempts to deserialize a JSON string to a value of type <typeparamref name="T"/> using the specified converters.
    /// </summary>
    /// <typeparam name="T">The type to deserialize to.</typeparam>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <param name="value">The resulting value if successful; otherwise, default.</param>
    /// <param name="converters">Optional JSON converters to use during deserialization.</param>
    /// <returns>True if deserialization succeeds; otherwise, false.</returns>
    public static bool TryDeserialize<T>(string json, out T? value, params JsonConverter[] converters)
    {
        JsonSerializerSettings? settings = (converters != null && converters.Length > 0)
            ? new JsonSerializerSettings { Converters = converters }
            : null;

        return TryDeserialize(json, settings, out value);
    }

    /// <summary>
    /// Attempts to deserialize a JSON string to a value of type <typeparamref name="T"/> using the specified serializer settings.
    /// </summary>
    /// <typeparam name="T">The type to deserialize to.</typeparam>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <param name="settings">Optional serializer settings.</param>
    /// <param name="value">The resulting value if successful; otherwise, default.</param>
    /// <returns>True if deserialization succeeds; otherwise, false.</returns>
    public static bool TryDeserialize<T>(string json, JsonSerializerSettings? settings, out T? value)
    {
        value = default;

        if (!TryParse(json, out JToken? token))
            return false;

        if (token is null)
            return false;

        try
        {
            value = token.ToObject<T>(JsonSerializer.Create(settings));
            return true;
        }
        catch (Exception e)
        {
            if (Debugger.IsAttached)
                Debug.WriteLine(e);
            return false;
        }
    }
}
