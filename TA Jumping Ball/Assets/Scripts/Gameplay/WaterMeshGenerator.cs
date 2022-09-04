using UnityEngine;

public class WaterMeshGenerator : MonoBehaviour
{
    [SerializeField] private Vector2 _leftLowerBound;
    [SerializeField] private Vector2 _rightUpperBound;
    [SerializeField] private Color _upperColor;
    [SerializeField] private Color _lowerColor;
    [SerializeField,Min(2)] private int _linesCount = 2;
    [SerializeField] private MeshFilter _meshFilter;

    private void Awake()
    {
        _CreateMesh();
    }

    private void _CreateMesh()
    {
        int verticesCount = _linesCount * 2;
        int reducedLinesCount = _linesCount - 1;
        float step = 1f / reducedLinesCount;
        int triangleIndicesCount = reducedLinesCount * 6;
        Vector3[] vertices = new Vector3[verticesCount];
        Color[] colors = new Color[verticesCount];
        int[] triangles = new int[triangleIndicesCount];

        Vector3 leftLower = _leftLowerBound;
        Vector3 leftUpper = new Vector2(_leftLowerBound.x, _rightUpperBound.y);
        Vector3 rightLower = new Vector2(_rightUpperBound.x, _leftLowerBound.y);
        Vector3 rightUpper = _rightUpperBound;
        
        vertices[0] = leftLower;
        colors[0] = _lowerColor;

        vertices[1] = leftUpper;
        colors[1] = _upperColor;

        for (int i = 0; i < reducedLinesCount; ++i)
        {
            float factor = step * (i + 1);
            int verticesOffset = 2 + i * 2;

            vertices[verticesOffset] = Vector3.Lerp(leftLower, rightLower, factor);
            colors[verticesOffset] = _lowerColor;

            vertices[verticesOffset + 1] = Vector3.Lerp(leftUpper, rightUpper, factor);
            colors[verticesOffset + 1] = _upperColor;

            int trianglesOffset = i * 6;

            triangles[trianglesOffset] = verticesOffset - 2;
            triangles[trianglesOffset + 1] = verticesOffset - 1;
            triangles[trianglesOffset + 2] = verticesOffset;

            triangles[trianglesOffset + 3] = verticesOffset;
            triangles[trianglesOffset + 4] = verticesOffset - 1;
            triangles[trianglesOffset + 5] = verticesOffset + 1;
        }

        Mesh mesh = new Mesh();
        mesh.SetVertices(vertices);
        mesh.SetColors(colors);
        mesh.SetTriangles(triangles, 0);
        _meshFilter.mesh = mesh;
    }

    private void OnValidate()
    {
        if (_rightUpperBound.x < _leftLowerBound.x) _rightUpperBound.x = _leftLowerBound.x;
        if (_rightUpperBound.y < _leftLowerBound.y) _rightUpperBound.y = _leftLowerBound.y;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 leftLower = transform.position + new Vector3(_leftLowerBound.x, _leftLowerBound.y, 0f);
        Vector3 rightUpper = transform.position + new Vector3(_rightUpperBound.x, _rightUpperBound.y, 0f);
        Gizmos.DrawLine(leftLower, rightUpper);
    }
}