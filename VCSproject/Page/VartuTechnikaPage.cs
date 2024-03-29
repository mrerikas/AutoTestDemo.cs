﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestProject.Page
{
    public class VartuTechnikaPage : BasePage
    {
        private IWebElement _doorsWidth => Driver.FindElement(By.Id("doors_width"));
        private IWebElement _doorsHeight => Driver.FindElement(By.Id("doors_height"));
        private IWebElement _automaticDoorsCheckBox => Driver.FindElement(By.Id("automatika"));
        private IWebElement _withWorkCheckBox => Driver.FindElement(By.Id("darbai"));
        private IWebElement _calculateButton => Driver.FindElement(By.Id("calc_submit"));
        private IWebElement _resultText => Driver.FindElement(By.CssSelector("#calc_result > div"));

        public VartuTechnikaPage(IWebDriver webdriver) : base(webdriver) { }

        public VartuTechnikaPage InsertDoorsWidth(string doorsWidth)
        {
            _doorsWidth.Clear();
            _doorsWidth.SendKeys(doorsWidth);
            return this;
        }

        public VartuTechnikaPage InsertDoorsHeight(string doorsHeight)
        {
            _doorsHeight.Clear();
            _doorsHeight.SendKeys(doorsHeight);
            return this;
        }

        public VartuTechnikaPage SelectOnlyDoors(string width, string height)
        {
            InsertDoorsWidth(width);
            InsertDoorsHeight(height);
            return this;
        }

        public VartuTechnikaPage SelectAutomaticDoorsCheckbox(bool automatic)
        {
            if (automatic != _automaticDoorsCheckBox.Selected)
                _automaticDoorsCheckBox.Click();
            return this;
        }

        public VartuTechnikaPage SelectWithWorkCheckbox(bool work)
        {
            if (work != _withWorkCheckBox.Selected)
                _withWorkCheckBox.Click();
            return this;
        }

        public VartuTechnikaPage ClickCalculateButton()
        {
            _calculateButton.Click();
            return this;
        }

        public void CheckResult(string expectedResult)
        {
            WaitForResult();
            Assert.IsTrue(_resultText.Text.Contains(expectedResult), $"Result is not as expected {expectedResult}, but was {_resultText.Text}");
        }

        private VartuTechnikaPage WaitForResult()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(d => _resultText.Displayed);
            return this;
        }
    }
}
