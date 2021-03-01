To use this app:
Load the project in VS and start debugging. This should open a webpage with swagger up, where you should be able to try getting a triangle by either its coordinates or its name (Row+Column).

Here are some sample calls:

Triangles by Name:
https://localhost:44356/ByName?name=A4
https://localhost:44356/ByName?name=E12
https://localhost:44356/ByName?name=E3

Triangles by Coordinates:
B7: https://localhost:44356/ByCoords?x1=30&y1=40&x2=40&y2=40&x3=30&y3=50
F6: https://localhost:44356/ByCoords?x1=20&y1=10&x2=30&y2=10&x3=30&y3=0

Update the Pixel Length to 5:
https://localhost:44356/UpdatePx?newLength=5

Triangle by Coordinates when the length is 5:
F6: https://localhost:44356/ByCoords?x1=10&y1=5&x2=15&y2=5&x3=15&y3=0

Update the Grid Height to a number between 1 and 26 instead to see that A will adjust accordingly:
https://localhost:44356/UpdateGridHeight?newHeight=20

Call A4 by name again to see the adjustment:
https://localhost:44356/ByName?name=A4

Notes:
For the time being, swagger is serving as a front-end. Happy to discuss any questions or take any feedback on the project overall. I tried to leave some comments throughout to describe how I was trying to solve the problem.

Overall, the calculation approach here is to look at which 2 vertices have matching X or matching Y coordinates, and use that to determine which direction the triangle is facing. From this information, we can translate back and forth between the column number or row number (later, translating this to the correct letter). See SetXCoordinates() or SetYCoordinates() for more comments around this.

Thanks for the fun puzzle!
