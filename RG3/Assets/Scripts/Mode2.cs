using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Mode2 : MonoBehaviour
{
    public void Tap()
    {
        SceneManager.LoadScene("User");
    }
}