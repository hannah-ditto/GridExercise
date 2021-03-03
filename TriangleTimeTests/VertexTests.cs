using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TriangleTime.Logic;

namespace TriangleTimeTests
{
    [TestClass]
    public class VertexTests
    {
        #region Constants

        public const int XCoord = 0;
        public const int YCoord = 3;


        #endregion

        [TestMethod]
        public void CreateNewVertex_UsingXYCoords_ReturnsVertexObjectWithValidCoords()
        {
            // Arrange + Act
            var testVertex = new Vertex(XCoord, YCoord);

            // Act + Assert
            Assert.AreEqual(XCoord, testVertex.XCoord);
            Assert.AreEqual(YCoord, testVertex.YCoord);



        }
    }
}
