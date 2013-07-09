namespace MonoDevelop.EmmetPluginTests.TestsInfo
{
    using System;
    using Mono.TextEditor;
    using MonoDevelop.EmmetPlugin.EmmetCore;

    public class ReflectCssTestInfo : EmmetTestInfo
    {
        #region implemented abstract members of MonoDevelop.EmmetPluginTests.EmmetTestInfo
        protected override string Name
        {
            get { return "ReflectCss.html"; }
        }

        protected override EmmetActions EmmetAction
        {
            get { return EmmetActions.ReflectCssValue; }
        }
        #endregion
    }
}

