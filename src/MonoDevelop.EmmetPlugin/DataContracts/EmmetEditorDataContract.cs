//  EmmetEditorDataContract.cs
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
    using System.Collections.Generic;
    using Mono.TextEditor;
    using Newtonsoft.Json;

    /// <summary>
    /// Emmet editor data contract.
    /// </summary>
    public class EmmetEditorDataContract
    {
        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>The file path.</value>
        [JsonProperty("filePath")]
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the caret offset position.
        /// </summary>
        /// <value>The caret offset position.</value>
        [JsonProperty("caretPos")]
        public int CaretPos { get; set; }

        /// <summary>
        /// Gets or sets the current line.
        /// </summary>
        /// <value>The current line.</value>
        [JsonProperty("currentLine")]
        public string CurrentLine { get; set; }

        /// <summary>
        /// Gets or sets the prompts.
        /// </summary>
        /// <value>The prompts.</value>
        [JsonProperty("prompts")]
        public List<string> Prompts { get; set; }

        /// <summary>
        /// Gets or sets the selection range.
        /// </summary>
        /// <value>The selection range.</value>
        [JsonProperty("selectionRange")]
        public EmmetRangeDataContract SelectionRange { get; set; }

        /// <summary>
        /// Gets or sets the current line range.
        /// </summary>
        /// <value>The current line range.</value>
        [JsonProperty("currentLineRange")]
        public EmmetRangeDataContract CurrentLineRange { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        [JsonProperty("content")]
        public string Content { get; set; }

        /// <summary>
        /// Create the EmmetEditorDataContract based on textEditorData.
        /// </summary>
        /// <returns>Emmet editor data contract</returns>
        /// <param name="textEditorData">Text editor data.</param>
        public static EmmetEditorDataContract Create(TextEditorData textEditorData)
        {
            var caretPos = textEditorData.LocationToOffset(textEditorData.Caret.Location);
            var startLinePos = textEditorData.LocationToOffset(new DocumentLocation(textEditorData.Caret.Location.Line, 0));
            EmmetRangeDataContract selectionRange;
            
            if (textEditorData.IsSomethingSelected)
            {
                var startSelection = textEditorData.MainSelection.GetAnchorOffset(textEditorData);
                var endSelection = textEditorData.MainSelection.GetLeadOffset(textEditorData);
                selectionRange = new EmmetRangeDataContract() 
                {
                    Start = (startSelection < endSelection) ? startSelection : endSelection,
                    End = (startSelection > endSelection) ? startSelection : endSelection
                };
            }
            else
            {
                selectionRange = new EmmetRangeDataContract() 
                {
                    Start = caretPos,
                    End = caretPos
                };
            }

            return new EmmetEditorDataContract() 
            {
                FilePath = textEditorData.Document.FileName,
                Content = textEditorData.Text,
                CaretPos = caretPos,
                Prompts = new List<string>(1),
                CurrentLine = textEditorData.GetLineText(textEditorData.Caret.Line),
                SelectionRange = selectionRange,
                CurrentLineRange = new EmmetRangeDataContract() 
                {
                    Start = startLinePos,
                    End = caretPos 
                }
            };
        }
    }
}