using UnityEngine;
public class Line : MonoBehaviour
{
    private LineRenderer lineRenderer;
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetPosition(Vector2 pos)
    {
        if (!CanAppend(pos)) return;
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, pos);
    }
    public void DrawPolygon(int vertexNumber, float radius, Vector3 centerPos)
    {
        if (!CanAppend(centerPos))
        {
            Debug.Log(":::DrawPolygon out");
            return;
        }
        lineRenderer.loop = true;
        float angle = 2 * Mathf.PI / vertexNumber;
        lineRenderer.positionCount = vertexNumber;

        for (int i = 0; i < vertexNumber; i++)
        {
            Matrix4x4 rotationMatrix = new Matrix4x4(new Vector4(Mathf.Cos(angle * i), Mathf.Sin(angle * i), 0, 0),
                                                     new Vector4(-1 * Mathf.Sin(angle * i), Mathf.Cos(angle * i), 0, 0),
                                       new Vector4(0, 0, 1, 0),
                                       new Vector4(0, 0, 0, 1));
            Vector3 initialRelativePosition = new Vector3(0, radius, 0);
            lineRenderer.SetPosition(i, centerPos + rotationMatrix.MultiplyPoint(initialRelativePosition));
        }
    }
    private bool CanAppend(Vector2 pos)
    {
        if (lineRenderer.positionCount == 0) return true;
        return Vector2.Distance(lineRenderer.GetPosition(lineRenderer.positionCount - 1), pos) > LineManager.instance.lineResolution;
    }
}
