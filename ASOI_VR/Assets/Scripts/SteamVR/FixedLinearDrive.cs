using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class FixedLinearDrive : LinearDrive
{
    protected override void HandAttachedUpdate(Hand hand)
    {
        if (enabled) base.HandAttachedUpdate(hand);
    }

    protected override void HandHoverUpdate(Hand hand)
    {
        if (enabled) base.HandHoverUpdate(hand);
    }
}
