![logo](branding/titlebar.png)

[![GitHub license](https://img.shields.io/github/license/emako/TryJsonConvert)](https://github.com/emako/TryJsonConvert/blob/master/LICENSE) [![NuGet](https://img.shields.io/nuget/v/TryJsonConvert.svg)](https://nuget.org/packages/TryJsonConvert) [![Actions](https://github.com/emako/TryJsonConvert/actions/workflows/library.nuget.yml/badge.svg)](https://github.com/emako/TryJsonConvert/actions/workflows/library.nuget.yml)

# TryJsonConvert

TryJsonConvert provides safe, exception-free try-method extensions for [Newtonsoft.Json](https://www.newtonsoft.com/json) to attempt serialization and deserialization. It is compatible with .NET Standard, .NET Core, .NET Framework, and .NET 5/6/7/8/9/10.

## Features
- Safe `TrySerialize` and `TryDeserialize` methods for JSON conversion
- Overloads for type, formatting, and custom converters
- No exceptions thrown on failure¡ªreturns `false` and outputs `null`
- Supports all major .NET platforms

## Installation
Install from NuGet:

```shell
Install-Package TryJsonConvert
```

Or via .NET CLI:

```shell
dotnet add package TryJsonConvert
```

## Usage

```csharp
using Newtonsoft.Json;

// Example class
data class Dummy { public int Id { get; set; } public string? Name { get; set; } }

var obj = new Dummy { Id = 1, Name = "abc" };

// Try to serialize
if (JsonTryConvert.TrySerialize(obj, out string? json))
{
    // Try to deserialize
    if (JsonTryConvert.TryDeserialize(json!, out Dummy? result))
    {
        // Use result
    }
}
```

### With Type and Formatting
```csharp
JsonTryConvert.TrySerialize(obj, typeof(Dummy), Formatting.Indented, out var json);
JsonTryConvert.TryDeserialize(json, typeof(Dummy), out var result);
```

### With Custom Converters
```csharp
JsonTryConvert.TrySerialize(obj, out var json, new MyCustomConverter());
JsonTryConvert.TryDeserialize(json, out Dummy? result, new MyCustomConverter());
```

## API
- `TrySerialize(object? value, out string? json)`
- `TrySerialize(object? value, Type? objectType, out string? json)`
- `TrySerialize(object? value, Type? objectType, Formatting formatting, out string? json)`
- `TrySerialize<T>(object? value, Type? objectType, Formatting formatting, out string? json, params JsonConverter[] converters)`
- `TrySerialize<T>(object? value, Type? objectType, out string? json, params JsonConverter[] converters)`
- `TrySerialize(object? value, Type? objectType, JsonSerializerSettings? settings, out string? json)`
- `TryDeserialize(string json, out T? value)`
- `TryDeserialize(string json, Type? objectType, out object? value)`
- `TryDeserialize(string json, Type? objectType, Formatting formatting, out object? value)`
- `TryDeserialize<T>(string json, Type? objectType, Formatting formatting, out object? value, params JsonConverter[] converters)`
- `TryDeserialize<T>(string json, Type? objectType, out object? value, params JsonConverter[] converters)`
- `TryDeserialize(string json, Type? objectType, JsonSerializerSettings? settings, out object? value)`

## License

[MIT](LICENSE)
