using System.Collections;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    public Animator anim;
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
    {         yield return new WaitForSeconds(5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void BackToMenu()
    {
        StartCoroutine(ReturnToMenu());
    }
}
