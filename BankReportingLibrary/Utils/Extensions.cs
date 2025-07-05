using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BankReportingLibrary.Utils;

public static class Extensions
{
    private static readonly JsonSerializerOptions _JsonOptions;

    static Extensions()
    {
        _JsonOptions = new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            ReadCommentHandling = JsonCommentHandling.Skip,
            WriteIndented = false,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };
    }

    /// <summary>
    /// JSON Serialize object
    /// </summary>
    /// <typeparam name="T">The type of the object</typeparam>
    /// <param name="toSerialize">The object</param>
    /// <returns>Serialized object</returns>
    public static string SerializeObject<T>(this T toSerialize)
    {
        //Serialize data
        return JsonSerializer.Serialize(toSerialize, _JsonOptions);
    }

    /// <summary>
    /// JSON De-serialize
    /// </summary>
    /// <typeparam name="T">The type of the object</typeparam>
    /// <param name="jsonData">JSON string</param>
    /// <returns>The object</returns>
    public static T DeserializeObject<T>(this string jsonData)
    {
        //De-Serialize data
        var res = JsonSerializer.Deserialize<T>(jsonData, _JsonOptions);
        if (res != null)
        {
            return res;
        }
        else
        {
            throw new JsonException("Null object deserialized!");
        }
    }

    /// <summary>
    /// De-serialize from XML
    /// </summary>
    /// <typeparam name="T">The type of the object</typeparam>
    /// <param name="xmlData">XML string</param>
    /// <returns>The object</returns>
    public static T? FromXML<T>(this string xmlData)
    {
        // De-Serialize data
        T? res;

        var serializer = new XmlSerializer(typeof(T));
        using (var reader = new StringReader(xmlData))
        {
            res = (T?)serializer.Deserialize(reader);
        }

        return res;
    }
}
