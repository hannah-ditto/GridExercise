using System;

namespace TriangleTime.Logic
{
    public static class Grid
    {

        #region Properties
        public static int SideLengthInPx { get; set; } = 10;
        public static int GridHeight { get; set; } = 6;
        #endregion

        #region Public Methods

        // for converting to correct pixel length 

        public static Vertex[] MultiplyByPixelLength(Vertex[] vertices)
        {
            foreach (Vertex vertex in vertices)
            {
                vertex.XCoord = vertex.XCoord * SideLengthInPx;
                vertex.YCoord = vertex.YCoord * SideLengthInPx;
            }

            return vertices;
        }

        public static Vertex[] DivideByPixelLength(Vertex[] vertices)
        {
            foreach (Vertex vertex in vertices)
            {

                if (vertex.XCoord % SideLengthInPx != 0 || vertex.YCoord % SideLengthInPx != 0)
                {
                    throw new Exception("Not valid coordinates.");
                }

                vertex.XCoord = vertex.XCoord / SideLengthInPx;
                vertex.YCoord = vertex.YCoord / SideLengthInPx;
            }

            return vertices;
        }
        
        #endregion
    }
}