using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopCase : MonoBehaviour
{
    private FixedCircularDrive _fixedCircular;
    private BoxCollider _trigger;
    public float UsageAngle = 90f;

    private void Start() 
    {
        _fixedCircular = GetComponent<FixedCircularDrive>();
        _trigger = GetComponent<BoxCollider>();
    }

    public void CheckAngle()
    {
        float OutAngle = Mathf.Abs(_fixedCircular.OutAngle);

        if (OutAngle >= UsageAngle) _trigger.enabled = true;
        else _trigger.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tablet"))
            if (other.TryGetComponent<Tablet>(out Tablet _tablet))
            {
                _tablet.Hang();
                _tablet.ChangeTag();
            }
    }
}
