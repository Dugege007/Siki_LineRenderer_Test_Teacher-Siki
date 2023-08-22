using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mspaint : MonoBehaviour
{
    private Color paintColor = Color.red;
    private float paintSize = 0.1f;
    private LineRenderer currentLine;
    public Material lineMaterial;
    private List<Vector3> points = new List<Vector3>();
    private bool isMouseDown = false;
    private Vector3 lastMousePosition = Vector3.zero;
    private float lineDistance = -0.02f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject go = new GameObject();
            go.transform.parent = transform;

            currentLine = go.AddComponent<LineRenderer>();
            currentLine.material = lineMaterial;
            currentLine.startWidth = paintSize;
            currentLine.endWidth = paintSize;
            currentLine.startColor = paintColor;
            currentLine.endColor = paintColor;
            currentLine.numCornerVertices = 5;
            currentLine.numCapVertices = 5;
            lineDistance -= 0.02f;

            Vector3 position = GetMousePoint();
            AddPosition(position);
            isMouseDown = true;
        }

        if (isMouseDown)
        {
            Vector3 position = GetMousePoint();
            if (Vector3.Distance(position, lastMousePosition) >= 0.1f)
                AddPosition(position);
        }

        if (Input.GetMouseButtonUp(0))
        {
            currentLine = null;
            points.Clear();
            isMouseDown = false;
        }
    }

    private Vector3 GetMousePoint()
    {
        //return Camera.main.WorldToScreenPoint(Input.mousePosition);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool isCollider = Physics.Raycast(ray, out hit);
        if (isCollider)
            return hit.point + Vector3.back * 0.1f;
        return Vector3.zero;
    }

    private void AddPosition(Vector3 position)
    {
        position.z += lineDistance;
        points.Add(position);
        currentLine.numPositions = points.Count;
        currentLine.SetPositions(points.ToArray());
        lastMousePosition = position;
    }

    #region OnValueChangedMethod
    public void OnRedColorSelected(bool isOn)
    {
        if (isOn)
        {
            paintColor = Color.red;
            Debug.Log("Color: Red");
        }
    }

    public void OnGreenColorSelected(bool isOn)
    {
        if (isOn)
        {
            paintColor = Color.green;
            Debug.Log("Color: Green");
        }
    }

    public void OnBlueColorSelected(bool isOn)
    {
        if (isOn)
        {
            paintColor = Color.blue;
            Debug.Log("Color: Blue");
        }
    }

    public void OnThinWidthSelected(bool isOn)
    {
        if (isOn)
        {
            paintSize = 0.1f;
            Debug.Log(paintSize);
        }
    }

    public void OnMidWidthSelected(bool isOn)
    {
        if (isOn)
        {
            paintSize = 0.3f;
            Debug.Log(paintSize);
        }
    }

    public void OnFatWidthSelected(bool isOn)
    {
        if (isOn)
        {
            paintSize = 0.5f;
            Debug.Log(paintSize);
        }
    }
    #endregion
}
