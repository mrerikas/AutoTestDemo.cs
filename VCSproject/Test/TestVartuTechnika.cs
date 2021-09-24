using AutoTestProject.Page;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestProject.Test
{
    class TestVartuTechnika
    {
        private static IWebDriver _driver;

        [OneTimeSetUp]
        public static void SetUp()
        {
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl("http://www.vartutechnika.lt/");
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _driver.Manage().Window.Maximize();
            //_driver.FindElement(By.Id("cookiescript_reject")).Click();
            IWebElement cookieScript = _driver.FindElement(By.XPath("//*[@id='cookiescript_close']"));
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => cookieScript.Displayed);
            cookieScript.Click();

        }

        [OneTimeTearDown]
        public static void TearDown()
        {
            _driver.Quit();
        }

        [TestCase("2000", "2000", true, false, "665.98€", TestName = "2000 + 2000 + automatic = 665.98€")]
        [TestCase("4000", "2000", true, true, "1006.43€", TestName = "4000 + 2000 + automatic + work = 1006.43€")]
        [TestCase("4000", "2000", false, false, "692.35€", TestName = "width + height = 692.35€")]
        [TestCase("5000", "2000", false, true, "989.21€", TestName = "5000 + 2000 + work = 989.21€")]
        public static void TestVartuKainas(string width, string height, bool automatic, bool work, string totalPay)
        {
            VartuTechnikaPage page = new VartuTechnikaPage(_driver);
            page.SelectOnlyDoors(width, height)
                .SelectAutomaticDoorsCheckbox(automatic)
                .SelectWithWorkCheckbox(work)
                .ClickCalculateButton()
                .CheckResult(totalPay);
        }

    }
}
