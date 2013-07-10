//  ConverterTools.cs
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Converter tools.
    /// </summary>
    public static class ConverterTools
    {
        /// <summary>
        /// The snakeCase separator.
        /// </summary>
        private const string Separator = "_";

        /// <summary>
        /// Converts input string from CamelCase to snake_case format.
        /// </summary>
        /// <returns>String in snake_case format.</returns>
        /// <param name="s">The input string in CamelCase format.</param>
        public static string ToSnakeCase(string s)
        {
            var parts = new List<string>();
            var currentWord = new StringBuilder();
 
            foreach (var c in s)
            {
                if (char.IsUpper(c) && currentWord.Length > 0)
                {
                    parts.Add(currentWord.ToString());
                    currentWord.Length = 0;
                }

                currentWord.Append(char.ToLower(c));
            }
 
            if (currentWord.Length > 0)
            {
                parts.Add(currentWord.ToString());
            }
 
            return string.Join(Separator, parts.ToArray());
        }

        /// <summary>
        /// Converts input string from snake_case to CamelCase.
        /// </summary>
        /// <returns>String in CamelCase format.</returns>
        /// <param name="s">Input string in snake_case format.</param>
        public static string FromSnakeCase(string s)
        {
            var parts = s.Split(Separator[0]).Select(p => System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(p)).ToArray();
            return string.Join(string.Empty, parts);
        }
    }
}