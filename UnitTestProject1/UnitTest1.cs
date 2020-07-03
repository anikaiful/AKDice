using Microsoft.VisualStudio.TestTools.UnitTesting;
using Anikaiful.Dice;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string s = "Lets see 5+5 is 10".Evaluate();
            Assert.AreEqual("Lets see 10 is 10", s);
        }

        [TestMethod]
        public void TestMethod2()
        {
            int s = "5+5".Evaluate<int>();
            Assert.AreEqual(10, s);
        }
    }
}
