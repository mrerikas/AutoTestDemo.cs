using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace AutoTestProject
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
            Console.WriteLine("Test finished..");
        }

        [TestCase("2000", "2000", true, false, "665.98€", TestName = "2000 + 2000 + automatic = 665.98€")]
        [TestCase("4000", "2000", true, true, "1006.43€", TestName = "4000 + 2000 + automatic + work = 1006.43€")]
        [TestCase("4000", "2000", false, false, "692.35€", TestName = "width + height = 692.35€")]
        [TestCase("5000", "2000", false, true, "989.21€", TestName = "5000 + 2000 + work = 989.21€")]
        public static void TestVartuKainas(string width, string height, bool automatic, bool work, string totalPay) 
        {
            IWebElement doorsWidth = _driver.FindElement(By.Id("doors_width"));
            IWebElement doorsHeight = _driver.FindElement(By.Id("doors_height"));
            doorsWidth.Clear();
            doorsWidth.SendKeys(width);
            doorsHeight.Clear();
            doorsHeight.SendKeys(height);
            IWebElement autoCheckBox = _driver.FindElement(By.Id("automatika"));
            if (automatic != autoCheckBox.Selected)
                autoCheckBox.Click();
            IWebElement workCheckBox = _driver.FindElement(By.Id("darbai"));
            if (work != workCheckBox.Selected)
                workCheckBox.Click();
            _driver.FindElement(By.Id("calc_submit")).Click();
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.CssSelector("#calc_result > div")).Displayed);
            IWebElement resultText = _driver.FindElement(By.CssSelector("#calc_result > div"));
            Assert.IsTrue(resultText.Text.Contains(totalPay), $"Result is not as expected {totalPay}, but was {resultText.Text}");
        }
    }
}
