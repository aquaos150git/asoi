using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class DinamogrammRenderer : MonoBehaviour
{
    [HideInInspector] public GameObject DinoVar;
    public Material LineMaterial;
    private LineRenderer _lr;
    private Vector3[] points;
    private Vector3 _currentPos;

    private bool Draw;
    private int index = 1;
    private float speed = 0f;

    private void TuneLine()
    {
        _lr = DinoVar.GetComponent<LineRenderer>();
        _lr.startWidth = 0.002f;
        _lr.endWidth = _lr.startWidth;
    }

    public void GetPathDistance()
    {
        int iter = 0;
        float distance = 0f;

        while(iter < points.Length - 1)
        {
            distance += Vector3.Distance(points[iter], points[iter + 1]);
            Debug.Log(distance);
            iter++;
        }

        speed = distance / 7f;
    }

    public void SetUpLine(Vector3[] points, Polyline DinoVar)
    {
        this.DinoVar = DinoVar.gameObject;
        this.points = points;
        _lr = DinoVar.GetComponent<LineRenderer>();
        _lr.positionCount = 2;
        _currentPos = points[0];
        TuneLine();
        GetPathDistance();
    }

    public void DrawIt()
    {
        for (int i = 0; i < points.Length; i++)
        {
            _lr.SetPosition(i, points[i]);
        }
    }

    public void SetDraw()
    {
        _lr.SetPosition(0, points[0]);
        Draw = true;
    }

    private void Update()
    {
        if (!Draw) return;
        _currentPos = Vector3.MoveTowards(_currentPos, 
        points[index], speed * Time.deltaTime);
        _lr.SetPosition(index, _currentPos);

        if (Vector3.Distance(_currentPos, points[index]) < 4f)
        {
            if (index == points.Length - 1) { Draw = false; }
            else 
            {
                _lr.positionCount++;
                index++;
            }
        }
    }
}
