using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    // Ссылка на аниматор
    //public Animator FirstAnimator;

    // Ссылка на отображение целого канваса
    [SerializeField] private GameObject _canvas;

    // Ссылка на отображение левой кнопки
    [SerializeField] private GameObject _leftButton;

    private Rigidbody _leftButtonRig;

    private Animation _anim;

    private void Start()
    {
        _anim = GetComponent<Animation>();
        _leftButtonRig = _leftButton.GetComponent<Rigidbody>();
    }

    // Анимация подъёма кнопок
    public void UpMagazine()
    {
        StartCoroutine(TimerDelay());
    }

    // Остановка анимации, после отпускания журнала из рук
    public void DownMagazine()
    {
        //FirstAnimator.SetTrigger("Down");
        OffCanvas();
    }

    // Задержка перед отображением анимаций, после взятия в руки журнала
    private IEnumerator TimerDelay()
    {
        yield return new WaitForSeconds(0.75f);
        OnCanvas();
        //FirstAnimator.SetTrigger("Up");
        _anim.Play("Up");
    }

    // Отключение всего канваса, включающего кнопки
    public void OffCanvas()
    {
        _canvas.SetActive(false);
    }

    // Включение всего канваса, а также двух кнопок
    public void OnCanvas()
    {
        if (_leftButtonRig) _leftButton.SetActive(true);
        _canvas.SetActive(true);
    }

}
