using ApiPays.Models;

namespace TestsPays
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestCountryCode()
        {
            Pays country = new Pays();
            country.CodePays = "FR";
            Assert.AreEqual("FR", country.CodePays);
        }
    }
}