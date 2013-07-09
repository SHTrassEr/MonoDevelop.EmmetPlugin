//  EmmetRangeDataContract.cs
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

namespace MonoDevelop.EmmetPlugin.DataContracts
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Emmet range data contract.
    /// </summary>
    public class EmmetRangeDataContract
    {
        /// <summary>
        /// Gets or sets the start of the range.
        /// </summary>
        /// <value>The start.</value>
        [JsonProperty("start")]
        public int Start { get; set; }

        /// <summary>
        /// Gets or sets the end of the range.
        /// </summary>
        /// <value>The end.</value>
        [JsonProperty("end")]
        public int End { get; set; }
    }
}