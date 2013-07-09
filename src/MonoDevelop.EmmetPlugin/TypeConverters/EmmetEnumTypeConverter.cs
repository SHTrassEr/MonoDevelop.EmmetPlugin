//  EmmetEnumTypeConverter.cs
//
//  Author:
//       SHTrassEr <shtrasser@gmail.com>
//
//  Copyright (c) 2013 SHTrassEr
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace MonoDevelop.EmmetPlugin.TypeConverters
{
    using System;
    using System.Linq;
    using MonoDevelop.EmmetPlugin.Callbacks;
    using MonoDevelop.EmmetPlugin.EmmetCore;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Custom enum type converter. Convert enum values to string in snake_case format.
    /// </summary>
    public class EmmetEnumTypeConverter : JsonConverter
    {
        /// <summary>
        /// Determines whether this instance can convert the specified objectType.
        /// </summary>
        /// <returns><c>true</c> if this instance can convert the specified objectType; otherwise, <c>false</c>.</returns>
        /// <param name="objectType">Object type.</param>
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(EmmetActions)) ||
                (objectType == typeof(EmmetCallbacks));
        }

        /// <summary>
        /// Convert enum values to string in snake_case format.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var v = value.ToString();
            writer.WriteValue(ConverterTools.ToSnakeCase(v));
        }

        /// <summary>
        /// Convert string in snake_case format to enum values.
        /// </summary>
        /// <returns>The json.</returns>
        /// <param name="reader">The reader.</param>
        /// <param name="objectType">Object type.</param>
        /// <param name="existingValue">Existing value.</param>
        /// <param name="serializer">The serializer.</param>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var v = ConverterTools.FromSnakeCase((string)existingValue);
            return Enum.GetValues(objectType).Cast<object>().First(a => a.ToString().Equals(v));
        }
    }
}