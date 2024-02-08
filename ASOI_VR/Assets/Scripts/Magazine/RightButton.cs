using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightButton : MonoBehaviour
{
    // —сылка на текстовый UI
    [SerializeField] private GameObject _text;
    // —сылка на объект, содержащий звук написани€ на странице
    public GameObject ButtonSound;

    // —сылка на правую кнопку
    [SerializeField] private GameObject _rightButton;

    // —сылки на каждую €чейку с текстом
    private GameObject _square;
    private GameObject _well;
    private GameObject _category;

    // ќбъ€вление статичных переменных, которые заполн€ютс€ по ходе процесса работы
    public static string SquareValue;
    public static string WellValue;
    public static string CategoryValue;

    private void Start() // ќбращение к дочерним объектам канваса, дл€ сокращени€ дальнейшего их заполнени€ текстом
    {
        _square = _text.transform.GetChild(0).gameObject;
        _well = _text.transform.GetChild(1).gameObject;
        _category = _text.transform.GetChild(2).gameObject;
    }

    // “риггер, срабатавыющий на соприкосновение
    // ƒанные статичные, нужно переработать в соответствии с принципом левой кнопки
    private void OnTriggerEnter(Collider other)
    {
        ButtonSound.GetComponent<AudioSource>().Play();
        _rightButton.SetActive(false);
        _square.GetComponent<Text>().text = "50 м^2";
        _well.GetComponent<Text>().text = "є1";
        _category.GetComponent<Text>().text = "3";
    }

}