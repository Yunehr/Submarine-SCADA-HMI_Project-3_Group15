using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyProjectTemplate.API.SubControls;

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
    }
}
