using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandBrake : MonoBehaviour
{

    [SerializeField] private PumpJack _pumpJack;
    [SerializeField] private AmperArrow _amperArrow;

    private FixedCircularDrive fixedCircularDrive;

    private void Start()
    {
        fixedCircularDrive = GetComponent<FixedCircularDrive>();
        fixedCircularDrive.onMinAngle.AddListener(
            delegate { _amperArrow.DefineBehaviour(false); });
        fixedCircularDrive.onMinAngle.AddListener(
            delegate { _pumpJack.DefineBehaviour(false); });


        fixedCircularDrive.onMaxAngle.AddListener(
            delegate { _amperArrow.AmperStop(); });
        fixedCircularDrive.onMaxAngle.AddListener(
            delegate { _amperArrow.DefineBehaviour(true); });
        fixedCircularDrive.onMaxAngle.AddListener(
            delegate { _pumpJack.DefineBehaviour(true); });
        fixedCircularDrive.onMaxAngle.AddListener(
            delegate { _pumpJack.StopAnim(); });
    }

}
