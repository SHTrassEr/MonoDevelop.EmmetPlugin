//  EmmetWrapWithAbbreviation.cs
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

namespace MonoDevelop.EmmetPlugin.EmmetCommands
{
    using System;
    using MonoDevelop.EmmetPlugin.DataContracts;
    using MonoDevelop.EmmetPlugin.Dialogs;
    using MonoDevelop.EmmetPlugin.EmmetCore;
    using MonoDevelop.Ide;

    /// <summary>
    /// Takes an abbreviation, expands it and places currently selected content in the last element of 
    /// generated snippet. If there’s no selection, action will silently call “Match Tag Pair” to wrap 
    /// context element.
    /// </summary>
    public class EmmetWrapWithAbbreviation : EmmetCommandHandler
    {
        #region implemented abstract members of MonoDevelop.EmmetPlugin.EmmetCommands.EmmetCommandHandler
        /// <summary>
        /// Gets the emmet action.
        /// </summary>
        /// <returns>The emmet action.</returns>
        protected override EmmetActions GetAction()
        {
            return EmmetActions.WrapWithAbbreviation;
        }

        /// <summary>
        /// Reads the abbreviation and set value to the context.
        /// </summary>
        /// <returns><c>true</c>, if abbreviation was readed correctly, <c>false</c> otherwise.</returns>
        /// <param name="editorDataContract">Editor data contract.</param>
        protected override bool FillContext(EmmetEditorDataContract editorDataContract)
        {
            base.FillContext(editorDataContract);

            using (var customDialog = new WrapWithAbbreviationDialog())
            {
                if ((Gtk.ResponseType)MessageService.ShowCustomDialog(customDialog) == Gtk.ResponseType.Ok)
                {
                    var abbreviation = customDialog.GetAbbreviation();
                    if (string.IsNullOrEmpty(abbreviation))
                    {
                        abbreviation = "div";
                    }

                    editorDataContract.Prompts.Add(abbreviation);
                    return true;
                }

                return false;
            }
        }
        #endregion
    }
}