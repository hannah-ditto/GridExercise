namespace TriangleTime.Logic
{
    public interface IShape
    {
        #region Properties
        Vertex[] Vertices { get; set; }
        string ShapeName { get; set; }
        int SideLengthInPx { get; set; }
        #endregion

        #region Public Methods

        bool ValidateShapeCoordinates(Vertex[] vertices);
        string ConvertCoordinatesToShapeName(Vertex[] vertices);
        Vertex[] ConvertShapeNameToCoordinates(string shapeName);

        #endregion

    }
}