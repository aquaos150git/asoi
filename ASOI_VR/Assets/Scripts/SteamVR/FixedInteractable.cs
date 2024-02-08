using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class FixedInteractable : Interactable
{
    protected override void OnAttachedToHand(Hand hand)
    {
        if (enabled) base.OnAttachedToHand(hand);
    }
    protected override void OnHandHoverBegin(Hand hand)
    {
        if (enabled) base.OnHandHoverBegin(hand);
    }
}
