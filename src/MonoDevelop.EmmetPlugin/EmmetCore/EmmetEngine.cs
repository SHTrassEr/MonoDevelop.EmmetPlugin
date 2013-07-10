//  EmmetEngine.cs
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

namespace MonoDevelop.EmmetPlugin.EmmetCore
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using MonoDevelop.EmmetPlugin.Callbacks;
    using MonoDevelop.EmmetPlugin.DataContracts;
    using MonoDevelop.EmmetPlugin.Dialogs;
    using MonoDevelop.EmmetPlugin.TypeConverters;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Emmet engine.
    /// </summary>
    public class EmmetEngine
    {
        /// <summary>
        /// The json serializer.
        /// </summary>
        private readonly JsonSerializer jsonSerializer;

        /// <summary>
        /// The nodejs process.
        /// </summary>
        private Process nodeProcess;

        /// <summary>
        /// Determine is process started.
        /// </summary>
        private bool isProcessStarted;

        /// <summary>
        /// The json text writer.
        /// </summary>
        private JsonTextWriter jsonTextWriter;

        /// <summary>
        /// Initializes a new instance of the <see cref="MonoDevelop.EmmetPlugin.EmmetCore.EmmetEngine"/> class.
        /// </summary>
        public EmmetEngine()
        {
            var settings = new JsonSerializerSettings
            {
                Converters = new[] { new EmmetEnumTypeConverter(), new EmmetEnumTypeConverter() }
            };

            this.jsonSerializer = JsonSerializer.CreateDefault(settings);

            this.isProcessStarted = false;
        }

        /// <summary>
        /// Gets the nodejs process. If there is no nodejs process, then new process will be created.
        /// </summary>
        /// <value>The nodejs process.</value>
        private Process NodeProcess
        {
            get
            {
                if (this.isProcessStarted)
                {
                    return this.nodeProcess;
                }

                if (this.nodeProcess != null)
                {
                    this.nodeProcess.Dispose();
                }

                var emmetFullJSPath = this.GetEmmetFullJSPath();
                this.nodeProcess = this.CreateNodeProcess(emmetFullJSPath);
                return this.nodeProcess;
            }
        }

        /// <summary>
        /// Gets the json text writer. if there no writer, then new writer will be created.
        /// </summary>
        /// <value>The json text writer.</value>
        private JsonTextWriter JsonTextWriter
        {
            get
            {
                if (this.isProcessStarted && this.jsonTextWriter != null)
                {
                    return this.jsonTextWriter;
                }

                if (this.jsonTextWriter != null)
                {
                    this.jsonTextWriter.Close();
                }

                this.jsonTextWriter = new JsonTextWriter(this.NodeProcess.StandardInput);
                return this.jsonTextWriter;
            }
        }

        /// <summary>
        /// Perform the specified action.
        /// </summary>
        /// <returns>The callback collection.</returns>
        /// <param name="action">The emmet action.</param>
        public IEnumerable<IEmmetCallback> Exec(EmmetActionDataContract action)
        {
            this.jsonSerializer.Serialize(this.JsonTextWriter, action);
            this.JsonTextWriter.Flush();
            this.NodeProcess.StandardInput.WriteLine();
            this.NodeProcess.StandardInput.Flush();
            var r = this.NodeProcess.StandardOutput.ReadLine();

            if (string.IsNullOrEmpty(r))
            {
                return Enumerable.Empty<IEmmetCallback>();
            }

            return JsonConvert.DeserializeObject<IEnumerable<EmmetCallbackDataContract>>(r).Select(dc => dc.CreateCallback());
        }

        /// <summary>
        /// Gets the emmet full JS path.
        /// </summary>
        /// <returns>The emmet full JS path.</returns>
        private string GetEmmetFullJSPath()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var directory = Directory.GetParent(assembly.Location).FullName;
            var emmetPath = Path.Combine(directory, "js", "emmet-proxy.js");
            return emmetPath;
        }

        /// <summary>
        /// Creates new nodejs process.
        /// </summary>
        /// <returns>The nodejs process.</returns>
        /// <param name="emmetFullJSPath">Emmet full JS path.</param>
        private Process CreateNodeProcess(string emmetFullJSPath)
        {
            this.isProcessStarted = false;
            var nodeProcess = new Process();
            nodeProcess.EnableRaisingEvents = true;
            nodeProcess.Exited += (sender, e) => this.isProcessStarted = false;

            var nodePath = EmmetSettingsPanel.GetNodeJSPath();
            nodeProcess.StartInfo.Arguments = string.Format(emmetFullJSPath);

            nodeProcess.StartInfo.FileName = nodePath;
            nodeProcess.StartInfo.UseShellExecute = false;
            nodeProcess.StartInfo.RedirectStandardOutput = true;
            nodeProcess.StartInfo.RedirectStandardError = false;
            nodeProcess.StartInfo.RedirectStandardInput = true;
            nodeProcess.StartInfo.StandardOutputEncoding = System.Text.Encoding.UTF8;
            this.isProcessStarted = nodeProcess.Start();
            nodeProcess.StandardInput.AutoFlush = true;
            return nodeProcess;
        }
    }
}