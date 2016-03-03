using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotControl;

namespace RVM1Testing
{
    [TestClass]
    public class MoveMasterRobotTests
    {
        MoveMasterRobot _robot = new MoveMasterRobot();
        [TestMethod]
        [ExpectedException(typeof(IOException),
    "A userId of null was inappropriately allowed.")]
        public void Pipapo()
        {
            _robot.Init(); 
        }
    }
}
