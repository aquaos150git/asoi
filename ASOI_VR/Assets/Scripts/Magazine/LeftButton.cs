using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftButton : MonoBehaviour
{
    // ������ �� ��������� UI
    [SerializeField] private GameObject _text;
    // ������ �� ������, ���������� ���� ��������� �� ��������

    public AudioSource ButtonSound;

    // ������ �� ����� ������
    [SerializeField] private GameObject _leftButton;

    // ������ �� ������ ������ � �������
    private Text _ogpd;
    private Text _department;
    private Text _field;
    private Text _square;
    private Text _well;
    private Text _category;
    private Text _horizont;
    private Text _date;
    private DateTime _dateTime;

    private Collider _collider;


    private void Start() // ��������� � �������� �������� �������, ��� ���������� ����������� �� ���������� �������
    {
        _ogpd = _text.transform.GetChild(0).GetComponent<Text>();
        _department = _text.transform.GetChild(1).GetComponent<Text>();
        _field = _text.transform.GetChild(2).GetComponent<Text>();
        _square = _text.transform.GetChild(3).GetComponent<Text>();
        _well = _text.transform.GetChild(4).GetComponent<Text>();
        _category = _text.transform.GetChild(5).GetComponent<Text>();
        _horizont = _text.transform.GetChild(6).GetComponent<Text>();
        _date = _text.transform.GetChild(7).GetComponent<Text>();
        _dateTime = DateTime.Today;
    }

    // �������, ������������� �� ���������������
    private void OnTriggerEnter(Collider other)
    {
        ButtonSound.Play();
        _leftButton.SetActive(false);
        _ogpd.text = "011";
        _department.text = "�3";
        _field.text = "�������������";
        _square.text = "808";
        _well.text = "�2";
        _category.text = "1";
        _horizont.text = "530";
        _date.text = _dateTime.ToShortDateString();
        //_collider.enabled = false;
    }

}
