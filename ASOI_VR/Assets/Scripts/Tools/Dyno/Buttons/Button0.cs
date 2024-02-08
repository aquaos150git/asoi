using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button0 : DynamographButton
{
    protected override void Start()
    {
        base.Start();

        hoverEvents.onHandHoverBegin.AddListener(
            delegate { Click(2f, fixedInteractable, hoverEvents); });
    }

    public override void Click(float delay, FixedInteractable fixedInteractable, FixedInteractableHoverEvents fixedInteractableHoverEvents)
    {
        base.Click(delay, fixedInteractable, fixedInteractableHoverEvents);

        PickWindow(dynamograph.State.WindowId, 2);

        if(dynamograph.State.WindowId == 14)
            dynamograph.State = new SaveDynomogramState(dynamograph);
    }
}
