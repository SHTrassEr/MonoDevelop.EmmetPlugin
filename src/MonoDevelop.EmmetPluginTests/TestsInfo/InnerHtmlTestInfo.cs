namespace MonoDevelop.EmmetPluginTests.TestsInfo
{
    using System;
    using Mono.TextEditor;
    using MonoDevelop.EmmetPlugin.EmmetCore;

    public class InnerHtmlTestInfo : EmmetTestInfo
    {
        #region implemented abstract members of MonoDevelop.EmmetPluginTests.EmmetTestInfo
        protected override string Name
        {
            get { return "InnerHtml.html"; }
        }

        protected override EmmetActions EmmetAction
        {
            get { return EmmetActions.ExpandAbbreviation; }
        }
        #endregion
    }
}

