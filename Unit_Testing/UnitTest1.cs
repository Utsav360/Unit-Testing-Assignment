// DOTNET Libraries...

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using System.Text.RegularExpressions;
using OpenQA.Selenium.Support.UI;



/* 
 * 
 * 
 * Assignment: 5 (Based on unit testing).
 * 
 * Subject: Software Quality and Testing.  
 * 
 * Professor : Karen Laurin.
 * 
 * 
 * @author Utsavkumar M Patel (000820474).
 * 
 * Statement of Authorships...
 * I, Utsavkumar M Patel, 000820474 certify that this material is my original work. 
 * No other person's work has been used without due acknowledgement.
 * 
 */



namespace A6Start

{
    /// <summary>
    /// This is KatalonAutomationExample TestClass which is mainly used for to select the browser user want to run the test cases 
    /// And select the correct driver location which help to open the browser and run the test cases. 
    /// </summary>
    [TestClass]
    public class KatalonAutomationExample
    {

        //Variable initialization

        private static IWebDriver driver;
        private StringBuilder verificationErrors;
        private bool acceptNextAlert = true;

        //Opted for chrome browser and commenting all other browsers. 

        // private const string BROWSER = "FIREFOX";
        private const string BROWSER = "CHROME";          
        // private const string BROWSER = "IE";
        // private const string BROWSER = "EDGE";


        // Web Driver and Bin location as per chosen browser(Chrome here..)
        private const string DRIVER_LOCATION = @"C:\Drivers";  
        private const string FIREFOX_BIN_LOCATION = @"C:\Program Files\Mozilla Firefox\firefox.exe";




        /// <summary>
        /// InitializeClass method to initialize the driver class as per opted browser..
        /// </summary>
        /// <param name="testContext"></param>
        [ClassInitialize]
        public static void InitializeClass(TestContext testContext)
        {
        
            
            // FIREFOX
            if (BROWSER == "FIREFOX")
            {
                
                FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(DRIVER_LOCATION);
           
                // Note that the line below needs to be the full exe Name not just the path
                service.FirefoxBinaryPath = FIREFOX_BIN_LOCATION;
                driver = new FirefoxDriver(service);   // WORKS 
            }
        
            // CHROME
            else if (BROWSER == "CHROME")
                driver = new ChromeDriver(DRIVER_LOCATION);  // WORKS ! 
            
            // INTERNET EXPLORER
            else if (BROWSER == "IE")
                // Internet EXPLORER NOTE : Must add DRIVER_LOCATION to Path
                driver = new InternetExplorerDriver();  // WORKS !
             
            // EDGE
            else if(BROWSER == "EDGE")
                driver = new EdgeDriver(DRIVER_LOCATION);



        }

        /// <summary>
        /// Cleanup class method for cleanup(Exception Handling..)
        /// </summary>
        [ClassCleanup]
        public static void CleanupClass()
        {
            
            try
            {
                
                //driver.Quit();   // quit does not close the window.
                driver.Close();
                driver.Dispose();

            }
            
            catch (Exception)
            {

                // Ignore errors if unable to close the browser.
            
            }
        
        }

        
        /// <summary>
        /// InitializeTest Method to initialize the test.
        /// </summary>
        [TestInitialize]
        public void InitializeTest()
        {

            verificationErrors = new StringBuilder();

        }

        //Test Clean method- returns true otherwise false and print the verifications errors..
        [TestCleanup]
        public void CleanupTest()
        {

            Assert.AreEqual("", verificationErrors.ToString());

        }

        


        /// <summary>
        /// RunAllTests Method to call and each test method as order....
        /// </summary>
        [TestMethod]
        public void RunAllTests()
        {
            // Put your test cases in order here...

            // Test Admin Login test case.
            TestLoginAdmin();

            // Create User test case.
            TheCreateUserTest();

            // Delete User test case.
            TheDeleteUserTest();

            // Directory Testing - 4 Cities.
            TheCityDirectoryTest("Ajax {1}");
            TheCityDirectoryTest("Barrie {3}");
            TheCityDirectoryTest("Calgary {4}");
            TheCityDirectoryTest("Bancroft {2}");
           
        }




