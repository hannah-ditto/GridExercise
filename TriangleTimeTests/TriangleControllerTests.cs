using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TriangleTime.Controllers;
using TriangleTime.Logic;

namespace TriangleTimeTests
{
    [TestClass]
    public class TriangleControllerTests
    {

        #region Constants

        public const int PixelLength = 10;
        public const int GridHeight = 6;

        // Left Triangle, Top Row
        public const string Triangle1Name = "A3";

        public const int X1forT1 = 10;
        public const int Y1forT1 = 50;
        public const int X2forT1 = 20;
        public const int Y2forT1 = 50;
        public const int X3forT1 = 10;
        public const int Y3forT1 = 60;

        // Right Triangle, Bottom Row
        public const string Triangle2Name = "F10";

        public const int X1forT2 = 40;
        public const int Y1forT2 = 10;
        public const int X2forT2 = 50;
        public const int Y2forT2 = 10;
        public const int X3forT2 = 50;
        public const int Y3forT2 = 0;


        #endregion

        #region Properties


        // A3 Vertices
        private Vertex[] T1Vertices { get; set; } =
        {
            new Vertex(X1forT1, Y1forT1),
            new Vertex(X2forT1, Y2forT1),
            new Vertex(X3forT1, Y3forT1),
        };


        // F10 Vertices
        Vertex[] T2Vertices { get; set; } =
        {
            new Vertex(X1forT2, Y1forT2),
            new Vertex(X2forT2, Y2forT2),
            new Vertex(X3forT2, Y3forT2),
        };

        // Invalid Vertices
        Vertex[] InvalidVertices { get; set; } =
        {
            new Vertex(X1forT2, Y1forT2),
            new Vertex(X1forT2, Y1forT2),
            new Vertex(X3forT2, Y3forT2),
        };

        #endregion

        [TestMethod]
        public void UpdatePx_GivenPixels_UpdatesGridSize()
        {
            // Arrange
            var controller = new TriangleController(new Logger<TriangleController>(new LoggerFactory()));

            // Act
            controller.UpdatePxLength(PixelLength);
            var responseGridPxLength = Grid.SideLengthInPx;

            // Assert
            Assert.AreEqual(PixelLength, responseGridPxLength);
        }

        [TestMethod]
        public void UpdateGridHeight_GivenHeight_UpdatesGridHeight()
        {
            // Arrange
            var controller = new TriangleController(new Logger<TriangleController>(new LoggerFactory()));

            // Act
            controller.UpdateGridHeight(GridHeight);
            var responseGridHeight = Grid.GridHeight;

            // Assert
            Assert.AreEqual(GridHeight, responseGridHeight);
        }

        [TestMethod]
        public void GetTriangleByName_GivenValidName_ReturnsTriangleCoordinates()
        {
            // Arrange
            var controller = new TriangleController(new Logger<TriangleController>(new LoggerFactory()));

            // Act
            var response = controller.GetTriangleByName(Triangle1Name);

            // Assert
            for (int i = 0; i < response.Vertices.Length; i++)
            {
                Assert.AreEqual(T1Vertices[i].XCoord, response.Vertices[i].XCoord);
                Assert.AreEqual(T1Vertices[i].YCoord, response.Vertices[i].YCoord);
            }
        }

        [TestMethod]
        public void GetTriangleByCoords_GivenValidCoords_ReturnsTriangleName()
        {
            // Arrange
            var controller = new TriangleController(new Logger<TriangleController>(new LoggerFactory()));

            // Act
            var response = controller.GetTriangleByCoordinates(X1forT1, Y1forT1, X2forT1, Y2forT1, X3forT1, Y3forT1);
           
            // Assert
            Assert.IsTrue(response.ShapeName == Triangle1Name);
        }
    }
}
