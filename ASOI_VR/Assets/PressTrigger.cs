using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

public class PressTrigger : MonoBehaviour
{
    public bool IsTrigger;
    public GameObject press;
    public Fixator fixator;

    private void OnTriggerEnter(Collider other)
    {
        if (IsTrigger)
        {
            GameObject tool = other.gameObject;

            if (tool.name == "press")
            {
                tool.GetComponent<Press>().SetTool();
                IsTrigger = false;
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        GameObject tool = other.gameObject;

        if (tool.name == "press")
        {
            if (!tool.GetComponent<FixedLinearDrive>().enabled)
            {
                tool.GetComponent<FixedThrowable>().onDetachFromHand.AddListener(TurnOffKinematicPress);
                fixator.GetComponent<FixedThrowable>().onDetachFromHand.AddListener(TurnOffKinematicFixator);

                fixator.GetComponent<FixedInteractable>().enabled = true;
                fixator.GetComponent<FixedThrowable>().enabled = true;

                fixator.IsHowered = true;
            }
        }
    }

    public void TurnOffKinematicPress()
    {
        press.GetComponent<Rigidbody>().isKinematic = false;
    }

    public void TurnOffKinematicFixator()
    {
        fixator.gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
}
