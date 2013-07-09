//  EmmetSetCaretPosCallback.cs
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
    /// Set new caret position.
    /// </summary>
    public class EmmetSetCaretPosCallback : IEmmetCallback
    {
        /// <summary>
        /// Offset of new caret position.
        /// </summary>
        private readonly int position;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonoDevelop.EmmetPlugin.Callbacks.EmmetSetCaretPosCallback"/> class.
        /// </summary>
        /// <param name="data">JSON data.</param>
        public EmmetSetCaretPosCallback(JObject data)
        {
            this.position = (int)data["pos"];
        }

        #region IEmmetCallback implementation
        /// <summary>
        /// Sets a editor's caret to a new position.
        /// </summary>
        /// <param name="textEditorData">Text editor data.</param>
        public void Exec(TextEditorData textEditorData)
        {
            var location = textEditorData.OffsetToLocation(this.position);
            textEditorData.SetCaretTo(location.Line, location.Column);
        }
        #endregion
    }
}