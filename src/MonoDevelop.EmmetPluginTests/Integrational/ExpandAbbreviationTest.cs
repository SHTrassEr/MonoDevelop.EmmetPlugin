namespace MonoDevelop.EmmetPluginTests.Integrational
{
    using System;
    using NUnit.Framework;
    using MonoDevelop.EmmetPluginTests.TestsInfo;
    using MonoDevelop.EmmetPlugin.EmmetCore;

    [TestFixture()]
    public class ExpandAbbreviationTest
    {
        private EmmetEngine engine;

        [TestFixtureSetUp()]
        public void CreateEngine()
        {
            this.engine = new EmmetEngine();
        }

        [Test()]
        public void HtmlTestInfoCase()
        {
            (new HtmlTestInfo()).RunTest(engine);
        }

        [Test()]
        public void InnerHtmlTestInfoCase()
        {
            (new InnerHtmlTestInfo()).RunTest(engine);
        }
    }
}

