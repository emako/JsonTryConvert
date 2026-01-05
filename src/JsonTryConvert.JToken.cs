using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;

namespace Newtonsoft.Json;

/// <summary>
/// <para>JToken vs JObject quick summary:</para>
/// <para><b>JToken</b>: Abstract base for all JSON nodes.</para>
/// <para><b>JObject</b>: Concrete implementation for JSON objects (key-value pairs).</para>
/// <para>👉 <c>JObject : JContainer : JToken</c></para>
/// <para>Class hierarchy:</para>
/// <code>
/// JToken
///  ├── JValue      // string / number / bool / null
///  ├── JContainer
///  │    ├── JObject    // { "a": 1 }
///  │    ├── JArray     // [1, 2, 3]
///  │    └── JProperty  // "a": 1
/// </code>
/// <para>JToken is a unified abstraction for any JSON node, including:</para>
/// <list type="bullet">
///   <item>an object { }</item>
///   <item>an array [ ]</item>
///   <item>a value "abc" / 123 / true / null</item>
///   <item>a property "a": 1</item>
/// </list>
/// </summary>
public static partial class JsonTryConvert
{
    public static bool TryParse(string json, out JToken? token)
    {
        try
        {
            token = JToken.Parse(json);
            return true;
        }
        catch (Exception e)
        {
            if (Debugger.IsAttached)
                Debug.WriteLine(e);
            token = null;
            return false;
        }
    }
}
