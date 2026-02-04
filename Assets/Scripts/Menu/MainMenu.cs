using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject creditsPanel;
    public FadeOut fadeOut;

    private void Awake()
    {
        mainMenuPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

    public void StartGame()
    {
        if (fadeOut != null)
        {
            fadeOut.LoadTargetScene(1);
            Debug.Log("Starting game with fade out");
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

    public void Credits()
    {
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        creditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}



