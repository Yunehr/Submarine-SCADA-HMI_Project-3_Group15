using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyProjectTemplate.API.SubSubController.SubControls;

namespace Submarine_SCADA_HMI.Tests.SubControlsTests
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
            CollectionAssert.AreEqual(expected, actual);
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
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ControlsBallastNotReq_IsValid()
        {
            //arrange
            IControls ct = new Controls();
            double offset = 40.0;
            double[] expected = { 0.0, 0.0, offset, 0.0 };

            //act
            ct.Pitch(2.1);
            ct.Thrust(70.0);
            ct.Turn(-1.2);
            ct.AdjBuoyancy(offset);
            double[] actual = ct.CalcVelocity();

            //assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ControlsPowerOn_IsValid()
        {
            //arrange
            IControls ct = new Controls();
            double offset = 40.0;
            double[] expected = { offset, 0.0, 0.0, offset };

            //act
            ct.Thrust(offset);
            ct.PowerOn();
            double[] actual = ct.CalcVelocity();

            //assert
            CollectionAssert.AreEqual(expected, actual);
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
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Controls_PitchPMath_IsValidMostly()
        {
            //arrange
            IControls ct = new Controls();
            double[] expected = { 86.60254038, 0.0, 50.0, 100.0 };

            //act
            ct.PowerOn();
            ct.Thrust(100.0);
            ct.Pitch(Math.PI / 6); //30 deg up
            double[] actual = ct.CalcVelocity();

            actual[0] = double.Round(actual[0], 8);//amount of precision my calculator gives
            actual[2] = double.Round(actual[2], 8);

            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Controls_PitchNMath_IsValidMostly()
        {
            //arrange
            IControls ct = new Controls();
            double[] expected = { 50.0, 0.0, -86.60254038, 100.0 };

            //act
            ct.PowerOn();
            ct.Thrust(100.0);
            ct.Pitch(-(Math.PI/3));//60 deg down
            double[] actual = ct.CalcVelocity();

            actual[0] = double.Round(actual[0], 8);//amount of precision my calculator gives
            actual[2] = double.Round(actual[2], 8);

            //assert
             CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Controls_PitchLimit_IsValidMostly()
        {
            //arrange
            IControls ct = new Controls();
            double[] expected = { 0.0, 0.0, 100.0, 100.0 };

            //act
            ct.PowerOn();
            ct.Thrust(100.0);
            ct.Pitch(Math.PI / 2);
            double[] actual = ct.CalcVelocity();

            actual[0] = double.Round(actual[0], 8);//amount of precision my calculator gives
            actual[2] = double.Round(actual[2], 8);

            //assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Controls_YawPMath_IsValidMostly()
        {
            //arrange
            IControls ct = new Controls();
            double[] expected = { 70.71067812, 70.71067812, 0.0, 100.0 };

            //act
            ct.PowerOn();
            ct.Thrust(100.0);
            ct.Turn(Math.PI/4); //45 deg left
            double[] actual = ct.CalcVelocity();

            actual[0] = double.Round(actual[0], 8);//amount of precision my calculator gives
            actual[1] = double.Round(actual[1], 8);

            //assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Controls_YawNMath_IsValidMostly()
        {
            //arrange
            IControls ct = new Controls();
            double[] expected = { 96.59258263, -25.88190451, 0.0, 100.0 };

            //act
            ct.PowerOn();
            ct.Thrust(100.0);
            ct.Turn(-(Math.PI/12)); //15 deg right
            double[] actual = ct.CalcVelocity();

            actual[0] = double.Round(actual[0], 8);//amount of precision my calculator gives
            actual[1] = double.Round(actual[1], 8);

            //assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Controls_YawLimit_IsValidMostly()
        {
            //arrange
            IControls ct = new Controls();
            double[] expected = { 0.0, 100.0, 0.0, 100.0 };

            //act
            ct.PowerOn();
            ct.Thrust(100.0);
            ct.Turn(Math.PI /2);
            double[] actual = ct.CalcVelocity();

            actual[0] = double.Round(actual[0], 8);//amount of precision my calculator gives
            actual[1] = double.Round(actual[1], 8);

            //assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Controls_PitchYaw_IsValidMostly()
        {
            //arrange
            IControls ct = new Controls();
            double[] expected = { 43.30127019, 75.0, 50.0, 100.0 };

            //act
            ct.PowerOn();
            ct.Thrust(100.0);
            ct.Pitch(Math.PI/6);//30 deg up
            ct.Turn(Math.PI/3);//60 deg left
            double[] actual = ct.CalcVelocity();

            actual[0] = double.Round(actual[0], 8);//amount of precision my calculator gives
            actual[1] = double.Round(actual[1], 8);
            actual[2] = double.Round(actual[2], 8);

            //assert
            CollectionAssert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void Controls_BuoyAddition_IsValidMostly()
        {
            //arrange
            IControls ct = new Controls();
            double[] expected = { 70.71067812, 0.0, 170.71067812, 100.0 };

            //act
            ct.PowerOn();
            ct.Thrust(100.0);
            ct.Pitch(Math.PI / 4); //45 deg up
            ct.AdjBuoyancy(100);
            double[] actual = ct.CalcVelocity();

            actual[0] = double.Round(actual[0], 8);//amount of precision my calculator gives
            actual[2] = double.Round(actual[2], 8);

            //assert
            CollectionAssert.AreEqual(expected, actual);

        }
    }
    }
