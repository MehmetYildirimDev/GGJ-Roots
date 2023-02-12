using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{



    public void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public void Win()
    {
        SceneManager.LoadScene("WinScene");
    }
}
