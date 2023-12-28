// Copyright (c) Jan-Niklas Schäfer. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThinkSharp.Licensing;
using ThinkSharp.Licensing.Signing.RSA;
using ThinkSharp.Licensing.Test.Signing;

namespace ThinkSharp.Licensing.Test
{
  [TestClass]
    public class SignedLicenseTest
    {
        [TestMethod]
        public void TestInitialization()
        {
            var file = new SignedLicense("HardwareID", "SerialNumber", DateTime.Now, DateTime.Now + TimeSpan.FromDays(10), null);
            AssertDefaultPropertiesAreValid(file);
            Assert.AreEqual(0, file.Properties.Count);
        }

        [TestMethod]
        public void TestInitialization_WithProperties()
        {
            var file = new SignedLicense("HardwareID", "SerialNumber", DateTime.Now, DateTime.Now + TimeSpan.FromDays(10), CreateProperties());
            AssertDefaultPropertiesAreValid(file);
            AssertPropertiesAreValid(file);
        }

        [TestMethod]
        public void TestInitialization_WithProperties_WithColone()
        {
            var properties = new Dictionary<string, string>();
            properties.Add("Pro:p1", "Val1");
            properties.Add("Prop2", "Val2");
            try
            {
                var file = new SignedLicense("HardwareID", "SerialNumber", DateTime.Now, DateTime.Now + TimeSpan.FromDays(10), properties);
                Assert.Fail("FormatException expected.");
            }
            catch (FormatException) { }
        }


        private static void AssertDefaultPropertiesAreValid(SignedLicense file)
        {
            Assert.AreEqual("HardwareID", file.HardwareIdentifier);
            Assert.AreEqual(DateTime.UtcNow.Date, file.IssueDate);
            Assert.AreEqual("SerialNumber", file.SerialNumber);
        }

        private static Dictionary<string, string> CreateProperties()
        {
            var properties = new Dictionary<string, string>();
            properties.Add("Prop1", "Val1");
            properties.Add("Prop2", "Val2");
            return properties;
        }

        private static void AssertPropertiesAreValid(SignedLicense file)
        {
            Assert.AreEqual(2, file.Properties.Count);
            Assert.AreEqual("Val1", file.Properties["Prop1"]);
            Assert.AreEqual("Val2", file.Properties["Prop2"]);
        }
    }
}
