using Newtonsoft.Json.Linq;

namespace Newtonsoft.Json;

public static partial class JsonTryConvert
{
    public static bool TryParse(string json, out JToken? token)
    {
        try
        {
            token = JToken.Parse(json);
            return true;
        }
        catch (JsonReaderException e)
        {
            _ = e;
            token = null;
            return false;
        }
    }
}
