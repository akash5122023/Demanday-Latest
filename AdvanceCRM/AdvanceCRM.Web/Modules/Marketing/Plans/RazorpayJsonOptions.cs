using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AdvanceCRM.Razorpay
{
    internal static class RazorpayJsonOptions
    {
        public static JsonSerializerOptions CreateDefault()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };

            options.Converters.Add(new RazorpayNotesJsonConverter());

            return options;
        }
    }

    internal sealed class RazorpayNotesJsonConverter : JsonConverter<Dictionary<string, string>>
    {
        public override Dictionary<string, string> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;

            if (reader.TokenType != JsonTokenType.StartObject)
                throw new JsonException($"Expected start of object for Razorpay notes but found {reader.TokenType}.");

            var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                    return result;

                if (reader.TokenType != JsonTokenType.PropertyName)
                    throw new JsonException($"Unexpected token {reader.TokenType} while reading Razorpay notes.");

                var key = reader.GetString() ?? string.Empty;

                if (!reader.Read())
                    throw new JsonException("Unexpected end while reading Razorpay notes value.");

                result[key] = ReadValueAsString(ref reader);
            }

            throw new JsonException("Unexpected end while reading Razorpay notes.");
        }

        public override void Write(Utf8JsonWriter writer, Dictionary<string, string> value, JsonSerializerOptions options)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            if (value == null)
            {
                writer.WriteNullValue();
                return;
            }

            writer.WriteStartObject();

            foreach (var pair in value)
            {
                writer.WritePropertyName(pair.Key ?? string.Empty);

                if (pair.Value is null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    writer.WriteStringValue(pair.Value);
                }
            }

            writer.WriteEndObject();
        }

        private static string ReadValueAsString(ref Utf8JsonReader reader)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    return reader.GetString();
                case JsonTokenType.Number:
                    if (reader.TryGetInt64(out var longValue))
                        return longValue.ToString(CultureInfo.InvariantCulture);
                    if (reader.TryGetUInt64(out var ulongValue))
                        return ulongValue.ToString(CultureInfo.InvariantCulture);
                    if (reader.TryGetDecimal(out var decimalValue))
                        return decimalValue.ToString(CultureInfo.InvariantCulture);
                    return reader.GetDouble().ToString(CultureInfo.InvariantCulture);
                case JsonTokenType.True:
                    return "true";
                case JsonTokenType.False:
                    return "false";
                case JsonTokenType.Null:
                    return null;
                default:
                    throw new JsonException($"Unsupported token {reader.TokenType} in Razorpay notes.");
            }
        }
    }
}
