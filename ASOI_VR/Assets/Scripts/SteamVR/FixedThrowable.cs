using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
public class FixedThrowable : Throwable
{

    protected void Start()
    {
        
    }

    protected override void OnAttachedToHand(Hand hand)
    {
        if (enabled) base.OnAttachedToHand(hand);
    }

    protected override void OnHandHoverBegin(Hand hand)
    {
        if (enabled) base.OnHandHoverBegin(hand);
    }

    protected override void HandHoverUpdate(Hand hand)
    {
        if (enabled) base.HandHoverUpdate(hand);
    }
}
