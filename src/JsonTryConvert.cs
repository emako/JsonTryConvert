using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;

namespace Newtonsoft.Json;

public static partial class JsonTryConvert
{
    public static bool TrySerialize(object? value, Type? objectType, out string? json)
    {
        return TrySerialize(value, objectType, default(JsonSerializerSettings), out json);
    }

    public static bool TrySerialize(object? value, Type? objectType, Formatting formatting, out string? json)
    {
        return TrySerialize(value, objectType, new JsonSerializerSettings()
        {
            Formatting = formatting,
        }, out json);
    }

    public static bool TrySerialize<T>(object? value, Type? objectType, Formatting formatting, out string? json, params JsonConverter[] converters)
    {
        JsonSerializerSettings? settings = (converters != null && converters.Length > 0)
            ? new JsonSerializerSettings { Converters = converters, Formatting = formatting }
            : null;

        return TrySerialize(value, objectType, settings, out json);
    }

    public static bool TrySerialize<T>(object? value, Type? objectType, out string? json, params JsonConverter[] converters)
    {
        JsonSerializerSettings? settings = (converters != null && converters.Length > 0)
            ? new JsonSerializerSettings { Converters = converters }
            : null;

        return TrySerialize(value, objectType, settings, out json);
    }

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

    public static bool TryDeserialize(string json, Type? objectType, out object? value)
    {
        return TryDeserialize(json, objectType, default(JsonSerializerSettings), out value);
    }

    public static bool TryDeserialize(string json, Type? objectType, Formatting formatting, out object? value)
    {
        return TryDeserialize(json, objectType, new JsonSerializerSettings()
        {
            Formatting = formatting,
        }, out value);
    }

    public static bool TryDeserialize<T>(string json, Type? objectType, Formatting formatting, out object? value, params JsonConverter[] converters)
    {
        JsonSerializerSettings? settings = (converters != null && converters.Length > 0)
            ? new JsonSerializerSettings { Converters = converters, Formatting = formatting }
            : null;

        return TryDeserialize(json, objectType, settings, out value);
    }

    public static bool TryDeserialize<T>(string json, Type? objectType, out object? value, params JsonConverter[] converters)
    {
        JsonSerializerSettings? settings = (converters != null && converters.Length > 0)
            ? new JsonSerializerSettings { Converters = converters }
            : null;

        return TryDeserialize(json, objectType, settings, out value);
    }

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
