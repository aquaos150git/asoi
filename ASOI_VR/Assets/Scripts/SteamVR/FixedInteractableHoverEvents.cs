using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class FixedInteractableHoverEvents : InteractableHoverEvents
{
    protected void Start()
    {

    }
    protected override void OnHandHoverBegin()
    {
        if (enabled) base.OnHandHoverBegin();
    }

    protected override void OnHandHoverEnd()
    {
        if(enabled) base.OnHandHoverEnd();
    }

    protected override void OnAttachedToHand(Hand hand)
    {
        if (enabled) base.OnAttachedToHand(hand);
    }

    protected override void OnDetachedFromHand(Hand hand)
    {
        if (enabled) base.OnDetachedFromHand(hand);
    }
}
