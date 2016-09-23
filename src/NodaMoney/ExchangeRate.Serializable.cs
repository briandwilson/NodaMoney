﻿using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
#if !PORTABLE
using System.Xml.Serialization;
#endif
using Newtonsoft.Json;
using NodaMoney.Serialization.JsonNet;

namespace NodaMoney
{
    /// <summary>A conversion of money of one currency into money of another currency</summary>
    [JsonConverter(typeof(ExchangeRateJsonConverter))]
    public partial struct ExchangeRate
#if !PORTABLE
        : IXmlSerializable
#endif
    {
        /// <summary>This method is reserved and should not be used. When implementing the IXmlSerializable interface, you should
        /// return null (Nothing in Visual Basic) from this method, and instead, if specifying a custom schema is required, apply
        /// the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute" /> to the class.</summary>
        /// <returns>An <see cref="T:System.Xml.Schema.XmlSchema" /> that describes the XML representation of the object that is
        /// produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)" /> method
        /// and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)" /> method.
        /// </returns>
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>Generates an object from its XML representation.</summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> stream from which the object is deserialized.</param>
        /// <exception cref="System.ArgumentNullException">The value of 'reader' cannot be null.</exception>
        /// <exception cref="System.Runtime.Serialization.SerializationException">The xml should have a content element with name ExchangeRate!</exception>
        public void ReadXml(XmlReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            if (reader.MoveToContent() != XmlNodeType.Element)
                throw new SerializationException("Couldn't find content element with name ExchangeRate!");

            BaseCurrency = Currency.FromCode(reader["BaseCurrency"]);
			QuoteCurrency = Currency.FromCode(reader["QuoteCurrency"]);
            Value = decimal.Parse(reader["Value"], CultureInfo.InvariantCulture);
        }

        /// <summary>Converts an object into its XML representation.</summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> stream to which the object is serialized.</param>
        /// <exception cref="System.ArgumentNullException">The value of 'writer' cannot be null.</exception>
        public void WriteXml(XmlWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            writer.WriteAttributeString("BaseCurrency", BaseCurrency.Code);
            writer.WriteAttributeString("QuoteCurrency", QuoteCurrency.Code);
            writer.WriteAttributeString("Value", Value.ToString(CultureInfo.InvariantCulture));
        }
    }
}