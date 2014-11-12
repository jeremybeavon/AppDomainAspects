using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppDomainAspects.Tests
{
    [TestClass]
    public class RunInDifferentAppDomainTests
    {
        private AppDomain domain;

        [TestInitialize]
        public void SetUp()
        {
            AppDomainSetup domainSetup = new AppDomainSetup()
            {
                ApplicationBase = AppDomain.CurrentDomain.BaseDirectory
            };
            domain = AppDomain.CreateDomain("Test", null, domainSetup);
            domain.SetData("TestData", "TestData");
            DefaultAppDomainProvider.AppDomain = domain;
        }

        [TestCleanup]
        public void TearDown()
        {
            DefaultAppDomainProvider.AppDomain = null;
            AppDomain.Unload(domain);
        }

        [TestMethod]
        [RunInDifferentAppDomain]
        public void TestSimpleCall()
        {
            AppDomain.CurrentDomain.FriendlyName.Should().Be("Test");
            AppDomain.CurrentDomain.GetData("TestData").Should().Be("TestData");
        }
    }
}
