//  EmmetCallbacks.cs
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

    /// <summary>
    /// Emmet callbacks.
    /// </summary>
    public enum EmmetCallbacks
    {
        /// <summary>
        /// Replace editor's content or it's part.
        /// </summary>
        ReplaceContent,

        /// <summary>
        /// Set new caret position.
        /// </summary>
        SetCaretPos,

        /// <summary>
        /// Create new selection.
        /// </summary>
        CreateSelection
    }
}