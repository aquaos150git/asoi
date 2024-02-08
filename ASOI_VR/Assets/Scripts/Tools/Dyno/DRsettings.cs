using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DRsettings : MonoBehaviour
{

    [SerializeField] private Vector3[] points;
    [SerializeField] private DinamogrammRenderer line;
    [SerializeField] private Polyline DinoVar1;
    [SerializeField] private Polyline DinoVar2;
    [SerializeField] private Polyline DinoVar3;
    [SerializeField] private Polyline DinoVar4;
    [SerializeField] private Polyline DinoVar5;
    private System.Random randomDirection;
    private Polyline _pickedDinoVar;

    private void Start()
    {
        randomDirection = new System.Random();
        PickDinoVar();
        line.SetUpLine(points, _pickedDinoVar);
    }

    public void PickDinoVar()
    {
        int directionChoice = randomDirection.Next(1, 6);

        switch(directionChoice)
        {
            case 1:
                points = DinoVar1.nodes.ToArray();
                _pickedDinoVar = DinoVar1;
                break;
            case 2:
                points = DinoVar2.nodes.ToArray();
                _pickedDinoVar = DinoVar2;
                break;
            case 3:
                points = DinoVar3.nodes.ToArray();
                _pickedDinoVar = DinoVar3;
                break;
            case 4:
                points = DinoVar4.nodes.ToArray();
                _pickedDinoVar = DinoVar4;
                break;
            case 5:
                points = DinoVar5.nodes.ToArray();
                _pickedDinoVar = DinoVar5;
                break;
        }
    }

}
