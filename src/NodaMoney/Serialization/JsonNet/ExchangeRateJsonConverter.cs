﻿using System;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NodaMoney.Serialization.JsonNet
{
    /// <summary>Converts a instance of ExchangeRate to and from JSON.</summary>
    public class ExchangeRateJsonConverter : JsonConverter
    {
        /// <summary>Gets a value indicating whether this <see cref="T:Newtonsoft.Json.JsonConverter" /> can read JSON.</summary>
        /// <value><c>true</c> if this <see cref="T:Newtonsoft.Json.JsonConverter" /> can read JSON; otherwise, <c>false</c>. </value>
        public override bool CanRead => true;

        /// <summary>Writes the JSON representation of the object.</summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter"/> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <exception cref="ArgumentNullException">The value of 'writer', 'value' and 'serializer' cannot be null.</exception>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (serializer == null)
                throw new ArgumentNullException(nameof(serializer));

            ExchangeRate exchangeRate = (ExchangeRate)value;

            writer.WriteStartObject();
            writer.WritePropertyName("baseCurrency");
            serializer.Serialize(writer, exchangeRate.BaseCurrency.Code);
            writer.WritePropertyName("quoteCurrency");
            serializer.Serialize(writer, exchangeRate.QuoteCurrency.Code);
            writer.WritePropertyName("value");
            serializer.Serialize(writer, exchangeRate.Value.ToString(CultureInfo.InvariantCulture));
            writer.WriteEndObject();
        }

        /// <summary>Reads the JSON representation of the object.</summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader"/> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        /// <exception cref="ArgumentNullException">The value of 'reader' cannot be null.</exception>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            JObject jsonObject = JObject.Load(reader);
            var properties = jsonObject.Properties().ToList();

            return new ExchangeRate(Currency.FromCode((string)properties[0].Value), Currency.FromCode((string)properties[1].Value), (decimal)properties[2].Value);
        }

        /// <summary>Determines whether this instance can convert the specified object type.</summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns><c>true</c> if this instance can convert the specified object type; otherwise,<c>false</c>.</returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ExchangeRate);
        }
    }

}
