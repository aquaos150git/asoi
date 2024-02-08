using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Valve.VR;
using Valve.VR.InteractionSystem;
using static Untouchable;
using static Valve.VR.SteamVR_Skeleton_Poser;

public class StakanStateManager : MonoBehaviour
{
    private Collider[] _colliders;
    private Collider _secondCollider;

    StakanBaseState currentState;
    public Untouchable UntouchableState = new Untouchable();
    public Touchable TouchableState = new Touchable();
    public CircularMotion CircularMotionState = new CircularMotion();
    public LinearMotion LinearMotionState = new LinearMotion();

    [HideInInspector] public FixedInteractable _interactable;
    [HideInInspector] public FixedThrowable _throwable;
    [HideInInspector] public FixedCircularDrive _circular;
    [HideInInspector] public FixedLinearDrive _linear;
    [HideInInspector] public LinearMapping _linearMapping;
    [HideInInspector] public Rigidbody _rigidbody;
    [HideInInspector] public Transform _transform;
    [HideInInspector] public SteamVR_Skeleton_Poser _poser;
    [HideInInspector] public PoseBlendingBehaviour _poseBlending;
    public SteamVR_Skeleton_Pose _pose;

    [HideInInspector] public Vector3 FinalPose = Vector3.zero;

    private bool _isAttached;
    public bool IsTouchable;

    private GameObject _tool;


    public BoxCollider FixatorTrigger;
    public Tool Tool;

    public void Start()
    {
        currentState = UntouchableState;

        _interactable = GetComponent<FixedInteractable>();
        _throwable = GetComponent<FixedThrowable>();
        _circular = GetComponent<FixedCircularDrive>();
        _linear = GetComponent<FixedLinearDrive>();
        _linearMapping = GetComponent<LinearMapping>();
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        _poser = GetComponent<SteamVR_Skeleton_Poser>();
        _poseBlending = _poser.PoseBlending;
        //_pose = GetComponent<SteamVR_Skeleton_Pose>();
        //_poser.skeletonMainPose = _pose;

        _colliders = GetComponentsInChildren<Collider>();
        _secondCollider = _colliders[1];

        currentState.EnterState(this);


        _circular.onMaxAngle.AddListener(OnMaxAngle);
    }


    public void OnMaxAngle()
    {
        FixatorTrigger.enabled = true;
        UpperStakanMax();
        Tool.UpperPosition();
        Tool.GetTool(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject tool = other.gameObject;
        if (currentState == TouchableState && tool.name == "tool#1")
        {
            _secondCollider.enabled = true;
            currentState.OnTriggerEnter(this, other);
            SaveTool(tool);
            //_poser.SetBlendingBehaviourEnabled("Hand", true);
            //_poser.SetBlendingBehaviourValue("Hand", 1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject tool = other.gameObject;
        if (currentState == LinearMotionState && tool.name == "tool#1")
        {
            _linear.enabled = true;
            _poser.SetBlendingBehaviourEnabled("Hand", true);
            _poser.SetBlendingBehaviourValue("Hand", 1);
        }
    }

    private void SaveTool(GameObject tool)
    {
        _tool = tool;
    }

    public void AttachedStakan() => _isAttached = false;
    public void Detached() => _isAttached = true;

    public void UpperStakanMax() => currentState.UpdateState(this);

    public void DisableDopCollider() => _secondCollider.enabled = false;

    public void LowerStakanMin() => currentState.UpdateState(this);

    public void SwitchState(StakanBaseState stakan)
    {
        currentState = stakan;
        stakan.EnterState(this);
        Debug.Log("Смена состояния");
    }

    private void FixedUpdate()
    {
        if (_isAttached && currentState == LinearMotionState) currentState.UpdateState(this);
        if (IsTouchable)
        {
            if (Mathf.Abs(_transform.localPosition.z - FinalPose.z) <= 0.005f)
            {
                SwitchState(TouchableState);

                _circular.onMaxAngle.RemoveAllListeners();
                _circular.onMinAngle.AddListener(NewUpperStakanMin);

                _poser.SetBlendingBehaviourEnabled("Hand", false);
                IsTouchable = false;
            }
        }

    }

    private void NewUpperStakanMin()
    {
        _tool.GetComponent<Tool>().IsLowerStakanEnabled = true;
        _tool.GetComponent<Tool>().GetTool(true);
        SwitchState(UntouchableState);
    }
}
