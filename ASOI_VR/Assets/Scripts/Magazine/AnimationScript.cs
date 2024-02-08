using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    // ������ �� ��������
    //public Animator FirstAnimator;

    // ������ �� ����������� ������ �������
    [SerializeField] private GameObject _canvas;

    // ������ �� ����������� ����� ������
    [SerializeField] private GameObject _leftButton;

    private Rigidbody _leftButtonRig;

    private Animation _anim;

    private void Start()
    {
        _anim = GetComponent<Animation>();
        _leftButtonRig = _leftButton.GetComponent<Rigidbody>();
    }

    // �������� ������� ������
    public void UpMagazine()
    {
        StartCoroutine(TimerDelay());
    }

    // ��������� ��������, ����� ���������� ������� �� ���
    public void DownMagazine()
    {
        //FirstAnimator.SetTrigger("Down");
        OffCanvas();
    }

    // �������� ����� ������������ ��������, ����� ������ � ���� �������
    private IEnumerator TimerDelay()
    {
        yield return new WaitForSeconds(0.75f);
        OnCanvas();
        //FirstAnimator.SetTrigger("Up");
        _anim.Play("Up");
    }

    // ���������� ����� �������, ����������� ������
    public void OffCanvas()
    {
        _canvas.SetActive(false);
    }

    // ��������� ����� �������, � ����� ���� ������
    public void OnCanvas()
    {
        if (_leftButtonRig) _leftButton.SetActive(true);
        _canvas.SetActive(true);
    }

}
