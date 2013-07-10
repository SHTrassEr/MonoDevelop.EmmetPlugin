namespace MonoDevelop.EmmetPluginTests.Integrational
{
    using System;
    using NUnit.Framework;
    using MonoDevelop.EmmetPluginTests.TestsInfo;
    using MonoDevelop.EmmetPlugin.EmmetCore;

    [TestFixture()]
    public class ReflectCssValueTest 
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
            (new ReflectCssTestInfo()).RunTest(engine);
        }
    }
}

