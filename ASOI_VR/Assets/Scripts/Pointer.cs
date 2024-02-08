using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Pointer : MonoBehaviour
{
    [SerializeField] private Transform _valve;
    private CircularDrive _valveCD;
    private float _minAngle = 0f;
    private float _maxAngle = 150f; //210
    private bool _pumpjack = true;

    public void PumpJack(bool _value)
    {
        _pumpjack = _value;
    }
    private void Start()
    {
        _valveCD = _valve.gameObject.GetComponent<CircularDrive>();
    }

    private void FixedUpdate()
    {
        if (_pumpjack)
        {
            float ValveAngle = Math.Abs(_valveCD.OutAngle);
            float InterpolationRatio = ValveAngle / Math.Abs(_valveCD.minAngle);
            float LerpedAngle = Mathf.Lerp(_maxAngle, _minAngle, InterpolationRatio);
            // Угол в -180f установлен из-за начального положения ВСЕЙ модели (EKM#1) с поворотом по оси y = 180f, чтобы это компенсировать добавляем -180f
            transform.localRotation = Quaternion.Euler(0, -(LerpedAngle + LerpedAngle * 0.01f * Mathf.Sin(25 * Time.time)), 0);
        }
        else transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}