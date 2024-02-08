using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class OilEffectScript : MonoBehaviour
{
    [SerializeField] private VisualEffect _vfx;
    private bool _oilProb;
    private bool _oilValve = true;
    private bool _isOil;
    private bool _pumpjack = true;
    private Coroutine _coroutineEffect;
    string _frequency = "Frequency";

    public int value;

    public void PumpJack(bool _value)
    {
        _pumpjack = _value;
        if (!_value)
        {
            _isOil = true;
            if (_oilProb)
            {
                StartCoroutine(StopOil());
            }
        }
        else OilEffect();
    }

    public void OilProb(bool _value)
    {
        _oilProb = _value;
        if (_value)
        {
            if (_pumpjack)
            {
                OilEffect();
            }

            else if (!_pumpjack && _oilValve && _isOil)
            {
                _vfx.Play();
                StartCoroutine(StopOil());
            }
        }
        else
        {
            OilEffect();
        }
    } // Сохранение информации, что кран  открыт
    public void OilValve(bool _value)
    {
        _oilValve = _value;
        OilEffect();
    } // Сохранение информации, что вентиль справа от крана открыт
    private void OilEffect()
    {
        if (_oilProb && _oilValve && (!_isOil || _pumpjack))
        {
            _isOil = !_isOil;
            if (_coroutineEffect != null) 
            { 
                StopCoroutine(_coroutineEffect);
                _vfx.SetInt(_frequency, 10);
            }
            _vfx.Play();
        }
        else if (!(_oilProb & _oilValve) && _isOil)
        {
            Debug.Log("Stop");
            _isOil = !_isOil;
            _coroutineEffect = StartCoroutine(StopOil());
        }
    } // Включение или отключение капель нефти  
    private IEnumerator StopOil()
    {
        float t = 5;
        while(t > 0)
        {
            _vfx.SetInt(_frequency, (int)t);
            yield return new WaitForSeconds(0.5f);
            t-=0.5f;
        }
            _vfx.Stop();
            _vfx.SetInt(_frequency, 10);
    } // Остановка капель нефти постепенно
    private void Start()
    {
        _vfx.Stop();
    } // В начале сцены отключить капли нефти

    private void FixedUpdate()
    {
        //Debug.Log(_isOil);
    }
}

