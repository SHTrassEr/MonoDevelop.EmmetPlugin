//  EmmetPrevEditPoint.cs
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
    using MonoDevelop.EmmetPlugin.EmmetCore;

    /// <summary>
    /// This action works for HTML code blocks and allows you to quickly traverse between important code points:
    /// between tags
    /// empty attributes
    /// newlines with indentation
    /// </summary>
    public class EmmetPrevEditPoint : EmmetCommandHandler
    {
        #region implemented abstract members of MonoDevelop.EmmetPlugin.EmmetCommands.EmmetCommandHandler
        /// <summary>
        /// Gets the emmet action.
        /// </summary>
        /// <returns>The emmet action.</returns>
        protected override EmmetActions GetAction()
        {
            return EmmetActions.PrevEditPoint;
        }
        #endregion
    }
}