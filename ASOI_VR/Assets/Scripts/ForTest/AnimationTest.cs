using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

public class AnimationTest : MonoBehaviour
{
    // �������� ������-�������
    private Animator _anim;
    // ��������� �����, ���������� �� ����������� ����������
    [SerializeField] private Transform _crankBoneTransform;

    public UnityEvent <Enum> OnPumpjackStart;
    public UnityEvent <Enum> OnPumpjackStop;

    // ��������� ������� ����������
    private enum CrankPosition
    {
        top,
        bottom,
        other
    }

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _crankBoneTransform = _crankBoneTransform.transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) StopAnim();
        if (Input.GetKeyDown(KeyCode.N)) StartAnim();
        if (Input.GetKeyDown(KeyCode.M)) ReduceAnimSpeed();
    }

    // ��������� �������� ��� ����������� �������
    public void StopAnim()
    {
        _anim.speed = 0;
        CrankPosition position = (CrankPosition) GetPosName();
        OnPumpjackStop.Invoke(position);
    }
    //������ ������� ��� ������� ������ "�����" �� ����
    public void StartAnim()
    {
        _anim.speed = 1;
        CrankPosition position = (CrankPosition) GetPosName();
        OnPumpjackStart.Invoke(position);
    }
    // ��������� ������� ��� ������� ������
    public void ReduceAnimSpeed()
    {
        StartCoroutine(ProgressiveStop());
    }
    // �������� ������� � ������� ��������� ���������
    private Enum GetPosName()
    {
        float CrankAngle = _crankBoneTransform.localEulerAngles.x;
        if (CrankAngle > 50 && CrankAngle < 90) 
            return CrankPosition.top;
        if (CrankAngle < 310 && CrankAngle > 260) 
            return CrankPosition.bottom;
        return CrankPosition.other;
    }
    // ����������� ��������� ������-�������
    private IEnumerator ProgressiveStop()
    {
        int DurationTime = 30;

        while (DurationTime >= 0) 
        {
            _anim.speed = (float) DurationTime / 30;
            yield return new WaitForSeconds(1f);
            DurationTime--;
        }

    }
}