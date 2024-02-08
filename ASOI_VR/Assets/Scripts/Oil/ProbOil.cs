using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ProbOil : MonoBehaviour
{
    // Основная рука
    private Hand _hand;

    // Путь к правой руке класса Hand.
    [SerializeField] private Hand _rightHand;

    // Путь к левой руке класса Hand.
    [SerializeField] private Hand _leftHand;

    // Используемые материалы "активной" части щупа.
    public Material Silver;
    public Material Oil;
    // Коллайдер, используемый в качестве триггера для понимания, вернул ли игрок щуп обратно в отверстие.
    public CapsuleCollider CapsuleCol;

    // Компоненты щупа.
    private Animation _anim;
    private Rigidbody _rb;
    private MeshRenderer _mr;
    private FixedThrowable _thr;
    private Transform _tr;


    // Маркер проигрывания анимации впервые.
    private bool isFirstAnimation = true;

    private void Start()
    {
        _anim = GetComponent<Animation>();
        _rb = GetComponent<Rigidbody>();
        _mr = GetComponent<MeshRenderer>();
        _thr = GetComponent<FixedThrowable>();
        _tr = GetComponent<Transform>();

        MaterialPicker();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != CapsuleCol.gameObject)
            return;

        _rb.isKinematic = true;
        DefineHand();
        _hand.DetachObject(gameObject);
        _thr.enabled = false;
        _tr.localPosition = new Vector3(2.25099993f, 1.079f, -3.31500006f);
        _tr.rotation = Quaternion.Euler(-90, 0, 0);
        _anim.Play("ProbOilIn"); // Анимация возврата щупа в отверстие.
    }

    // Генератор случайного целого числа от 0 до 1.
    private int GenerateRandomNumber()
    {
        float RandomFloat = Random.value;
        int RandomNumber = (int) (RandomFloat + 0.5);

        return RandomNumber;
    }

    // Выбор и замена материала на основе полученного случайного числа.
    private void MaterialPicker()
    {
        Material[] Materials = _mr.materials;

        if (GenerateRandomNumber() == 0)
            Materials[_mr.materials.Length - 1] = Silver;
        else
            Materials[_mr.materials.Length - 1] = Oil;

        _mr.materials = Materials;
    }

    // Одноразовое проигрывание анимации вытаскивания щупа.
    public void PlayAnim()
    {
        if (!isFirstAnimation)
            return;

        _anim.Play("ProbOilOut"); // Анимация доставания щупа из отверстия.
        isFirstAnimation = !isFirstAnimation;
        StartCoroutine(Delay(time: 1f, isThrowable: true, TriggerState: false));
    }

    public void ProbOilTake()
    {
        StartCoroutine(Delay(time: 5f, isThrowable: true, TriggerState: true));
    }

    // Включение/отключение возможности взять объект после срабатывания анимации.
    private IEnumerator Delay(float time, bool isThrowable, bool TriggerState)
    {
        yield return new WaitForSeconds(time);
        _thr.enabled = isThrowable;
        CapsuleCol.enabled = TriggerState;
    }

    public Hand DefineHand()
    {
        // Если объект взят в правую руку, то помечаем её как основную.
        if (_rightHand.currentAttachedObjectInfo.HasValue)
        {
            _hand = _rightHand;
        }
        else
        {
            _hand = _leftHand;
        }

        return _hand;

    }
}
