using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    // Ссылка на два стакана
    public GameObject UpperStakan;
    public GameObject LowerStakan;

    // Позиция верхней и нижней чашки
    private Transform _transformUpperStakan;
    private Transform _transformLowerStakan;

    private LowerStakan _lowerStakanScript;
    private StakanStateManager _upperStakanScript;

    // FixedCircularDrive верхней и нижней чашки
    private FixedCircularDrive _circularUpperStakan;
    private FixedCircularDrive _circularLowerStakan;

    // Поля инструмента
    private Transform _transformTool;
    private Rigidbody _rigidbody;
    private FixedThrowable _throwable;
    private Collider _colliderTool;


    private Collider[] _colliders;
    private Collider _collider1;
    private Collider _collider2;

    //public Collider _collider1; // Коллайдер инструмента
    //public Collider _collider2; // Коллайдер инструмента

    private Vector3 _positionLowerStakan = new Vector3(0.00065f, -0.002692f, -0.000175f);
    private Vector3 _positionUpperStakan = new Vector3(0.00064f, -0.002745f, 0f);

    private Vector3 _angleStakan = new Vector3(0, 360, -180);

    public bool IsLowerStakanEnabled;

    private void Start()
    {
        _circularUpperStakan = UpperStakan.GetComponent<FixedCircularDrive>();
        _circularLowerStakan = LowerStakan.GetComponent <FixedCircularDrive>();

        _transformUpperStakan = UpperStakan.GetComponent<Transform>();
        _transformLowerStakan = LowerStakan.GetComponent<Transform>();

        _lowerStakanScript = LowerStakan.GetComponent<LowerStakan>();
        _upperStakanScript = UpperStakan.GetComponent<StakanStateManager>();

        _colliderTool = GetComponent<Collider>();
        _transformTool = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();
        _throwable = GetComponent<FixedThrowable>();
        _circularLowerStakan.childCollider = _colliderTool;

        _colliders = GetComponentsInChildren<Collider>();
        _collider1 = _colliders[0];
        _collider2 = _colliders[1];
    }

    public void SetParent(bool stakanUp)
    {
        VR_Player.Instance.DetachFromHand(this.gameObject);

        _throwable.enabled = false;
        _rigidbody.isKinematic = true;

        if (stakanUp == true)
        {
            _transformTool.SetParent(_transformUpperStakan);
            _circularUpperStakan.childCollider = _colliderTool;
            //_transformTool.localPosition = _positionUpperStakan;
            UpperPosition();
        }

        else
        {
            _transformTool.SetParent(_transformLowerStakan);
            _circularLowerStakan.childCollider = _colliderTool;
            //_transformTool.localPosition = _positionLowerStakan;
            LowerPosition();
        }

        _transformTool.localEulerAngles = _angleStakan;

        //_circular.enabled = true;

        _circularUpperStakan.childCollider = _colliderTool;

        Debug.Log("SetParent()");

        OnCollider(false);
    }

    public void GetTool(bool stakanUp)
    {
        Debug.Log("Взять можно");
        _throwable.enabled = true;
        gameObject.transform.parent = null;

        if (stakanUp == true)
        {
            _circularUpperStakan.childCollider = null;
            _upperStakanScript.DisableDopCollider();
        }
        else
        {
            _circularLowerStakan.childCollider = null;
            _lowerStakanScript.DisableDopCollider();
            _circularLowerStakan.enabled = false;
        }

        OnCollider(true);
    }

    private void OnCollider(bool _isCollider)
    {
        _collider1.enabled = _isCollider;
        _collider2.enabled = _isCollider;
    }

    public void LowerPosition() => _transformTool.localPosition = _positionLowerStakan;

    public void UpperPosition() => _transformTool.localPosition = _positionUpperStakan;
}
