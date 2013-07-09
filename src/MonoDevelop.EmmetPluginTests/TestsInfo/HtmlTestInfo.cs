namespace MonoDevelop.EmmetPluginTests.TestsInfo
{
    using System;
    using Mono.TextEditor;
    using MonoDevelop.EmmetPlugin.EmmetCore;

    public class HtmlTestInfo : EmmetTestInfo
    {
        #region implemented abstract members of MonoDevelop.EmmetPluginTests.EmmetTestInfo
        protected override string Name
        {
            get { return "Html.html"; }
        }

        protected override EmmetActions EmmetAction
        {
            get { return EmmetActions.ExpandAbbreviation; }
        }
        #endregion
    }
}

