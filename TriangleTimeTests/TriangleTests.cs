using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TriangleTime.Logic;

namespace TriangleTimeTests
{
    [TestClass]
    public class TriangleTests
    {
        #region Constants


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

        #region Test Methods


        [TestInitialize]

        public void Setup()
        {
           
        }

        
        [TestMethod]
        public void ConvertCoordinatesToShapeName_UsingValidCoordinates_ReturnsLeftTriangleName()
        {
            // Arrange
            Triangle a3 = new Triangle(Triangle1Name);

            // Act
            string triangleName = a3.ConvertCoordinatesToShapeName(a3.Vertices);

            // Assert

            Assert.AreEqual(Triangle1Name, triangleName);

        }

        [TestMethod]
        public void ConvertCoordinatesToShapeName_UsingValidCoordinates_ReturnsRightTriangleName()
        {
            // Arrange
            Triangle a3 = new Triangle(Triangle2Name);

            // Act
            string triangleName = a3.ConvertCoordinatesToShapeName(a3.Vertices);

            // Assert

            Assert.AreEqual(Triangle2Name, triangleName);

        }


        [TestMethod]
        public void ConvertShapeNameToCoordinates_UsingName_ReturnsLeftTriangleCoordinates()
        {
            // Arrange
            Triangle t1 = new Triangle(T1Vertices);

            // Act
            Vertex[] t1Vertices = t1.Vertices;

            // Assert
            Assert.AreEqual(T1Vertices, t1Vertices);
        }

        [TestMethod]
        public void ConvertShapeNameToCoordinates_UsingName_ReturnRightTriangleCoordinates()
        {
            // Arrange
            Triangle t2 = new Triangle(T2Vertices);

            // Act
            Vertex[] t2Vertices = t2.Vertices;

            // Assert
            Assert.AreEqual(T2Vertices, t2Vertices);
        }


        [TestMethod]
        public void ValidateShapeCoordinates_UsingInvalidCoordinates_ThrowsException()
        {
            // Arrange
            Triangle testTriangle = new Triangle(Triangle2Name);

            // Act + Assert

            Assert.ThrowsException<System.Exception>(() =>
            {
                testTriangle.ValidateShapeCoordinates(InvalidVertices);
            });


        }


        [TestMethod]
        public void ValidateShapeCoordinates_UsingValidCoordinates_ReturnsTrue()
        {
            // Arrange
            Triangle testTriangle = new Triangle(Triangle2Name);

            // there is some logic inside the triangle class to convert to simplified versions of the vertices before doing this check
            // for testing the check outside this case, we will just fake it till we make it
            IShape fakeTriangleValidVertices = A.Fake<IShape>();
            A.CallTo(() => fakeTriangleValidVertices.Vertices).Returns(
                new Vertex[] 
                {
                    new Vertex(0,1),
                    new Vertex(1,0),
                    new Vertex(0,0)
                }
                );

            // Act
            bool isTriangle = testTriangle.ValidateShapeCoordinates(fakeTriangleValidVertices.Vertices);

            // Assert
            Assert.IsTrue(isTriangle);

        }

        #endregion


    }
}
