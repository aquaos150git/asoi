using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Press : MonoBehaviour
{
    private Vector3 _startPosition = new Vector3(39.11629f, 1.22f, 45.87f);
    private Vector3 _startRotation = new Vector3(270, 0, 0);

    [HideInInspector] public int Count;
    private bool _position; // 0 - Верхнее, 1 - Нижнее

    private LinearMapping _linearMapping;
    private FixedLinearDrive _linearDrive;
    private FixedThrowable _throwable;

    private Rigidbody _rigidbody;
    private Transform _transform;


    private void Start()
    {
        _linearDrive = GetComponent<FixedLinearDrive>();
        _linearMapping = GetComponent<LinearMapping>();
        _throwable = GetComponent<FixedThrowable>();

        _rigidbody= GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }

    public void SetTool()
    {
        VR_Player.Instance.DetachFromHand(this.gameObject);

        _throwable.enabled = false;
        _linearDrive.enabled = true;
        _rigidbody.isKinematic = true;
        _transform.position = _startPosition;
        _transform.localEulerAngles= _startRotation;
        gameObject.transform.parent = null;
    }

    private void GetTool()
    {
        _throwable.enabled = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (_linearMapping.value == 1 && !_position)
        {
            Count++;
            Debug.Log(Count);
            _position = true;
        }

        else if (_linearMapping.value <= 0.5f && _position) 
        { 
            _position = false; 
            Debug.Log("sssssssssssssssssssssssssss");

            if (Count == 5)
            {
                VR_Player.Instance.DetachFromHand(this.gameObject);
                _linearDrive.enabled = false;
                GetTool();
            }
        }
    }

}
