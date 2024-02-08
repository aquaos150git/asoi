using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamographButton : MonoBehaviour
{
    protected FixedInteractableHoverEvents hoverEvents;
    protected FixedInteractable fixedInteractable;
    [SerializeField] protected Dynamograph dynamograph;
    [SerializeField] protected AudioSource _audioSource;

    protected virtual void Start()
    {
        hoverEvents = GetComponent<FixedInteractableHoverEvents>();
        fixedInteractable = GetComponent<FixedInteractable>();
    }

    public virtual void Click(float delay, FixedInteractable fixedInteractable,
        FixedInteractableHoverEvents fixedInteractableHoverEvents)
    {
        StartCoroutine(ClickDelay(delay, fixedInteractable, fixedInteractableHoverEvents));
        PlayClickAudio();
    }

    protected IEnumerator ClickDelay(float delay, FixedInteractable fixedInteractable,
        FixedInteractableHoverEvents fixedInteractableHoverEvents)
    {
        fixedInteractableHoverEvents.enabled = false;
        fixedInteractable.enabled = false;
        yield return new WaitForSeconds(delay);
        fixedInteractable.enabled = true;
        fixedInteractableHoverEvents.enabled = true;
    }

    public void PlayClickAudio() => _audioSource.Play();

    public virtual void NextWindow(Dynamograph dynamograph, int WindowId)
    {
        switch (WindowId)
        {
            case 2:
                dynamograph.State = new EnterWellNumberState(dynamograph);
                break;
            case 3:
                dynamograph.State = new EnterBushNumberState(dynamograph);
                break;
            case 4:
                dynamograph.State = new EnterFieldNumberState(dynamograph);
                break;
            case 5:
                dynamograph.State = new EnterResearchTypeState(dynamograph);
                break;
            case 6:
                dynamograph.State = new EnterStemDiametrState(dynamograph);
                break;
            case 7:
                dynamograph.State = new EnterCrankHoleState(dynamograph);
                break;
            case 8:
                dynamograph.State = new EnterCyclesState(dynamograph);
                break;
            case 9:
                dynamograph.State = new EnterBufferPressureState(dynamograph);
                break;
            case 10:
                dynamograph.State = new EnterShift1State(dynamograph);
                break;
        }
    }

    public void PickWindow(int CurrentWindowId, int PickedWindowId)
    {
        switch (CurrentWindowId)
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
                NextWindow(dynamograph, PickedWindowId);
                break;
        }
    }

}
