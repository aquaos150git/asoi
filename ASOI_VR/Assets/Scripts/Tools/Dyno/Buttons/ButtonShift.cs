using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonShift : DynamographButton
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

        switch (dynamograph.State.WindowId)
        {
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
                dynamograph.State = new EnterShift1State(dynamograph);
                break;
            case 11:
                dynamograph.State = new EnterShift2State(dynamograph);
                break;
            case 12:
                dynamograph.State.Redo(dynamograph);
                break;
            case 13:
                dynamograph.State.Redo(dynamograph);
                break;
            /*case 14: 
                dynamograph.State = new PaintingDynomogramState(dynamograph);
                break;*/
        }
    }
}
