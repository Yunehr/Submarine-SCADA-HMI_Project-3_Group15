using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyProjectTemplate.API.SubMovement.SubNav;

namespace Submarine_SCADA_HMI.Tests.SubMovementTests
{
    [TestClass]
    public class Navi_Tests
    {
        [TestMethod]
        public void NormalUsage_IsValid()
        {
            //arrange
            INavi n = new Navi();
            double[] expected = { 203.3, 13.4, -81.0 };

            //act
            n.UpdatePos(203.3, 13.4, -81.0);

            //assert
            Assert.AreEqual(expected[0], n.X);
            Assert.AreEqual(expected[1], n.Y);
            Assert.AreEqual(expected[2], n.Z);
        }

        [TestMethod]
        public void MapLimit_IsValid()
        {
            //arrange
            INavi n = new Navi();
            double expected = 500;
            //act
            n.UpdatePos(750.0, 0.0, 0.0);

            //assert
            Assert.AreEqual(expected, n.X);
        }

        [TestMethod]
        public void NegMapLimit_IsValid()
        {
            //arrange
            INavi n = new Navi();
            double expected = -500;
            //act
            n.UpdatePos(0.0, -750.0, 0.0);

            //assert
            Assert.AreEqual(expected, n.Y);
        }

        [TestMethod]
        public void DepthLimit_IsValid()
        {
            //arrange
            INavi n = new Navi();
            double expected = -440;
            //act
            n.UpdatePos(0.0, -0.0, -800.0);

            //assert
            Assert.AreEqual(expected, n.Z);
        }

        [TestMethod]
        public void SurfaceLimit_IsValid()
        {
            //arrange
            INavi n = new Navi();
            double expected = 0.0;
            //act
            n.UpdatePos(0.0, -0.0, 220.0);

            //assert
            Assert.AreEqual(expected, n.Z);
        }




    }
}
