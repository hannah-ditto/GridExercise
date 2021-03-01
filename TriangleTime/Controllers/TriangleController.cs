using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TriangleTime.Logic;

namespace TriangleTime.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TriangleController : ControllerBase
    {
        #region Fields
        
        private readonly ILogger<TriangleController> _logger;

        #endregion

        #region Construction/Finalization

        public TriangleController(ILogger<TriangleController> logger)
        {
            _logger = logger;
        }

        #endregion

        #region Routes

        [HttpPost("~/UpdatePx")]
        public int UpdatePxLength(int newLength)
        {
            return Grid.SideLengthInPx = newLength;
        }

        [HttpPost("~/UpdateGridHeight")]
        public void UpdateGridHeight(int newHeight)
        {
            if (newHeight > 26 || newHeight < 0)
            {
                _logger.Log(LogLevel.Error,"Heights greater than 26 or less than 0 are ignored.");
            }
            else
            {
                Grid.GridHeight = newHeight;
            }
        }

        [HttpGet("~/ByName")]
        public Triangle GetTriangleByName(string name)
        {
            Triangle triangle = new Triangle(name);
            return triangle;
        }

        [HttpGet("~/ByCoords")]
        public Triangle GetTriangleByCoordinates([FromQuery] int x1, int y1, int x2, int y2, int x3, int y3)
        {

            Vertex[] vertices = new Vertex[]
            {
                (new Vertex(x1, y1)),
                (new Vertex(x2, y2)),
                (new Vertex(x3, y3))
            };

            Triangle triangle = new Triangle(vertices);
            return triangle;
        }
        
        #endregion

    }
}
