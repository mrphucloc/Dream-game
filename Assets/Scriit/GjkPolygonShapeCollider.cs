using UnityEngine;
namespace GJKPhysic2D
{
    public interface IVertexShape
    {
        Vector2 Support(Vector2 direction);
        void OnShappeTriggerEnter(GjkPolygonShapeCollider other);
    }
}

public class GjkPolygonShapeCollider : MonoBehaviour, IVertexShape
{
    [SerializeField] private Vector2[] _vertices;

    public Color DebugColor = Color.green;

    public Vector2 Support(Vector2 direction)
    {
        Vector2 worldDirection = direction.normalized;
        float maxDot = float.NegativeInfinity;
        Vector2 bestPoint = Vector2.zero;

        foreach (Vector2 vertex in _vertices)
        {
            Vector2 worldVertex = transform.TransformPoint(vertex);
            float dot = Vector2.Dot(worldVertex, worldDirection);

            if (dot > maxDot)
            {
                maxDot = dot;
                bestPoint = worldVertex;
            }
        }
        return bestPoint;
    }
    private void OnDrawGizmos()
    {
        if(_vertices == null || _vertices.Length < 2)
        {
            return;
        }

        Gizmos.color = DebugColor;
        for (int i = 0; i < _vertices.Length; i++) 
        { 
            Vector2 localStart = _vertices[i];
            Vector2 localEnd = _vertices[(i + 1) % _vertices.Length];

            Vector2 worldStart = transform.TransformPoint(localStart);
            Vector2 worldEnd = transform.TransformPoint(localEnd);
        }

    }
    public void OnShappeTriggerEnter(GjkPolygonShapeCollider other)
    {
        DebugColor = Color.red;
    }
}