        /// <summary>
        /// TestLoginAdmin method to Test the Admin User Functionality.
        /// </summary>
        public void TestLoginAdmin()
        {
            
            //Navigate to the website and login via admin credentials and verify access...
            driver.Navigate().GoToUrl("https://csunix.mohawkcollege.ca/tooltime/comp10066/A3/login.php");

            driver.FindElement(By.Id("username")).Click();
            driver.FindElement(By.Id("username")).Clear();
            driver.FindElement(By.Id("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).Clear();
            driver.FindElement(By.Name("password")).SendKeys("adminP6ss");
            driver.FindElement(By.Name("Submit")).Click();
            driver.FindElement(By.Id("loginname")).Click();
            
            try
            {
                Assert.AreEqual("User: admin", driver.FindElement(By.Id("loginname")).Text);
            }
            
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
            
            try
            {
                Assert.AreEqual("User Admin", driver.FindElement(By.LinkText("User Admin")).Text);
            }
            
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }
            
            driver.FindElement(By.LinkText("Logout")).Click();
            driver.FindElement(By.Id("loginname")).Click();
            
            try
            {
                Assert.AreEqual("Not Logged In", driver.FindElement(By.Id("loginname")).Text);
            }
            
            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

        }




        /// <summary>
        /// TheCreateUserTest Method to Create New non-admin active user by via admin account
        /// And then verify new user functionality.
        /// </summary>
        public void TheCreateUserTest()
        {
            //Navigate to Website...
            driver.Navigate().GoToUrl("https://csunix.mohawkcollege.ca/tooltime/comp10066/A3/");

            try
            {
                Assert.AreEqual("Welcome to COMP10066 Assignment#2", driver.FindElement(By.XPath("//div[@id='body']/div/h2")).Text);
            }

            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

            try
            {
                Assert.AreEqual("Not Logged In", driver.FindElement(By.Id("loginname")).Text);
            }

            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

            driver.FindElement(By.LinkText("Login")).Click();
            driver.FindElement(By.Id("username")).Click();
            driver.FindElement(By.Id("username")).Clear();
            driver.FindElement(By.Id("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).Click();
            driver.FindElement(By.Name("password")).Clear();
            driver.FindElement(By.Name("password")).SendKeys("adminP6ss");
            driver.FindElement(By.Name("Submit")).Click();

            try
            {
                Assert.AreEqual("User: admin", driver.FindElement(By.Id("loginname")).Text);
            }

            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

            try
            {
                Assert.AreEqual("Navigation", driver.FindElement(By.XPath("//div[@id='body']/div[2]/h3")).Text);
            }

            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

            try
            {
                Assert.AreEqual("Member Services", driver.FindElement(By.XPath("//div[@id='body']/div[2]/h3[2]")).Text);
            }

            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

            try
            {
                Assert.AreEqual("Admin", driver.FindElement(By.XPath("//div[@id='body']/div[2]/h3[3]")).Text);
            }

            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

            //Create NewUser Account via Admin Account....
            driver.FindElement(By.LinkText("User Admin")).Click();
            driver.FindElement(By.Id("username")).Click();
            driver.FindElement(By.Id("username")).Clear();
            driver.FindElement(By.Id("username")).SendKeys("Utsav");
            driver.FindElement(By.Id("password")).Click();
            driver.FindElement(By.Id("password")).Clear();
            driver.FindElement(By.Id("password")).SendKeys("Utsav$123");
            driver.FindElement(By.Name("activate")).Click();
            driver.FindElement(By.XPath("(//input[@name='admin'])[2]")).Click();
            driver.FindElement(By.Id("email")).Click();
            driver.FindElement(By.Id("email")).Clear();
            driver.FindElement(By.Id("email")).SendKeys("utsav$123@gmail.com");
            driver.FindElement(By.Name("Add New Member")).Click();

            try
            {
                Assert.AreEqual("Record successfully inserted.", driver.FindElement(By.XPath("//div[@id='body']/div/div")).Text);
            }

            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.XPath("//div[@id='body']/div")).Text, "^[\\s\\S]*Utsav[\\s\\S]*$"));
            }

            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

            try
            {
                Assert.AreEqual("User: admin", driver.FindElement(By.Id("loginname")).Text);
            }

            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

