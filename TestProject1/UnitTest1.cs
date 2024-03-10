using AngleSharp.Dom;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WebDriverManager.DriverConfigs.Impl;

namespace TestProject1
{
    public class Tests
    {
        private IWebDriver webdriver;

        [SetUp]
        public void Setup()
        {
            InitializeWebDriver();
        }

        private void InitializeWebDriver()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());

            ChromeOptions options = new ChromeOptions();
            options.AcceptInsecureCertificates = true;
            options.AddExcludedArgument("enable-automation");
            options.AddArgument("disable-extensions");
            options.AddArgument("disable-infobars");
            options.AddArgument("disable-notifications");
            options.AddArgument("disable-web-security");
            options.AddArgument("disable-prefetch-disable");
            options.AddArgument("dns-prefetch-disable");
            options.AddArgument("disable-browser-side-navigation");
            options.AddArgument("disable-gpu");
            options.AddArgument("always-authorize-plugins");
            options.AddArgument("load-extension=src/main/resources/chrome_load_stopper");
            options.AddArgument("--start-maximized");

            webdriver = new ChromeDriver(options);
        }

        [TearDown]
        public void Teardown()
        {
           // webdriver.Quit();
        }

        [Test]
        public void Test1()
        {
            // webdriver.Navigate().GoToUrl("https://www.amazon.in");
            // Thread.Sleep(3000);
            // webdriver.FindElement(By.Id("twotabsearchtextbox")).SendKeys("T-Shirts");
            // webdriver.FindElement(By.Name("field-keywords")).SendKeys("T-Shirts");
            // webdriver.FindElement(By.CssSelector("#twotabsearchtextbox")).SendKeys("T-Shirts");
            // webdriver.FindElement(By.CssSelector("input[type='text']")).SendKeys("T-Shirts");
            // webdriver.FindElement(By.CssSelector(".nav-input")).SendKeys("T-Shirts");
            // webdriver.FindElement(By.LinkText("Best Sellers")).Click();
            // webdriver.FindElement(By.PartialLinkText("Sellers"));
            // webdriver.FindElement(By.XPath("//input[@id='twotabsearchtextbox']")).SendKeys("T-Shirts");
            // IList<IWebElement> totalElements = webdriver.FindElements(By.TagName("div"));

            webdriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            webdriver.Navigate().GoToUrl("https://www.google.com");

            WebDriverWait wait = new(webdriver, TimeSpan.FromSeconds(5));
            IWebElement cookiePopup = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("CXQnmb")));
            cookiePopup.FindElement(By.Id("L2AGLb")).Click();

            webdriver.FindElement(By.XPath("//textarea[@name='q']")).SendKeys("Selenium");
            webdriver.FindElement(By.XPath("//textarea[@name='q']")).SendKeys(Keys.Enter);
            webdriver.FindElement(By.XPath("//a[@href='https://www.selenium.dev/']")).Click();
        }

        [Test]
        public void Test2()
        {
            string url = Directory.GetParent("../../../") + "\\HTMLPage1.html";
            webdriver.Navigate().GoToUrl(url);
            string parentTab = webdriver.CurrentWindowHandle;
            Thread.Sleep(2000);
            webdriver.FindElement(By.Id("newTab")).Click();

            Thread.Sleep(2000);
            webdriver.FindElement(By.Id("newTab2")).Click();

            Thread.Sleep(5000);
            IList<string> tabs = webdriver.WindowHandles;
            foreach (var tab in tabs) {
                webdriver.SwitchTo().Window(tab);

                if (webdriver.Title.ToLower().Contains("google")) { 
                    WebDriverWait wait = new(webdriver, TimeSpan.FromSeconds(3));
                    IWebElement cookiePopup = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("CXQnmb")));
                    cookiePopup.FindElement(By.Id("L2AGLb")).Click();
                    webdriver.FindElement(By.Name("q")).SendKeys("Selenium");
                    webdriver.FindElement(By.Name("q")).SendKeys(Keys.Enter);

                    IWebElement seleniumAnchor = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//a[@href='https://www.selenium.dev/']")));
                    seleniumAnchor.Click();
                    break;
                }    
            }

            webdriver.Close();
            Thread.Sleep(2000);
            webdriver.SwitchTo().Window(parentTab);
        }

        [Test]
        public void Test3()
        {
            string url = Directory.GetParent("../../../") + "\\HTMLPage1.html";
            webdriver.Navigate().GoToUrl(url);

            IWebElement element = webdriver.FindElement(By.Id("testing"));
            Thread.Sleep(2000);
            element.SendKeys("Hello Jol");

            string text = element.Text;
            Console.WriteLine($"2- {text}");

            element.Click();
            Thread.Sleep(2000);
            element.SendKeys(Keys.Control + "a");
            Thread.Sleep(2000);
            element.SendKeys(Keys.Backspace);
            // element.Clear();
        }

        [Test]
        public void Test4()
        {
            webdriver.Navigate().GoToUrl("https://demo.nopcommerce.com/register?returnUrl=%2F");
            Thread.Sleep(2000);

            IWebElement firstNameInput = webdriver.FindElement(By.Id("FirstName"));
            IWebElement newsLetterCheckbox = webdriver.FindElement(By.Id("Newsletter"));

            if (firstNameInput.Displayed && firstNameInput.Enabled)
            {
                firstNameInput.SendKeys("John");
            }

            if(newsLetterCheckbox.Selected) {
                newsLetterCheckbox.Click();
            }
        }

        [Test]
        public void Test5()
        {
            webdriver.Navigate().GoToUrl("http://swisnl.github.io/jQuery-contextMenu/demo.html");
            Thread.Sleep(2000);

            IWebElement element = webdriver.FindElement(By.XPath("//span[text()='right click me']"));
            Actions actions = new(webdriver);
            actions.ContextClick(element).Perform();
        }

        [Test]
        public void Test6()
        {
            //string url = Directory.GetParent("../../../") + "\\HTMLPage1.html";
            webdriver.Navigate().GoToUrl("https://www.browserstack.com");
            Thread.Sleep(2000);

            IJavaScriptExecutor js = (IJavaScriptExecutor)webdriver;
            js.ExecuteScript("window.scrollBy(0, document.body.scrollHeight);");
            Thread.Sleep(2000);
            IWebElement elementToView = webdriver.FindElement(By.XPath("//a[@id='view-all-testimonials']"));
            js.ExecuteScript("arguments[0].scrollIntoView();", elementToView);
            // WebDriverWait wait = new WebDriverWait(webdriver, TimeSpan.FromSeconds(10));
            // IWebElement element = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("newTab2")));
            // js.ExecuteScript("arguments[0].click();", element);
        }

        [Test]
        public void Test7()
        {
            
            webdriver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");
            Thread.Sleep(2000);
            IWebElement element = webdriver.FindElement(By.XPath("//button[contains(text(),'Click for JS Alert')]"));
            element.Click();

            Thread.Sleep(2000);
            webdriver.SwitchTo().Alert().Accept();

            Thread.Sleep(2000);
            IWebElement element2 = webdriver.FindElement(By.XPath("//button[contains(text(),'Click for JS Confirm')]"));
            element2.Click();

            Thread.Sleep(2000);
            webdriver.SwitchTo().Alert().Dismiss();

            Thread.Sleep(2000);
            IWebElement element3 = webdriver.FindElement(By.XPath("//button[contains(text(),'Click for JS Prompt')]"));
            element3.Click();

            IAlert alert = webdriver.SwitchTo().Alert();
            alert.SendKeys("test");
            alert.Accept();
        }

        [Test]
        public void Test8()
        {

            webdriver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_alerts");
            Thread.Sleep(2000);
            
            ITakesScreenshot screenshot = (ITakesScreenshot)webdriver;
            screenshot.GetScreenshot().SaveAsFile("C:\\Users\\João\\Desktop\\Programação\\ASP-MVS\\TestProject1\\TestProject1\\shot.png");
        }

        [Test]
        public void Test9()
        {

            webdriver.Navigate().GoToUrl("C:\\Users\\João\\Desktop\\Programação\\ASP-MVS\\TestProject1\\TestProject1\\HTMLPage1.html");
            Thread.Sleep(2000);
            IWebElement elementToWrite = webdriver.FindElement(By.Id("testing"));
            IWebElement elementoToCopy = webdriver.FindElement(By.Id("testing2"));

            elementToWrite.SendKeys("testing");
            Thread.Sleep(2000);
            Actions actions = new(webdriver);
            
            actions.KeyDown(Keys.Control);
            actions.SendKeys("a");
            actions.Perform();

            actions.KeyUp(Keys.Control);
            actions.KeyDown(Keys.Control);
            actions.SendKeys("c");
            actions.Perform();
            Thread.Sleep(2000);

            elementoToCopy.Click();
            actions.KeyDown(Keys.Control);
            actions.SendKeys("v");
            actions.Perform();
        }

        [Test]
        public void Test10()
        {

            webdriver.Navigate().GoToUrl("http://demo.nopcommerce.com/login?returnUrl=%2F");
            webdriver.Navigate().Refresh();

            for(int i = 0; i < 5; i++)
            {
                try
                {
                    By by = By.Id("Email");
                    IWebElement email = GetWebElement(webdriver, by);
                    email.SendKeys("Testing");
                    break;
                }
                catch(StaleElementReferenceException ex) { }
            }
        }

        private IWebElement GetWebElement(IWebDriver driver, By by)
        {
            WebDriverWait wait = new (driver, TimeSpan.FromSeconds(30));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(ElementNotVisibleException));
            return wait.Until(ExpectedConditions.ElementToBeClickable(by));
        }
    }
}