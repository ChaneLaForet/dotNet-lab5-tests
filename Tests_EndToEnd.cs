using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Tests
{
    public class Tests_EndToEnd
    {
        private IWebDriver _driver;

        [SetUp]
        public void SetupDriver()
        {
            _driver = new ChromeDriver("C:\\Users\\HP\\Documents\\chrome-driver");
        }

        [TearDown]
        public void CloseBrowser()
        {
            _driver.Close();
        }

        [Test]
        public void MovieTitleExists()
        {
            _driver.Url = "http://localhost:4200/movies";
            bool foundTitle = false;

            try
            {
                var titles = _driver.FindElements(By.TagName("ion-title"));
                if (titles[1].GetAttribute("textContent") == "Movies")
                    foundTitle = true;
            }
            catch (NoSuchElementException)
            {
                Assert.Fail("Movies title not found.");
            }
            Assert.IsTrue(foundTitle);
        }

        [Test]
        public void MovieTitleExists2()
        {
            _driver.Url = "http://localhost:4200/movies";
            bool foundTitle = false;

            wait(5000);
            
            foreach (var ionLabel in _driver.FindElements(By.TagName("ion-label")))
            {
                if (ionLabel.Text == "The Shawshank Redemption")
                {
                    foundTitle = true;
                    break;
                }
            }
            Assert.IsTrue(foundTitle);
        }

        [Test]
        public void MovieTitleInMovieDetailsExists()
        {
            _driver.Url = "http://localhost:4200/movies";
            bool foundTitle = false;

            wait(3000);

            var arrow = _driver.FindElement(By.XPath("//*[@id='content1']/app-movies/ion-content/ion-list/ion-item[1]/div"));

            arrow.Click();

            wait(1000);

            try
            {
                foreach (var span in _driver.FindElements(By.TagName("span")))
                {
                    if (span.Text == "Title")
                    {
                        foundTitle = true;
                        break;
                    }
                }
            }
            catch (NoSuchElementException)
            {
                Assert.Fail("The Title field was not found.");
            }
            Assert.IsTrue(foundTitle);
        }

        private void wait(int timeout)
        {
            //var timeout = 5000;
            var wait = new WebDriverWait(_driver, TimeSpan.FromMilliseconds(timeout));
            try
            {
                wait.Until(d => false);
            }
            catch (WebDriverTimeoutException wdtex)
            {

            }
        }
    }
}