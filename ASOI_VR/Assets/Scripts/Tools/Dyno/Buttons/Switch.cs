using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : DynamographButton
{
    private Animator _animator;
    private bool isOn;

    protected override void Start()
    {
        base.Start();
        _animator = GetComponent<Animator>();
        isOn = false;

        hoverEvents.onHandHoverBegin.AddListener(
            delegate { Click(2f, fixedInteractable, hoverEvents); });
    }

    public override void Click(float delay, FixedInteractable fixedInteractable,
        FixedInteractableHoverEvents fixedInteractableHoverEvents)
    {
        base.Click(delay, fixedInteractable, fixedInteractableHoverEvents);
        isOn = !isOn;
        _animator.SetTrigger("Switch");

        if (isOn && dynamograph.State.GetType() == typeof(DisabledDynamographState))
            dynamograph.Redo();
        else dynamograph.TurnOff();
    }
}
