﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetTrade.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using NetTradeTests.Samples;
using System.Linq;

namespace NetTrade.Helpers.Tests
{
    [TestClass()]
    public class RobotParameterToolsTests
    {
        [TestMethod()]
        public void GetParametersTest()
        {
            var sampleBot = new SampleBot();

            var robotParameters = RobotParameterTools.GetParameters(sampleBot.GetType());

            Assert.AreEqual(3, robotParameters.Count());
        }
    }
}