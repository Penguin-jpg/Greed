using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void onPlayButtonClick()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void onQuitButtonClick()
    {
        Application.Quit();
    }
}
