using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;

namespace Newtonsoft.Json;

/// <summary>
/// Provides try-method extensions for Newtonsoft.Json to safely attempt serialization and deserialization.
/// </summary>
public static partial class JsonTryConvert
{
    /// <summary>
    /// Attempts to serialize an object to a JSON string using the specified type.
    /// </summary>
    /// <param name="value">The object to serialize.</param>
    /// <param name="objectType">The type to use during serialization. If null, the object's runtime type is used.</param>
    /// <param name="json">The resulting JSON string if successful; otherwise, null.</param>
    /// <returns>True if serialization succeeds; otherwise, false.</returns>
    public static bool TrySerialize(object? value, Type? objectType, out string? json)
    {
        return TrySerialize(value, objectType, default(JsonSerializerSettings), out json);
    }

    /// <summary>
    /// Attempts to serialize an object to a JSON string using the specified type and formatting.
    /// </summary>
    /// <param name="value">The object to serialize.</param>
    /// <param name="objectType">The type to use during serialization. If null, the object's runtime type is used.</param>
    /// <param name="formatting">The formatting options for the output JSON.</param>
    /// <param name="json">The resulting JSON string if successful; otherwise, null.</param>
    /// <returns>True if serialization succeeds; otherwise, false.</returns>
    public static bool TrySerialize(object? value, Type? objectType, Formatting formatting, out string? json)
    {
        return TrySerialize(value, objectType, new JsonSerializerSettings()
        {
            Formatting = formatting,
        }, out json);
    }

    /// <summary>
    /// Attempts to serialize an object to a JSON string using the specified type, formatting, and converters.
    /// </summary>
    /// <typeparam name="T">The type parameter (not used, for API symmetry).</typeparam>
    /// <param name="value">The object to serialize.</param>
    /// <param name="objectType">The type to use during serialization. If null, the object's runtime type is used.</param>
    /// <param name="formatting">The formatting options for the output JSON.</param>
    /// <param name="json">The resulting JSON string if successful; otherwise, null.</param>
    /// <param name="converters">Optional JSON converters to use during serialization.</param>
    /// <returns>True if serialization succeeds; otherwise, false.</returns>
    public static bool TrySerialize<T>(object? value, Type? objectType, Formatting formatting, out string? json, params JsonConverter[] converters)
    {
        JsonSerializerSettings? settings = (converters != null && converters.Length > 0)
            ? new JsonSerializerSettings { Converters = converters, Formatting = formatting }
            : null;

        return TrySerialize(value, objectType, settings, out json);
    }

    /// <summary>
    /// Attempts to serialize an object to a JSON string using the specified type and converters.
    /// </summary>
    /// <typeparam name="T">The type parameter (not used, for API symmetry).</typeparam>
    /// <param name="value">The object to serialize.</param>
    /// <param name="objectType">The type to use during serialization. If null, the object's runtime type is used.</param>
    /// <param name="json">The resulting JSON string if successful; otherwise, null.</param>
    /// <param name="converters">Optional JSON converters to use during serialization.</param>
    /// <returns>True if serialization succeeds; otherwise, false.</returns>
    public static bool TrySerialize<T>(object? value, Type? objectType, out string? json, params JsonConverter[] converters)
    {
        JsonSerializerSettings? settings = (converters != null && converters.Length > 0)
            ? new JsonSerializerSettings { Converters = converters }
            : null;

        return TrySerialize(value, objectType, settings, out json);
    }

    /// <summary>
    /// Attempts to serialize an object to a JSON string using the specified type and serializer settings.
    /// </summary>
    /// <param name="value">The object to serialize.</param>
    /// <param name="objectType">The type to use during serialization. If null, the object's runtime type is used.</param>
    /// <param name="settings">Optional serializer settings.</param>
    /// <param name="json">The resulting JSON string if successful; otherwise, null.</param>
    /// <returns>True if serialization succeeds; otherwise, false.</returns>
    public static bool TrySerialize(object? value, Type? objectType, JsonSerializerSettings? settings, out string? json)
    {
        json = default;

        try
        {
            JsonSerializer serializer = JsonSerializer.Create(settings);

            using StringWriter stringWriter = new();
            using JsonTextWriter jsonWriter = new(stringWriter)
            {
                Formatting = settings?.Formatting ?? Formatting.None,
            };

            if (objectType is null)
            {
                serializer.Serialize(jsonWriter, value);
            }
            else
            {
                serializer.Serialize(jsonWriter, value, objectType);
            }

            jsonWriter.Flush();
            json = stringWriter.ToString();
            return true;
        }
        catch (Exception e)
        {
            if (Debugger.IsAttached)
                Debug.WriteLine(e);
            return false;
        }
    }

