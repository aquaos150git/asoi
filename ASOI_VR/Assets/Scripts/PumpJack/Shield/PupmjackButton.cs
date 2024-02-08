using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PupmjackButton : MonoBehaviour
{
    private Animator _animator;
    private FixedInteractableHoverEvents _hoverEvents;
    private string _animationName;
    private AudioSource _audioSource;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _hoverEvents = GetComponent<FixedInteractableHoverEvents>();
        _audioSource = GetComponent<AudioSource>();
        _animationName = _animator.runtimeAnimatorController.animationClips[0].name;
        Debug.Log(_animationName);
    }

    public void Click()
    {
        StartCoroutine(ClickDelay());
        _animator.Play(_animationName, 0, 0.001f);
        _audioSource.Play();
        Debug.Log(_animationName + " true");
    }

    // Задержка для повторного нажатия
    private IEnumerator ClickDelay()
    {
        _hoverEvents.enabled = false;
        yield return new WaitForSeconds(2f);
        _hoverEvents.enabled = true;
    }
}
