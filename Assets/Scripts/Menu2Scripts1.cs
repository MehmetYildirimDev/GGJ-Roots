using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu2Scripts1 : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void PlayButton()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
