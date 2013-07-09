namespace MonoDevelop.EmmetPluginTests.TestsInfo
{
    using System;
    using System.IO;
    using System.Reflection;
    using Mono.TextEditor;
    using MonoDevelop.EmmetPlugin.EmmetCore;
    using MonoDevelop.EmmetPlugin.DataContracts;
    using NUnit.Framework;

    public abstract class EmmetTestInfo
    {
        private const string InputPrefix = "TestInput";
        private const string OutputPrefix = "TestOutput";
        private const string TestDataDirectory = "TestsData";
        private const string CaretStartLabel = "${0}";

        protected string InputFileName
        {
            get { return string.Concat(InputPrefix, Name); }
        }

        protected string OutputFileName
        {
            get { return string.Concat(OutputPrefix, Name); }
        }

        protected string ReadFile(string fileName, string indentationString, bool tabToSpaces)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var directory = Directory.GetParent(assembly.Location).FullName;
            var filePath = Path.Combine(directory, TestDataDirectory, fileName);
            var text = File.ReadAllText(filePath); 
            if (tabToSpaces)
            {
                text = text.Replace("\t", indentationString);
            }

            return text;
        }

        public TextEditorData GetTextEditor(int tabSize, bool tabToSpaces)
        {
            var editor = new TextEditorData();
            editor.Document.FileName = InputFileName;
            editor.Options.TabSize = tabSize;
            editor.Options.TabsToSpaces = tabToSpaces;
            var text = ReadFile(InputFileName, editor.Options.IndentationString, tabToSpaces); 
            var startOffset = text.IndexOf(CaretStartLabel);
            text = text.Remove(startOffset, CaretStartLabel.Length);
            editor.Text = text;
            var startCaretPos = editor.OffsetToLocation(startOffset);
            editor.SetCaretTo(startCaretPos.Line, startCaretPos.Column);
            return editor;
        }

        public void RunTest(EmmetEngine engine)
        {
            RunTest(engine, EmmetAction, 1, false);
            RunTest(engine, EmmetAction, 2, true);
            RunTest(engine, EmmetAction, 3, true);
            RunTest(engine, EmmetAction, 4, true);
            RunTest(engine, EmmetAction, 8, true);
        }

        private void RunTest(EmmetEngine engine, EmmetActions action, int tabSize, bool tabToSpaces)
        {
            var editor = this.GetTextEditor(tabSize, tabToSpaces);
            var actionDC = new EmmetActionDataContract()
            {
                Action = action,
                Context = EmmetEditorDataContract.Create(editor)
            };

            var callbacks = engine.Exec(actionDC);
            foreach (var c in callbacks)
            {
                c.Exec(editor);
            }

            var expected = ReadFile(OutputFileName, editor.Options.IndentationString, tabToSpaces);
            Assert.AreEqual(expected, editor.Text);
        }

        protected abstract string Name { get; }

        protected abstract EmmetActions EmmetAction { get; }

    }
}
