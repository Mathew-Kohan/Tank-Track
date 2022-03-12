using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuaseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseMenuUI;

    public void Resume()
    {
        _pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        _pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        _pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void LoadMenu()
    {
        Application.Quit();
    }
}
