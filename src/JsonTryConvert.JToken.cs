using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;

namespace Newtonsoft.Json;

/// <summary>
/// Provides helper methods for safely parsing JSON strings into <see cref="JToken"/> objects.
/// </summary>
public static partial class JsonTryConvert
{
    /// <summary>
    /// Attempts to parse a JSON string into a <see cref="JToken"/>.
    /// </summary>
    /// <param name="json">The JSON string to parse.</param>
    /// <param name="token">The resulting <see cref="JToken"/> if successful; otherwise, null.</param>
    /// <returns>True if parsing succeeds; otherwise, false.</returns>
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
