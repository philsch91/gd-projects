# Curves and Splines

## Bezier Curves

Used for a curve or change in direction by defining a path from the first to the last point with points in between.

#### Types

- Quadratic functions with 3 points
- Cubic functions with 4 points

It is possible to calculcate the first derivative for these polynomial functions.
The results of the first derivative for the functions are lines tangents to the curve. This tangents can be used for the velocity or speed when moving on the curve. Normalizing the velocity induces the direction of the curve.

## Splines

Combine two or more curves by appending them into a single one.
Although the spline is continuous by appending the curves, it sharply changes the direction between the curve sections because of the sudden change of direction and speed of the shared control point for each curve. 

#### Smooth Splines

- Mirror the third point of the previous curve and the second point of the following curve around the shared point so that the velocity for both points is equal. The combined first and second derivatives are continous.
- Align the third point of the previous curve and the second point of the following curve. With that, the velocity changes abrubt, but the direction is continuous. The combined first derivative is continuous, while the second derivate is not.

## Tests

### Spline

- Create an empty game object and add an BezierSpline script as a component to it
- Press the Add Curve button to add an additional curve with 3 points to the game object
- Select the points in the scene and change the position of them via the BezierSpline script in the inspector

1. x = 1, y = 0
2. x = 2, y = 1
3. x = 3, y = -2
4. x = 4, y = 2
5. x = 5, y = 3
6. x = 6, y = 1
7. x = 7, y = 0

- The spline consisting of two or more curves could be not smooth, depending on the position of the points.
- Select the point of the spline that causes the unsmooth transition and change the mode to Mirrored or Aligned
- The spline is then recalculated for a complete smooth curve
- The previous and following point of the control point are recalculated
- The color of the recalculated points is set to Cyan for Mirrored and Yellow for Aligned

### Spline Loop

- Create an empty game object and add an BezierSpline script as a component to it
- Press the Add Curve button to add an additional curve with 3 points to the game object
- Select the points in the scene and change the position of them via the BezierSpline script in the inspector

1. x = 1, y = 0, z = 0
2. x = 2, y = 0, z = 2
3. x = 3, y = 0, z = 3
4. x = 4, y = 0, z = 0
5. x = 3.5, y = 0, z = -3
6. x = 3, y = 0, z = -2
7. x = 1, y = 0, z = -1

- Select the last point (7) and enable the Loop checkbox
- With that, the position of the last point is then set to the position of the first point
- Click in the highlighted center of the big (first) dot to select the last point
- Click in the surrounding area of the center of the big (first) dot to select the first point
- Select the last point and change the mode to Mirrored or Aligned
- The spline is then recalculated for a smooth curve on that point
