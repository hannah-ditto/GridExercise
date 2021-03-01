using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TriangleTime.Logic
{
    public class Triangle : IShape
    {
        #region Fields
        private Vertex _unmatchedX = new Vertex(0,0);
        private Vertex _unmatchedY = new Vertex(0, 0);
        private Vertex _matchedY = new Vertex(0, 0);
        private Vertex _matchedX = new Vertex(0, 0);

        #endregion

        #region Properties

        public Vertex[] Vertices { get; set; } = new Vertex[] { new Vertex(0, 0), new Vertex(0, 0), new Vertex(0, 0) };
        public string ShapeName { get; set; }
        public int SideLengthInPx
        {
            set { Grid.SideLengthInPx = value; }
            get { return Grid.SideLengthInPx; }
        }

        #endregion
        
        #region Private Properties

        private char RowName { get; set; }
        private int ColNumber { get; set; }

        #endregion

        #region Construction/Finalization
        public Triangle (Vertex[] vertices)
        {
            Grid.DivideByPixelLength(vertices);
            if (ValidateShape(vertices))
            {
                Vertices = vertices;
            }
            ColNumber = SetColumnNumber();
            RowName = SetRowName();
            ShapeName = CreateShapeName();
            Grid.MultiplyByPixelLength(Vertices);
        }

        public Triangle(string shapeName)
        {
            ShapeName = shapeName;
            SplitTriangleName(ShapeName);
            SetXCoordinates();
            SetYCoordinates();
            AddVertices();
            Grid.MultiplyByPixelLength(Vertices);
        }
        #endregion

        #region Public Methods
        
        public bool ValidateShape(Vertex[] vertices)
        {
            // this must remain true for the entire duration of this method in order to validate
            bool isTriangle;
            try
            {
                FindUnmatchedVertices(vertices);

                // check the X or Y coordinate number of the matching vertices
                _matchedY = vertices.First(y => y.YCoord != _unmatchedY.YCoord);
                _matchedX = vertices.First(x => x.XCoord != _unmatchedX.XCoord);
                isTriangle = (ValidateCoordinateProximity(_unmatchedX.XCoord, _matchedX.XCoord) &&
                ValidateCoordinateProximity(_unmatchedY.YCoord, _matchedY.YCoord));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return isTriangle;

        }

        #endregion

        #region Private Methods
        
        // for converting from name to coordinates
        private void SplitTriangleName(string TriangleName)
        {
            Regex r = new Regex(@"(?<Col>[A-Z]+)(?<Row>[0-9]+)",
                RegexOptions.Compiled);

            var rowMatch= r.Match(TriangleName).Groups["Col"].Value;
            var colMatch= r.Match(TriangleName).Groups["Row"].Value;

            RowName = Convert.ToChar(rowMatch);
            ColNumber = Convert.ToInt32(colMatch);
        }

        private string CreateShapeName()
        {
            return RowName.ToString()+ColNumber;
        }

        private void AddVertices()
        {
           // Console.WriteLine($"Matched X: {_matchedX.XCoord}, Unmatched X: {_unmatchedX.XCoord}.");
            //Console.WriteLine($"Matched Y: {_matchedY.YCoord}, Unmatched Y: {_unmatchedY.YCoord}.");

            Vertices[0].XCoord = _matchedX.XCoord;
            Vertices[0].YCoord = _matchedY.YCoord;

            Vertices[1].XCoord = _unmatchedX.XCoord;
            Vertices[1].YCoord = _matchedY.YCoord;

            Vertices[2].XCoord = _matchedX.XCoord;
            Vertices[2].YCoord = _unmatchedY.YCoord;
        }

        private void SetYCoordinates()
        {

            int rowNameNumber = (int)RowName; // A: 65, Y = 6
            int distanceFromGridTop = rowNameNumber - 65; // 0, Y = 6
            int distanceFromYAxis = Grid.GridHeight - distanceFromGridTop; // 6, Y = 6

            // if the column number is even, the matched coords
            // are on the top side of the triangle and will match
            // the distanceFromYAxis, the unmatched will be -1

            if (ColNumber % 2 == 0)
            {
                _unmatchedY.YCoord = distanceFromYAxis - 1;
                _matchedY.YCoord = distanceFromYAxis;

            }

            // if the column number is odd, the matched coords
            // are on the bottom side of the triangle, and those
            // coordinates will equal the distanceFromYAxis - 1
            else if (ColNumber % 2 != 0)
            {
                _unmatchedY.YCoord = distanceFromYAxis;
                _matchedY.YCoord = distanceFromYAxis - 1;
            }
        }

        private void SetXCoordinates()
        {
            // if the column number is even, the matched coords
            // are on the right side of the triangle, and those
            // coordinates will equal the col number / 2

            if (ColNumber % 2 == 0)
            {
                // we add 1 to account for int rounding behavior
                _unmatchedX.XCoord = (ColNumber / 2)-1;
                _matchedX.XCoord = ColNumber/ 2;

            }

            // if the column number is odd, the matched coords
            // are on the left side of the triangle, and those
            // coordinates will equal the (col number / 2) + 1
            else if (ColNumber % 2 != 0)
            {
                // we add 1 to account for int rounding behavior
                _unmatchedX.XCoord = (ColNumber + 1)/ 2;
                _matchedX.XCoord = (ColNumber - 1) / 2;
            }

        }

        // for converting to correct pixel length 

        // for converting from coordinates to name

        private void FindUnmatchedVertices(Vertex[] vertices)
        {
            Vertex referenceVertex = vertices[0];
            // either using the X or Y coordinate of the reference vertex
            // we check to find which other Xs or Ys are unmatched

            IEnumerable<Vertex> unmatchedXSet = vertices.Where(x => x.YCoord != referenceVertex.YCoord);
            IEnumerable<Vertex> unmatchedYSet = vertices.Where(y => y.YCoord != referenceVertex.YCoord);

            // set the unmatched X vertex
            switch (unmatchedXSet.ToList().Count)
            {
                // if we found 2 that didn't match, the reference vertex contained the unmatched X
                case 2:
                    _unmatchedX = referenceVertex;
                    break;
                // if we only found 1 that didn't match, set it as the unmatched X
                case 1:
                    _unmatchedX = unmatchedXSet.First();
                    break;
                default:
                    throw new Exception("Not a valid triangle.");
            }

            // set the unmatched Y vertex

            switch (unmatchedYSet.ToList().Count)
            {
                // if we found 2 that didn't match, the reference vertex contained the unmatched Y
                case 2:
                    _unmatchedY = referenceVertex;
                    break;
                // if we only found 1 that didn't match, set it as the unmatched Y
                case 1:
                    _unmatchedY = unmatchedYSet.First();
                    break;
                default:
                    throw new Exception("Not a valid triangle.");
            }
        }

        private bool ValidateCoordinateProximity(int unmatchedCoord, int matchedCoord)
        {
           // check to make sure that X or Y coords are not more than 1 apart
           int differenceX = unmatchedCoord - matchedCoord;

           if (differenceX > 1 || differenceX < -1)
           {
               throw new Exception("Not the right kind of triangle.");
           }
           else
           {
               return true;
           }
           
        }

        private char SetRowName()
        {
            char rowName;
            int unmatchedYCoord = _unmatchedY.YCoord;
            int compareUnmatchedY = _unmatchedY.YCoord.CompareTo(_matchedY.YCoord);

            // Row lettering starts at the top of the grid, so we set that
            int gridHeight = Grid.GridHeight;
            // A = 65
            int charNum = 65 + gridHeight;
            int rowNameNumber;

            if (compareUnmatchedY < 0)
            {
                // If the non-equal Y coordinate is less than the equal Y coords
                // then the equal coords are on the top side of the triangle
                // and the distance from X axis is equal to the matched Y coords
                rowNameNumber = charNum - unmatchedYCoord-1;
            }

            else
            {
                // Or if not, and the unmatched Y coord is greater than the 
                // matched coords, the matched set are on the bottom side and
                // the row number is equal to the matched Y coordinates + 1
                rowNameNumber = charNum - unmatchedYCoord;
            } 
            
            rowName = (char)rowNameNumber;

            return rowName;

        }

        private int SetColumnNumber()
        {
            int colNumber;
            int unmatchedXCoord = _unmatchedX.XCoord;
            int compareUnmatchedX = _unmatchedX.XCoord.CompareTo(_matchedX.XCoord);

            if (compareUnmatchedX < 0)
            {
                // If the unmatched X coordinate is less than the matched X coords
                // then it is on the left side of the triangle, so we double the 
                // number and then add 1
                colNumber = (unmatchedXCoord * 2)+1;
            }
            else
            {
                // Or if not, and the unmatched X coordinate is greater than the 
                // matched X coords, the unmatched coordinate is on the right, so
                // simply double that number to get the column
                colNumber = (unmatchedXCoord * 2);
            }

            return colNumber;
        }
       
        #endregion
        
    }
}
