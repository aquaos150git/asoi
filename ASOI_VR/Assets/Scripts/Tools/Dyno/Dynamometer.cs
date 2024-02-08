using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamometer : MonoBehaviour
{
    [SerializeField] private GameObject _parent;
    [SerializeField] private GameObject _rotator;
    [SerializeField] private Material Diod;
    [SerializeField] private Material RedDiod;
    [SerializeField] private Material GreenDiod;
    [SerializeField] private MeshRenderer RightDiodMesh;
    [SerializeField] private MeshRenderer LeftDiodMesh;
    [SerializeField] private MeshRenderer MiddleDiodMesh;
    private Vector3 _attachedpos = new Vector3(1e-05f, 0.00047f, -0.001438f);
    private Vector3 _attachedrot = new Vector3(0, 180, 90);
    private FixedThrowable fixedThrowable;
    private Rigidbody _rigidbody;
    private FixedCircularDrive _rotatorFCD;
    private int pinPosition = 0;

    private void Start()
    {
        fixedThrowable = GetComponent<FixedThrowable>();
        _rigidbody = GetComponent<Rigidbody>();
        _rotatorFCD = _rotator.GetComponent<FixedCircularDrive>();
        RightDiodMesh.material = Diod;
        LeftDiodMesh.material = Diod;
        MiddleDiodMesh.material = Diod;
    }

    public void AttachDynamometer()
    {
        this.tag = "Untagged";
        VR_Player.Instance.DetachFromHand(this.gameObject);
        fixedThrowable.enabled = false;
        _rigidbody.isKinematic = true;
        this.transform.SetParent(_parent.transform);
        _rotatorFCD.enabled = true;
        this.transform.localPosition = _attachedpos;
        this.transform.localEulerAngles = _attachedrot;
        //VR_Player.Instance.AttachToHand(this.gameObject);
    }

    public void Unttach()
    {
        fixedThrowable.enabled = true;
        //this.transform.parent = null;
        RightDiodMesh.material = Diod;
        LeftDiodMesh.material = Diod;
        MiddleDiodMesh.material = Diod;
    }

    private void FixedUpdate()
    {
        if (!_rotatorFCD.enabled)
            return;

        if (_rotatorFCD.OutAngle > 0f && _rotatorFCD.OutAngle <= 60f && pinPosition != 0)
        {
            RightDiodMesh.material = GreenDiod;
            LeftDiodMesh.material = Diod;
            MiddleDiodMesh.material = Diod;
            pinPosition = 0;
            return;
        }
        
        if (_rotatorFCD.OutAngle > 60f && _rotatorFCD.OutAngle <= 120f && pinPosition != 1)
        {
            RightDiodMesh.material = GreenDiod;
            LeftDiodMesh.material = Diod;
            MiddleDiodMesh.material = GreenDiod;
            pinPosition = 1;
            return;
        }

        if (_rotatorFCD.OutAngle > 120f && _rotatorFCD.OutAngle <= 180f && pinPosition != 2)
        {
            RightDiodMesh.material = Diod;
            LeftDiodMesh.material = Diod;
            MiddleDiodMesh.material = GreenDiod;
            pinPosition = 2;
            return;
        }

        if (_rotatorFCD.OutAngle > 180f && _rotatorFCD.OutAngle <= 240f && pinPosition != 3)
        {
            RightDiodMesh.material = Diod;
            LeftDiodMesh.material = RedDiod;
            MiddleDiodMesh.material = Diod;
            pinPosition = 3;
            return;
        }

        if (_rotatorFCD.OutAngle > 240f && _rotatorFCD.OutAngle <= 300f && pinPosition != 4)
        {
            RightDiodMesh.material = Diod;
            LeftDiodMesh.material = RedDiod;
            MiddleDiodMesh.material = RedDiod;
            pinPosition = 4;
            return;
        }

        if (_rotatorFCD.OutAngle > 300f && pinPosition != 5)
        {
            RightDiodMesh.material = RedDiod;
            LeftDiodMesh.material = RedDiod;
            MiddleDiodMesh.material = RedDiod;
            pinPosition = 5;
            return;
        }
    }
}