            // Logout ...
            driver.FindElement(By.LinkText("Logout")).Click();

            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.XPath("//div[@id='body']/div")).Text, "^[\\s\\S]*Utsav[\\s\\S]*$"));
            }

            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

            // Login via newly created User Account...
            driver.FindElement(By.Id("username")).Click();
            driver.FindElement(By.Id("username")).Clear();
            driver.FindElement(By.Id("username")).SendKeys("Utsav");
            driver.FindElement(By.Name("password")).Click();
            driver.FindElement(By.Name("password")).Clear();
            driver.FindElement(By.Name("password")).SendKeys("Utsav$123");
            driver.FindElement(By.Name("Submit")).Click();

            // Verify New User Functionality....
            try
            {
                Assert.AreEqual("User: Utsav", driver.FindElement(By.Id("loginname")).Text);
            }

            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

            try
            {
                Assert.AreEqual("Navigation", driver.FindElement(By.XPath("//div[@id='body']/div[2]/h3")).Text);
            }

            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

            driver.FindElement(By.LinkText("Home")).Click();
            driver.FindElement(By.LinkText("About")).Click();

            try
            {
                Assert.AreEqual("About User Authorization", driver.FindElement(By.XPath("//div[@id='body']/div/h2")).Text);
            }

            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

            driver.FindElement(By.LinkText("Turn Debug On")).Click();

            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.Id("footer")).Text, "^[\\s\\S]*Utsav[\\s\\S]*$"));
            }

            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

            driver.FindElement(By.LinkText("Turn Debug Off")).Click();

            try
            {
                Assert.AreEqual("Member Services", driver.FindElement(By.XPath("//div[@id='body']/div[2]/h3[2]")).Text);
            }

            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

            driver.FindElement(By.LinkText("News")).Click();

            try
            {
                Assert.AreEqual("Latest News - May 2019", driver.FindElement(By.XPath("//div[@id='body']/div/h3")).Text);
            }

            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

            driver.FindElement(By.LinkText("Directory")).Click();

            try
            {
                Assert.AreEqual("Company Search", driver.FindElement(By.XPath("//div[@id='body']/div/h2")).Text);
            }

            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

            driver.FindElement(By.LinkText("FAQ")).Click();

            try
            {
                Assert.AreEqual("Frequently Asked Questions", driver.FindElement(By.XPath("//div[@id='body']/div/h2")).Text);
            }

            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            } 

            // Logout....
            driver.FindElement(By.LinkText("Logout")).Click();

            try
            {
                Assert.AreEqual("Not Logged In", driver.FindElement(By.Id("loginname")).Text);
            }

            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

            driver.FindElement(By.LinkText("Home")).Click();

            try
            {
                Assert.AreEqual("Not Logged In", driver.FindElement(By.Id("loginname")).Text);
            }

            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

        }





        /// <summary>
        /// TheDeleteUserTest method to Delete Created User Account by using AdminUser Account...
        /// </summary>
        public void TheDeleteUserTest()
        {
            // Navigate to website...
            driver.Navigate().GoToUrl("https://csunix.mohawkcollege.ca/tooltime/comp10066/A3/index.php");

            try
            {
                Assert.AreEqual("Welcome to COMP10066 Assignment#2", driver.FindElement(By.XPath("//div[@id='body']/div/h2")).Text);
            }

            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

            try
            {
                Assert.AreEqual("Not Logged In", driver.FindElement(By.Id("loginname")).Text);
            }

            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

            driver.FindElement(By.LinkText("Login")).Click();

            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.XPath("//div[@id='body']/div")).Text, "^[\\s\\S]*admin[\\s\\S]*$"));
            }

            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

            // Login via Admin Credentials...
            driver.FindElement(By.Id("username")).Click();
            driver.FindElement(By.Id("username")).Clear();
            driver.FindElement(By.Id("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).Click();
            driver.FindElement(By.Name("password")).Clear();
            driver.FindElement(By.Name("password")).SendKeys("adminP6ss");
            driver.FindElement(By.Name("Submit")).Click();

            try
            {
                Assert.AreEqual("User: admin", driver.FindElement(By.Id("loginname")).Text);
            }

            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

            driver.FindElement(By.LinkText("User Admin")).Click();

            try
            {
                Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.XPath("//div[@id='body']/div")).Text, "^[\\s\\S]*Utsav[\\s\\S]*$"));
            }

            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

            // Verify the User name in the table.... 
            driver.FindElement(By.XPath("//td[@id='Utsav']/a/img")).Click();
            driver.FindElement(By.LinkText("here")).Click();

            //Delete User....
            try
            {
                Assert.AreEqual("User Utsav was successfully deleted.", driver.FindElement(By.XPath("//div[@id='body']/div/div")).Text);
            }

            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

            try
            {
                Assert.AreEqual("User: admin", driver.FindElement(By.Id("loginname")).Text);
            }

            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

            // Logout...
            driver.FindElement(By.LinkText("Logout")).Click();

            try
            {
                Assert.AreEqual("Not Logged In", driver.FindElement(By.Id("loginname")).Text);
            }

            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

            driver.FindElement(By.LinkText("Home")).Click();

            try
            {
                Assert.AreEqual("Not Logged In", driver.FindElement(By.Id("loginname")).Text);
            }

            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

        }





        /// <summary>
        /// TheCityDirectoryTest method to select and verify the 4 different city by using XPATH ...
        /// </summary>
        /// <param name="dropDownText">return string dropDownText...</param>
        public void TheCityDirectoryTest(string dropDownText) 
        {

            //Navigate to Website...
            driver.Navigate().GoToUrl("https://csunix.mohawkcollege.ca/tooltime/comp10066/A3/index.php");

            try
            {
                Assert.AreEqual("Welcome to COMP10066 Assignment#2", driver.FindElement(By.XPath("//div[@id='body']/div/h2")).Text);
            }

            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

            try
            {
                Assert.AreEqual("Not Logged In", driver.FindElement(By.Id("loginname")).Text);
            }

            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

            // Login Via Admin User Account...
            driver.FindElement(By.LinkText("Login")).Click();
            driver.FindElement(By.Id("username")).Click();
            driver.FindElement(By.Id("username")).Clear();
            driver.FindElement(By.Id("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).Click();
            driver.FindElement(By.Name("password")).Clear();
            driver.FindElement(By.Name("password")).SendKeys("adminP6ss");
            driver.FindElement(By.Name("Submit")).Click();

            try
            {
                Assert.AreEqual("User: admin", driver.FindElement(By.Id("loginname")).Text);
            }

            catch (Exception e)
            {
                verificationErrors.Append(e.Message);
            }

            // Go to Directory page and click on city...
            driver.FindElement(By.LinkText("Directory")).Click();
            driver.FindElement(By.Id("city")).Click();
            // Dropdown for city search...
            new SelectElement(driver.FindElement(By.Id("city"))).SelectByText(dropDownText);
            driver.FindElement(By.Id("city")).Click();
            driver.FindElement(By.Name("submit")).Click();  
            // Split method to split the dropDownText string
            string splitTextPart = dropDownText.Split("{")[1]; 
            string city = dropDownText.Split(" {")[0];
            string stringNumber = splitTextPart.Split("}")[0];
            int number = int.Parse(stringNumber);
            int counter = number;

            //For loop which runs as per number of cities....
            for (int i = 1; i <= number; i++)
            {

                try
                {
                    //Assert.IsTrue(city, driver.FindElement(By.XPath("//ul[@id='company_"+i+"']/li[3]")).Text);    
                    Assert.IsTrue(Regex.IsMatch(driver.FindElement(By.XPath("//ul[@id='company_" + i + "']//li[3]")).Text, "^[\\s\\S]*" + city + "[\\s\\S]*$"));
                
                }

                catch (Exception e)
                {

                    counter--;
                    verificationErrors.Append(e.Message);

                }

            }


            try
            {

                Assert.AreEqual("User: admin", driver.FindElement(By.Id("loginname")).Text);

            }

            catch (Exception e)
            {

                verificationErrors.Append(e.Message);

            }

            driver.FindElement(By.LinkText("Logout")).Click();
            driver.FindElement(By.LinkText("Home")).Click();

            try
            {

                Assert.AreEqual("Not Logged In", driver.FindElement(By.Id("loginname")).Text);

            }

            catch (Exception e)
            {

                verificationErrors.Append(e.Message);

            }

        }


        /// <summary>
        /// IsElementPresent Method to check if element is present or not...
        /// </summary>
        /// <param name="by">returns true if element found otherwise returns false...</param>
        /// <returns></returns>
        private bool IsElementPresent(By by)
        {
            
            try
            {
                driver.FindElement(by);
                return true;
            }
            
            catch (NoSuchElementException)
            {
                return false;
            }

        }

        /// <summary>
        ///  IsAlertPresent Method check wheather alert present or not...
        /// </summary>
        /// <returns>returns true if alert is present otherwise returns false...</returns>
        private bool IsAlertPresent()
        {
            
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            
            catch (NoAlertPresentException)
            {
                return false;
            }

        }


        /// <summary>
        /// CloseAlertAndGetItsText method to check if alert is closed or not and return the text if it is closed...
        /// </summary>
        /// <returns>returns string alerttext if alert is closed..</returns>
        private string CloseAlertAndGetItsText()
        {
            
            try
            {
                
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                
                else
                {
                    alert.Dismiss();
                }
                
                return alertText;
            }

            finally
            
            {
                acceptNextAlert = true;
            }
        

        }
    
    }

}

