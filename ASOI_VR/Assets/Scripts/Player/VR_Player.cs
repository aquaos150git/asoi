using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class VR_Player : MonoBehaviour
{
    // Координаты джойстика контроллера.
    public SteamVR_Action_Vector2 Input;

    public static VR_Player Instance { get; private set; }

    [SerializeField] private float _speed = 3f;
    [SerializeField] private Hand _rightHand;
    [SerializeField] private Hand _leftHand;

    private CharacterController _characterController;
    private Hand _mainHand;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();

        if (Instance != null && Instance != this )
            Destroy(this.gameObject);
        else
            Instance = this;
    }


    // Обновляем позицию персонажа каждый кадр
    private void Update()
    {

        // Наклонен ли джойстик.
        if (Input.axis.magnitude > 0.1f)
        {

            // Координаты перемещения игрока.
            Vector3 _direction = Player.instance.hmdTransform.TransformDirection(
                new Vector3(Input.axis.x, 0, Input.axis.y));

            // Перемещение игрока по координатам джойстика.
            _characterController.Move(_speed * Time.deltaTime *
                Vector3.ProjectOnPlane(_direction, Vector3.up) -
                new Vector3(0, 9.81f, 0) * Time.deltaTime);
        }

    }

    public Hand DefineHand()
    {
        if (_rightHand.currentAttachedObjectInfo.HasValue) 
            _mainHand = _rightHand;

        if (_leftHand.currentAttachedObjectInfo.HasValue) 
            _mainHand = _leftHand;

        return _mainHand;
    }

    public void DetachFromLeftHand(GameObject gameObject)
    {
        DefineHand();

        if (_mainHand == _leftHand) _mainHand.DetachObject(gameObject);
    }

    public void DetachFromHand(GameObject gameObject)
    {
        DefineHand();
        _mainHand.DetachObject(gameObject);
    }

    public void AttachToHand(GameObject gameObject)
    {
        _mainHand.AttachObject(gameObject, GrabTypes.Grip);
    }

}