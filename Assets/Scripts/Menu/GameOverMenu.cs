using System.Collections;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    public Animator anim;
    public FadeOut fadeOut;
    private void OnEnable()
    {
        PlayGameOverAnimation();
    }
    
    private void Start()
    {
        this.gameObject.SetActive(false);
        EventManager.Instance.OnPlayerDied += Initialize;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnPlayerDied -= Initialize;
    }
    public void Initialize()
    {
        this.gameObject.SetActive(true);
    }
    public void PlayGameOverAnimation()
    {
        this.gameObject.SetActive(true);
        anim.Play("BGFade", 0);
        anim.Play("TextholderFade",1);
        anim.Play("TextAnim",2);
    }
   
    public IEnumerator ReturnToMenu()
    {
        if (fadeOut != null)
        {
            Debug.Log("Starting fade out animation");
            fadeOut.LoadTargetScene(0);
            yield return new WaitForSeconds(1f);

        }
        else
        {
            yield return new WaitForSeconds(5f);
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    public void BackToMenu()
    {
        StartCoroutine(ReturnToMenu());
    }
}
