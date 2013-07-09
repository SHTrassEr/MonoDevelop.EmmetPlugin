//  EmmetCreateSelectionCallback.cs
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

namespace MonoDevelop.EmmetPlugin.Callbacks
{
    using System;
    using Mono.TextEditor;
    using MonoDevelop.Ide.Gui;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Creates selection from <code>start</code> to <code>end</code> character indexes.
    /// </summary>
    public class EmmetCreateSelectionCallback : IEmmetCallback
    {
        /// <summary>
        /// Start selection index.
        /// </summary>
        private readonly int start;

        /// <summary>
        /// End selection index.
        /// </summary>
        private readonly int end;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="MonoDevelop.EmmetPlugin.Callbacks.EmmetCreateSelectionCallback"/> class.
        /// </summary>
        /// <param name="data">JSON data.</param>
        public EmmetCreateSelectionCallback(JObject data)
        {
            this.start = (int)data["start"];
            this.end = (int)data["end"];
        }

        #region IEmmetCallback implementation
        /// <summary>
        /// Create selection in document.
        /// </summary>
        /// <param name="textEditorData">Text editor data.</param>
        public void Exec(TextEditorData textEditorData)
        {
            textEditorData.ClearSelection();
            textEditorData.SetSelection(this.start, this.end);
        }
        #endregion
    }
}