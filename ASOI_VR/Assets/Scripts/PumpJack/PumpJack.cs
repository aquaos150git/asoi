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

    // �������� ������-�������
    private Animator _anim;
    private AudioSource _audioSource;
    // ��������� �����, ���������� �� ����������� ����������
    [SerializeField] private Transform _crankBoneTransform;

    // ��������� ������� ����������
    // 0 - ��������
    // 1 - � ������ ���������
    // 2 - � ������� ���������
    // 3 - ������

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _crankBoneTransform = _crankBoneTransform.transform;
        pumpJackBehaviour = new DefaultPJBehaviour(_anim, this);
    }

    // ��������� �������� ��� ����������� �������
    public void StopAnim()
    {
        _audioSource.Stop();
        pumpJackBehaviour.StopAnim();
    }
    //������ ������� ��� ������� ������ "�����" �� ����
    public void StartAnim()
    {
        _audioSource.Play();
        pumpJackBehaviour.StartAnim();
    }

    // ��������� ������� ��� ������� ������
    public void ReduceAnimSpeed()
    {
        _audioSource.Stop();
        pumpJackBehaviour.ReduceAnimSpeed();
    }

    // �������� ������� � ������� ��������� ���������
    public int GetPosName()
    {
        float CrankAngle = _crankBoneTransform.localEulerAngles.x;
        if (CrankAngle > 50 && CrankAngle < 90)
            return 2;
        if (CrankAngle < 310 && CrankAngle > 260)
            return 1;
        return 3;
    }
    
    // ����������� ��������� ������-�������
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

        Debug.Log("������� ������");
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
        Debug.Log("������ ���� ������");
    }

    public void StartAnim()
    {
        if (_pj.coroutine != null) _pj.StopRoutine();
        _animator.speed = 1;
        int position = 0;
        _pj.OnPumpjackStart.Invoke(position);
        Debug.Log("������ ����� ������");
    }

    public void StopAnim()
    {
        if (_pj.coroutine != null) _pj.StopRoutine();
        _animator.speed = 0;
        int position = _pj.GetPosName();
        _pj.OnPumpjackStop.Invoke(position);
        Debug.Log("������");
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
        // ������
        Debug.Log("������ ���� ������ hb");
    }

    public void StartAnim()
    {
        // �����
        Debug.Log("������ ����� ������ hb");
    }

    public void StopAnim()
    {
        Debug.Log("������ hb");
        if (_pj.coroutine != null) _pj.StopRoutine();
        _animator.speed = 0;
        int position = _pj.GetPosName();
        _pj.OnPumpjackStop.Invoke(position);
    }
}
#endregion