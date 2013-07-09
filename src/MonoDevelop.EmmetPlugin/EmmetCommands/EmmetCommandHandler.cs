//  EmmetCommandHandler.cs
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
    using Mono.TextEditor;  
    using MonoDevelop.Components.Commands;
    using MonoDevelop.EmmetPlugin.DataContracts;
    using MonoDevelop.EmmetPlugin.EmmetCore;
    using MonoDevelop.Ide; 
    using MonoDevelop.Ide.Gui; 
    using MonoDevelop.Ide.Gui.Content;  

    /// <summary>
    /// Monodevelop command handler.
    /// </summary>
    public abstract class EmmetCommandHandler : CommandHandler
    {
        /// <summary>
        /// The emmet engine.
        /// </summary>
        private EmmetEngine engine;

        /// <summary>
        /// Gets the emmet engine. If there is no emmet engine, new one will be created.
        /// </summary>
        /// <value>The engine.</value>
        private EmmetEngine Engine
        {
            get { return this.engine ?? (this.engine = new EmmetEngine()); }
        }

        /// <summary>
        /// Update menu item.
        /// </summary>
        /// <param name="info">Command info.</param>
        protected override void Update(CommandInfo info)  
        {  
            MonoDevelop.Ide.Gui.Document doc = IdeApp.Workbench. ActiveDocument;  
            info.Enabled = doc != null && doc.GetContent<ITextEditorDataProvider>() != null;  
        }  

        /// <summary>
        /// Run this command.
        /// </summary>
        protected override void Run()
        {
            lock (this.Engine)
            {
                MonoDevelop.Ide.Gui.Document doc = IdeApp.Workbench.ActiveDocument;  
                var textEditorData = doc.GetContent<ITextEditorDataProvider>().GetTextEditorData();  
                var action = new EmmetActionDataContract() 
                {
                    Action = this.GetAction(),
                    Context = EmmetEditorDataContract.Create(textEditorData)
                };
    
                if (this.FillContext(action.Context))
                {
                    var callbacks = this.Engine.Exec(action);
                    foreach (var c in callbacks)
                    {
                        c.Exec(doc.Editor);
                    }
                }

                base.Run();
            }
        }

        /// <summary>
        /// Fills the emmet editor data contract.
        /// </summary>
        /// <returns><c>true</c>, if context was filled correctly, <c>false</c> otherwise.</returns>
        /// <param name="editorDataContract">Editor data contract.</param>
        protected virtual bool FillContext(EmmetEditorDataContract editorDataContract)
        {
            return true;
        }

        /// <summary>
        /// Gets the emmet action.
        /// </summary>
        /// <returns>The emmet action.</returns>
        protected abstract EmmetActions GetAction();
    }
}