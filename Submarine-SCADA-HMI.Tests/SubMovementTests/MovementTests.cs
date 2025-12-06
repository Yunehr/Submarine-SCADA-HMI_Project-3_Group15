using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyProjectTemplate.API.SubMovement;

namespace Submarine_SCADA_HMI.Tests.SubMovementTests
{
    //tests here will be somewhat less thorough since most of it has been done already in the other classes
    [TestClass]
    public class Movement_Tests
    {
        [TestMethod]
        public void PowerReqs_AreValid()
        {
            //arrange
            IMovement m = new Movement();
            double[] expected = { 0.0, 0.0, -1.0, 0.0 };

            //act
            m.changeThrust(34.2);
            m.changeRudder(45.2);
            m.changePitch(12.5);
            m.changeBallast(-20.0);

            m.TestingRunStartOnce();

            //assert
            Assert.AreEqual(expected[0], m.GetPosX());
            Assert.AreEqual(expected[1], m.GetPosY());
            Assert.AreEqual(expected[2], m.GetPosZ());
            Assert.AreEqual(expected[3], m.GetSpeed());
        }
        [TestMethod]
        public void PowerReqs2_AreValid()
        {
            //arrange
            IMovement m = new Movement();
            double[] expected = { 0.0, 0.0, 0.0, 0.0 };
            m.Power(true);
            m.Power(false);

            //act
            m.changeThrust(34.2);
            m.changeRudder(45.2);
            m.changePitch(12.5);

            m.TestingRunStartOnce();

            //assert
            Assert.AreEqual(expected[0], m.GetPosX());
            Assert.AreEqual(expected[1], m.GetPosY());
            Assert.AreEqual(expected[2], m.GetPosZ());
            Assert.AreEqual(expected[3], m.GetSpeed());
        }

        [TestMethod]
        public void Velocity_IsValid()
        {
            //arrange
            IMovement m = new Movement();
            double[] expected = { 10.0, 0.0, -5.0, 10.0 };
            m.Power(true);

            //act
            m.changeThrust(100.0);
            m.changeBallast(-100.0);

            m.TestingRunStartOnce();

            //assert
            Assert.AreEqual(expected[0], m.GetPosX());
            Assert.AreEqual(expected[1], m.GetPosY());
            Assert.AreEqual(expected[2], m.GetPosZ());
            Assert.AreEqual(expected[3], m.GetSpeed());
        }

        [TestMethod]
        public void Velocity_IsMaintained()
        {
            //arrange
            IMovement m = new Movement();
            double[] expected = { 30.0, 0.0, -15.0, 10.0 };
            m.Power(true);

            //act
            m.changeThrust(100.0);
            m.changeBallast(-100.0);

            m.TestingRunStartOnce();
            m.TestingRunStartOnce();
            m.TestingRunStartOnce();

            //assert
            Assert.AreEqual(expected[0], m.GetPosX());
            Assert.AreEqual(expected[1], m.GetPosY());
            Assert.AreEqual(expected[2], m.GetPosZ());
            Assert.AreEqual(expected[3], m.GetSpeed());
        }

        [TestMethod]
        public void VelocityChange_IsValid()
        {
            //arrange
            IMovement m = new Movement();
            double[] expected = { 20.0, 10.0, -10.0, 10.0 };
            m.Power(true);

            //act
            m.changeThrust(100.0);

            m.TestingRunStartOnce();
            m.TestingRunStartOnce();

            m.changeRudder(90);
            m.TestingRunStartOnce();
            m.changePitch(-90);
            m.TestingRunStartOnce();

            //assert
            Assert.AreEqual(expected[0], m.GetPosX());
            Assert.AreEqual(expected[1], m.GetPosY());
            Assert.AreEqual(expected[2], m.GetPosZ());
            Assert.AreEqual(expected[3], m.GetSpeed());
        }
    }
}
