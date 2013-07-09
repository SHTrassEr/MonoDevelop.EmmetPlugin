//  EmmetReplaceContentCallback.cs
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
    using System.Collections.Generic;
    using System.Text;
    using Mono.TextEditor;
    using MonoDevelop.Ide.CodeTemplates;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Replace editor's content or it's part from <code>start</code> to <code>end</code> character indexes. 
    /// If <code>value</code> contains <code>caret_placeholder</code>, the editor will put caret into this position.
    /// </summary>
    public class EmmetReplaceContentCallback : IEmmetCallback
    {
        /// <summary>
        /// Content you want to paste.
        /// </summary>
        private readonly string value;

        /// <summary>
        /// Start index of editor's content.
        /// </summary>
        private readonly int start;

        /// <summary>
        /// End index of editor's content.
        /// </summary>
        private readonly int end;

        /// <summary>
        /// Do not auto indent <code>value</code>.
        /// </summary>
        private readonly bool noIndent;

        /// <summary>
        /// The tab stops.
        /// </summary>
        private List<int> tabStops;

        /// <summary>
        /// The text editor data.
        /// </summary>
        private TextEditorData textEditorData;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="MonoDevelop.EmmetPlugin.Callbacks.EmmetReplaceContentCallback"/> class.
        /// </summary>
        /// <param name="data">JSON data.</param>
        public EmmetReplaceContentCallback(JObject data)
        {
            this.value = (string)data["value"];
            this.start = (int)data["start"];
            this.end = (int)data["end"];
            this.noIndent = (data["no_indent"] == null) ? false : (bool)data["no_indent"];
            this.tabStops = new List<int>();
        }

        #region IEmmetCallback implementation

        /// <summary>
        /// Replace editor's content or it's part from <code>start</code> to <code>end</code> character indexes. 
        /// If <code>value</code> contains <code>caret_placeholder</code>, the editor will put caret into this position.
        /// </summary>
        /// <param name="textEditorData">Text editor data.</param>
        public void Exec(TextEditorData textEditorData)
        {
            var value = this.PrepareValue(textEditorData);
            this.textEditorData = textEditorData;
            this.textEditorData.Replace(this.start, this.end - this.start, value);
            if (this.tabStops.Count > 0)
            {
                var newCaretPos = this.textEditorData.OffsetToLocation(this.tabStops[0]);
                this.textEditorData.SetCaretTo(newCaretPos.Line, newCaretPos.Column);
            }
        }
        #endregion

        /// <summary>
        /// Prepares the value: set indentations, remove tabstop symbols.
        /// </summary>
        /// <returns>Prepared value.</returns>
        /// <param name="textEditorData">Text editor data.</param>
        private string PrepareValue(TextEditorData textEditorData)
        {
            var indentationString = textEditorData.Options.IndentationString;
            var startLine = textEditorData.GetLineByOffset(this.start);
            var lineIndent = textEditorData.Document.GetLineIndent(startLine);

            var lines = this.value.Split('\n');
            var sb = new StringBuilder(this.PrepareLine(lines[0], indentationString, this.start));
            for (var l = 1; l < lines.Length; l++)
            {
                var preparedLine = this.PrepareLine(lines[l], indentationString, this.start + sb.Length);
                sb.Append(Environment.NewLine);
                if (!this.noIndent)
                {
                    sb.Append(lineIndent);
                }

                sb.Append(preparedLine);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Prepares the line: set indentations, remove tabstop symbols.
        /// </summary>
        /// <returns>Prepared line string.</returns>
        /// <param name="line">The line to be prepared.</param>
        /// <param name="indentationString">Indentation string.</param>
        /// <param name="offset">Editor's offset.</param>
        private string PrepareLine(string line, string indentationString, int offset)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < line.Length; i++)
            {
                var c = line[i];
                switch (c)
                {
                    case '\t':
                        sb.Append(indentationString);
                        continue;
                    case '\\':
                        if (i < line.Length - 1)
                        {
                            i++;
                            sb.Append(line[i]);
                        }
                        else
                        {
                            sb.Append(c);
                        }

                        continue;
                    case '$':
                        var closeIndex = line.IndexOf('}', i);
                        if (closeIndex > 0)
                        {
                            this.tabStops.Add(offset + sb.Length);
                            i = closeIndex;
                        }
                        else
                        {
                            sb.Append(c);
                        }

                        continue;
                    default:
                        sb.Append(c);
                        continue;
                }
            }

            return sb.ToString();
        }

        /*
        private void OnTextReplacing(object sender, DocumentChangeEventArgs e)
        {
            if (e.InsertedText == "\t")
            {
                throw(new InvalidOperationException("dd"));
            }
            else if (e.InsertedText.Contains("\n") || e.InsertedText.Contains("\r"))
            {
                this.textEditorData.Document.TextReplacing -= OnTextReplacing;
            }
        }
        */
    }
}