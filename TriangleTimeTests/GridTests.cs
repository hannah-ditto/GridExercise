using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TriangleTime.Logic;

namespace TriangleTimeTests
{

    [TestClass]
    
    public class GridTests
    {


        #region Properties

        private Vertex[] _dehydratedVertices { get; } = {
            new Vertex(2, 2),
            new Vertex(2, 1),
            new Vertex(1, 1)
        };

        private Vertex[] _hydratedVertices { get; } =
        {
            new Vertex(20, 20),
            new Vertex(20, 10),
            new Vertex(10, 10)
        };

        #endregion


        [TestMethod]
        public void MultiplyByPixelLength_GivenPixels_UpdatesVerticesArray()
        {
            // Arrange + Act
           Vertex[] testVertices = Grid.MultiplyByPixelLength(_dehydratedVertices);

           for (int i = 0; i < testVertices.Length; i++)
           {
               // Assert
               Assert.AreEqual(_hydratedVertices[i].XCoord, testVertices[i].XCoord);
               Assert.AreEqual(_hydratedVertices[i].YCoord, testVertices[i].YCoord);

           }
        }

        [TestMethod]
        public void DivideByPixelLength_GivenPixels_UpdatesVerticesArray()
        {
            // Arrange + Act
            Vertex[] testVertices = Grid.DivideByPixelLength(_hydratedVertices);

            for (int i = 0; i < testVertices.Length; i++)
            {
                // Assert
                Assert.AreEqual(_dehydratedVertices[i].XCoord, testVertices[i].XCoord);
                Assert.AreEqual(_dehydratedVertices[i].YCoord, testVertices[i].YCoord);

            }
        }
    }
}
