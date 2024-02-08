using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PumpJack : MonoBehaviour
{
    public UnityEvent<int> OnPumpjackStart;
    public UnityEvent<int> OnPumpjackStop;
    public Coroutine coroutine;

    public IPupmJackBehaviour pumpJackBehaviour { private get;  set; }

    // Аниматор станка-качалки
    private Animator _anim;
    private AudioSource _audioSource;
    // Трансформ кости, отвечающей за перемещение кривошипов
    [SerializeField] private Transform _crankBoneTransform;

    // Возможные позиции кривошипов
    // 0 - работает
    // 1 - в нижнем положении
    // 2 - в верхнем положении
    // 3 - другое

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _crankBoneTransform = _crankBoneTransform.transform;
        pumpJackBehaviour = new DefaultPJBehaviour(_anim, this);
    }

    // Остановка анимации при затягивании ручника
    public void StopAnim()
    {
        _audioSource.Stop();
        pumpJackBehaviour.StopAnim();
    }
    //Запуск качалки при нажатии кнопки "старт" на щите
    public void StartAnim()
    {
        _audioSource.Play();
        pumpJackBehaviour.StartAnim();
    }

    // Остановка качалки при нажатии кнопки
    public void ReduceAnimSpeed()
    {
        _audioSource.Stop();
        pumpJackBehaviour.ReduceAnimSpeed();
    }

    // Название позиции в которой находятся кривошипы
    public int GetPosName()
    {
        float CrankAngle = _crankBoneTransform.localEulerAngles.x;
        if (CrankAngle > 50 && CrankAngle < 90)
            return 2;
        if (CrankAngle < 310 && CrankAngle > 260)
            return 1;
        return 3;
    }
    
    // Постепенная остановка станка-качалки
    public IEnumerator ProgressiveStop()
    {
        int DurationTime = 20;

        while (DurationTime >= 0)
        {
            _anim.speed = (float)DurationTime / 20;
            yield return new WaitForSeconds(1f);
            DurationTime--;
        }

        int position = GetPosName();
        OnPumpjackStop.Invoke(position);
    }

    public Coroutine StartRoutine()
    {
        coroutine = StartCoroutine(ProgressiveStop());
        return coroutine;
    }

    public Coroutine StopRoutine()
    {
        StopCoroutine(coroutine);
        return null;
    }

    public void DefineBehaviour(bool IsHandBraked)
    {
        if (!IsHandBraked) pumpJackBehaviour = new
                DefaultPJBehaviour(_anim, this);

        if (IsHandBraked) pumpJackBehaviour = new
                HandBrakedPJBehaviour(_anim, this);

        Debug.Log("Качалка дефайн");
    }
}

#region Behaviour
public interface IPupmJackBehaviour 
{
    void StartAnim();
    void ReduceAnimSpeed();
    void StopAnim();
}

class DefaultPJBehaviour : IPupmJackBehaviour
{

    private Animator _animator;
    private PumpJack _pj;

    public DefaultPJBehaviour(Animator animator, PumpJack pj)
    {
        _animator = animator;
        _pj = pj;
    }

    public void ReduceAnimSpeed()
    {
        if (_pj.coroutine != null) _pj.StopRoutine();
        _pj.StartRoutine();
        Debug.Log("Кнопка стоп нажата");
    }

    public void StartAnim()
    {
        if (_pj.coroutine != null) _pj.StopRoutine();
        _animator.speed = 1;
        int position = 0;
        _pj.OnPumpjackStart.Invoke(position);
        Debug.Log("Кнопка старт нажата");
    }

    public void StopAnim()
    {
        if (_pj.coroutine != null) _pj.StopRoutine();
        _animator.speed = 0;
        int position = _pj.GetPosName();
        _pj.OnPumpjackStop.Invoke(position);
        Debug.Log("Ручник");
    }
}

class HandBrakedPJBehaviour : IPupmJackBehaviour
{

    private Animator _animator;
    private PumpJack _pj;

    public HandBrakedPJBehaviour(Animator animator, PumpJack pj)
    {
        _animator = animator;
        _pj = pj;
    }

    public void ReduceAnimSpeed()
    {
        // Ничего
        Debug.Log("Кнопка стоп нажата hb");
    }

    public void StartAnim()
    {
        // Звуки
        Debug.Log("Кнопка старт нажата hb");
    }

    public void StopAnim()
    {
        Debug.Log("Ручник hb");
        if (_pj.coroutine != null) _pj.StopRoutine();
        _animator.speed = 0;
        int position = _pj.GetPosName();
        _pj.OnPumpjackStop.Invoke(position);
    }
}
#endregion