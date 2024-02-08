using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Valve.VR.InteractionSystem;

public class TriggerFixator : MonoBehaviour
{
    public GameObject Stakan;
    private LinearMapping _linearMapping;
    private FixedLinearDrive _linearDrive;
    private Collider _collider;
    private bool _fixatorAttached;
    public Collider Hook;

    private Transform _transform;

    private void Start()
    {
        _linearMapping = Stakan.GetComponent<LinearMapping>();
        _linearDrive = Stakan.GetComponent<FixedLinearDrive>();
        _collider = GetComponent<Collider>();
        _transform = GetComponent<Transform>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_fixatorAttached) return;
        GameObject tool = other.gameObject;
        Debug.Log(tool.name);
        if (tool.name == "fixator#1" && _linearMapping.value >= 0.5f)
        {
            _fixatorAttached = true;
            Stakan.GetComponent<StakanStateManager>().FinalPose = new Vector3(0, 0, 0.39f);
            _linearDrive.startPosition = _transform;
            tool.GetComponent<Fixator>().SetTool();
            Hook.GetComponent<Hook>().HookEnabled = true;
        }
    }

    public void NewEndPosition()
    {
        if (_fixatorAttached)
        {
            //_linearDrive.startPosition = _transform;
        }
    }
}
