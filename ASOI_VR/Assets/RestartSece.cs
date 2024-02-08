using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartSece : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("PumpJackScene");
    }
}