    /// <summary>
    /// Attempts to deserialize a JSON string to an object of the specified type.
    /// </summary>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <param name="objectType">The type to deserialize to. If null, deserializes to a dynamic object.</param>
    /// <param name="value">The resulting object if successful; otherwise, null.</param>
    /// <returns>True if deserialization succeeds; otherwise, false.</returns>
    public static bool TryDeserialize(string json, Type? objectType, out object? value)
    {
        return TryDeserialize(json, objectType, default(JsonSerializerSettings), out value);
    }

    /// <summary>
    /// Attempts to deserialize a JSON string to an object of the specified type using the given formatting.
    /// </summary>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <param name="objectType">The type to deserialize to. If null, deserializes to a dynamic object.</param>
    /// <param name="formatting">The formatting options for the JSON parser.</param>
    /// <param name="value">The resulting object if successful; otherwise, null.</param>
    /// <returns>True if deserialization succeeds; otherwise, false.</returns>
    public static bool TryDeserialize(string json, Type? objectType, Formatting formatting, out object? value)
    {
        return TryDeserialize(json, objectType, new JsonSerializerSettings()
        {
            Formatting = formatting,
        }, out value);
    }

    /// <summary>
    /// Attempts to deserialize a JSON string to an object of the specified type using formatting and converters.
    /// </summary>
    /// <typeparam name="T">The type parameter (not used, for API symmetry).</typeparam>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <param name="objectType">The type to deserialize to. If null, deserializes to a dynamic object.</param>
    /// <param name="formatting">The formatting options for the JSON parser.</param>
    /// <param name="value">The resulting object if successful; otherwise, null.</param>
    /// <param name="converters">Optional JSON converters to use during deserialization.</param>
    /// <returns>True if deserialization succeeds; otherwise, false.</returns>
    public static bool TryDeserialize<T>(string json, Type? objectType, Formatting formatting, out object? value, params JsonConverter[] converters)
    {
        JsonSerializerSettings? settings = (converters != null && converters.Length > 0)
            ? new JsonSerializerSettings { Converters = converters, Formatting = formatting }
            : null;

        return TryDeserialize(json, objectType, settings, out value);
    }

    /// <summary>
    /// Attempts to deserialize a JSON string to an object of the specified type using converters.
    /// </summary>
    /// <typeparam name="T">The type parameter (not used, for API symmetry).</typeparam>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <param name="objectType">The type to deserialize to. If null, deserializes to a dynamic object.</param>
    /// <param name="value">The resulting object if successful; otherwise, null.</param>
    /// <param name="converters">Optional JSON converters to use during deserialization.</param>
    /// <returns>True if deserialization succeeds; otherwise, false.</returns>
    public static bool TryDeserialize<T>(string json, Type? objectType, out object? value, params JsonConverter[] converters)
    {
        JsonSerializerSettings? settings = (converters != null && converters.Length > 0)
            ? new JsonSerializerSettings { Converters = converters }
            : null;

        return TryDeserialize(json, objectType, settings, out value);
    }

    /// <summary>
    /// Attempts to deserialize a JSON string to an object of the specified type using serializer settings.
    /// </summary>
    /// <param name="json">The JSON string to deserialize.</param>
    /// <param name="objectType">The type to deserialize to. If null, deserializes to a dynamic object.</param>
    /// <param name="settings">Optional serializer settings.</param>
    /// <param name="value">The resulting object if successful; otherwise, null.</param>
    /// <returns>True if deserialization succeeds; otherwise, false.</returns>
    public static bool TryDeserialize(string json, Type? objectType, JsonSerializerSettings? settings, out object? value)
    {
        value = default;

        if (!TryParse(json, out JToken? token))
            return false;

        if (token is null)
            return false;

        try
        {
            value = token.ToObject(objectType, JsonSerializer.Create(settings));
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
