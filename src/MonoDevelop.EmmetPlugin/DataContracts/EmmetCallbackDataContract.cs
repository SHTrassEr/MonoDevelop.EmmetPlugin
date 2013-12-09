//  EmmetCallbackDataContract.cs
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
    using MonoDevelop.EmmetPlugin.Callbacks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Emmet callback data contract.
    /// </summary>
    public class EmmetCallbackDataContract
    { 
        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>The action.</value>
        [JsonProperty("action")]
        public EmmetCallbacks Action { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        [JsonProperty("data")]
        public JObject Data { get; set; }

        /// <summary>
        /// Creates the callback.
        /// </summary>
        /// <returns>The callback.</returns>
        public IEmmetCallback CreateCallback()
        {
            switch (this.Action)
            {
                case EmmetCallbacks.ReplaceContent:
                    return new EmmetReplaceContentCallback(this.Data);
                case EmmetCallbacks.SetCaretPos:
                    return new EmmetSetCaretPosCallback(this.Data);
                case EmmetCallbacks.CreateSelection:
                    return new EmmetCreateSelectionCallback(this.Data);
            }

            throw new InvalidOperationException(string.Format("Unknown action: '{0}'", this.Action.ToString()));
        }
    }
}