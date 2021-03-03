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
        private Vertex[] _vertices = new Vertex[3];

        #endregion

        #region Properties

        public Vertex[] Vertices
        {
            get => Grid.MultiplyByPixelLength(_vertices);
            set => _vertices = value;
        }

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
            if (ValidateShapeCoordinates(vertices))
            {
                Vertices = vertices;
                ShapeName = ConvertCoordinatesToShapeName(vertices);
            }
            
        }
        
        public Triangle(string shapeName)
        {
            ShapeName = shapeName;
            Vertices = ConvertShapeNameToCoordinates(shapeName);
        }

        #endregion

        #region Public Methods
        
        public bool ValidateShapeCoordinates(Vertex[] vertices)
        {
            
            // this must remain true for the entire duration of this method in order to validate
            bool isTriangle;
            try
            {
                SetMatchStatusForVertices(vertices, out _unmatchedX,
                    out _unmatchedY, out _matchedX,
                    out _matchedY);

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
        public Vertex[] ConvertShapeNameToCoordinates(string shapeName)
        {
            SplitTriangleName(ShapeName);
            AddVertices();
            return AddVertices();
        }
        public string ConvertCoordinatesToShapeName(Vertex[] vertices)
        {
            ColNumber = SetColumnNumber();
            RowName = SetRowName();
            return RowName.ToString() + ColNumber;
        }


        #endregion

        #region Private Methods

        // for converting from name to coordinates
        private void SplitTriangleName(string TriangleName)
        {
            Regex r = new Regex(@"(?<Col>[A-Z]+)(?<Row>[0-9]+)",
                RegexOptions.Compiled);

            string rowMatch= r.Match(TriangleName).Groups["Col"].Value;
            string colMatch= r.Match(TriangleName).Groups["Row"].Value;

            RowName = Convert.ToChar(rowMatch);
            ColNumber = Convert.ToInt32(colMatch);
        }

  
        private Vertex[] AddVertices()
        {
            SetXCoordinates();
            SetYCoordinates();

            Vertex[] vertexArr =
            {
               new Vertex(0,0),
               new Vertex(0,0),
               new Vertex(0,0)
            };

            vertexArr[0].XCoord = _matchedX.XCoord;
            vertexArr[0].YCoord = _matchedY.YCoord;

            vertexArr[1].XCoord = _unmatchedX.XCoord;
            vertexArr[1].YCoord = _matchedY.YCoord;

            vertexArr[2].XCoord = _matchedX.XCoord;
            vertexArr[2].YCoord = _unmatchedY.YCoord;

            return vertexArr;
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

        // for converting from coordinates to name
        private static void SetMatchStatusForVertices(Vertex[] vertices, out Vertex unmatchedX, out Vertex unmatchedY, out Vertex matchedX, out Vertex matchedY)
        {

            Vertex referenceVertex = vertices[0];
            // either using the X or Y coordinate of the reference vertex
            // we check to find which other Xs or Ys are unmatched

            IEnumerable<Vertex> unmatchedXSet = vertices.Where(x => x.XCoord != referenceVertex.XCoord);
            IEnumerable<Vertex> unmatchedYSet = vertices.Where(y => y.YCoord != referenceVertex.YCoord);

            // set the unmatched X vertex
            switch (unmatchedXSet.ToList().Count)
            {
                // if we found 2 that didn't match, the reference vertex contained the unmatched X
                case 2:
                    unmatchedX = referenceVertex;
                    break;
                // if we only found 1 that didn't match, set it as the unmatched X
                case 1:
                    unmatchedX = unmatchedXSet.First();
                    break;
                // if we find anything else, this is not a valid triangle
                default:
                    throw new Exception("Not a valid triangle.");
            }

            // set the unmatched Y vertex
            switch (unmatchedYSet.ToList().Count)
            {
                // if we found 2 that didn't match, the reference vertex contained the unmatched Y
                case 2:
                    unmatchedY = referenceVertex;
                    break;
                // if we only found 1 that didn't match, set it as the unmatched Y
                case 1:
                    unmatchedY = unmatchedYSet.First();
                    break;
                // if we find anything else, this is not a valid triangle
                default:
                    throw new Exception("Not a valid triangle.");
            }

            int unmatchedXCoord = unmatchedX.XCoord;
            int unmatchedYCoord = unmatchedY.YCoord;


            // once we know which are the unmatched, we can pick from the other 2 in the set to define the 'matched' number
            matchedY = vertices.First(y => y.YCoord != unmatchedYCoord);
            matchedX = vertices.First(x => x.XCoord != unmatchedXCoord);




        }

        private static bool ValidateCoordinateProximity(int unmatchedCoord, int matchedCoord)
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
            int matchedXCoord = _matchedX.XCoord;
            int compareMatchedX = _matchedX.XCoord.CompareTo(_unmatchedX.XCoord);

            if (compareMatchedX < 0)
            {
                // If the matched X coordinate is less than the unmatched X coords
                // then they are on the left side of the triangle, so we double the 
                // matched X coord to get the preceeding column and add 1 to get
                // the current column
 
                colNumber = (matchedXCoord *2)+1;
            }
            else
            {
                // Or if not, and the matched X coordinates are greater than the 
                // unmatched X coord, then the matched coordinates are on the right, so
                // we double for the matched X coordinate to get the current column
                colNumber = (matchedXCoord * 2);
            }

            return colNumber;
        }
       
        #endregion
        
    }
}
