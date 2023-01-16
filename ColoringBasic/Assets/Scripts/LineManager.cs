using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    [SerializeField] private Line line;
    private Line currentLine;
    private Camera cam;
    [Range(0.01f, 1)]
    public float lineResolution = 0.8f;
    public static LineManager instance;
    private float offset = 2f;
    private Collider2D currentCollider;
    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(instance);
        cam = Camera.main;
    }

    void Update()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if (Input.GetMouseButtonDown(0))
        {
            if (hit.collider != null)
            {
                currentLine = Instantiate(line, mousePos, Quaternion.identity);
                currentLine.DrawPolygon(24, 0.05f, mousePos);
                currentLine = null;
                // currentLine.SetPosition(mousePos * 1.01f);
                currentCollider = hit.collider;
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (currentCollider == hit.collider)//to check the color stay only inside intended collider(selected part of the image)
            {
                if (currentLine)
                {
                    currentLine.SetPosition(mousePos);
                }
                else
                {
                    currentLine = Instantiate(line, mousePos, Quaternion.identity);
                }
            }
        }
        if (hit.collider == null)
        {
            currentLine = null;
        }
    }

}
