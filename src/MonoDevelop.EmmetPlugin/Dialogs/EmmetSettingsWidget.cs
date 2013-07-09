//  EmmetSettingsWidget.cs
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

namespace MonoDevelop.EmmetPlugin.Dialogs
{
    using System;

    /// <summary>
    /// Emmet settings widget.
    /// </summary>
    [System.ComponentModel.ToolboxItem(true)]
    public partial class EmmetSettingsWidget : Gtk.Bin
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MonoDevelop.EmmetPlugin.Dialogs.EmmetSettingsWidget"/> class.
        /// </summary>
        public EmmetSettingsWidget()
        {
            this.Build();
            this.nodeJSPathEntry.Text = EmmetSettingsPanel.GetNodeJSPath();
        }

        /// <summary>
        /// Gets the node JS path.
        /// </summary>
        /// <returns>The node JS path.</returns>
        public string GetNodeJSPath()
        {
            return this.nodeJSPathEntry.Text; 
        }
    }
}