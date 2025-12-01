using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyProjectTemplate.API.SubSubController.SubControls;

namespace Submarine_SCADA_HMI.Tests.SubControlsTests
{
    [TestClass]
    public class CTRLElements_Tests
    {

        [TestMethod]
        public void BasicControlElement_IsValid()
        {
            //arrange
            double difference = 5.0;// no meaning in particular

            //act
            ControlElement ce = new Ballast(); //ballast has no modifications, so using it
            ce.Offset = difference;

            //assert
            Assert.AreEqual(5.0, ce.Offset);
        }

        [TestMethod]
        public void defaultControlElement_IsValid()
        {
            //arrange
            double expected = 0.0;

            //act
            ControlElement ce = new Ballast(); //ballast has no modifications, so using it

            //assert
            Assert.AreEqual(expected, ce.Offset);
        }

        [TestMethod]
        public void CTRLElement_upperbound_IsValid()
        {
            //arrange
            double difference = 105.0;
            double expected = 100.0;

            //act
            ControlElement ce = new Ballast(); //ballast has no modifications, so using it
            ce.Offset = difference;

            //assert
            Assert.AreEqual(expected, ce.Offset);
        }

        [TestMethod]
        public void CTRLElement_lowerbound_IsValid()
        {
            //arrange
            double difference = -105.0;
            double expected = -100.0;

            //act
            ControlElement ce = new Ballast(); //ballast has no modifications, so using it
            ce.Offset = difference;

            //assert
            Assert.AreEqual(expected, ce.Offset);
        }

        [TestMethod]
        public void CTRLElement_CustomUpperbound_IsValid()
        {
            //arrange
            double difference = 4.0;
            double expected = (Math.PI/2.0);

            //act
            ControlElement ce = new Rudder(); //either rudder or sternp work for this test
            ce.Offset = difference;

            //assert
            Assert.AreEqual(expected, ce.Offset);
        }

        [TestMethod]
        public void CTRLElement_CustomLowerbound_IsValid()
        {
            //arrange
            double difference = -4.0;
            double expected = -(Math.PI/2);

            //act
            ControlElement ce = new Rudder();
            ce.Offset = difference;

            //assert
            Assert.AreEqual(expected, ce.Offset);
        }


    }

    [TestClass]
    public class Propeller_Tests
    {
        [TestMethod]
        public void DefaultPropeller_IsValid()
        {
            //arrange
            double expected1 = 0.0;
            bool expected2 = false;

            //act
            IPropeller pp = new Propeller();

            //assert
            Assert.AreEqual(expected1, pp.Offset);
            Assert.AreEqual(expected2, pp.IsOn);
        }

        //offset tests covered in ctrlelement tests

        [TestMethod]
        public void PropPowerOn_IsValid()
        {
            //arrange
            bool expected = true;

            //act
            IPropeller pp = new Propeller();
            pp.TurnOn();

            //assert
            Assert.AreEqual(expected, pp.IsOn);
        }

        [TestMethod]
        public void PropPowerOff_IsValid()
        {
            //arrange
            bool expected = false;

            //act
            IPropeller pp = new Propeller();
            pp.TurnOn();
            pp.TurnOff();

            //assert
            Assert.AreEqual(expected, pp.IsOn);
        }
    }
}
