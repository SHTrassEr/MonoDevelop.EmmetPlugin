//  EmmetSettingsPanel.cs
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
    using MonoDevelop.Core;
    using MonoDevelop.Ide.Gui.Dialogs;

    /// <summary>
    /// Emmet settings panel.
    /// </summary>
    public class EmmetSettingsPanel : OptionsPanel
    {
        /// <summary>
        /// The name of the node JS path property.
        /// </summary>
        private const string NodeJSPathPropName = "Monodevelop.EmmetPlugin.NodeJSPath";

        /// <summary>
        /// The settings widget.
        /// </summary>
        private EmmetSettingsWidget settingsWidget;

        /// <summary>
        /// Gets the settings widget. If there is no setting widget, the new one will be created.
        /// </summary>
        /// <value>The settings widget.</value>
        private EmmetSettingsWidget SettingsWidget
        {
            get { return this.settingsWidget ?? (this.settingsWidget = new EmmetSettingsWidget()); }
        }

        /// <summary>
        /// Gets the node JS path.
        /// </summary>
        /// <returns>The node JS path.</returns>
        public static string GetNodeJSPath()
        {
            return PropertyService.Get<string>(NodeJSPathPropName, "node");
        }

        #region implemented abstract members of OptionsPanel
        /// <summary>
        /// Creates the panel widget.
        /// </summary>
        /// <returns>The panel widget.</returns>
        public override Gtk.Widget CreatePanelWidget()
        {
            return this.SettingsWidget;
        }

        /// <summary>
        /// Applies the settings changes.
        /// </summary>
        public override void ApplyChanges()
        {
            PropertyService.Set(NodeJSPathPropName, this.SettingsWidget.GetNodeJSPath());
        }
        #endregion
    }
}