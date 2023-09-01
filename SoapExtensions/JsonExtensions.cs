using Newtonsoft.Json;

namespace SoapExtensions
{
    /// <summary>
    /// Extension methods for serializing and deserializing JSON
    /// </summary>
    public static class JsonExtensions
    {
        /// <summary>
        /// Deserializes the JSON string into an object of the specified type
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to</typeparam>
        /// <param name="json">The JSON string to deserialize</param>
        /// <returns>The deserialized object, or null if deserialization fails</returns>
        public static T? To<T>(this string json)
          => JsonConvert.DeserializeObject<T>(json);

        /// <summary>
        /// Deserializes the JSON string into an object of the specified type
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to</typeparam>
        /// <param name="json">The JSON string to deserialize</param>
        /// <param name="obj">The deserialized object, or null if deserialization fails</param>
        public static void To<T>(this string json, out T? obj)
          => obj = json.To<T>();

        /// <summary>
        /// Serializes the object into a JSON string
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize</typeparam>
        /// <param name="obj">The object to serialize</param>
        /// <returns>The serialized JSON string</returns>
        public static string ToJson<T>(this T obj)
          => JsonConvert.SerializeObject(obj);
    }
}