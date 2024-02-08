using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamometerTrigger : MonoBehaviour
{
    private BoxCollider _trigger;

    private void Start()
    {
        _trigger = GetComponent<BoxCollider>();
    }

    public void EnableTrigger(int CrankPosition)
    {
        if (CrankPosition == 2) _trigger.enabled = true;
    }

    public void DisableTrigger(int CrankPosition)
    {
        _trigger.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dynamometer"))
            if (other.TryGetComponent<Dynamometer>(out Dynamometer dynamometer))
            {
                dynamometer.AttachDynamometer();
            }
    }
}
