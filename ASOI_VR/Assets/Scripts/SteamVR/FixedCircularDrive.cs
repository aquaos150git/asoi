using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class FixedCircularDrive : CircularDrive
{

    protected override void UpdateAll()
    {
        if (enabled) base.UpdateAll();
    }

    protected override void OnHandHoverBegin(Hand hand)
    {
        if(enabled) base.OnHandHoverBegin(hand);
    }

    protected override void HandHoverUpdate(Hand hand)
    {
        if (enabled) base.HandHoverUpdate(hand);
    }

    protected override void HandAttachedUpdate(Hand hand)
    {
        base.HandAttachedUpdate(hand);
    }
}
