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
    public class TestSeleniumInputPage
    {
        private static IWebDriver _driver;

        [OneTimeSetUp]
        public static void SetUp()
        {
            _driver = new ChromeDriver();
            _driver.Url = "https://www.seleniumeasy.com/test/basic-first-form-demo.html";
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            IWebElement popUp = _driver.FindElement(By.CssSelector("#at-cv-lightbox-close"));
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => popUp.Displayed);
            popUp.Click();
        }

        [OneTimeTearDown]
        public static void TearDown()
        {
            _driver.Quit();
        }

        [Test]
        public void TestSeleniumFirstBlock()
        {
            SeleniumInputPage page = new SeleniumInputPage(_driver);
            string myText = "Karolina";

            page.InsertText(myText);
            page.ClickShowButtonMessage();
            page.CheckResult(myText);
        }

        [TestCase("2", "2", "4", TestName = "2 plus 2 = 4")]
        [TestCase("-5", "3", "-2", TestName = "-5 plus 3 = -2")]
        [TestCase("a", "b", "NaN", TestName = "a plus b = NaN")]
        public void TestSeleniumSecondBlock(string firstInput, string secondINput, string result)
        {
            SeleniumInputPage page = new SeleniumInputPage(_driver);
            page.InsertBothInputs(firstInput, secondINput);
            page.ClickGetTotalButton();
            page.CheckSumResult(result);
        }

    }
}
