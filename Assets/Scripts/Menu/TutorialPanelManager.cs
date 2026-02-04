using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPanelManager : MonoBehaviour
{
    public List<GameObject> tutorialPanels;
    public Button previewButton;
    public Button nextButton;
    public Button startGame;
    public FadeOut fadeOut;

    private int currentPanelIndex = 0;

   public void Start()
    {
        UpdatePanelVisibility();
        previewButton.onClick.AddListener(ShowPreviousPanel);
        nextButton.onClick.AddListener(ShowNextPanel);
        if(fadeOut != null)
        {
            startGame.onClick.AddListener(() => fadeOut.LoadTargetScene(2));
        }
        else
        {
            startGame.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene(2));
        }
        startGame.gameObject.SetActive(false);
    }
    private void ShowPreviousPanel()
    {
        if (tutorialPanels == null || tutorialPanels.Count == 0)
            return;

        if (currentPanelIndex > 0)
        {
            currentPanelIndex--;
            UpdatePanelVisibility();
        }
    }
    private void ShowNextPanel()
    {
        if (tutorialPanels == null || tutorialPanels.Count == 0)
            return;

        if (currentPanelIndex < tutorialPanels.Count - 1)
        {
            currentPanelIndex++;
            UpdatePanelVisibility();
        }
        else
        {
            Debug.Log("Reached last panel");
            UpdatePanelVisibility();
        }
    }
    private void UpdatePanelVisibility()
    {
        for (int i = 0; i < tutorialPanels.Count; i++)
        {
            tutorialPanels[i].SetActive(i == currentPanelIndex);
                     
        }
        bool isLast = currentPanelIndex >= tutorialPanels.Count - 1;

        if (isLast)
        {
            Debug.Log("Tutorial Ended");
            EndedTutorial();
        }
        else
        {
            previewButton.gameObject.SetActive(currentPanelIndex > 0);
            nextButton.gameObject.SetActive(true);
            startGame.gameObject.SetActive(false);
        }
    }

    public void EndedTutorial()
    {
        
        previewButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        startGame.gameObject.SetActive(true);
    }

}
