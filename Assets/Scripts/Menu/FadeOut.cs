using UnityEngine;

public class FadeOut : MonoBehaviour
{
    private int targetSceneIndex = -1;   
    public Animator anim;

    private void Start()
    {
        this.gameObject.SetActive(false);
    }
    public void PlayAnim()
    {
        this.gameObject.SetActive(true);
        anim.SetTrigger("FadeOut");
    }   

    public void LoadTargetScene(int sceneIndex)
    {
        targetSceneIndex = sceneIndex;
        PlayAnim();
    }

    public void EndAnim()
    {
        if (targetSceneIndex >= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(targetSceneIndex);
            Debug.Log("Loading scene: " + targetSceneIndex);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }


}
