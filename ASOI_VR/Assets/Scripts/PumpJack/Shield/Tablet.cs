using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tablet : MonoBehaviour
{
    private Vector3 _hangedPosition = new Vector3(38.409f, 1.687f, 38.569f);
    private Vector3 _hangedEulerRotation = new Vector3(270, 90, 0);

    private Rigidbody _rigidbody;
    private Transform _transform;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>().transform;
    }

    public void Hang()
    {
        VR_Player.Instance.DetachFromHand(this.gameObject);
        _rigidbody.isKinematic = true;
        _transform.position = _hangedPosition;
        _transform.eulerAngles= _hangedEulerRotation;
    }

    public void TakeOff() => _rigidbody.isKinematic = false;

    public void ChangeTag() => StartCoroutine(ChangeDelay());

    private IEnumerator ChangeDelay()
    {
        this.tag = "Untagged";
        yield return new WaitForSeconds(2f);
        this.tag = "Tablet";
    }
}
