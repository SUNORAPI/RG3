using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Mode1 : MonoBehaviour
{
    public void Tap()
    {
        SceneManager.LoadScene("MusicSelect");
    }
}
