using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Button : MonoBehaviour
{
    private bool BalanceTrigger = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Balansir"))
        {
            BalanceTrigger = true;
            Debug.Log(BalanceTrigger);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Balansir"))
        {
            BalanceTrigger = false;
            Debug.Log(BalanceTrigger);
        }
    }

}
