using System.Collections.Generic;
using UnityEngine;

public class GJKPhysic2D : MonoBehaviour
{
    public const int MaxIterations = 30;
    public static bool IsColliding(IVertexShape shapeA, IVertexShape shapeB)
    {
        Vector2 direction = Random.insideUnitCircle.normalized;
        List<Vector2> simplex = new List<Vector2>();

        Vector2 point = support(shapeA, shapeB, direction);
        simplex.Add(point);
        direction = -point;

        for (int i = 0; i < MaxIterations; i++)
        {
            point = support(shapeA, shapeB, direction);
            if (Vector2.Dot(point, direction) <= 0)
            {
                return false; // No collision
            }

            simplex.Add(point);

            if (UpdateSimplexAndDirection(ref simplex, ref direction))
            {
                return true; // Collision detected
            }
        }
        return false; // No collision after max iterations
    }
    private static bool UpdateSimplexAndDirection(ref List<Vector2> simplex, ref Vector2 direction)
    {
        if (simplex.Count == 2) 
        {
            Vector2 a = simplex[1];
            Vector2 b = simplex[0];
            Vector2 ab = b - a;
            Vector2 ao = -a; // Origin to point A

            direction = Vector2.Perpendicular(ab).normalized;
        }
        return false;
    }
    private static Vector2 support(IVertexShape shapeA, IVertexShape shapeB, Vector2 direction)
    {
        Vector2 pointA = shapeA.GetSupportPoint(direction);
        Vector2 pointB = shapeB.GetSupportPoint(-direction);
        return pointA - pointB;
    }
}