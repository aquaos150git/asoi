using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seal_trigger : MonoBehaviour
{
    private int _count;
    public bool IsTrigger;
    public GameObject PressTrigger;

    private void OnTriggerEnter(Collider other)
    {
        var Name = other.gameObject.name;

        if (IsTrigger && (Name.Equals("new_s1") || Name.Equals("new_s2") || Name.Equals("new_s3")))
        {
            VR_Player.Instance.DetachFromHand(other.gameObject);
            other.gameObject.GetComponent<FixedInteractable>().enabled = false;
            other.gameObject.GetComponent<FixedThrowable>().enabled = false;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.GetComponent<Animation>().Play();
            _count++;

            if (_count == 3)
            {
                PressTrigger.GetComponent<BoxCollider>().enabled = true;
                PressTrigger.GetComponent<PressTrigger>().IsTrigger = true;
            }
        }
    }
}
