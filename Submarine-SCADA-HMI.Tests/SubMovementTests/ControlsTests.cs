using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyProjectTemplate.API.SubMovement.SubControls;

namespace Submarine_SCADA_HMI.Tests.SubMovementTests
{
    [TestClass]
    public class ControlsTests
    {
        [TestMethod]
        public void basicControls_IsValid()
        {
            //arrange
            IControls ct = new Controls();
            double[] expected = { 0.0, 0.0, 0.0, 0.0 };

            //act
            double[] actual = ct.CalcVelocity();

            //assert
                //collectionassert is really annoying and doesn't properly tell you whats wrong
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
        }

        [TestMethod]
        public void ControlsPowerReq_IsValid()
        {
            //arrange
            IControls ct = new Controls();
            double[] expected = { 0.0, 0.0, 0.0, 0.0 };

            //act
            ct.Pitch(2.1);
            ct.Thrust(70.0);
            ct.Turn(-1.2);
            double[] actual = ct.CalcVelocity();

            //assert
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
        }

        [TestMethod]
        public void ControlsBallastNotReq_IsValid()
        {
            //arrange
            IControls ct = new Controls();
            double offset = 40.0;
            double[] expected = { 0.0, 0.0, offset/20, 0.0 };

            //act
            ct.Pitch(2.1);
            ct.Thrust(70.0);
            ct.Turn(-1.2);
            ct.AdjBuoyancy(offset);
            double[] actual = ct.CalcVelocity();

            //assert
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
        }

        [TestMethod]
        public void ControlsPowerOn_IsValid()
        {
            //arrange
            IControls ct = new Controls();
            double offset = 40.0;
            double[] expected = { offset/10, 0.0, 0.0, offset/10 };

            //act
            ct.Thrust(offset);
            ct.PowerOn();
            double[] actual = ct.CalcVelocity();

            //assert
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
        }

        [TestMethod]
        public void ControlsPowerOff_IsValid()
        {
            //arrange
            IControls ct = new Controls();
            double[] expected = { 0.0, 0.0, 0.0, 0.0 };

            //act
            ct.Pitch(2.1);
            ct.Thrust(38.0);
            ct.Turn(-1.2);
            ct.PowerOn();
            ct.PowerOff();
            double[] actual = ct.CalcVelocity();

            //assert
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
        }

        [TestMethod]
        public void Controls_PitchPMath_IsValidMostly()
        {
            //arrange
            IControls ct = new Controls();
            double[] expected = { 8.66025404, 0.0, 5.0, 10.0};

            //act
            ct.PowerOn();
            ct.Thrust(100.0);
            ct.Pitch(30.0); 
            double[] actual = ct.CalcVelocity();

            actual[0] = double.Round(actual[0], 8);//amount of precision my calculator gives
            actual[2] = double.Round(actual[2], 8);

            //Assert
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
        }

        [TestMethod]
        public void Controls_PitchNMath_IsValidMostly()
        {
            //arrange
            IControls ct = new Controls();
            double[] expected = { 5.0, 0.0, -8.66025404, 10.0 };

            //act
            ct.PowerOn();
            ct.Thrust(100.0);
            ct.Pitch(-60.0);//60 deg down
            double[] actual = ct.CalcVelocity();

            actual[0] = double.Round(actual[0], 8);//amount of precision my calculator gives
            actual[2] = double.Round(actual[2], 8);

            //assert
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
        }

        [TestMethod]
        public void Controls_PitchLimit_IsValidMostly()
        {
            //arrange
            IControls ct = new Controls();
            double[] expected = { 0.0, 0.0, 10.0, 10.0 };

            //act
            ct.PowerOn();
            ct.Thrust(100.0);
            ct.Pitch(90.0);
            double[] actual = ct.CalcVelocity();

            actual[0] = double.Round(actual[0], 8);//amount of precision my calculator gives
            actual[2] = double.Round(actual[2], 8);

            //assert
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
        }

        [TestMethod]
        public void Controls_YawPMath_IsValidMostly()
        {
            //arrange
            IControls ct = new Controls();
            double[] expected = { 7.07106781, 7.07106781, 0.0, 10.0 };

            //act
            ct.PowerOn();
            ct.Thrust(100.0);
            ct.Turn(45.0); 
            double[] actual = ct.CalcVelocity();

            actual[0] = double.Round(actual[0], 8);//amount of precision my calculator gives
            actual[1] = double.Round(actual[1], 8);

            //assert
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
        }

        [TestMethod]
        public void Controls_YawNMath_IsValidMostly()
        {
            //arrange
            IControls ct = new Controls();
            double[] expected = { 9.65925826, -2.58819045, 0.0, 10.0 };

            //act
            ct.PowerOn();
            ct.Thrust(100.0);
            ct.Turn(-15.0); //15 deg right
            double[] actual = ct.CalcVelocity();

            actual[0] = double.Round(actual[0], 8);//amount of precision my calculator gives
            actual[1] = double.Round(actual[1], 8);

            //assert
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
        }

        [TestMethod]
        public void Controls_YawRollover_IsValidMostly()
        {
            //arrange
            IControls ct = new Controls();
            double[] expected = { 0.0, 10.0, 0.0, 10.0 };
            //act
            ct.PowerOn();
            ct.Thrust(100.0);
            ct.Turn(450);//should resolve to 90d
            double[] actual = ct.CalcVelocity();

            actual[0] = double.Round(actual[0], 8);//amount of precision my calculator gives
            actual[1] = double.Round(actual[1], 8);

            //assert
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
        }

        [TestMethod]
        public void Controls_YawNegRollover_IsValidMostly()
        {
            //arrange
            IControls ct = new Controls();
            double[] expected = { 0.0, -10.0, 0.0, 10.0 };
            //act
            ct.PowerOn();
            ct.Thrust(100.0);
            ct.Turn(-450);//should resolve to -90d
            double[] actual = ct.CalcVelocity();

            actual[0] = double.Round(actual[0], 8);//amount of precision my calculator gives
            actual[1] = double.Round(actual[1], 8);

            //assert
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
        }

        [TestMethod]
        public void Controls_PitchYaw_IsValidMostly()
        {
            //arrange
            IControls ct = new Controls();
            double[] expected = { 4.33012702, 7.5, 5.0, 10.0 };

            //act
            ct.PowerOn();
            ct.Thrust(100.0);
            ct.Pitch(30);
            ct.Turn(60);
            double[] actual = ct.CalcVelocity();

            actual[0] = double.Round(actual[0], 8);//amount of precision my calculator gives
            actual[1] = double.Round(actual[1], 8);
            actual[2] = double.Round(actual[2], 8);

            //assert
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
        }


        [TestMethod]
        public void Controls_BuoyAddition_IsValidMostly()
        {
            //arrange
            IControls ct = new Controls();
            double[] expected = { 7.07106781, 0.0, 12.07106781, 10.0 };

            //act
            ct.PowerOn();
            ct.Thrust(100.0);
            ct.Pitch(45.0);
            ct.AdjBuoyancy(100);
            double[] actual = ct.CalcVelocity();

            actual[0] = double.Round(actual[0], 8);//amount of precision my calculator gives
            actual[2] = double.Round(actual[2], 8);

            //assert
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
            Assert.AreEqual(expected[2], actual[2]);
            Assert.AreEqual(expected[3], actual[3]);
        }
    }
    }
