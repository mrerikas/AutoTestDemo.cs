using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestProject
{
    class TestSeleniumWebpage
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
        public static void TestSeleniumPage() 
        {
            _driver.FindElement(By.Id("user-message")).SendKeys("sss");
            _driver.FindElement(By.ClassName("btn-default")).Click();
            IWebElement outputText = _driver.FindElement(By.CssSelector("#display"));
            Assert.AreEqual("sss", outputText.Text, "Text is different");
        }

        [TestCase("2", "2", "4", TestName = "2 plus 2 = 4")]
        [TestCase("-5", "3", "-2", TestName = "-5 plus 3 = -2")]
        [TestCase("a", "b", "NaN", TestName = "a plus b = NaN")]
        public static void TestSum(string firstNum, string secondNum, string result) 
        {
            IWebElement firstInput = _driver.FindElement(By.Id("sum1"));
            IWebElement secondInput = _driver.FindElement(By.Id("sum2"));
            firstInput.Clear();
            firstInput.SendKeys(firstNum);
            secondInput.Clear();
            secondInput.SendKeys(secondNum);
            _driver.FindElement(By.CssSelector("#gettotal > button")).Click();
            IWebElement resultFromPage = _driver.FindElement(By.CssSelector("#displayvalue"));
            Assert.AreEqual(result, resultFromPage.Text, "wrong result");
        }
    }
}

