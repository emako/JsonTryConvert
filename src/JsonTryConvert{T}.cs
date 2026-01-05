using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;

namespace Newtonsoft.Json;

public static partial class JsonTryConvert
{
    public static bool TrySerialize<T>(T? value, out string? json)
    {
        return TrySerialize(value, typeof(T), default(JsonSerializerSettings), out json);
    }

    public static bool TrySerialize<T>(T? value, Formatting formatting, out string? json)
    {
        return TrySerialize(value, typeof(T), new JsonSerializerSettings()
        {
            Formatting = formatting,
        }, out json);
    }

    public static bool TrySerialize<T>(T? value, Formatting formatting, out string? json, params JsonConverter[] converters)
    {
        JsonSerializerSettings? settings = (converters != null && converters.Length > 0)
            ? new JsonSerializerSettings { Converters = converters, Formatting = formatting }
            : null;

        return TrySerialize(value, typeof(T), settings, out json);
    }

    public static bool TrySerialize<T>(T? value, out string? json, params JsonConverter[] converters)
    {
        JsonSerializerSettings? settings = (converters != null && converters.Length > 0)
            ? new JsonSerializerSettings { Converters = converters }
            : null;

        return TrySerialize(value, typeof(T), settings, out json);
    }

    public static bool TrySerialize<T>(T? value, JsonSerializerSettings? settings, out string? json)
    {
        return TrySerialize(value, typeof(T), settings, out json);
    }

    public static bool TryDeserialize<T>(string json, out T? value)
    {
        return TryDeserialize(json, default(JsonSerializerSettings), out value);
    }

    public static bool TryDeserialize<T>(string json, Formatting formatting, out T? value)
    {
        return TryDeserialize(json, new JsonSerializerSettings()
        {
            Formatting = formatting,
        }, out value);
    }

    public static bool TryDeserialize<T>(string json, Formatting formatting, out T? value, params JsonConverter[] converters)
    {
        JsonSerializerSettings? settings = (converters != null && converters.Length > 0)
            ? new JsonSerializerSettings { Converters = converters, Formatting = formatting }
            : null;

        return TryDeserialize(json, settings, out value);
    }

    public static bool TryDeserialize<T>(string json, out T? value, params JsonConverter[] converters)
    {
        JsonSerializerSettings? settings = (converters != null && converters.Length > 0)
            ? new JsonSerializerSettings { Converters = converters }
            : null;

        return TryDeserialize(json, settings, out value);
    }

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
