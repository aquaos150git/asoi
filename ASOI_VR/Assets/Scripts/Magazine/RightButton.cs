using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightButton : MonoBehaviour
{
    // ������ �� ��������� UI
    [SerializeField] private GameObject _text;
    // ������ �� ������, ���������� ���� ��������� �� ��������
    public GameObject ButtonSound;

    // ������ �� ������ ������
    [SerializeField] private GameObject _rightButton;

    // ������ �� ������ ������ � �������
    private GameObject _square;
    private GameObject _well;
    private GameObject _category;

    // ���������� ��������� ����������, ������� ����������� �� ���� �������� ������
    public static string SquareValue;
    public static string WellValue;
    public static string CategoryValue;

    private void Start() // ��������� � �������� �������� �������, ��� ���������� ����������� �� ���������� �������
    {
        _square = _text.transform.GetChild(0).gameObject;
        _well = _text.transform.GetChild(1).gameObject;
        _category = _text.transform.GetChild(2).gameObject;
    }

    // �������, ������������� �� ���������������
    // ������ ���������, ����� ������������ � ������������ � ��������� ����� ������
    private void OnTriggerEnter(Collider other)
    {
        ButtonSound.GetComponent<AudioSource>().Play();
        _rightButton.SetActive(false);
        _square.GetComponent<Text>().text = "50 �^2";
        _well.GetComponent<Text>().text = "�1";
        _category.GetComponent<Text>().text = "3";
    }

}