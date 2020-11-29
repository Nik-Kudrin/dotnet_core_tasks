//using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace ExampleDotnetCore.Codility
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement EmailInput
        {
            get
            {
                try
                {
                    return _driver.FindElement(By.Id("email-input"));
                }
                catch
                {
                    // TODO: instead of null return special object type
                    return null;
                }
            }
        }

        public IWebElement PasswordInput
        {
            get
            {
                try
                {
                    return _driver.FindElement(By.Id("password-input"));
                }
                catch
                {
                    // TODO: instead of null return special object type
                    return null;
                }
            }
        }

        public IWebElement LoginButton
        {
            get
            {
                try
                {
                    return _driver.FindElement(By.Id("login-button"));
                }
                catch
                {
                    // TODO: instead of null return special object type
                    return null;
                }
            }
        }

        public IWebElement SuccessfullLoginMessage
        {
            get
            {
                try
                {
                    return _driver.FindElement(By.CssSelector("div[class=\"message success\"]"));
                }
                catch
                {
                    // TODO: instead of null return special object type
                    return null;
                }
            }
        }

        public IWebElement FailedLoginMessage
        {
            get
            {
                try
                {
                    return _driver.FindElement(By.CssSelector("div[class=\"message error\"]"));
                }
                catch
                {
                    // TODO: instead of null return special object type
                    return null;
                }
            }
        }

        public List<IWebElement> ValidationErrors
        {
            get
            {
                try
                {
                    return new List<IWebElement>(_driver.FindElements(By.CssSelector("div[class=\"validation error\"]")));
                }
                catch
                {
                    // TODO: instead of null return special object type
                    return null;
                }
            }
        }

        public void TypeUserCredentials(string login, string password)
        {
            EmailInput.SendKeys(login);
            PasswordInput.SendKeys(password);
            LoginButton.Click();
        }
    }

    public static class WebElementValidationExtension
    {
        public static void CheckIsElementPresentedOnThePage(this IWebElement element, string elementName)
        {
            Assert.NotNull(element);
            Assert.True(element.Displayed, $"{elementName} isn't displayed");
        }
    }

    public class LoginPageTest
    {
        private IWebDriver _driver;

        [Fact]
        public void CheckAreAllInputFieldsPresentedOnLoginPage()
        {
            var loginPage = new LoginPage(_driver);
            var emailInput = loginPage.EmailInput;
            var passwordInput = loginPage.PasswordInput;
            var loginButton = loginPage.PasswordInput;

            emailInput.CheckIsElementPresentedOnThePage("Email input");
            passwordInput.CheckIsElementPresentedOnThePage("Password input");
            loginButton.CheckIsElementPresentedOnThePage("Login button");
        }


        [Fact]
        public void ValidCredentialsShouldWork()
        {
            var login = "login@codility.com";
            var password = "password";

            var loginPage = new LoginPage(_driver);
            loginPage.TypeUserCredentials(login, password);

            var loginMessage = loginPage.SuccessfullLoginMessage;
            loginMessage.CheckIsElementPresentedOnThePage("Successfull login message");
            Assert.Equal("Welcome to Codility", loginMessage.Text);
        }

        [Fact]
        public void InvalidCredentialsShouldFail()
        {
            var login = "unknown@codility.com";
            var password = "password";

            var loginPage = new LoginPage(_driver);
            loginPage.TypeUserCredentials(login, password);

            var loginMessage = loginPage.FailedLoginMessage;
            loginMessage.CheckIsElementPresentedOnThePage("Failed login message");
            Assert.Equal("You shall not pass! Arr!", loginMessage.Text);
        }

        [Fact]
        public void CheckEmailValidationMessage()
        {
            var login = "email";
            var password = "password";

            var loginPage = new LoginPage(_driver);
            loginPage.TypeUserCredentials(login, password);

            var validationMessage = loginPage.ValidationErrors.FirstOrDefault();
            validationMessage.CheckIsElementPresentedOnThePage("Email validation message");
            Assert.Equal("Enter a valid email", validationMessage.Text);
        }

        [Fact]
        public void CheckEmptyCredentials()
        {
            var loginPage = new LoginPage(_driver);
            loginPage.TypeUserCredentials("", "");

            var validationMessages = loginPage.ValidationErrors;

            Assert.Equal(2, validationMessages.Count);


            validationMessages.ForEach(message => message.CheckIsElementPresentedOnThePage("Validation message"));

            var emailIsRequired = "Email is required";
            var isEmailIsRequiredMessageExist = validationMessages.Any(item => item.Text == emailIsRequired);
            Assert.True(isEmailIsRequiredMessageExist, emailIsRequired + " message isn't present on the page");

            var passwordIsRequired = "Password is required";
            var isPasswordIsRequiredMessageExist = validationMessages.Any(item => item.Text == emailIsRequired);
            Assert.True(isPasswordIsRequiredMessageExist, passwordIsRequired + " message isn't present on the page");

        }
    }
}
