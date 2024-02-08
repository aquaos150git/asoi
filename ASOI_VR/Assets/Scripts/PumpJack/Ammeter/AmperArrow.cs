using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmperArrow : MonoBehaviour
{

    private Animator _animator;

    public ICursorBehaviour cursorBehaviour {private get; set;}

    void Start()
    {
        _animator = GetComponent<Animator>();
        cursorBehaviour = new DefaultCBehaviour(_animator);
    }


    // Включение амперметра при нажатии кнопки "Старт"
    public void AmperStart() => cursorBehaviour.AmperStart();

    // Выключение амперметра при нажатии кнопки "Стоп"
    public void AmperStop() => cursorBehaviour.AmperStop();

    public void DefineBehaviour(bool IsHandBraked)
    {
        if (!IsHandBraked) cursorBehaviour = new
                DefaultCBehaviour(_animator);

        if (IsHandBraked) cursorBehaviour = new
                HandBrakedCBehaviour(_animator);
    }
}

#region Behaviour

public interface ICursorBehaviour
{
    void AmperStart();
    void AmperStop();
}


class DefaultCBehaviour : ICursorBehaviour
{
    private Animator _animator;

    public DefaultCBehaviour(Animator animator)
    {
        _animator = animator;
        _animator.SetBool("IsHandBrake", false);
    }

    public void AmperStart()
    {
        _animator.SetBool("IsStart", true);
        _animator.SetBool("IsStop", false);
    }

    public void AmperStop()
    {
        _animator.SetBool("IsStart", false);
        _animator.SetBool("IsStop", true);
    }
}


class HandBrakedCBehaviour : ICursorBehaviour
{
    private Animator _animator;

    public HandBrakedCBehaviour(Animator animator)
    {
        _animator = animator;
        _animator.SetBool("IsHandBrake", true);
    }

    public void AmperStart()
    {
        _animator.SetBool("IsStart", true);
    }

    public void AmperStop()
    {
        _animator.SetBool("IsStart", false);
    }
}
#endregion