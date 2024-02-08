using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Fixator : MonoBehaviour
{
    private FixedThrowable _throwable;
    private Rigidbody _rigidbody;
    private Vector3 _transformTrigger = new Vector3(39.1154f, 1.4265f, 45.8696f);
    private Vector3 _angleTrigger = new Vector3(-90f, 0f, 0f);
    private Transform _transform;
    private FixedInteractable _interactable;
    public Transform Trigger;

    public Transform StartFinal;
    public StakanStateManager ssm;
    public LinearDrive Stakan;


    private Collider[] _colliders;
    private Collider _collider1;
    private Collider _collider2;

    public bool IsHowered;

    private void Start()
    {
        _throwable = GetComponent<FixedThrowable>();
        _interactable = GetComponent<FixedInteractable>();
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();

        _colliders = GetComponentsInChildren<Collider>();
        _collider1 = _colliders[0];
        _collider2 = _colliders[1];
    }
    public void SetTool()
    {
        Debug.Log("Скрипт фиксатора");
        VR_Player.Instance.DetachFromHand(this.gameObject);
        gameObject.transform.parent = null;
       
        //_collider1.enabled = false;
        //_collider2.enabled = false;

        _interactable.enabled = false;
        _throwable.enabled = false;
        _rigidbody.isKinematic = true;

        _transform.position = _transformTrigger;
        _transform.eulerAngles = _angleTrigger;
        //_transform.SetParent(Trigger);

    }

    public void GetTool()
    {
        if (IsHowered) 
        {
            Stakan.startPosition = StartFinal;
            ssm.FinalPose = Vector3.zero;
            IsHowered = false;
            ssm.IsTouchable = true;
        }
    }
}
