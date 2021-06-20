using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium;
namespace AppiumWithV8
{
    public class Tests
    {
        public static AppiumDriver<AndroidElement> _driver;
        public static WebDriverWait wait;

        [SetUp]
        public void Setup()
        {

            AppiumOptions driverOption = new AppiumOptions();

            string appPath = @"G:\app-debugv8-1.apk";
            driverOption.AddAdditionalCapability(MobileCapabilityType.NoReset, false);
            driverOption.AddAdditionalCapability(MobileCapabilityType.FullReset, false);
            driverOption.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            driverOption.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Redmi 8A");
            driverOption.AddAdditionalCapability(MobileCapabilityType.App, appPath);
            driverOption.AddAdditionalCapability("autoAcceptAlerts", true);
            driverOption.AddAdditionalCapability("chromedriverExecutable", @"C:\chromedriver_win32\chromedriver.exe");
            driverOption.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, "com.moneyfellows.mobileapp");
            driverOption.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, "MainActivity");
            driverOption.AddAdditionalCapability(MobileCapabilityType.AutoWebview, true);
            //time Span to increase waiting time due to time -out in old devices
            _driver = new AndroidDriver<AndroidElement>(new Uri("http://localhost:4723/wd/hub"), driverOption, TimeSpan.FromSeconds(200000));
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2000);
            var contexts = ((IContextAware)_driver).Contexts;
            string webviewContext = null;
            for (var i = 0; i < contexts.Count; i++)
            {
                Console.WriteLine(contexts[i]);
                if (contexts[i].Contains("WEBVIEW"))
                {
                    webviewContext = contexts[i];
                    break;
                }

            }
              ((IContextAware)_driver).Context = webviewContext;

            wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
        }


        [Test]
        public void Test1()
        {

            wait.Until(c => c.FindElement(By.XPath("//*[@id='slideBtn_1']"))).Click();
            wait.Until(c => c.FindElement(By.XPath("//*[@id='slideBtn_2']"))).Click();
            wait.Until(c => c.FindElement(By.XPath("//*[@id='slideBtn_3']"))).Click();

            Assert.Pass();
        }
    }
}
