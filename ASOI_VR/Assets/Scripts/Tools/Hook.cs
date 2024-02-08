using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    private int count;
    public bool HookEnabled;
    public Collider SCollider;
    private void OnTriggerEnter(Collider other)
    {
        if (!HookEnabled) return;
        var Name = other.gameObject.name;
        if (other.GetComponent<BoxCollider>() && (Name.Equals("old_s1") || Name.Equals("old_s2") || Name.Equals("old_s3")))
        {
            Debug.Log("Должен выйти старый сальник");
            Destroy(other.gameObject.GetComponent<BoxCollider>());
            other.gameObject.GetComponent<Animation>().Play();
            other.gameObject.GetComponent<FixedInteractable>().enabled = true;
            other.gameObject.GetComponent<FixedThrowable>().enabled = true;
            count++;
            if (count >= 3) 
            { 
                SCollider.enabled = true;
                SCollider.isTrigger = true;
            }
        }

    }
}
