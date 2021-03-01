namespace TriangleTime.Logic
{
    public class Vertex
    {
        #region Properties

        public int XCoord { get; set; }

        public int YCoord { get; set; }

        #endregion

        #region Construction/Finalization
        
        public Vertex(int x, int y)
        {
            XCoord = x;
            YCoord = y;
        }

        #endregion

    }
}
