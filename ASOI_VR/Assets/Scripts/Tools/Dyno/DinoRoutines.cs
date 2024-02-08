using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DinoRoutines : MonoBehaviour
{
    public static DinoRoutines Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }

    public Coroutine ChangeScreen(float delay, Dynamograph dynamograph)
    {
        Coroutine coroutine = StartCoroutine(ChangeDelay(delay, dynamograph));
        return coroutine;
    }

    private IEnumerator ChangeDelay(float delay, Dynamograph dynamograph)
    {
        yield return new WaitForSeconds(delay);
        dynamograph.State = new SelectionModeDynamographState(dynamograph);
    }

}
